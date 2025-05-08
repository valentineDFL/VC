using VC.Auth.General;
using VC.Auth.Models;
using VC.Auth.Repositories;

namespace VC.Auth.Application;

public class UserService(
    IAccountRepository _accountRepository,
    IEncrypt _encrypt)
    : IUserService
{
    public void Create(User user)
    {
        user = new User
        {
            UserId = Guid.CreateVersion7(),
            Email = user.Email
        };

        user.Salt = Guid.CreateVersion7().ToString();
        user.Password = _encrypt.HashPassword(user.Password, user.Salt);

        _accountRepository.Add(user);
    }

    public void Login(User user)
    {
        
    }

    public void Register(User user)
    {
        throw new NotImplementedException();
    }
}