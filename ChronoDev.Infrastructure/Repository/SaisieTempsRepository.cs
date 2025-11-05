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
    public class SaisieTempsRepository:ISaisieTempsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public SaisieTempsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyCollection<SaisieTemps>> GetAllSaisieTemps()
        {
            var listSaisiTemps = await _dbContext.SaisiesTemps.AsNoTracking().ToListAsync();
            return listSaisiTemps;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var saisieExist=await _dbContext.SaisiesTemps.FindAsync(id);
            if (saisieExist == null)
                return false;
            else
                 _dbContext.SaisiesTemps.Remove(saisieExist);
                return true;
        }
        public async Task AddSaisieAsync(SaisieTemps saisieTemps)
        {
            await _dbContext.SaisiesTemps.AddAsync(saisieTemps);
        }
        public async Task<List<SaisieTemps>> GetSaisiesByDeveloppeurAsync(string nom)
        {
            
            return await _dbContext.SaisiesTemps
                .AsNoTracking()
                .Include(s => s.Tache)
                .Include(s => s.Validations)
                .Where(s => s.Utilisateur.nom == nom)
                .OrderByDescending(s => s.dateSaisie)
                .ToListAsync();
        }
       
        public async Task<IReadOnlyCollection<SaisieTemps>> GetTotalHeuresDeveloppeurSemaine()
        {
            // date du lundi de cette semaine
            var startOfWeek = DateTime.Now.Date.AddDays(-(int)DateTime.Now.DayOfWeek + 1);

            // fin de la semaine (dimanche)
            var endOfWeek = startOfWeek.AddDays(7);

            return await _dbContext.SaisiesTemps
                .AsNoTracking()
                .Include(s => s.Utilisateur)
                .Where(s => s.dateSaisie >= startOfWeek && s.dateSaisie < endOfWeek)
                .ToListAsync();
        }
    }
}
