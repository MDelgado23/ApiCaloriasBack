using CaloriasApi.Models;

namespace CaloriasApi.DTOs
{
    public class LoginResponse
    {
        public bool Autenticado { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
