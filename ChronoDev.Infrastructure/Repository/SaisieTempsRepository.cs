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
    }
}
