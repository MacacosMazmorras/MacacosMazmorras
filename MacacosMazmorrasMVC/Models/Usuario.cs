namespace MacacosMazmorrasMVC.Models
{
    public class Usuario
    {
        private int usuarioId;
        private string usuarioName;
        private string usuarioMail;
        private string usuarioPassword;

        public int UsuarioId { get { return usuarioId; } set { usuarioId = value; } }
        public string UsuarioName { get {  return usuarioName; } set { usuarioName = value; } }
        public string UsuarioMail { get {  return usuarioMail; } set { usuarioMail = value; } }
        public string UsuarioPassword { get {  return usuarioPassword; } set { usuarioPassword = value; } }

        public Usuario() { }

        public Usuario(string usuarioMail, string usuarioPassword)
        {
            UsuarioMail = usuarioMail;
            UsuarioPassword = usuarioPassword;
        }
        public Usuario(string usuarioName, string usuarioMail, string usuarioPassword)
        {
            UsuarioName = usuarioName;
            UsuarioMail = usuarioMail;
            UsuarioPassword = usuarioPassword;
        }
        public Usuario(int usuarioId, string usuarioName, string usuarioMail, string usuarioPassword)
        {
            UsuarioId = usuarioId;
            UsuarioName = usuarioName;
            UsuarioMail = usuarioMail;
            UsuarioPassword = usuarioPassword;
        }
    }
}
