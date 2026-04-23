﻿CREATE DATABASE CuaHangTrucTuyen;
USE CuaHangTrucTuyen;

CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    BirthDate DATE NULL,
    Gender CHAR(1) NULL,  -- 'M' cho Nam, 'F' cho Nữ, hoặc 'O' cho Khác (DÙNG CHO TRANG LOGIN)
);

CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),   -- ID danh mục
    CategoryName NVARCHAR(100) NOT NULL,        -- Tên danh mục								DÙNG ĐỂ PHÂN LOẠI SẢN PH
);
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),    -- ID sản phẩm
    ProductName NVARCHAR(100) NOT NULL,         -- Tên sản phẩm
    Description NVARCHAR(1000),                 -- Mô tả chi tiết sản phẩm
    Price DECIMAL(18, 2) NOT NULL,              -- Giá sản phẩm
    Quantity INT NOT NULL,                      -- Số lượng trong kho
    CategoryID INT FOREIGN KEY REFERENCES Categories(CategoryID),  -- ID danh mục sản phẩm
	Categoryname INT FOREIGN KEY REFERENCES Categories(CategoryID),  -- ID danh mục sản phẩm

    CreatedDate DATETIME DEFAULT GETDATE()      -- Ngày tạo sản phẩm                         DÙNG CHO TRANG PRODUCT, SHOP,DETAIL
	img nvarchar(50)
);

ALTER TABLE Products
ADD CategoryName NVARCHAR(100);

-- Step 2: Update CategoryName for existing products


CREATE TABLE ShoppingCart (
    CartID INT PRIMARY KEY IDENTITY,
    UserID INT,
    ProductID INT,
    Quantity INT,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID) -- dùng cho trang shopcontroller, productcontroller, view cart và shop và detail
);

CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY(1,1),      -- ID đơn hàng
    UserID INT FOREIGN KEY REFERENCES Users(UserID),  -- ID người dùng
    OrderDate DATETIME DEFAULT GETDATE(),       -- Ngày đặt hàng
    ShippingAddress NVARCHAR(200),              -- Địa chỉ giao hàng
    ShippingCity NVARCHAR(100),                 -- Thành phố giao hàng
    ShippingCountry NVARCHAR(100),              -- Quốc gia giao hàng
    TotalAmount DECIMAL(18, 2) NOT NULL,        -- Tổng số tiền đơn hàng
    Notes NVARCHAR(MAX)                         -- Ghi chú hoặc thông tin bổ sung
);

select *
from Orders

ALTER TABLE Orders
ADD PhoneNumber NVARCHAR(20),                 -- Số điện thoại
    LastName NVARCHAR(100),                   -- Họ khách hàng
    FirstName NVARCHAR(100);                  -- Tên khách hàng

ALTER TABLE Products
DROP COLUMN CategoryName;



CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),      -- ID thanh toán
    OrderID INT FOREIGN KEY REFERENCES Orders(OrderID),  -- ID đơn hàng
    PaymentDate DATETIME DEFAULT GETDATE(),       -- Ngày thanh toán
    Amount DECIMAL(18, 2) NOT NULL,               -- Số tiền thanh toán
);

DELETE FROM Orders;

SELECT ProductID, ProductName, CategoryID FROM Products

select *
from Orders




DROP TABLE IF EXISTS Payments;
DROP TABLE IF EXISTS OrderDetails;
DROP TABLE IF EXISTS Orders;
DROP TABLE IF EXISTS Customers;


INSERT INTO Users (Username, Password, Email, BirthDate, Gender)
VALUES 
('user1', 'password1', 'user1@example.com', '1990-01-01', 'M'),
('user2', 'password2', 'user2@example.com', '1992-02-02', 'F'),
('user3', 'password3', 'user3@example.com', '1993-03-03', 'M');



INSERT INTO Products (ProductName, Description, Price, Quantity)
VALUES 
('Nước Trái Cây', 'Nước ép trái cây tự nhiên được làm từ các loại trái cây tươi như cam, táo, và dứa, thu hoạch từ các vườn trái cây tại Đà Lạt. Sau khi sơ chế, các loại trái cây được ép lạnh để giữ lại tối đa dưỡng chất và hương vị tươi ngon. Không chứa chất bảo quản, nước ép này cung cấp nhiều vitamin và khoáng chất, tốt cho hệ tiêu hóa và tăng cường sức khỏe.', 25000, 100),

('Nước Cam', 'Nước cam tươi nguyên chất được làm từ những quả cam chín mọng thu hoạch tại vườn cam ở Bến Tre. Cam được rửa sạch, bóc vỏ và ép lấy nước bằng công nghệ ép lạnh để giữ nguyên hương vị và chất dinh dưỡng. Giàu vitamin C, nước cam giúp tăng cường hệ miễn dịch và làm đẹp da.', 20000, 150),

('Hạt Dinh Dưỡng', 'Các loại hạt dinh dưỡng bao gồm hạnh nhân, óc chó, và hạt chia, được nhập khẩu từ Mỹ và Úc. Hạt được sơ chế bằng cách rang khô và đóng gói cẩn thận, giữ nguyên hương vị tự nhiên. Sản phẩm này cung cấp nguồn năng lượng dồi dào, giúp tăng cường trí nhớ và tốt cho tim mạch.', 80000, 50),

('Dưa Hấu', 'Dưa hấu tươi ngon được trồng tại các nông trại ở Long An, nổi tiếng với trái to, vỏ mỏng và vị ngọt đậm. Sau khi thu hoạch, dưa hấu được kiểm tra chất lượng, làm sạch và đóng gói kỹ lưỡng. Đây là loại trái cây giải nhiệt tuyệt vời, giúp cung cấp nước và vitamin cần thiết cho cơ thể.', 30000, 70),

('Táo', 'Táo tươi nhập khẩu từ New Zealand, loại táo giòn ngọt với vỏ mỏng và thịt táo thơm ngon. Táo được rửa sạch, đóng gói theo quy trình khép kín và bảo quản lạnh để giữ nguyên độ tươi ngon. Táo giàu chất xơ và vitamin C, tốt cho tiêu hóa và hỗ trợ giảm cân.', 50000, 120),

('Nho Khô', 'Nho khô chất lượng cao được làm từ những trái nho tươi thu hoạch tại các vườn nho ở California, Mỹ. Nho được phơi khô tự nhiên dưới ánh nắng mặt trời, không sử dụng chất bảo quản. Nho khô giàu chất chống oxy hóa, giúp giảm nguy cơ mắc các bệnh tim mạch và ung thư.', 45000, 80),

('Nho Mỹ Tươi', 'Nho tươi nhập khẩu từ California, Mỹ, loại nho đen không hạt với vị ngọt đậm và hương thơm đặc trưng. Sau khi thu hoạch, nho được rửa sạch và bảo quản trong môi trường lạnh để giữ nguyên chất lượng. Nho tươi cung cấp nhiều vitamin và chất chống oxy hóa, tốt cho sức khỏe tim mạch.', 120000, 30),

('Bánh Mỳ Kẹp', 'Bánh mì kẹp thịt tươi ngon được làm từ bánh mì nướng giòn, thịt heo nướng mềm, rau sống, và sốt mayonnaise. Các nguyên liệu được sơ chế và chế biến tại chỗ hàng ngày, đảm bảo độ tươi ngon và an toàn thực phẩm. Bánh mì kẹp cung cấp năng lượng và dưỡng chất, thích hợp cho bữa sáng hoặc bữa ăn nhẹ.', 20000, 200),

('Xoài', 'Xoài chín tự nhiên được thu hoạch từ các vườn xoài tại Đồng Nai, nổi tiếng với vị ngọt thanh và hương thơm đặc trưng. Xoài được rửa sạch, chọn lọc và đóng gói cẩn thận để giữ nguyên chất lượng. Xoài là nguồn cung cấp vitamin A và C, giúp tăng cường sức đề kháng và làm đẹp da.', 35000, 100),

('Thịt Bò (500g)', 'Thịt bò tươi được lấy từ những con bò nuôi tại trang trại ở Tây Ninh, nơi nổi tiếng với chất lượng thịt bò cao. Thịt bò được sơ chế và đóng gói ngay sau khi giết mổ, đảm bảo độ tươi ngon và an toàn vệ sinh. Thịt bò giàu protein, sắt và vitamin B12, tốt cho sự phát triển cơ bắp và sức khỏe tổng thể.', 150000, 40),

('Chuối 4 Quả', 'Chuối tươi ngon, loại chuối xiêm được trồng tại các vườn chuối ở Tiền Giang, nổi tiếng với vị ngọt và mềm. Chuối được thu hoạch, làm sạch và đóng gói trong bó 4 quả, giữ nguyên hương vị tự nhiên. Chuối cung cấp kali, giúp điều hòa huyết áp và hỗ trợ tiêu hóa.', 18000, 90),

('Ổi Ruột Hồng', 'Ổi ruột hồng tươi được trồng tại các vườn ổi ở Bình Dương, nổi tiếng với ruột đỏ hồng, vị ngọt thanh và thơm mát. Sau khi thu hoạch, ổi được làm sạch và đóng gói kỹ lưỡng. Ổi ruột hồng là nguồn cung cấp vitamin C dồi dào, giúp tăng cường hệ miễn dịch và làm đẹp da.', 25000, 75);


INSERT INTO Categories (CategoryName)
VALUES 
('Thực phẩm chế biến'),
('Trái cây tươi'),
('Thực phẩm khô & Hạt'),
('Nước uống tự nhiên'),
('Gói thực phẩm ngẫu nhiên');

ALTER TABLE Products
ADD CategoryID INT;
ALTER TABLE Products
ADD CONSTRAINT FK_Products_Categories
FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID);

UPDATE Products
SET CategoryID = 1 -- ID của "Thực phẩm chế biến"
WHERE ProductName IN ('Bánh Mì Kẹp', 'Gà Rán');

select *
from Products
select *
from Categories

UPDATE Products
SET CategoryID = 1 -- CategoryID tương ứng với 'Thực phẩm chế biến'
WHERE ProductID IN (11); -- Cập nhật theo ProductID cụ thể

UPDATE Products
SET CategoryID = 2 -- CategoryID tương ứng với 'Trái cây tươi'
WHERE ProductID IN (3,4,5,7,8,9);
UPDATE Products
SET CategoryID = 3 -- CategoryID tương ứng với 'Thực phẩm khô & Hạt'
WHERE ProductID IN (6,12); -- Cập nhật theo ProductID cụ thể

UPDATE Products
SET CategoryID = 4 -- CategoryID tương ứng với 'Nước hoa quả'
WHERE ProductID IN (1,2); -- Cập nhật theo ProductID cụ thể

SELECT * FROM Products WHERE CategoryID = 1; -- Thực phẩm chế biến
SELECT * FROM Products WHERE CategoryID = 2; -- Trái cây tươi
SELECT * FROM Products WHERE CategoryID = 3; -- Thực phẩm khô & Hạt
SELECT * FROM Products WHERE CategoryID = 4; -- Nước uống tự nhiên

SELECT * 
FROM Categories 
WHERE CategoryName = 'Thực phẩm chế biến';

SELECT * FROM Products WHERE CategoryID = (SELECT CategoryID FROM Categories WHERE ProductID = 1);

select *
from Categories
where CategoryID =1



