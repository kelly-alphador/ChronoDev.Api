using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoDev.Application.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage ="le nom est obligatoir")]
        [StringLength(25,ErrorMessage ="le nom ne depasse pas 25 caracteres")]
        public string Nom {  get; set; }
        [Required(ErrorMessage ="le prenom est obligatoir")]
        [StringLength(25,ErrorMessage ="Le prenom ne depasse pas 50 caracteres")]
        public string prenom {  get; set; }
        [Required(ErrorMessage ="l'Email est obligatoire")]
        [EmailAddress(ErrorMessage ="email invalide")]
        public string Email {  get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        [MinLength(6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Le confirm mot de passe est obligatoire")]
        [Compare("Password",ErrorMessage = "Les mots de passe ne correspondent pas")]
        public string PasswordConfirm {  get; set; }
        [Required(ErrorMessage = "Le rôle est obligatoire")]
        [RegularExpression(("^(?i)(Manager|ChefProjet|Developpeur)$"),
           ErrorMessage = "Le rôle doit être: Manager, ChefProjet ou Developpeur")]
        public string Role {  get; set; }
    }
}
