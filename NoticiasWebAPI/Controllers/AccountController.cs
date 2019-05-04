using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NoticiasWebAPI.Models;

namespace NoticiasWebAPI.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration; // <-- se almacena la llave en una variable de ambiente.

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this._configuration = configuration;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserInfo model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password); // Crea el usuario.
                if (result.Succeeded)
                {
                    return BuildToken(model);
                }
                else
                {
                    return BadRequest("Username or password invalid");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return BuildToken(userInfo);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        private IActionResult BuildToken(UserInfo userInfo) // <-- Construye el token apartir de la información del usuario.
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim("miValor", "Lo que yo quiera"), // Colocamos los datos del usuario que queremos pasar por el token
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // genera un identificador para el Token
            };

            // Encoding.UTF8.GetBytes convierte un string en un areglo de bytes
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Llave_super_secreta"]));  //  "Llave_super_secreta" no estará en el codigo fuente
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // algorimo de incriptado SecurityAlgorithms.HmacSha256

            var expiracion = DateTime.UtcNow.AddHours(1); // tiempo de expiración del token

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: "yourdomain.com", // entidad que emite el token
               audience: "yourdomain.com",
               claims: claims,
               expires: expiracion,
               signingCredentials: creds); // "creds" son las credenciales de login con la llave secreta y el argloritmo.

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = expiracion
            });

        }



    }
}