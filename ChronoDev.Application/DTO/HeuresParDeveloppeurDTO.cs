using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoDev.Application.DTO
{
    public class HeuresParDeveloppeurDTO
    {
        public string Developpeur { get; set; } = string.Empty;
        public double TotalHeures { get; set; }
        public List<HeuresParProjetDTO> Projets { get; set; } = new();
    }
}
