using AppQuizApi.Domain.IServices;
using AppQuizApi.Domain.Models;
using AppQuizApi.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppQuizApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly IUserService _userService;
        public AppController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("addUser")]
        public async Task<IActionResult> AddUser(User user)
        {
            try
            {
                if (await _userService.ValidateExistence(user))
                {
                    return BadRequest(new { message = $"El usuario {user.UserName} ya existe" });
                }

                user.Password = AppUtilities.EncryptPassword(user.Password);
                await _userService.SaveUser(user);
                return Ok(new { message = $"Usuario guardado" });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(User user)
        {
            user.Password = AppUtilities.EncryptPassword(user.Password);
            User? userSelected = await _userService.Login(user);
            if (userSelected != null)
            {
                userSelected.Password = "";
                var userInfoResponse = new { userSelected.Id, userSelected.UserName, userSelected.Avatar };
                return Ok(userInfoResponse);
            }
            return this.BadRequest(new { message = "Nombre de usuario o contaseña incorrectos" });
        }

        //[Route("CambiarPassword")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[HttpPut]
        //public async Task<IActionResult> CambiarPassword([FromBody] Passwords passwords)
        //{
        //    try
        //    {
        //        var identity = this.HttpContext.User.Identity as ClaimsIdentity;

        //        Int32 idUsuario = JwtConfigurator.intGetTokenIdUsuario(identity);
        //        String passwordEnciptado = AppUtilities.EncryptPassword(passwords.Password);
        //        var usuario = await this._userService.ValidatePassword(idUsuario, passwordEnciptado);
        //        if (usuario == null)
        //        {
        //            return this.BadRequest(new { message = "La contraseña es incorrecta" });
        //        }
        //        else
        //        {
        //            usuario.Password = AppUtilities.EncryptPassword(passwords.NewPassword);
        //            await this._userService.UpdatePassword(usuario);
        //            return this.Ok(new { message = "La password fue actualizada con éxtito!" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.BadRequest(ex.Message);
        //    }
        //}
    }
}
