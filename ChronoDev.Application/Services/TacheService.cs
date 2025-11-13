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
    public class TacheService
    {
        private readonly ITacheRepository _Tacherepository;
        private readonly IUnitOfWork _unitOfWork;
        public TacheService(ITacheRepository tacherepository, IUnitOfWork unitOfWork)
        {
            _Tacherepository = tacherepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse> GetAllTache()
        {
            try
            {
                var ListesTaches = await _Tacherepository.GetAll();
                return ApiResponse.OK(true,"Donnees retourner avec succes",ListesTaches);
            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(500, false, $"{ex.Message}");
            }
        }
        public async Task<ApiResponse> GetAllTacheByProject(int projectid)
        {
            try
            {
                var listTachesByProject=await _Tacherepository.GetAllByProjectId(projectid);
                var listTachesByProjectIdNom = listTachesByProject.Select(t => new TacheIdNomDto
                {
                    Id = t.id,
                    Nom = t.nom,
                }).ToList();
                return ApiResponse.OK(true, "Donnees retourner avec succes", listTachesByProjectIdNom);
            }
            catch (Exception ex) 
            {
                return ApiResponse.Fail(500, false, $"{ex.Message}");
            }
        }
        public async Task<ApiResponse> AddTache(TacheAddDto tacheAddDto)
        {
            try
            {
                var tache = new Tache
                {
                    nom = tacheAddDto.Nom,
                    dureeEstimee = tacheAddDto.DureeEstimee,
                    dateDebut = tacheAddDto.DateDebut,
                    dateFin = tacheAddDto.DateFin,
                    ProjetId = tacheAddDto.ProjetId,
                };
                await _Tacherepository.AddTacheAsync(tache);
                await _unitOfWork.SaveChangesAsync();
                return ApiResponse.OK(true, "donnees enregistrer avec succes");
            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(500, false, $"{ex.Message}");
            }
        }
        public async Task<ApiResponse> Delete(int id)
        {
            try
            {
                var delete=await _Tacherepository.DeleteAsync(id);
                if(!delete)
                {
                    return ApiResponse.Fail(404,false, $"cette {id} de tache n'existe pas");
                }
                else
                {
                    await _unitOfWork.SaveChangesAsync();
                    return ApiResponse.OK(true, "donnees supprimer avec succes");
                }
            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(500, false, $"{ex.Message}");
            }
        }
    }
}
