using VC.Auth.Models;
using VC.Auth.Repositories;

namespace VC.Auth.Infrastructure.Persistence.Repositories;

public class AccountRepository : IAccountRepository
{
    private static IDictionary<string, User> _users = new Dictionary<string, User>();
    
    public void Add(User user)
    {
        throw new NotImplementedException();
    }
}