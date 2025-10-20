using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoDev.Application.DTO;
using ChronoDev.Domaine.Entities;
using ChronoDev.Domaine.Interface;
using ChronoDev.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ChronoDev.Infrastructure.Repository
{
    public class ProjectRepository:IProjectRepository
    {
        public readonly ApplicationDbContext _context;
        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<Projet>> GetAllProjectsAsync()
        {
            return await _context.Projets.ToListAsync();
        }
        public async Task<IReadOnlyList<Projet>> GetProjectsByIdAsync(int projectId)
        {
            return await _context.Projets.Where(p=>p.id==projectId).ToListAsync();
        }
        public async Task AddProject(Projet projet)
        {
             _context.Projets.Add(projet);
        }
    }
}
