using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Libreria.Data.MainModels
{
    public partial class LibreriaContext : DbContext
    {
        public LibreriaContext()
        {
        }

        public LibreriaContext(DbContextOptions<LibreriaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<ClienteJuridico> ClienteJuridicos { get; set; } = null!;
        public virtual DbSet<ClienteNatural> ClienteNaturals { get; set; } = null!;
        public virtual DbSet<Compra> Compras { get; set; } = null!;
        public virtual DbSet<DetalleCompra> DetalleCompras { get; set; } = null!;
        public virtual DbSet<DetalleEncargo> DetalleEncargos { get; set; } = null!;
        public virtual DbSet<DetalleVentum> DetalleVenta { get; set; } = null!;
        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<Encargo> Encargos { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<Ventum> Venta { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=Libreria;User Id=sa;Password=passw0rd*");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__Cliente__D5946642CC953E1C");

                entity.ToTable("Cliente");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<ClienteJuridico>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ClienteJuridico");

                entity.Property(e => e.ApellidoRepresentante)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("Apellido_representante");

                entity.Property(e => e.ClienteJuridicoId).ValueGeneratedOnAdd();

                entity.Property(e => e.FechaConstitucion)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_constitucion");

                entity.Property(e => e.NombreCliente)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_cliente");

                entity.Property(e => e.NombreRepresentante)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_representante");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK__ClienteJu__IdCli__31EC6D26");
            });

            modelBuilder.Entity<ClienteNatural>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ClienteNatural");

                entity.Property(e => e.ClienteNaturalId).ValueGeneratedOnAdd();

                entity.Property(e => e.PrimerApellido)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("Primer_apellido");

                entity.Property(e => e.PrimerNombre)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("Primer_nombre");

                entity.Property(e => e.SegundoApellido)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("Segundo_apellido");

                entity.Property(e => e.SegundoNombre)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("Segundo_nombre");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK__ClienteNa__IdCli__33D4B598");
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasKey(e => e.IdCompra)
                    .HasName("PK__Compras__0A5CDB5CECF04CE6");

                entity.Property(e => e.FechaCompra)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Fecha_compra");

                entity.Property(e => e.Subtotal).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(19, 2)");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdEmpleado)
                    .HasConstraintName("FK__Compras__IdEmple__3D5E1FD2");
            });

            modelBuilder.Entity<DetalleCompra>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.DetalleCompraslId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.IdCompraNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdCompra)
                    .HasConstraintName("FK__DetalleCo__IdCom__3F466844");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__DetalleCo__IdPro__403A8C7D");
            });

            modelBuilder.Entity<DetalleEncargo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DetalleEncargo");

                entity.Property(e => e.DetalleEncargolId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.IdEncagoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdEncago)
                    .HasConstraintName("FK__DetalleEn__IdEnc__45F365D3");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__DetalleEn__IdPro__46E78A0C");
            });

            modelBuilder.Entity<DetalleVentum>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.DetalleVentalId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK__DetalleVe__IdPro__3A81B327");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdVenta)
                    .HasConstraintName("FK__DetalleVe__IdVen__398D8EEE");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado)
                    .HasName("PK__Empleado__CE6D8B9E3663B429");

                entity.Property(e => e.Direccion).IsUnicode(false);

                entity.Property(e => e.PrimerApellido)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("Primer_apellido");

                entity.Property(e => e.PrimerNombre)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("Primer_nombre");

                entity.Property(e => e.SegundoApellido)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("Segundo_apellido");

                entity.Property(e => e.SegundoNombre)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("Segundo_nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Titulo).HasMaxLength(25);
            });

            modelBuilder.Entity<Encargo>(entity =>
            {
                entity.HasKey(e => e.IdEncargo)
                    .HasName("PK__Encargo__6A9E6F7649C5DBC7");

                entity.ToTable("Encargo");

                entity.Property(e => e.Abono).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Estado).HasMaxLength(15);

                entity.Property(e => e.Fecha).HasColumnType("smalldatetime");

                entity.Property(e => e.MontoTotal)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("Monto_total");

                entity.Property(e => e.Pago)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Encargos)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK__Encargo__IdClien__4316F928");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Encargos)
                    .HasForeignKey(d => d.IdEmpleado)
                    .HasConstraintName("FK__Encargo__IdEmple__440B1D61");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__Producto__09889210BCBF1640");

                entity.ToTable("Producto");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_producto");

                entity.Property(e => e.Precio).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Upc)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("UPC")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Roles__2A49584C50F20CCA");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__5B65BF97752B80A0");

                entity.ToTable("Usuario");

                entity.Property(e => e.Contra)
                    .IsUnicode(false)
                    .HasColumnName("contra");

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdEmpleado)
                    .HasConstraintName("FK__Usuario__IdEmple__2A4B4B5E");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__Usuario__IdRol__29572725");
            });

            modelBuilder.Entity<Ventum>(entity =>
            {
                entity.HasKey(e => e.IdVenta)
                    .HasName("PK__Venta__BC1240BDE861A8D9");

                entity.Property(e => e.FechaVenta)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Fecha_venta");

                entity.Property(e => e.Pago)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.TotalVenta)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("Total_venta");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK__Venta__IdCliente__36B12243");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.IdEmpleado)
                    .HasConstraintName("FK__Venta__IdEmplead__37A5467C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
