using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibidoMusic.Datos
{
    public class ServicioSocioDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int ServicioId { get; set; }
        public string NombreServicio { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
