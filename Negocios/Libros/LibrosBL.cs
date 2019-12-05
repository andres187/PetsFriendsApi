using LibidoMusic.Contexts;
using LibidoMusic.Datos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LibidoMusic.Negocios
{
    public class LibrosBL
    {
        private readonly ApplicationDbContext _context;
        public LibrosBL(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<AutorDto> Libros()
        {
            var prueba = "pru";
            return _context.Autores.Include(x => x.Books)
                .Select(x =>
                new AutorDto
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    FechaNacimiento = x.FechaNacimiento,
                    Books = x.Books.Select(y =>
                    new LibroDto
                    {
                        Id = y.Id,
                        Titulo = y.Titulo,
                        AutorId = y.AutorId
                    }).ToList()
                }).ToList();
        }
    }
}
