using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoDev.Domaine.Entities;

namespace ChronoDev.Domaine.Interface
{
    public interface ITacheRepository
    {
        Task<IReadOnlyCollection<Tache>> GetAll();
        Task<IReadOnlyCollection<Tache>> GetAllByProjectId(int projectId);
        Task AddTacheAsync(Tache tache);
        Task<bool> DeleteAsync(int idtache);
    }
}
