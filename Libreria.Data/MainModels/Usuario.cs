using System;
using System.Collections.Generic;

namespace Libreria.Data.MainModels
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string Contra { get; set; } = null!;
        public int? IdRol { get; set; }
        public int? IdEmpleado { get; set; }

        public virtual Empleado? IdEmpleadoNavigation { get; set; }
        public virtual Role? IdRolNavigation { get; set; }
    }
}
