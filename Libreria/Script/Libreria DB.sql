create database Libreria
go
use Libreria

create table Empleados(
IdEmpleado int identity (1,1) primary key not null,
Primer_nombre varchar(40) not null,
Segundo_nombre varchar(40) ,
Primer_apellido varchar(40) not null,
Segundo_apellido varchar(40),
Direccion varchar(max) not null,
Telefono char(8) check (telefono like '[5|7|8][0-9][0-9][0-9][0-9][0-9][0-9][0-9]') not null,
Titulo nvarchar(25) not null
)

create table Roles(
IdRol int identity (1,1) primary key not null,
Nombre varchar(20),
Descripcion varchar(250) --ENcompra,ENVenta
)

Create table Usuario(
IdUsuario int identity(1,1) primary key not null, 
NombreUsuario varchar(20) not null,
contra varchar(max) not null,
IdRol int foreign key references Roles(IdRol),
IdEmpleado int foreign key references Empleados(IdEmpleado)

)

create table Producto(
IdProducto int identity (1,1) primary key not null,
Nombre_producto varchar(50) not null,
Cantidad int not null,
Precio decimal(19,2) not null,
UPC char(14) check (UPC like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')not null
)
create table Cliente(
IdCliente int identity(1,1) primary key not null,
Email varchar(max) not null,
Telefono char(8) check (telefono like '[5|7|8][0-9][0-9][0-9][0-9][0-9][0-9][0-9]') not null,
Estado varchar(15) not null
)
create table ClienteJuridico(
Nombre_cliente varchar(60) not null,
Nombre_representante varchar(40) not null,
Apellido_representante varchar(40) not null,
Fecha_constitucion date not null,
IdCliente int foreign key references Cliente(IdCliente)
)

create table ClienteNatural(
Primer_nombre varchar(40) not null,
Segundo_nombre varchar(40) ,
Primer_apellido varchar(40) not null,
Segundo_apellido varchar(40),
IdCliente int foreign key references Cliente(IdCliente)
)

create table Venta(
IdVenta int identity (1,1) primary key not null,
Fecha_venta smalldatetime not null,
Total_venta decimal(19,2) not null,
Pago varchar(8) not null,
IdCliente int foreign key references Cliente(IdCliente),
IdEmpleado int foreign key references Empleados(IdEmpleado)
)

Create table DetalleVenta(
IdVenta int foreign key references Venta(IdVenta),
IdProducto int foreign key references Producto(IdProducto),
Cantidad int not null
)

create table Compras(
IdCompra int identity (1,1) primary key not null,
Fecha_compra smalldatetime not null,
Subtotal decimal(19,2) not null,
Total decimal(19,2)not null,
IdEmpleado int foreign key references Empleados(IdEmpleado)
)

create table DetalleCompras(
IdCompra int foreign key references Compras(IdCompra),
IdProducto int foreign key references Producto(IdProducto),
Cantidad int not null
)
go

create table Encargo(
IdEncargo int identity (1,1) primary key not null,
Fecha smalldatetime not null,
Abono decimal (19,2) not null,
Monto_total decimal(19,2) not null,
Pago char(8) not null,
Estado nvarchar(15) not null,
IdCliente int foreign key references Cliente(IdCliente),
IdEmpleado int foreign key references Empleados(IdEmpleado)
)
go

Create table DetalleEncargo(
IdEncago int foreign key references Encargo(IdEncargo),
IdProducto int foreign key references Producto(IdProducto),
Cantidad int not null
)
go

--Procedimientos almacenados
--create procedure Insertar_Empleado
--@PN varchar(40),
--@SN varchar(40),
--@PA varchar(40),
--@SA varchar(40),
--@Dir varchar(max),
--@Tel char(8),
--@pass varchar(max)
--as 
--begin
--insert into Empleados(Primer_nombre,Segundo_nombre,Primer_apellido,Segundo_apellido,Direccion,Telefono,contra) values(@PN,@SN,@PA,@SA,@Dir,@Tel, @pass)
--end
--go

----Registrar Cliente
--create procedure Insertar_Cliente_natural


----Listar Cliente(se filtra en las tablas Juridicas y naturales)

----Actualizar Cliente

----Eliminar Cliente

----Buscar Cliente

----Registrar Producto
--create procedure Insertar_Producto
--@Nombre_producto varchar(50),
--@Cant int,
--@Precio decimal (19,2),
--@UPC char(14)
--as
--begin
--insert into Producto (Nombre_producto,Cantidad,Precio,UPC) values (@Nombre_producto,@Cant,@Precio,@UPC)
--end
--go
----Buscar Producto
--create procedure Buscar_Producto
--@Nomproduct varchar(50)
--as
--select IdProducto, Nombre_producto,Cantidad,Precio,UPC from Producto where @Nomproduct=Nombre_producto
--go
----Listar Producto
--create procedure Listar_Producto
--as
--begin
--Select  IdProducto as ID, Nombre_producto,Cantidad,Precio,UPC from Producto
--end

----Actualizar Producto
--create procedure Actualizar_Producto
--@id int,
--@Nombre_producto varchar(50),
--@Cant int,
--@Precio decimal (19,2),
--@UPC char(14)
--as
--if exists (select IdProducto from Producto where IdProducto=@id)
--begin
--	update Producto set Nombre_producto=@Nombre_producto where IdProducto=@id
--	update Producto set Cantidad= @Cant where IdProducto=@id
--	update Producto set Precio = @Precio where IdProducto= @id
--	update Producto set UPC = @UPC where IdProducto= @id

--end

--Registrar Empleado y login

--Buscar empleado

--Actualizar empleado

--Eliminar empleado y login

--Actualizar login

--Registrar venta


--Actualizar venta

--Listar venta

--Cancelar venta

--Registrar encargo

--Actualizar Encargo

--Buscar Encargo

--Listar Encargo

--Registrar Compra

--Listar Compra

--Buscar Compra

--Actualizar Compra

--Eliminar Compra

