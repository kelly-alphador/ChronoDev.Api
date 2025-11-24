using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoDev.Domaine.Entities;

namespace ChronoDev.Domaine.Interface
{
    public interface IProjectRepository
    {
         Task<List<(int id, string nom)>> GetIdName();
         Task<List<(string nom,DateTime DateCreation,
             double dureeEstime,DateTime datefin,
             int nbjour,string manager)>> GetProjectById(int id);
         Task<IReadOnlyCollection<Projet>> GetByName(string name);
         Task AddProjectAsync(Projet projet);
         Task<bool> ProjetExistAsync(string nom);
         Task<bool> DeleteAsync(int id);
         Task<int> GetTotalProjectAsync();
         Task<bool> UpdateAsync(Projet projet);
       
    }
}
