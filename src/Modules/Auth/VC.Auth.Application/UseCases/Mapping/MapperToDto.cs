using VC.Auth.Application.UseCases.Models;
using VC.Auth.Models;

namespace VC.Auth.Application.UseCases.Mapping;

public static class MapperToDto
{
    public static AuthDto ConvertToDto(User user)
        => new AuthDto()
        {
            TenantId = user.TenantId,
            Email = user.Email,
            Username = user.Username,
            Password = user.Password
        };
    
    public static List<AuthDto> ConvertToDtoList(ICollection<User> users) => users.Select(ConvertToDto).ToList();
}