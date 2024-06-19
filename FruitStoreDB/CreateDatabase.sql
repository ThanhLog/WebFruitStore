CREATE DATABASE StoreDB
GO

USE StoreDB
GO

-- Tạo bảng tài khoản
CREATE TABLE Account (
    UserName NVARCHAR(50) PRIMARY KEY,
    Email NVARCHAR(100),
    Password NVARCHAR(50)
);

-- Tạo bảng người dùng
CREATE TABLE Users (
    UserName NVARCHAR(50) PRIMARY KEY,
    FullName NVARCHAR(100),
    BirthDate DATE,
    Address NVARCHAR(200),
    Email NVARCHAR(100),
    PhoneNumber NVARCHAR(20)
);

-- Tạo bảng sản phẩm
CREATE TABLE Product (
    ProductID INT PRIMARY KEY,
    Image NVARCHAR(200),
    ProductName NVARCHAR(200),
    Origin NVARCHAR(100),
    Price DECIMAL(18, 2),
    Sold INT,
    Stock INT,
    Imported INT
);

-- Tạo bảng giỏ hàng
CREATE TABLE ShoppingCart (
    UserName NVARCHAR(50),
    ProductID INT,
    Image NVARCHAR(200),
    ProductName NVARCHAR(200),
    Quantity INT DEFAULT 1,
    Price DECIMAL(18, 2),
    Origin NVARCHAR(100),
    PRIMARY KEY (UserName, ProductID),
    FOREIGN KEY (UserName) REFERENCES Account(UserName),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

-- Tạo bảng đơn đặt hàng
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY,
    UserName NVARCHAR(50),
    OrderDate DATE,
    FullName NVARCHAR(100),
    ProductID INT,
    Image NVARCHAR(200),
    ProductName NVARCHAR(200),
    Quantity INT,
    Price DECIMAL(18, 2),
    TotalPricePerProduct DECIMAL(18, 2),
    TotalPriceOfOrder DECIMAL(18, 2),
    FOREIGN KEY (UserName) REFERENCES Account(UserName),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

-- Tạo bảng phản hồi
CREATE TABLE Feedback (
    FeedbackID INT PRIMARY KEY,
    UserName NVARCHAR(50),
    FullName NVARCHAR(100),
    Rating INT,
    Comment NVARCHAR(MAX),
    ReviewDate DATE,
    FOREIGN KEY (UserName) REFERENCES Account(UserName)
);


