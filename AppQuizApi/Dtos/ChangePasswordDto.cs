namespace AppQuizApi.Dtos
{
    /// <summary>
    /// Clase implementada para la actualización de contraseña de un usuario
    /// </summary>
    public class ChangePasswordDto
    {
        public int UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
