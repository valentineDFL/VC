using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VC.Tenants.UnitOfWork
{
    public interface IDbSaver
    {
        public Task SaveAsync();
    }
}
