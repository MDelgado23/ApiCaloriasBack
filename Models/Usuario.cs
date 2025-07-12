namespace CaloriasApi.Models
{
    public enum Rol
    {
        Usuario,
        Administrador
    }

    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public Rol RolUsuario { get; set; }

        public ICollection<Alimento> Alimentos { get; set; } = new List<Alimento>();
        public ICollection<Registro> Registros { get; set; } = new List<Registro>();
    }
}
