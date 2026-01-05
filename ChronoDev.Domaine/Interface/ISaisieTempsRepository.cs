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
        Task<IReadOnlyCollection<SaisieTemps>> GetAllSaisieTempsWithValidations();
        Task<bool> DeleteAsync(int id);
        Task AddSaisieAsync(SaisieTemps saisieTemps);
        Task<List<SaisieTemps>> GetSaisiesByDeveloppeurAsync(string userName);
        Task<IReadOnlyCollection<SaisieTemps>> GetTotalHeuresDeveloppeurParSemaine();
        Task<IReadOnlyCollection<SaisieTemps>> GetSaisiesTempsParSemaineAsync(DateTime debut, DateTime fin);
        Task<IReadOnlyCollection<SaisieTemps>> GetSaisiesTempsParUtilisateurAsync(int utilisateurId, DateTime debut, DateTime fin);
    }
}
