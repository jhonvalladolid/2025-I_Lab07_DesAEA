USE Lab07DB;

-- Crear la tabla 'customers'
CREATE TABLE customers (
    customer_id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(255) NOT NULL,
    address NVARCHAR(255),
    phone NVARCHAR(15),
    active BIT NOT NULL DEFAULT 1
);

-- Crear la tabla 'products'
CREATE TABLE products (
    product_id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(255) NOT NULL,
    price DECIMAL(10, 2) NOT NULL,
    stock INT NOT NULL,
    active BIT NOT NULL DEFAULT 1
);

-- Crear la tabla 'invoices'
CREATE TABLE invoices (
    invoice_id INT PRIMARY KEY IDENTITY(1,1),
    customer_id INT,
    date DATETIME NOT NULL,
    total DECIMAL(10, 2) NOT NULL,
    active BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (customer_id) REFERENCES customers(customer_id)
);

-- Crear la tabla 'invoice_details'
CREATE TABLE invoice_details (
    detail_id INT PRIMARY KEY IDENTITY(1,1),
    invoice_id INT,
    product_id INT,
    quantity INT NOT NULL,
    price DECIMAL(10, 2) NOT NULL,
    subtotal DECIMAL(10, 2) NOT NULL,
    active BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (invoice_id) REFERENCES invoices(invoice_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);


-- Insertar datos en la tabla 'customers'
INSERT INTO customers (name, address, phone, active)
VALUES
    ('John Doe', '123 Main St', '555-1234', 1),
    ('Jane Smith', '456 Elm St', '555-5678', 1),
    ('Bob Johnson', '789 Oak St', '555-9012', 1);

-- Insertar datos en la tabla 'products'
INSERT INTO products (name, price, stock, active)
VALUES
    ('Product A', 10.99, 100, 1),
    ('Product B', 15.99, 75, 1),
    ('Product C', 7.49, 150, 1);

-- Insertar datos en la tabla 'invoices'
INSERT INTO invoices (customer_id, date, total, active)
VALUES
    (1, '2023-10-10 10:00:00', 32.97, 1),
    (2, '2023-10-11 11:30:00', 47.97, 1),
    (3, '2023-10-12 09:15:00', 22.47, 1);

-- Insertar datos en la tabla 'invoice_details'
INSERT INTO invoice_details (invoice_id, product_id, quantity, price, subtotal, active)
VALUES
    (1, 1, 3, 10.99, 32.97, 1),
    (2, 2, 2, 15.99, 31.98, 1),
    (3, 3, 5, 7.49, 37.45, 1);