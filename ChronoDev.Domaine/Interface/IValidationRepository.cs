using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoDev.Domaine.Entities;

namespace ChronoDev.Domaine.Interface
{
    public interface IValidationRepository
    {
        Task CreateValidation(Validation validation);
        Task<Validation?> GetBySaisieIdAsync(int saisieId);
        void UpdateValidation(Validation validation);
    }
}
