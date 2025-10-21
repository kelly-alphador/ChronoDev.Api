using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoDev.Application.DTO;
using ChronoDev.Domaine.Interface;

namespace ChronoDev.Application.Services
{
    public class ProjetService
    {
        private readonly IProjectRepository _projectRepository;
        public ProjetService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ApiResponse> GetAllProject()
        {
            var projets = await _projectRepository.GetAll();

            return new ApiResponse
            {
                Success = true,
                Data = projets
            };
        }
        public async Task<ApiResponse> GetProjectByName(string name)
        {
            try
            {
                var result=await _projectRepository.GetByName(name);
                if(result==null || !result.Any())
                {
                    return ApiResponse.OK(false, "aucun donnee trouve");
                }
                else
                {
                    return ApiResponse.OK(true, "donnee trouve", result);
                }
                
            }
            catch (Exception ex) 
            {
                return ApiResponse.Fail(false, ex.Message);
            }
        }
    }
}
