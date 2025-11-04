using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoDev.Domaine.Entities;

namespace ChronoDev.Domaine.Interface
{
    public interface ISaisieTempsRepository
    {
        Task<IReadOnlyCollection<SaisieTemps>> GetAllSaisieTemps();
    }
}
