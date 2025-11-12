using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoDev.Domaine.Entities;
using ChronoDev.Domaine.Interface;
using ChronoDev.Infrastructure.Context;

namespace ChronoDev.Infrastructure.Repository
{
    public class ValidationRepository:IValidationRepository
    {
        private readonly ApplicationDbContext _context;
        public ValidationRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task CreateValidation(Validation validation)
        {
            await _context.Validations.AddAsync(validation);
        }
    }
}
