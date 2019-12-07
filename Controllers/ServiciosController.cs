using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LibidoMusic.Contexts;
using LibidoMusic.Datos;
using LibidoMusic.Models;
using LibidoMusic.Negocios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibidoMusic.Controllers
{
    [Route("api")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private readonly ServiciosBL serviciosBL;
        private readonly UserManager<ApplicationUser> _userManager;
        public ServiciosController(ServiciosBL servicios, UserManager<ApplicationUser> userManager)
        {
            serviciosBL = servicios;
            _userManager = userManager;
        }
        // GET: api/Servicios
        [HttpGet("servicios")]
        public ActionResult<IList<ServicioDto>> GetServicios()
        {
            return Ok(new { data = serviciosBL.Servicios() });
        }

        [HttpGet("serviciossocios/{servicioId}")]
        [Authorize(Roles = "Usuario,Socio")]
        public ActionResult<IList<ServicioSocioDto>> GetServiciosSociosByIdServicio(int servicioId)
        {
            return Ok(new { data = serviciosBL.ServiciosSocios(servicioId) });
        }

        [HttpPost("serviciosocio")]
        [Authorize(Roles = "Socio")]
        public ActionResult<ServicioSocioDto> CrearServicioSocio([FromQuery] ServicioSocioDto servicioSocioDto)
        {
            string userName = HttpContext.User.Identity.Name.ToString();
            return Ok(new { data = serviciosBL.CrearServicioSocio(servicioSocioDto, userName) });
        }
    }
}
