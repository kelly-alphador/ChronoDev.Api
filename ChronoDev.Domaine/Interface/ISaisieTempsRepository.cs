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
        Task<bool> DeleteAsync(int id);
        Task AddSaisieAsync(SaisieTemps saisieTemps);
        Task<List<SaisieTemps>> GetSaisiesByDeveloppeurAsync(string userName);
        Task<IReadOnlyCollection<SaisieTemps>> GetTotalHeuresDeveloppeurSemaine();
    }
}
