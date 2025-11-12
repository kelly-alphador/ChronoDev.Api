using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoDev.Application.DTO;
using ChronoDev.Domaine.Entities;
using ChronoDev.Domaine.Interface;

namespace ChronoDev.Application.Services
{
    public class ValidationService
    {
        private readonly IValidationRepository _validationRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ValidationService(IValidationRepository repository,IUnitOfWork unitOfWork) 
        {
            _validationRepository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse> Add(ValadiationDto dto)
        {
            try
            {
                var validation = new Validation
                {
                    commentaire=dto.commentaire,
                    dateValidation = DateTime.Now,
                    Decision = dto.Decision,
                    ManagerId = dto.ManagerId,
                    SaisieDeTempsId = dto.SaisieDeTempsId,
                };
                await _validationRepository.CreateValidation(validation);
                await _unitOfWork.SaveChangesAsync();
                return ApiResponse.OK(true, "donnees ajouter avec succees");
            }
            catch (Exception ex) 
            {
                return ApiResponse.Fail(500,false, ex.Message);
            }
        }
    }
}
