USE Lab07DB;
GO

CREATE OR ALTER PROC USP_ListarCustomers
AS
BEGIN
    SELECT 
        customer_id,
        name,
        address,
        phone
    FROM customers
    WHERE active = 1
END;
GO

CREATE OR ALTER PROC USP_ListarProducts
AS
BEGIN
    SELECT 
        product_id,
        name,
        price,
        stock
    FROM products
    WHERE active = 1
END;
GO

CREATE OR ALTER PROCEDURE USP_InsertarProducto
    @name NVARCHAR(255),
    @price DECIMAL(10,2),
    @stock INT
AS
BEGIN
    INSERT INTO products (name, price, stock, active)
    VALUES (@name, @price, @stock, 1)
END;
GO


CREATE OR ALTER PROCEDURE USP_ListarProductosPaginado
    @Page INT,
    @PageSize INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        product_id,
        name,
        price,
        stock
    FROM products
    WHERE active = 1
    ORDER BY product_id
    OFFSET (@Page - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END;
GO

CREATE OR ALTER PROCEDURE USP_ListarProductosPaginadoFiltrado
    @Nombre NVARCHAR(255),
    @Page INT,
    @PageSize INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        product_id,
        name,
        price,
        stock
    FROM products
    WHERE active = 1 AND name LIKE '%' + @Nombre + '%'
    ORDER BY product_id
    OFFSET (@Page - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END;
GO

CREATE OR ALTER PROCEDURE USP_ActualizarProducto
    @ProductId INT,
    @Name NVARCHAR(255),
    @Price DECIMAL(10,2),
    @Stock INT
AS
BEGIN
    UPDATE products
    SET name = @Name,
        price = @Price,
        stock = @Stock
    WHERE product_id = @ProductId AND active = 1
END
GO

CREATE OR ALTER PROCEDURE USP_EliminarProductoLogico
    @ProductId INT
AS
BEGIN
    UPDATE products
    SET active = 0
    WHERE product_id = @ProductId AND active = 1;
END;
GO
