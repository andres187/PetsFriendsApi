using LibidoMusic.Models;
using LibidoMusic.Negocios;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibidoMusic.Controllers
{
    [Route("api")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly Token _token;
        public CuentasController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, Token token)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _token = token;
        }

        [HttpPost("usuario/crear")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserInfo userInfo)
        {
            userInfo.Role = "Usuario";
            var user = new ApplicationUser { UserName = userInfo.Email, Email = userInfo.Email };
            var result = await _userManager.CreateAsync(user, userInfo.Password);
            await _userManager.AddToRoleAsync(user, userInfo.Role);
            if (result.Succeeded)
            {
                var role = _userManager.GetRolesAsync(user);
                return Ok(new { data = _token.BuildToken(userInfo, role) });
            }
            else
            {
                return Ok(new { error = "Usuario o contraseña incorrectas." });
            }
        }

        [HttpPost("usuario/login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo userInfo)
        {
            var user = await _userManager.FindByNameAsync(userInfo.Email);
            var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var role = _userManager.GetRolesAsync(user);
                if (role.Result.Contains("Usuario"))
                {
                    return Ok(new { data = _token.BuildToken(userInfo, role) });
                }
                return Ok(new { error = "No tienes los permisos para ingresar." });
            }
            else
            {
                return Ok(new { error = "Usuario o contraseña incorrectas." });
            }
        }

        [HttpPost("socio/crear")]
        public async Task<ActionResult<UserToken>> CreateSocio([FromBody] UserInfo userInfo)
        {
            userInfo.Role = "Socio";
            var user = new ApplicationUser { UserName = userInfo.Email, Email = userInfo.Email };
            var result = await _userManager.CreateAsync(user, userInfo.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, userInfo.Role);
                var role = _userManager.GetRolesAsync(user);
                return Ok(new { data = _token.BuildToken(userInfo, role) });
            }
            else
            {
                return Ok(new { error = "Usuario o contraseña incorrectas." });
            }
        }

        [HttpPost("socio/login")]
        public async Task<ActionResult<UserToken>> LoginSocio([FromBody] UserInfo userInfo)
        {
            var user = await _userManager.FindByNameAsync(userInfo.Email);
            var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var role = _userManager.GetRolesAsync(user);
                if (role.Result.Contains("Socio"))
                {
                    return Ok(new { data = _token.BuildToken(userInfo, role) });
                }
                return Ok(new { error = "No tienes los permisos para ingresar." });
            }
            else
            {
                return Ok(new { error = "Usuario o contraseña incorrectas." });
            }
        }


    }
}
