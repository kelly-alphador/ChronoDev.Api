using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoDev.Domaine.Entities;
using ChronoDev.Domaine.Interface;
using ChronoDev.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ChronoDev.Infrastructure.Repository
{
    public class ProjectRepository:IProjectRepository
    {
        private readonly ApplicationDbContext _context;
        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyCollection<Projet>> GetAll()
        {
            return await _context.Projets.AsNoTracking().ToListAsync();
        }
        public async Task<IReadOnlyCollection<Projet>> GetByName(string name)
        {
            //on verifie si il est null ou vide 
            if (string.IsNullOrEmpty(name))
            {
                return new List<Projet>();
            }
            //supprimer l'espace et transforme en minuscule
            var searchName=name.Trim().ToLower();

            return await _context.Projets.
                Where(p=>p.nom.ToLower()
                .Contains(searchName))
                .AsNoTracking()
                .OrderBy(p=>p.nom)
                .ToListAsync();
        }
    }
}
