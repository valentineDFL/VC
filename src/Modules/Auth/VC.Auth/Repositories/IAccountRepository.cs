using VC.Auth.Models;

namespace VC.Auth.Repositories;

public interface IAccountRepository
{
    void Add(User user);
}