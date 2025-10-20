using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoDev.Domaine.Interface
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
