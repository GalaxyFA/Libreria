using Libreria.Data.MainModels;
using System.ComponentModel.DataAnnotations;

namespace Libreria.Data.Empleados;

public class RegistroEmpleado
{
    public RegistroEmpleado()
    {
    }

    [Required]
    public string PrimerNombre { get; set; } = null!;
    public string? SegundoNombre { get; set; }
    public string PrimerApellido { get; set; } = null!;
    public string? SegundoApellido { get; set; }
    public string Direccion { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    [Required]
    public string Titulo { get; set; } = null!;
}
