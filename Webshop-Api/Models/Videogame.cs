using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop_Api.Models
{
    public class Videogame
    {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }
        public string gener { get; set; }
        public string language { get; set; }
        public string synopsis { get; set; }
        public int qualification { get; set; }
        public string datepremier { get; set; }
        public string developers { get; set; }
        public string platforms { get; set; }
        public string image { get; set; }

    }
}
