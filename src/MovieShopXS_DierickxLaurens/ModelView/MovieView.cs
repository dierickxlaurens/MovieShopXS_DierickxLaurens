using MovieShopXS_DierickxLaurens.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopXS_DierickxLaurens.ModelView
{
    public class MovieView
    {
        public int MovieId { get; set; }
        public string Titel { get; set; }
        public int Jaar { get; set; }
        public string Beschrijving { get; set; }
        public Byte Rating { get; set; } 
        public string Regisseur { get; set; }
        public string Image { get; set; }
        public  ICollection<ActeurView> Acteurs { get; set; }
    }
}
