-- This script creates the database, the schema, and populates it with mock data.
-- The schema is based on the provided ERD diagrams.

-- =================================================================
-- 0. DATABASE CREATION
-- =================================================================
-- Create the database if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'eStoreDatabase')
BEGIN
    CREATE DATABASE eStoreDatabase;
END
GO

-- Switch to the newly created database context
USE eStoreDatabase;
GO

-- =================================================================
-- 1. TABLE CREATION
-- =================================================================
-- First, we create all the tables without foreign key constraints.
-- Constraints will be added later to avoid dependency issues during creation.

-- Creating the Categories table
CREATE TABLE Categories (
    CategoryId INT IDENTITY(1,1) NOT NULL,
    CategoryName VARCHAR(40) NULL,
    Description NVARCHAR(MAX) NULL
);
GO

-- Creating the Products table
CREATE TABLE Products (
    ProductId INT IDENTITY(1,1) NOT NULL,
    CategoryId INT NULL,
    ProductName VARCHAR(40) NULL,
    Weight VARCHAR(20) NOT NULL,
    UnitPrice MONEY NULL,
    UnitsInStock INT NOT NULL
);
GO

-- Creating the Member table
CREATE TABLE Member (
    MemberId INT IDENTITY(1,1) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    CompanyName VARCHAR(40) NOT NULL,
    City VARCHAR(15) NOT NULL,
    Country VARCHAR(15) NOT NULL,
    Password VARCHAR(30) NOT NULL -- In a real application, passwords should be hashed.
);
GO

-- Creating the Order table
CREATE TABLE [Order] (
    OrderId INT IDENTITY(1,1) NOT NULL,
    MemberId INT NOT NULL,
    OrderDate DATETIME NOT NULL,
    RequiredDate DATETIME NULL,
    ShippedDate DATETIME NULL,
    Freight MONEY NULL
);
GO

-- Creating the OrderDetail table
CREATE TABLE OrderDetail (
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    UnitPrice MONEY NOT NULL,
    Quantity INT NOT NULL,
    Discount FLOAT NOT NULL
);
GO

-- =================================================================
-- 2. ADDING PRIMARY KEYS
-- =================================================================
-- Now we define the primary keys for each table.

ALTER TABLE Categories ADD CONSTRAINT PK_Categories PRIMARY KEY (CategoryId);
GO
ALTER TABLE Products ADD CONSTRAINT PK_Products PRIMARY KEY (ProductId);
GO
ALTER TABLE Member ADD CONSTRAINT PK_Member PRIMARY KEY (MemberId);
GO
ALTER TABLE [Order] ADD CONSTRAINT PK_Order PRIMARY KEY (OrderId);
GO
-- OrderDetail has a composite primary key
ALTER TABLE OrderDetail ADD CONSTRAINT PK_OrderDetail PRIMARY KEY (OrderId, ProductId);
GO

-- =================================================================
-- 3. ADDING FOREIGN KEYS
-- =================================================================
-- Finally, we establish the relationships between the tables using foreign keys.

-- Products -> Categories
ALTER TABLE Products ADD CONSTRAINT FK_Products_Categories
FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId);
GO

-- Order -> Member
ALTER TABLE [Order] ADD CONSTRAINT FK_Order_Member
FOREIGN KEY (MemberId) REFERENCES Member(MemberId);
GO

-- OrderDetail -> Order
ALTER TABLE OrderDetail ADD CONSTRAINT FK_OrderDetail_Order
FOREIGN KEY (OrderId) REFERENCES [Order](OrderId);
GO

-- OrderDetail -> Products
ALTER TABLE OrderDetail ADD CONSTRAINT FK_OrderDetail_Products
FOREIGN KEY (ProductId) REFERENCES Products(ProductId);
GO

-- =================================================================
-- 4. POPULATING WITH MOCK DATA
-- =================================================================
-- Inserting sample data into the tables.

-- Inserting into Categories
INSERT INTO Categories (CategoryName, Description) VALUES
('Beverages', 'Soft drinks, coffees, teas, beers, and ales'),
('Condiments', 'Sweet and savory sauces, relishes, spreads, and seasonings'),
('Confections', 'Desserts, candies, and sweet breads'),
('Dairy Products', 'Cheeses'),
('Grains/Cereals', 'Breads, crackers, pasta, and cereal');
GO

-- Inserting into Products
INSERT INTO Products (ProductName, CategoryId, UnitPrice, Weight, UnitsInStock) VALUES
('Chai', 1, 18.00, '10 boxes x 20 bags', 39),
('Chang', 1, 19.00, '24 - 12 oz bottles', 17),
('Aniseed Syrup', 2, 10.00, '12 - 550 ml bottles', 13),
('Chef Anton''s Cajun Seasoning', 2, 22.00, '48 - 6 oz jars', 53),
('Grandma''s Boysenberry Spread', 2, 25.00, '12 - 8 oz jars', 120),
('Northwoods Cranberry Sauce', 2, 40.00, '12 - 12 oz jars', 6),
('Gumbär Gummibärchen', 3, 31.23, '100 - 250 g bags', 15),
('Schoggi Schokolade', 3, 43.90, '100 - 100 g pieces', 49),
('Gorgonzola Telino', 4, 12.50, '12 - 100 g pkgs', 0),
('Mascarpone Fabioli', 4, 32.00, '24 - 200 g pkgs', 9),
('Singaporean Hokkien Fried Mee', 5, 14.00, '32 - 1 kg pkgs', 26),
('Filo Mix', 5, 7.00, '16 - 2 kg boxes', 38);
GO

-- Inserting into Member
INSERT INTO Member (Email, CompanyName, City, Country, Password) VALUES
('maria.a@example.com', 'Alfreds Futterkiste', 'Berlin', 'Germany', 'password123'),
('ana.t@example.com', 'Ana Trujillo Emparedados', 'México D.F.', 'Mexico', 'password456'),
('antonio.m@example.com', 'Antonio Moreno Taquería', 'México D.F.', 'Mexico', 'password789'),
('thomas.h@example.com', 'Thomas Hardy', 'London', 'UK', 'passwordABC'),
('1@1.com', 'New User Inc.', 'New York', 'USA', '123456'),
('2@2.com', 'Another User Co.', 'Tokyo', 'Japan', '123456');
GO

-- Inserting into Order
-- Note: GETDATE() is used for the order date for simplicity.
INSERT INTO [Order] (MemberId, OrderDate, RequiredDate, ShippedDate, Freight) VALUES
(1, GETDATE() - 20, GETDATE() - 5, GETDATE() - 15, 32.38),
(2, GETDATE() - 18, GETDATE() - 3, GETDATE() - 12, 11.61),
(3, GETDATE() - 15, GETDATE() + 1, GETDATE() - 10, 65.83),
(1, GETDATE() - 10, GETDATE() + 5, GETDATE() - 8, 41.34),
(4, GETDATE() - 5, GETDATE() + 10, NULL, 51.30);
GO

-- Inserting into OrderDetail
INSERT INTO OrderDetail (OrderId, ProductId, UnitPrice, Quantity, Discount) VALUES
(1, 11, 14.00, 12, 0),
(1, 1, 18.00, 10, 0),
(2, 2, 19.00, 5, 0),
(3, 3, 10.00, 3, 0.1),
(3, 7, 31.23, 10, 0.1),
(4, 8, 43.90, 1, 0),
(5, 10, 32.00, 20, 0.05),
(5, 12, 7.00, 30, 0.05);
GO

-- =================================================================
-- SCRIPT END
-- =================================================================
-- You can now run queries against the created and populated tables.
-- For example:
-- SELECT * FROM Member;
-- SELECT p.ProductName, c.CategoryName FROM Products p JOIN Categories c ON p.CategoryId = c.CategoryId;

