using AppQuizApi.Domain.IServices;
using AppQuizApi.Domain.Models;
using AppQuizApi.Dtos;
using AppQuizApi.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace AppQuizApi.Controllers
//Clase encargada del manejo de solicitudes relacionadas a la administración de usuarios
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly string secretKey;
        private readonly IUserService _userService;
        public UserController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            secretKey = config.GetSection("settings").GetSection("secretKey").ToString()!;
        }

        //Conrolador encargado de la autenticación del usuario
        [HttpPost("login")]
        public async Task<IActionResult> Login(User user)
        {
            user.Password = AppUtilities.EncryptPassword(user.Password);
            User? userSelected = await _userService.Login(user);
            if (userSelected != null)
            {
                userSelected.Password = "";
                var userInfoResponse = new { userSelected.Id, userSelected.UserName, userSelected.Avatar };
                var tokenCreated = AppUtilities.GenerateToken(secretKey, userInfoResponse);
                return Ok(new { token = tokenCreated });
            }
            return this.BadRequest(new { message = "Nombre de usuario o contaseña incorrectos" });
        }

        //Controlador encargado del registro de un nuevo usuario
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

        //Controlador encargado de la actualización de contraseñas
        [Route("changePassword")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
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

        //Controlador encargado de traer la lista avatars disponibles
        [Route("getAvatars")]
        [HttpPost]
        public async Task<IActionResult> GetAvatars()
        {
            var avatarList = await _userService.GetAvatars();
            if (avatarList != null)
            {
                return Ok(avatarList);
            }
            else
            {
                return BadRequest(new { message = "Ha ocurrido un error al obtener los avatars" });
            }
        }

        //Conrolador encargado de la actualización del avatar seleccionado por el usuario
        [Route("updateAvatar")]
        [HttpPost]
        public async Task<IActionResult> UpdateAvatar(User user)
        {
            var avatarList = await _userService.UpdateAvatar(user);
            if (avatarList)
            {
                var userInfoResponse = new { user.Id, user.UserName, user.Avatar };
                var tokenCreated = AppUtilities.GenerateToken(secretKey, userInfoResponse);
                return Ok(new { message = "Se ha actualizado tu avatar", token = tokenCreated });
            }
            else
            {
                return BadRequest(new { message = "Ha ocurrido un error al actualizar tu avatar" });
            }
        }
    }
}
