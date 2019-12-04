using LibidoMusic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibidoMusic.Entities
{
    public class ServicioSocio
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public Servicio Servicio { get; set; }
        public ApplicationUser User { get; set; }
    }
}
