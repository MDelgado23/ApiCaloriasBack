namespace CaloriasApi.Models
{
    public class Registro
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public double Cantidad { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public int AlimentoId { get; set; }
        public Alimento? Alimento { get; set; }
    }
}
