-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE ObtenerProducto
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Producto.Id, Producto.IdSubCategoria, Producto.Nombre, Producto.Descripcion, Producto.Precio, 
	Producto.Stock, Producto.CodigoBarras 
	FROM Producto 
	INNER JOIN SubCategorias ON Producto.IdSubCategoria = SubCategorias.Id 
	INNER JOIN Categorias ON SubCategorias.IdCategoria = Categorias.Id
END
GO

CREATE PROCEDURE ObtenerunProducto
	-- Add the parameters for the stored procedure here
	@Id uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Producto.Id, Producto.IdSubCategoria, Producto.Nombre, Producto.Descripcion, Producto.Precio, 
	Producto.Stock, Producto.CodigoBarras 
	FROM Producto 
	INNER JOIN SubCategorias ON Producto.IdSubCategoria = SubCategorias.Id 
	INNER JOIN Categorias ON SubCategorias.IdCategoria = Categorias.Id
	WHERE (Producto.Id = @Id)
END
GO


CREATE PROCEDURE EliminarProducto
	-- Add the parameters for the stored procedure here
	@Id uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE
	FROM Producto 
	WHERE (Producto.Id = @Id)
END
GO

CREATE PROCEDURE InsertarProducto
	-- Add the parameters for the stored procedure here
	@Id AS uniqueidentifier,
	@IdSubCategoria AS uniqueidentifier,
	@Nombre AS Varchar(MAX),
	@Descripcion AS Varchar(MAX),
	@Precio AS Decimal(18,2),
	@Stock AS INT,
	@CodigoBarras AS Varchar(MAX)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[Producto]
           ([Id]
           ,[IdSubCategoria]
           ,[Nombre]
           ,[Descripcion]
           ,[Precio]
           ,[Stock]
           ,[CodigoBarras])
     VALUES
           (@Id,
		    @IdSubCategoria,
		    @Nombre,
		    @Descripcion,
			@Precio,
			@Stock,
			@CodigoBarras)
END
GO

CREATE PROCEDURE EditarProducto
	-- Add the parameters for the stored procedure here
	@Id AS uniqueidentifier,
	@IdSubCategoria AS uniqueidentifier,
	@Nombre AS Varchar(MAX),
	@Descripcion AS Varchar(MAX),
	@Precio AS Decimal(18,2),
	@Stock AS INT,
	@CodigoBarras AS Varchar(MAX)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[Producto]
	SET
           [IdSubCategoria] = @IdSubCategoria
           ,[Nombre] = @Nombre
           ,[Descripcion] = @Descripcion
           ,[Precio] = @Precio
           ,[Stock] = @Stock
           ,[CodigoBarras] = @CodigoBarras
     WHERE Id = @Id
END
GO
