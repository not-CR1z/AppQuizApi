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
        private readonly IQuizService _quizService;
        public AppController(IUserService userService, IQuizService quizService)
        {
            _userService = userService;
            _quizService = quizService;
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
            var userSelected = await _userService.GetUserInfo(user);
            if (userSelected == null)
            {
                return this.BadRequest(new { message = "Nombre de usuario o contaseña incorrectos" });
            }
            var quizes = await _quizService.GetQuizzes();
            userSelected.Password.Trim();
            return Ok(new { userSelected, quizes });
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
