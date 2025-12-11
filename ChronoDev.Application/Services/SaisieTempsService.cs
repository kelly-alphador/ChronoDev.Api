using System;
using System.Collections.Generic;
using System.Globalization;
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
        public async Task<ApiResponse> TotalHeureDeveloppeurParSemaine()
        {
            try
            {
                var listSaisietemps = await _repository.GetTotalHeuresDeveloppeurParSemaine();

                var result = listSaisietemps
                    .GroupBy(s => new
                    {
                        UtilisateurId = s.UtilisateurId,
                        Nom = s.Utilisateur.nom,
                        Semaine = ISOWeek.GetWeekOfYear(s.dateSaisie),
                        Annee = s.dateSaisie.Year
                    })
                    .Select(g => new HeureDeveloppeurSemaineDto
                    {
                        UtilisateurId = g.Key.UtilisateurId,
                        NomDeveloppeur = g.Key.Nom,
                        Semaine = g.Key.Semaine,
                        Annee = g.Key.Annee,
                        DateDebutSemaine = g.Min(s => s.dateSaisie).Date.AddDays(-(int)g.Min(s => s.dateSaisie).DayOfWeek + 1),
                        DateFinSemaine = g.Min(s => s.dateSaisie).Date.AddDays(-(int)g.Min(s => s.dateSaisie).DayOfWeek + 1).AddDays(6),
                        Duree = TimeSpan.FromMinutes(
                            g.Sum(s => (s.heure_fin - s.heure_deb).TotalMinutes)
                        ).ToString(@"hh\:mm"),
                        TotalHeures = g.Sum(s => (s.heure_fin - s.heure_deb).TotalHours)
                    })
                    .OrderByDescending(x => x.Annee)
                    .ThenByDescending(x => x.Semaine)
                    .ThenBy(x => x.UtilisateurId)
                    .ToList();

                return ApiResponse.OK(true, "Données retournées avec succès", result);
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
        public async Task<List<HeuresSemaineManagerDTO>> GetHeuresParSemaineAsync(DateTime debut, DateTime fin)
        {
            var saisies = await _repository.GetSaisiesTempsParSemaineAsync(debut, fin);

            var grouped = saisies
                .GroupBy(s => s.Utilisateur)
                .Select(g => new HeuresSemaineManagerDTO
                {
                    Developpeur = g.Key != null ? $"{g.Key.nom} {g.Key.prenom}" : "",
                    TotalHeures = Math.Round(g.Sum(s => (s.heure_fin - s.heure_deb).TotalHours), 2),
                    NombreProjets = g.Select(s => s.Tache?.Projet?.id).Where(id => id != null).Distinct().Count(),
                    Projets = g.Select(s => s.Tache?.Projet?.nom)
                                .Where(n => !string.IsNullOrEmpty(n))
                                .Distinct()
                                .ToList(),
                    Etats = g.Select(s => s.Statut ?? "En attente")
                             .Distinct()
                             .ToList()
                })
                .ToList();

            return grouped;
        }
        public async Task<List<HeuresParDeveloppeurDTO>> GetHeuresParMoisAsync(int annee, int mois)
        {
            var debut = new DateTime(annee, mois, 1);
            var fin = debut.AddMonths(1).AddSeconds(-1); 

            var saisies = await _repository.GetSaisiesTempsParSemaineAsync(debut, fin);

            var grouped = saisies
                .GroupBy(s => s.Utilisateur)
                .Select(g => new HeuresParDeveloppeurDTO
                {
                    Developpeur = g.Key != null ? $"{g.Key.nom} {g.Key.prenom}" : "",
                    TotalHeures = Math.Round(g.Sum(s => (s.heure_fin - s.heure_deb).TotalHours), 2),
                    Projets = g
                        .Where(s => s.Tache?.Projet != null)
                        .GroupBy(s => s.Tache.Projet.nom)
                        .Select(p => new HeuresParProjetDTO
                        {
                            Projet = p.Key,
                            Heures = Math.Round(p.Sum(s => (s.heure_fin - s.heure_deb).TotalHours), 2)
                        })
                        .ToList()
                })
                .ToList();

            return grouped;
        }
        public async Task<List<HeuresParDeveloppeurDTO>> GetHeuresParMoisAsyncByUser(int utilisateurId, int annee, int mois)
        {
            var debut = new DateTime(annee, mois, 1);
            var fin = debut.AddMonths(1).AddSeconds(-1);

            var saisies = await _repository.GetSaisiesTempsParUtilisateurAsync(utilisateurId, debut, fin);

            var grouped = saisies
                .GroupBy(s => s.Utilisateur)
                .Select(g => new HeuresParDeveloppeurDTO
                {
                    Developpeur = g.Key != null ? $"{g.Key.nom} {g.Key.prenom}" : "",
                    TotalHeures = Math.Round(g.Sum(s => (s.heure_fin - s.heure_deb).TotalHours), 2),
                    Projets = g
                        .Where(s => s.Tache?.Projet != null)
                        .GroupBy(s => s.Tache.Projet.nom)
                        .Select(p => new HeuresParProjetDTO
                        {
                            Projet = p.Key,
                            Heures = Math.Round(p.Sum(s => (s.heure_fin - s.heure_deb).TotalHours), 2)
                        })
                        .ToList()
                })
                .ToList();

            return grouped;
        }
        public async Task<HeuresSemaineDTO> GetHeuresParSemaineParUtilisateurAsync(int utilisateurId, DateTime debut, DateTime fin)
        {
            var saisies = await _repository.GetSaisiesTempsParUtilisateurAsync(utilisateurId, debut, fin);

            if (saisies == null || !saisies.Any())
                return new HeuresSemaineDTO
                {
                    Developpeur = "",
                    TotalHeures = 0,
                    Projets = new List<string>(),
                    Etats = new List<string>()
                };

            var totalHeures = Math.Round(saisies.Sum(s => (s.heure_fin - s.heure_deb).TotalHours), 2);

            var projets = saisies
                .Where(s => s.Tache?.Projet != null)
                .Select(s => s.Tache.Projet.nom)
                .Distinct()
                .ToList();

            var etats = saisies
                .Select(s => s.Statut ?? "En attente")
                .Distinct()
                .ToList();

           
            string developpeur = saisies.FirstOrDefault()?.Utilisateur != null
                ? $"{saisies.First().Utilisateur.nom} {saisies.First().Utilisateur.prenom}"
                : "";

            return new HeuresSemaineDTO
            {
                Developpeur = developpeur,
                TotalHeures = totalHeures,
                Projets = projets,
                Etats = etats
            };
        }
        public async Task<IReadOnlyCollection<SaisieTempsDto>> GetSaisiesDeLaSemaineAsync(int utilisateurId, DateTime debut, DateTime fin)
        {
            var entities = await _repository.GetSaisiesTempsParUtilisateurAsync(utilisateurId, debut, fin);

            return entities.Select(e => new SaisieTempsDto
            {
                SaisieId = e.id,
                DateSaisie = e.dateSaisie,
                Heures = e.Duree,
                NomProjet = e.Tache.Projet.nom,
                NomTache = e.Tache.nom,
                NomUtilisateur = e.Utilisateur.nom,
                Prenom=e.Utilisateur.prenom
            }).ToList();
        }

    }
}
