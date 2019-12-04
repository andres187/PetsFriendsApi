using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibidoMusic.Contexts;
using LibidoMusic.Datos;
using LibidoMusic.Entities;
using LibidoMusic.Negocios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibidoMusic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ValuesController : ControllerBase
    {
        private readonly LibrosBL librosBL;
        public ValuesController(LibrosBL libros)
        {
            librosBL = libros;
        }
        // GET api/values
        [HttpGet]


        [AllowAnonymous]
        public ActionResult<IList<AutorDto>> Get()
        {
            //return _context.Libro.Include(x => x.Autor).ToList();
            return librosBL.Libros();
        }
    }
}
