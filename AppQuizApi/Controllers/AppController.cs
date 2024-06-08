using AppQuizApi.Domain.IServices;
using AppQuizApi.Domain.Models;
using AppQuizApi.Dtos;
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
                if (await _userService.ValidateUserExistence(user))
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

        [Route("changePassword")]
        [HttpPost]
        public async Task<IActionResult> CambiarPassword( ChangePasswordDto changePasswordDto)
        {
            changePasswordDto.CurrentPassword = AppUtilities.EncryptPassword(changePasswordDto.CurrentPassword);
            changePasswordDto.NewPassword = AppUtilities.EncryptPassword(changePasswordDto.NewPassword);
            var wasChaged = await _userService.UpdatePassword(changePasswordDto);
            if (wasChaged)
            {
                return Ok(new { message = "Tu contraseña se cambiada correctamente" });
            }
            return BadRequest(new { message = "No se pudo ejecutar la actualización" });
        }
        [Route("getAvatars")]
        [HttpPost]
        public async Task<IActionResult> GetAvatars()
        {
            var avatarList = await _userService.GetAvatars();
            if(avatarList != null)
            {
                return Ok(avatarList);
            }
            else
            {
                return BadRequest(new { message = "Ha ocurrido un error al obtener los avatars" });
            }
        }
        [Route("updateAvatar")]
        [HttpPost]
        public async Task<IActionResult> UpdateAvatar(User user)
        {
            var avatarList = await _userService.UpdateAvatar(user);
            if(avatarList)
            {
                return Ok(new { message = "Se ha actualizado tu avatar" });
            }
            else
            {
                return BadRequest(new { message = "Ha ocurrido un error al actualizar tu avatar" });
            }
        }
    }
}
