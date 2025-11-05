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
                return ApiResponse.Fail(500, false, $"une erreur est {ex.Message}");   
            }
        }
        public async Task<ApiResponse> Delete(int id)
        {
            try
            {
                var delete = await _repository.DeleteAsync(id);
                if (!delete)
                {
                    return ApiResponse.Fail(404,false, "donnees n'existe pas");
                }
                else
                {
                    await _unitOfWork.SaveChangesAsync();
                    return ApiResponse.OK(true, "donnees supprimer avec success");
                }
            }
            catch (Exception ex) 
            {
                return ApiResponse.Fail(500, false, $"une erreur est survenu {ex.Message}");
            }
           
        }
        public async Task<ApiResponse> AddSaisieTemps(SaisieAddDto saisieAddDto)
        {
            try
            {
                var saisie = new SaisieTemps
                {
                    commentaire = saisieAddDto.commentaire,
                    dateSaisie = saisieAddDto.dateSaisie,
                    heure_deb = saisieAddDto.heure_deb,
                    heure_fin = saisieAddDto.heure_fin,
                    Statut = saisieAddDto.Statut,
                    TacheId = saisieAddDto.TacheId,
                    UtilisateurId = saisieAddDto.UtilisateurId
                };
                await _repository.AddSaisieAsync(saisie);
                await _unitOfWork.SaveChangesAsync();
                return ApiResponse.OK(true, "donnees enregistrer avec succes");
            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(500, false, ex.Message);
            }
        }
        public async Task<ApiResponse> TotalHeureDeveloppeur()
        {
            try
            {
                var listSaisitemps = await _repository.GetTotalHeuresDeveloppeurSemaine();
                var list = listSaisitemps
                    .GroupBy(s => s.Utilisateur.nom)
                    .Select(g => new HeureDevelopperDto
                    {
                        nomDeveloppeur = g.Key,
                        Duree = TimeSpan.FromMinutes(
                            g.Sum(s => (s.heure_fin - s.heure_deb).TotalMinutes)
                        ).ToString(@"hh\:mm")
                    })
                    .ToList();
                return ApiResponse.OK(true,"Données retournées avec succès", list);
            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(500, false, ex.Message);
            }
        }

        public async Task<ApiResponse> GetByDeveloppeur(string nom)
        {
            try
            {
                var listSaisiParDeveloppeur = await _repository.GetSaisiesByDeveloppeurAsync(nom);
                if (!listSaisiParDeveloppeur.Any())
                {
                    return ApiResponse.Fail(404,false, $"le donnees n'existe pas");
                }
                else
                {
                    var list = listSaisiParDeveloppeur
                    .Select(s => new SaisieTempsReadDto
                    {
                        Id = s.id,
                        DateSaisie = s.dateSaisie,
                        HeureDebut = s.heure_deb,
                        HeureFin = s.heure_fin,
                        Duree = s.heure_fin - s.heure_deb,
                        Commentaire = s.commentaire,
                        Statut = s.Statut,
                        TacheNom = s.Tache.nom,
                        NombreValidations = s.Validations.Count
                    }).ToList();
                    return ApiResponse.OK(true, "donnees retourner avec succes", list);
                }
        
            }
            catch (Exception ex) 
            {
                return ApiResponse.Fail(500,false, $"{ex.Message}");
            }
            
        }
    }
}
