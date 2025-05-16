using VC.Auth.Application.Models;
using VC.Auth.Models;

namespace VC.Auth.Application.Mapping;

public static class MapperToDto
{
    public static AuthDto ConvertToDto(User user)
        => new AuthDto()
        {
            TenantId = user.TenantId,
            Email = user.Email,
            Username = user.Username,
            Password = user.PasswordHash
        };
    
    public static List<AuthDto> ConvertToDtoList(ICollection<User> users) => users.Select(ConvertToDto).ToList();
}