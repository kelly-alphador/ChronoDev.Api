using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ChronoDev.Domaine.Entities
{
    public class ApplicationUser:IdentityUser<int>
    {
        public string nom {  get; set; }
        public string prenom { get; set; }
    }
}
