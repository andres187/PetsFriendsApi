using LibidoMusic.Models;
using System.Collections.Generic;

namespace LibidoMusic.Entities
{
    public class Servicio
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public ApplicationUser User { get; set; }
        public List<ServicioSocio> ServicioSocios { get; set; }
    }
}
