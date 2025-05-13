using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace VC.WebUi.Services;

public class ApiService
{
    private readonly HttpClient _http;
    private const string ApiVersionHeader = "X-Api-Version";
    private const string ApiVersion = "v1";

    public ApiService(HttpClient http)
    {
        _http = http;
    }

    private void SetDefaultHeaders()
    {
 
    }

    // ============================
    // Работа с сотрудниками (Employees)
    // ============================

    public async Task<List<EmployeeDto>> GetEmployeesAsync()
    {
        SetDefaultHeaders();
        return await _http.GetFromJsonAsync<List<EmployeeDto>>("api/v1/employees") 
               ?? new List<EmployeeDto>();
    }

    public async Task<ResultOfGuid> CreateEmployeeAsync(CreateEmployeeParams request)
    {
        SetDefaultHeaders();
        var response = await _http.PostAsJsonAsync("api/v1/employees", request);
        return await response.Content.ReadFromJsonAsync<ResultOfGuid>();
    }

    // ============================
    // Работа с графиками работы (WorkSchedules)
    // ============================

    public async Task<WorkScheduleDetailsDto> GetEmployeeWorkSchedule(Guid employeeId)
    {
        SetDefaultHeaders();
        return await _http.GetFromJsonAsync<WorkScheduleDetailsDto>($"api/v1/employees/{employeeId}/work-schedule")
               ?? new WorkScheduleDetailsDto();
    }

    public async Task<ResultOfGuid> CreateWorkScheduleAsync(Guid employeeId, CreateWorkScheduleRequest request)
    {
        SetDefaultHeaders();
        var response = await _http.PostAsJsonAsync($"api/v1/employees/{employeeId}/work-schedule", request);
        return await response.Content.ReadFromJsonAsync<ResultOfGuid>();
    }

    public async Task<ResultOfGuid> AddWorkScheduleExceptionAsync(Guid employeeId, AddWorkingHourExceptionRequest request)
    {
        SetDefaultHeaders();
        var response = await _http.PostAsJsonAsync($"api/v1/employees/{employeeId}/work-schedule/exceptions", request);
        return await response.Content.ReadFromJsonAsync<ResultOfGuid>();
    }

    // ============================
    // Свободные слоты сотрудника (Available Slots)
    // ============================

    public async Task<List<AvailableSlotDto>> GetEmployeeAvailableSlots(Guid employeeId, DateOnly date)
    {
        SetDefaultHeaders();
        var dateString = date.ToString("yyyy-MM-dd");
        return await _http.GetFromJsonAsync<List<AvailableSlotDto>>($"api/v1/employees/{employeeId}/available-slots?date={dateString}")
               ?? new List<AvailableSlotDto>();
    }

    // ============================
    // Работа с ресурсами (Resources)
    // ============================

    public async Task<List<Resource2>> GetAllResourcesAsync()
    {
        SetDefaultHeaders();
        return await _http.GetFromJsonAsync<List<Resource2>>($"api/v1/resources")
               ?? [];
    }   
    
    public async Task<Resource2> GetResourceById(Guid id)
    {
        SetDefaultHeaders();
        return await _http.GetFromJsonAsync<Resource2>($"api/v1/resources/{id}")
               ?? new Resource2();
    }

    public async Task<ResultOfGuid> CreateResourceAsync(CreateResourceRequest request)
    {
        SetDefaultHeaders();
        var response = await _http.PostAsJsonAsync("api/v1/resources", request);
        return await response.Content.ReadFromJsonAsync<ResultOfGuid>();
    }

    public async Task<ResultOfGuid> UpdateResourceAsync(Guid id, UpdateResourceRequest request)
    {
        SetDefaultHeaders();
        var response = await _http.PutAsJsonAsync($"api/v1/resources/{id}", request);
        return await response.Content.ReadFromJsonAsync<ResultOfGuid>();
    }

    public async Task<ResultOfGuid> DeleteResourceAsync(Guid id)
    {
        SetDefaultHeaders();
        var response = await _http.DeleteAsync($"api/v1/resources/{id}");
        return await response.Content.ReadFromJsonAsync<ResultOfGuid>();
    }

    // ============================
    // Работа с услугами (Services)
    // ============================

    public async Task<ServiceDetailsDto> GetServiceById(Guid id)
    {
        SetDefaultHeaders();
        return await _http.GetFromJsonAsync<ServiceDetailsDto>($"api/v1/services/{id}")
               ?? new ServiceDetailsDto();
    }

    public async Task<List<ServiceDetailsDto>> GetAllServicesAsync()
    {
        try
        {
            SetDefaultHeaders();
            var response = await _http.GetAsync("api/v1/services");

            if (!response.IsSuccessStatusCode)
            {
                var errorText = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ошибка: {response.StatusCode}, Тело: {errorText}");
                return new List<ServiceDetailsDto>();
            }

            var result = await response.Content.ReadFromJsonAsync<List<ServiceDetailsDto>>();
            return result ?? new List<ServiceDetailsDto>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Исключение: {ex.Message}");
            return new List<ServiceDetailsDto>();
        }
    }

    public async Task<ResultOfGuid> CreateServiceAsync(CreateServiceRequest request)
    {
        SetDefaultHeaders();
        var response = await _http.PostAsJsonAsync("api/v1/services", request);
        return await response.Content.ReadFromJsonAsync<ResultOfGuid>();
    }

    public async Task<ResultOfGuid> UpdateServiceAsync(Guid id, UpdateServiceRequest request)
    {
        SetDefaultHeaders();
        var response = await _http.PutAsJsonAsync($"api/v1/services/{id}", request);
        return await response.Content.ReadFromJsonAsync<ResultOfGuid>();
    }

    public async Task<ResultOfGuid> DeleteServiceAsync(Guid id)
    {
        SetDefaultHeaders();
        var response = await _http.DeleteAsync($"api/v1/services/{id}");
        return await response.Content.ReadFromJsonAsync<ResultOfGuid>();
    }
}



















public class CreateEmployeeParams
{
    [Required(ErrorMessage = "ФИО обязательно")]
    public string FullName { get; set; }

    public string? Specialisation { get; set; }
}
public class AddWorkingHourExceptionRequest
{
    [Required]
    public DateOnly Date { get; set; }

    public bool IsDayOff { get; set; } = false;

    public string? StartTime { get; set; }

    public string? EndTime { get; set; }
}
public class WorkScheduleItemDto
{
    public int DayOfWeek { get; set; }
    public string StartTime { get; set; } = "";
    public string EndTime { get; set; } = "";
}

public class CreateWorkScheduleRequest
{
    public List<WorkScheduleItemDto> Items { get; set; } = new();
}

public class ResourceDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public int Count { get; set; }
}

public class CreateResourceRequest
{
    [Required] public required string Title { get; set; }
    [Required] public required string Description { get; set; }
    [Range(1, int.MaxValue)] public int Count { get; set; }
}

public class UpdateResourceRequest
{
    [Required] public required string Title { get; set; }
    [Required] public required string Description { get; set; }
    [Range(1, int.MaxValue)] public int Count { get; set; }
}

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
}
public class EmployeeAssignmentDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public double Price { get; set; }
    public string Duration { get; set; } = "";
}

public class EmployeeAssignmentDto2
{
    public Guid EmployeeId { get; set; }
    public double Price { get; set; }
    public string Duration { get; set; } = "";
}

public class ServiceDetailsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public double BasePrice { get; set; }
    public string BaseDuration { get; set; } = "";
    public string? Description { get; set; }
    public CategoryDto? Category { get; set; }
    public bool IsActive { get; set; }
    public List<ResourceDto> RequiredResources { get; set; } = new();
    public List<EmployeeAssignmentDto> EmployeeAssignments { get; set; } = new();
}

public class CreateServiceRequest
{
    [Required] public string Title { get; set; }
    [Required] public double BasePrice { get; set; }
    [Required] public string BaseDuration { get; set; }
    public string? Description { get; set; }
    public Guid? CategoryId { get; set; }
    public List<Guid>? RequiredResources { get; set; }
    public List<EmployeeAssignmentDto2>? EmployeeAssignments { get; set; }
}

public class UpdateServiceRequest
{
    [Required] public string Title { get; set; }
    [Required] public double BasePrice { get; set; }
    [Required] public string BaseDuration { get; set; }
    public string? Description { get; set; }
    public Guid? CategoryId { get; set; }
    public List<Guid>? RequiredResources { get; set; }
    public List<EmployeeAssignmentDto2>? EmployeeAssignments { get; set; }
}

public class ResultOfGuid
{
    public Guid ValueOrDefault { get; set; }
    public Guid Value { get; set; }
    public bool IsFailed { get; set; }
    public bool IsSuccess { get; set; }
    public List<IError>? Errors { get; set; }
    public List<IReason>? Reasons { get; set; }
    public List<ISuccess>? Successes { get; set; }
}

public class IError
{
    public string Message { get; set; } = "";
    public object? Metadata { get; set; }
}

public class IReason
{
    public string Message { get; set; } = "";
    public object? Metadata { get; set; }
}

public class ISuccess
{
    public string Message { get; set; } = "";
    public object? Metadata { get; set; }
}

public class EmployeeDto
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    public string? Specialisation { get; set; }
}

public class WorkScheduleDetailsDto
{
    public List<WorkScheduleItemDto> Items { get; set; } = new();
}

public class AvailableSlotDto
{
    public string StartTime { get; set; } = "";
    public string EndTime { get; set; } = "";
}

public class Resource2
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public int Count { get; set; }
}