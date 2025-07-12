namespace CaloriasApi.Models
{
    public class Alimento
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public double CaloriasPorUnidad { get; set; }
        public bool EsGlobal { get; set; }

        public int? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
