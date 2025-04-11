using Microsoft.EntityFrameworkCore;
using VC.Services.Entities;

namespace VC.Services.Infrastructure.Persistens;
public class ServiceDbContext : DbContext
{
    public DbSet<Service>
}
