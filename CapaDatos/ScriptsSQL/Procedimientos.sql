USE Lab07DB;

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
END


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
END


CREATE OR ALTER PROCEDURE USP_InsertarProducto
    @name NVARCHAR(255),
    @price DECIMAL(10,2),
    @stock INT
AS
BEGIN
    INSERT INTO products (name, price, stock, active)
    VALUES (@name, @price, @stock, 1)
END