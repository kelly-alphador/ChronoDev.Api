using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoDev.Application.DTO;
using ChronoDev.Domaine.Interface;

namespace ChronoDev.Application.Services
{
    public class SaisieTempsService
    {
        private readonly ISaisieTempsRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public SaisieTempsService(ISaisieTempsRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse> GetAll()
        {
            try
            {
                var listSaisieTemps=await _repository.GetAllSaisieTemps();
                return new ApiResponse
                {
                    Success = true,
                    Data = listSaisieTemps,
                    Message = "donnees retourner avec succes"
                };
            }
            catch (Exception ex) 
            {
                return ApiResponse.Fail(false, $"une erreur est {ex.Message}");   
            }
        }
        public async Task<ApiResponse> Delete(int id)
        {
            try
            {
                var delete = await _repository.DeleteAsync(id);
                if (!delete)
                {
                    return ApiResponse.Fail(false, "donnees n'existe pas");
                }
                else
                {
                    await _unitOfWork.SaveChangesAsync();
                    return ApiResponse.OK(true, "donnees supprimer avec success");
                }
            }
            catch (Exception ex) 
            {
                return ApiResponse.Fail(false, $"une erreur est survenu {ex.Message}");
            }
           
        }
    }
}
