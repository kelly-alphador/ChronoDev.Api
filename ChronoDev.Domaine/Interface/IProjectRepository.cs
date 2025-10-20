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
        Task<IReadOnlyList<Projet>> GetAllProjectsAsync();
        Task<IReadOnlyList<Projet>> GetProjectsByIdAsync(int projectId);
        Task AddProject(Projet projet);
    }
}
