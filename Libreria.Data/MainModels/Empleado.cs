namespace Libreria.Data.MainModels
{
    public partial class Empleado
    {
        public Empleado()
        {
            Compras = new HashSet<Compra>();
            Encargos = new HashSet<Encargo>();
            Usuarios = new HashSet<Usuario>();
            Venta = new HashSet<Ventum>();
        }

        public int IdEmpleado { get; set; }
        public string PrimerNombre { get; set; } = null!;
        public string? SegundoNombre { get; set; }
        public string PrimerApellido { get; set; } = null!;
        public string? SegundoApellido { get; set; }
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Titulo { get; set; } = null!;

        public virtual ICollection<Compra> Compras { get; set; }
        public virtual ICollection<Encargo> Encargos { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
        public virtual ICollection<Ventum> Venta { get; set; }
    }
}
