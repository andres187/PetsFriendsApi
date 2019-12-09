using LibidoMusic.Contexts;
using LibidoMusic.Datos;
using LibidoMusic.Entities;
using LibidoMusic.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace LibidoMusic.Negocios
{
    public class ServiciosBL
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ServiciosBL(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public List<ServicioDto> Servicios()
        {
            return _context.Servicios
                .Select(x =>
                new ServicioDto
                {
                    Id = x.Id,
                    Titulo = x.Titulo
                }).ToList();
        }

        public List<ServicioSocioDto> ServiciosSocios(int servicioId)
        {
            var resuesta = _context.ServiciosSocios
                .Select(x =>
                new ServicioSocioDto
                {
                    Id = x.Id,
                    ServicioId = x.Servicio.Id,
                    NombreServicio = x.Servicio.Titulo,
                    Titulo = x.Titulo,
                    UserId = x.User.Id,
                    UserName = x.User.UserName
                }).Where(y => y.ServicioId == servicioId).ToList();

            return resuesta;
        }

        public List<ServicioSocioDto> MisServicios(string userName)
        {
            var user = _userManager.FindByNameAsync(userName).GetAwaiter().GetResult();

            var resuesta = _context.ServiciosSocios
                .Select(x =>
                new ServicioSocioDto
                {
                    Id = x.Id,
                    ServicioId = x.Servicio.Id,
                    NombreServicio = x.Servicio.Titulo,
                    Titulo = x.Titulo,
                    UserId = x.User.Id,
                    UserName = x.User.UserName
                }).Where(y => y.UserId == user.Id).ToList();

            return resuesta;
        }

        public ServicioSocioDto CrearServicioSocio(ServicioSocioDto servicioSocioDto, string userName)
        {
            var servicioSave = _context.Servicios.FirstOrDefault(x => x.Id == servicioSocioDto.ServicioId);
            var user = _userManager.FindByNameAsync(userName).GetAwaiter().GetResult();

            ServicioSocio servicioSocio = new ServicioSocio{
                Titulo = servicioSocioDto.Titulo,
                Servicio = servicioSave,
                User = user
            };

            _context.ServiciosSocios.Add(servicioSocio);
            _context.SaveChanges();

            return servicioSocioDto;
        }
    }
}
