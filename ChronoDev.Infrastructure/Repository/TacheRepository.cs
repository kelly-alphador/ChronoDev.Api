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
    public class TacheRepository:ITacheRepository
    {
        private readonly ApplicationDbContext _context;
        public TacheRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyCollection<Tache>> GetAll()
        {
            return await _context.Taches.AsNoTracking().ToListAsync();
        }
        public async Task<IReadOnlyCollection<Tache>> GetAllByProjectId(int projectId)
        {
            return await _context.Taches.Where(t=>t.ProjetId==projectId).AsNoTracking().ToListAsync();
        }
        public async Task AddTacheAsync(Tache tache)
        {
            await _context.Taches.AddAsync(tache);
        }
        public async Task<bool> DeleteAsync(int idtache)
        {
            var tacheExist = await _context.Taches.FindAsync(idtache);
            if (tacheExist == null)
            {
                return false;
            }
            else
            {
                _context.Taches.Remove(tacheExist);
                return true;
            }
        }
    }
}
