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
    public class ProjetService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProjetService(IProjectRepository projectRepository, IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
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
                var result = await _projectRepository.GetByName(name);
                if (result == null || !result.Any())
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
        public async Task<ApiResponse> AddProject(ProjectAddDto dto)
        {
            try
            {
                var projetexist = await _projectRepository.ProjetExistAsync(dto.nom);
                if (projetexist)
                {
                    return ApiResponse.Fail(false, "un projet avec cette nom existe deja");
                }
                var projet = new Projet
                {
                    nom = dto.nom,
                    dateCreation = dto.dateCreation,
                    dateFin = dto.dateFin,
                    dureeEstimee = dto.dureeEstimee,
                    ManagerId = dto.ManagerId,
                };
                await _projectRepository.AddProjectAsync(projet);
                await _unitOfWork.SaveChangesAsync();
                return ApiResponse.OK(true, "donnees enregister");

            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(false, ex.Message);
            }
        }

        public async Task<ApiResponse> Remove(int id)
        {
            try
            {
                var deleted = await _projectRepository.DeleteAsync(id);
                if (!deleted)
                {
                    return ApiResponse.Fail(false, "Ce projet n'existe pas");
                }

                await _unitOfWork.SaveChangesAsync();
                return ApiResponse.OK(true, "Données supprimées avec succès");
            }
            catch (Exception ex)
            {
                // Log l'exception ici
                return ApiResponse.Fail(false, "Erreur lors de la suppression");
            }
        }
        public async Task<ApiResponse> GetTotalProject()
        {
            try
            {
                var count=await _projectRepository.GetTotalProjectAsync();
                return ApiResponse.OK(false, "donnees retourner avec succes",count);
            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(false, "aucun donnees");
            }
        }
        public async Task<ApiResponse> UpdateProject(ProjectUpdateDto projectUpdateDto)
        {
            try
            {
                // Mapper DTO vers Entity
                var projet = new Projet
                {
                    id = projectUpdateDto.Id,
                    nom = projectUpdateDto.Nom,
                    dateCreation = projectUpdateDto.DateCreation,
                    dateFin = projectUpdateDto.DateFin,
                    dureeEstimee = projectUpdateDto.DureeEstimee
                };

                
                var updated = await _projectRepository.UpdateAsync(projet);

                if (!updated)
                {
                    return ApiResponse.Fail(false, "Ce projet n'existe pas");
                }

                // Sauvegarder les changements
                await _unitOfWork.SaveChangesAsync();

                return ApiResponse.OK(true, "Projet mis à jour avec succès");
            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(false, $"Error: {ex.Message}");
            }
        }
    }
}