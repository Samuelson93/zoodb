using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZooApp
{
    public class Especies
    {
        public long idEspecie { get; set; }
        public string nombre { get; set; }
        public short nPlazas { get; set; }
        public bool esMascota { get; set; }
        public Clasificacion Clasificacion { get; set; }
        public  TipoAnimal TipoAnimal { get; set; }

    }


}