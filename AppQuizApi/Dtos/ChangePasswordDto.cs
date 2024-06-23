namespace AppQuizApi.Dtos
{
    // Clase implementada para la actualización de contraseña de un usuario
    public class ChangePasswordDto
    {
        public int UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
