using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoDev.Application.DTO
{
    public class HeuresSemaineManagerDTO
    {
        public string Developpeur { get; set; } = string.Empty;
        public double TotalHeures { get; set; }
        public int NombreProjets { get; set; }
        public List<string> Projets { get; set; } = new();
        public List<string> Etats { get; set; } = new();
    }
}
