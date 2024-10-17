-- Tạo database
CREATE DATABASE VegetariansAssistantV3;
GO

-- Sử dụng database
USE VegetariansAssistantV3;
GO

-- Bảng Dietary_Preferences: Lưu trữ các tùy chọn dinh dưỡng
CREATE TABLE Dietary_Preferences (
    id INT PRIMARY KEY IDENTITY(1,1),  -- Thiết lập tự động tăng cho cột id
    preference_name VARCHAR(50) NOT NULL -- Tên sở thích ăn uống
);


INSERT INTO Dietary_Preferences (preference_name)
VALUES
('Vegan'),         -- Thuần chay
('Lacto'),         -- Chay có sữa
('Ovo'),           -- Chay có trứng
('Lacto-Ovo'),     -- Chay có sữa và trứng
('Pescatarian');   -- Chay bán phần (ăn cá)

CREATE TABLE Roles (
    role_id INT PRIMARY KEY IDENTITY(1,1), -- Khóa chính, tự động tăng
    role_name VARCHAR(50) NOT NULL         -- Tên vai trò (vd: admin, customer, staff)
);
INSERT INTO Roles (role_name)
VALUES
('Admin'),       -- Vai trò Quản trị viên
('Staff'),       -- Vai trò Nhân viên
('Customer'),   -- Vai trò Khách hàng
('Moderator'),    -- Vai trò Khách hàng
('Nutritionist');    -- Vai trò nutritionist
-- Bảng Users: Lưu trữ thông tin người dùng
CREATE TABLE Users (
    user_id INT PRIMARY KEY IDENTITY(1,1), -- Khóa ch ính, tự động tăng
    username VARCHAR(50) ,         -- Tên đăng nhập
	fullname VARCHAR(50) ,         -- Tên đầy đủ
    password VARCHAR(50) NOT NULL,         -- Mật khẩu
    email VARCHAR(100),                    -- Email người dùng
    phone_number VARCHAR(15),              -- Số điện thoại
	address VARCHAR(15),                   -- địa chỉ 
    image_url VARCHAR(255),                -- Ảnh đại diện của người dùng
    height INT,                            -- Chiều cao của người dùng
    weight INT,                            -- Cân nặng của người dùng
    age INT,                               -- Tuổi
    gender VARCHAR(10),                    -- Giới tính
    profession VARCHAR(50),                -- Nghề nghiệp
    dietary_preference_id INT,             -- Tham chiếu đến bảng Dietary_Preferences (sở thích ăn uống)
    status VARCHAR(20) DEFAULT 'active',   -- Trạng thái người dùng (active, inactive, banned, banSocical)
    role_id INT,                           -- Tham chiếu đến bảng Roles (vai trò người dùng)
	activity_level VARCHAR(10),            -- Cột lưu cường độ hoạt động thể thao(low , med,hig)
    is_phone_verified BIT DEFAULT 0,       -- Xác minh số điện thoại
    FOREIGN KEY (dietary_preference_id) REFERENCES Dietary_Preferences(id), -- Liên kết với bảng Dietary_Preferences
    FOREIGN KEY (role_id) REFERENCES Roles(role_id) -- Liên kết với bảng Roles
);


-- Bảng Dishes (sửa lại để liên kết với bảng Categories)
CREATE TABLE Dishes (
    dish_id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(100) NOT NULL,         -- Tên món ăn, hỗ trợ Unicode
    dish_type NVARCHAR(50),              -- Loại món ăn (e.g., món khai vị, món chính)
    description NVARCHAR(MAX),           -- Miêu tả món ăn, hỗ trợ Unicode
    ingredients NVARCHAR(MAX),           -- Thành phần món ăn, hỗ trợ Unicode
    recipe NVARCHAR(MAX),                -- Cách làm món ăn, hỗ trợ Unicode
    image_url NVARCHAR(255),             -- Hình ảnh món ăn (URL)
    dietary_preference_id INT,           -- Liên kết đến sở thích ăn uống (chay, vegan,...)
    price DECIMAL(10,2),                 -- Giá của món ăn
    FOREIGN KEY (dietary_preference_id) REFERENCES Dietary_Preferences(id)  -- Liên kết đến bảng Dietary_Preferences
);


INSERT INTO Dishes (name, dish_type, description, ingredients, recipe, image_url, dietary_preference_id, price)
VALUES
(N'Salad Rau Xanh', N'Món khai vị', N'Salad rau xanh tươi với xốt dầu giấm', 
 N'Rau xanh, cà chua, dưa leo, xốt dầu giấm', N'Trộn đều rau xanh, cà chua, dưa leo với xốt dầu giấm', 
 N'https://example.com/salad_rau_xanh.jpg', 1, 25.00),

(N'Gỏi Cuốn Chay', N'Món khai vị', N'Gỏi cuốn chay với rau củ và đậu hũ', 
 N'Bánh tráng, rau xanh, đậu hũ, bún', N'Cuốn các nguyên liệu lại bằng bánh tráng và chấm với nước chấm chay', 
 N'https://example.com/goi_cuon_chay.jpg', 2, 30.00),

(N'Phở Chay', N'Món chính', N'Phở chay thơm ngon với nước dùng từ rau củ', 
 N'Bánh phở, rau thơm, nước dùng chay', N'Nấu nước dùng từ rau củ và chan vào bánh phở', 
 N'https://example.com/pho_chay.jpg', 3, 50.00),

(N'Cơm Chiên Dương Châu Chay', N'Món chính', N'Cơm chiên Dương Châu chay với rau củ và đậu hũ', 
 N'Cơm, rau củ, đậu hũ, gia vị', N'Chiên cơm với các loại rau củ và đậu hũ', 
 N'https://example.com/com_chien_duong_chau_chay.jpg', 4, 55.00),

(N'Bún Riêu Chay', N'Món chính', N'Bún riêu chay với nước lèo từ đậu hũ và rau củ', 
 N'Bún, cà chua, đậu hũ, rau thơm', N'Nấu nước lèo từ cà chua và đậu hũ, ăn kèm với bún', 
 N'https://example.com/bun_rieu_chay.jpg', 5, 45.00),

(N'Mì Quảng Chay', N'Món chính', N'Mì Quảng chay đặc trưng miền Trung', 
 N'Mì Quảng, nước dùng chay, rau sống', N'Nấu nước dùng từ rau củ và ăn kèm với mì Quảng', 
 N'https://example.com/mi_quang_chay.jpg', 1, 60.00),

(N'Đậu Hủ Sốt Cà Chua', N'Món chính', N'Đậu hủ sốt cà chua đậm đà', 
 N'Đậu hũ, cà chua, gia vị', N'Chiên đậu hũ và nấu sốt cà chua', 
 N'https://example.com/dau_hu_sot_ca_chua.jpg', 2, 40.00),

(N'Bánh Xèo Chay', N'Món chính', N'Bánh xèo chay với nhân rau củ và nấm', 
 N'Bột bánh xèo, nấm, rau củ, dầu ăn', N'Chiên bánh xèo với nhân nấm và rau củ', 
 N'https://example.com/banh_xeo_chay.jpg', 3, 45.00),

(N'Chè Đậu Xanh', N'Món tráng miệng', N'Chè đậu xanh ngọt mát', 
 N'Đậu xanh, nước cốt dừa, đường', N'Nấu đậu xanh với nước dừa và đường', 
 N'https://example.com/che_dau_xanh.jpg', 4, 20.00),

(N'Bánh Flan Chay', N'Món tráng miệng', N'Bánh flan chay từ sữa đậu nành và đường caramel', 
 N'Sữa đậu nành, đường, caramen', N'Đổ sữa đậu nành vào khuôn caramen và hấp chín', 
 N'https://example.com/banh_flan_chay.jpg', 5, 30.00),

(N'Xôi Đậu Phộng', N'Món tráng miệng', N'Xôi đậu phộng bùi béo', 
 N'Nếp, đậu phộng, nước cốt dừa', N'Nấu xôi với đậu phộng và nước cốt dừa', 
 N'https://example.com/xoi_dau_phong.jpg', 1, 25.00),

(N'Sinh Tố Bơ', N'Đồ uống', N'Sinh tố bơ tươi ngon', 
 N'Bơ, sữa đặc, đường, đá', N'Xay nhuyễn bơ với sữa đặc và đá', 
 N'https://example.com/sinh_to_bo.jpg', 2, 25.00),

(N'Trá Chanh Chanh Dây', N'Đồ uống', N'Trá chanh chanh dây chua ngọt', 
 N'Chanh dây, trà xanh, đường, đá', N'Pha trà xanh và thêm chanh dây, đường', 
 N'https://example.com/tra_chanh_chanh_day.jpg', 3, 20.00),

(N'Cà Ri Chay Lacto', N'Món chính', N'Cà ri chay với nước cốt dừa và khoai tây', 
 N'Khoai tây, nước cốt dừa, cà rốt, gia vị', N'Nấu cà ri chay với nước cốt dừa và khoai tây', 
 N'https://example.com/ca_ri_chay_lacto.jpg', 4, 50.00),

(N'Bún Mắm Chay', N'Món chính', N'Bún mắm chay vị đậm đà', 
 N'Bún, rau thơm, mắm chay, đậu hũ', N'Nấu mắm chay và ăn kèm với bún', 
 N'https://example.com/bun_mam_chay.jpg', 5, 45.00),

(N'Tào Phớ Chay', N'Món tráng miệng', N'Tào phớ chay thanh mát', 
 N'Đậu nành, đường, nước gừng', N'Nấu đậu nành và chế biến thành tào phớ', 
 N'https://example.com/tao_pho_chay.jpg', 1, 20.00),

(N'Bánh Mì Chay', N'Món chính', N'Bánh mì chay với pate chay và rau sống', 
 N'Bánh mì, pate chay, rau sống', N'Nhồi pate chay và rau sống vào bánh mì', 
 N'https://example.com/banh_mi_chay.jpg', 2, 30.00),

(N'Nấm Kho Tiêu Chay', N'Món chính', N'Nấm kho tiêu đậm đà vị mặn ngọt', 
 N'Nấm hương, tiêu, nước tương, gia vị', N'Kho nấm hương với tiêu và nước tương', 
 N'https://example.com/nam_kho_tieu_chay.jpg', 3, 40.00),

(N'Sinh Tố Dâu', N'Đồ uống', N'Sinh tố dâu tươi mát', 
 N'Dâu tây, sữa chua, mật ong', N'Xay nhuyễn dâu tây với sữa chua và mật ong', 
 N'https://example.com/sinh_to_dau.jpg', 4, 25.00),

(N'Bánh Chuối Nướng Chay', N'Món tráng miệng', N'Bánh chuối nướng giòn ngon', 
 N'Chuối, bột mì, đường', N'Nướng chuối với bột mì và đường cho giòn', 
 N'https://example.com/banh_chuoi_nuong_chay.jpg', 5, 35.00);



--1 món ăn có 1 thành phần dinh dưỡng (fix)
CREATE TABLE Nutritional_Info (
    nutritional_info_id INT PRIMARY KEY IDENTITY(1,1),  -- Khóa chính, tự động tăng
    dish_id INT,                                       -- Liên kết đến bảng Dishes
    calories INT,                                      -- Lượng calo
    protein DECIMAL(5,2),                              -- Lượng protein
    carbs DECIMAL(5,2),                                -- Lượng carbohydrate
    fat DECIMAL(5,2),                                  -- Lượng chất béo
   
    FOREIGN KEY (dish_id) REFERENCES Dishes(dish_id)   -- Khóa ngoại liên kết với bảng Dishes
);


CREATE TABLE Fixed_Menus (
    fixed_menu_id INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(100),               -- Tên của menu (menu 1, menu 2,...)
    description TEXT                 -- Mô tả cho menu (tuỳ chọn)
);


CREATE TABLE Fixed_Menu_Items (
    id INT PRIMARY KEY IDENTITY(1,1),
    fixed_menu_id INT,              -- Liên kết đến bảng Fixed_Menus
    dish_id INT,                    -- Liên kết đến bảng Dishes
    /*meal_time VARCHAR(10),          -- Phân loại món ăn theo bữa (breakfast, lunch, dinner)*/
    FOREIGN KEY (fixed_menu_id) REFERENCES Fixed_Menus(fixed_menu_id),
    FOREIGN KEY (dish_id) REFERENCES Dishes(dish_id)
	
);


CREATE TABLE Status (
    status_id INT PRIMARY KEY IDENTITY(1,1),
    status_name VARCHAR(50) NOT NULL -- Tên trạng thái (pending, in_progress, delivering, delivered, cancelled)
);

INSERT INTO Status (status_name)
VALUES
('pending'),        -- Đơn hàng đang chờ xử lý
('in_progress'),    -- Đơn hàng đang được xử lý
('delivering'),      -- Đơn hàng đang  được giao
('delivered'),      -- Đơn hàng đã giao xong
('cancelled');      -- Đơn hàng đã bị hủy


-- Bảng Orders: Lưu trữ thông tin đơn hàng của người dùng, bao gồm start_date và end_date
CREATE TABLE Orders (
    order_id INT PRIMARY KEY IDENTITY(1,1),  -- Khóa chính, tự động tăng
    user_id INT,                             -- ID của người dùng (khách hàng)
    total_price DECIMAL(10,2),               -- Tổng giá trị đơn hàng (bao gồm phí ship)
    order_date DATETIME,                     -- Ngày tạo đơn hàng
    delivery_address VARCHAR(255),           -- Địa chỉ giao hàng
    payment_method VARCHAR(20),              -- Phương thức thanh toán
	note TEXT,                       -- note cho nhà hàng 
    delivery_fee DECIMAL(10,2) NULL,         -- Phí ship được tính toán từ API bên thứ 3
    status_id INT,                           -- Liên kết với bảng Status (trạng thái của đơn hàng)
    completed_time DATETIME NULL,            -- Thời gian khi đơn hàng được giao hoàn tất
    FOREIGN KEY (user_id) REFERENCES Users(user_id),               -- Khóa ngoại liên kết với bảng Users
    FOREIGN KEY (status_id) REFERENCES Status(status_id)           -- Khóa ngoại liên kết với bảng Status
);




-- Bảng OrderItems: Chi tiết đơn hàng
CREATE TABLE OrderItems (
    order_item_id INT PRIMARY KEY IDENTITY(1,1),
    order_id INT,                            -- Liên kết đến bảng Orders
    dish_id INT,                             -- Liên kết đến bảng Dishes
    quantity INT,                            -- Số lượng món ăn trong đơn hàng
    FOREIGN KEY (order_id) REFERENCES Orders(order_id),   -- Liên kết đến bảng Orders
    FOREIGN KEY (dish_id) REFERENCES Dishes(dish_id)      -- Liên kết đến bảng Dishes
);

-- Bảng Articles: Lưu trữ các bài viết về ăn chay
CREATE TABLE Articles (
    article_id INT PRIMARY KEY IDENTITY(1,1),
    title VARCHAR(255),
    content TEXT,
	status VARCHAR(20) DEFAULT 'pending',
    author_id INT,
    moderate_date DATE,
    FOREIGN KEY (author_id) REFERENCES Users(user_id)
);

--Lưu trữ tim 
CREATE TABLE Article_Likes (
    like_id INT PRIMARY KEY IDENTITY(1,1),  -- Khóa chính, tự động tăng
    article_id INT,                         -- Liên kết với bảng Articles
    user_id INT,                            -- Liên kết với bảng Users (người đã "tim")
    like_date DATETIME DEFAULT GETDATE(),   -- Ngày "tim" bài viết
    FOREIGN KEY (article_id) REFERENCES Articles(article_id), -- Khóa ngoại đến bài viết
    FOREIGN KEY (user_id) REFERENCES Users(user_id)           -- Khóa ngoại đến người dùng
);

-- Bảng Comments: Bình luận của người dùng về món ăn và thực đơn

CREATE TABLE Comments (
    comment_id INT PRIMARY KEY IDENTITY(1,1),
    article_id INT NOT NULL,              -- Liên kết đến bảng Articles
    user_id INT NOT NULL,                 -- Liên kết đến người dùng
    content TEXT,                         -- Nội dung comment
    post_date DATETIME DEFAULT GETDATE(), -- Ngày comment
    FOREIGN KEY (article_id) REFERENCES Articles(article_id),
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);

CREATE TABLE Feedbacks (
    feedback_id INT PRIMARY KEY IDENTITY(1,1),
    dish_id INT NOT NULL,                 -- Liên kết đến bảng Dishes
    user_id INT NOT NULL,                 -- Liên kết đến người dùng
    order_id INT NOT NULL,                -- Liên kết đến đơn hàng (đảm bảo người dùng đã mua món ăn)
    rating DECIMAL(2,1) NULL CHECK (rating >= 0 AND rating <= 5), -- Rating cho món ăn
    feedback_content TEXT,                -- Nội dung feedback
    feedback_date DATETIME DEFAULT GETDATE(), -- Ngày phản hồi
    FOREIGN KEY (dish_id) REFERENCES Dishes(dish_id),
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (order_id) REFERENCES Orders(order_id)
);



-- Bảng để lưu nhiều hình ảnh cho bài viết
CREATE TABLE ArticleImages (
    article_image_id INT PRIMARY KEY IDENTITY(1,1),
    article_id INT,
    image_url VARCHAR(255),
    FOREIGN KEY (article_id) REFERENCES Articles(article_id)
);

CREATE TABLE Notification_Types (
    notification_type_id INT PRIMARY KEY IDENTITY(1,1),
    notification_type_name VARCHAR(50) NOT NULL  -- Tên loại thông báo (new_article, order_status, promotion, friend_request, etc.)
);

INSERT INTO Notification_Types (notification_type_name)
VALUES 
('new_article'),
('order_status'),
('new_promotion'),
('new_follower');


CREATE TABLE Notifications (
    notification_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT,
    notification_type_id INT, -- Liên kết đến bảng Notification_Types
    content TEXT,
    sent_date DATETIME DEFAULT GETDATE(),
    status VARCHAR(20) DEFAULT 'unread', -- Các giá trị: 'unread', 'read'
    device_token VARCHAR(255), -- Thêm device token để đẩy thông báo
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (notification_type_id) REFERENCES Notification_Types(notification_type_id)
);



--cài đặt thông báo 
CREATE TABLE Notification_Settings (
    setting_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT,
    new_article_notification BIT DEFAULT 1,  -- 1: Bật, 0: Tắt thông báo bài viết mới
    order_status_notification BIT DEFAULT 1,  -- 1: Bật, 0: Tắt thông báo đơn hàng
    promotion_notification BIT DEFAULT 1,  -- 1: Bật, 0: Tắt thông báo khuyến mãi
    follow_notification BIT DEFAULT 1, -- Thêm thông báo kết bạn mới
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);

CREATE TABLE Followers (
    follower_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT,                            -- Người được theo dõi (followed)
    follower_user_id INT,                   -- Người theo dõi (follower)
    follow_date DATETIME,                   -- Ngày bắt đầu theo dõi
    FOREIGN KEY (user_id) REFERENCES Users(user_id),          -- Khóa ngoại tới người được theo dõi
    FOREIGN KEY (follower_user_id) REFERENCES Users(user_id)  -- Khóa ngoại tới người theo dõi
);

CREATE TABLE Followings (
    following_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT,                             -- Người theo dõi (follower)
    following_user_id INT,                   -- Người được theo dõi (followed)
    follow_date DATETIME,                    -- Ngày bắt đầu theo dõi
    FOREIGN KEY (user_id) REFERENCES Users(user_id),          -- Khóa ngoại tới người theo dõi
    FOREIGN KEY (following_user_id) REFERENCES Users(user_id) -- Khóa ngoại tới người được theo dõi
);


--phương thức thanh toán 
CREATE TABLE Payment_Methods (
    payment_method_id INT PRIMARY KEY IDENTITY(1,1),
    payment_method_name VARCHAR(50) NOT NULL -- Tên phương thức (direct, bank card, zalo pay)
);
--thông tin thanh toán 
CREATE TABLE Payment_Details (
    payment_id INT PRIMARY KEY IDENTITY(1,1),
    order_id INT,                                -- Liên kết với bảng Orders
    payment_method_id INT,                       -- Liên kết với bảng Payment_Methods
    payment_status VARCHAR(20),                  -- Trạng thái thanh toán (pending, completed, failed)
    transaction_id VARCHAR(100) NULL,            -- Mã giao dịch (cho thanh toán qua thẻ, Zalo Pay)
    payment_date DATETIME,                       -- Ngày thanh toán
    amount DECIMAL(10,2),                        -- Số tiền thanh toán
    FOREIGN KEY (order_id) REFERENCES Orders(order_id),                  -- Khóa ngoại tới bảng Orders
    FOREIGN KEY (payment_method_id) REFERENCES Payment_Methods(payment_method_id)  -- Khóa ngoại tới bảng Payment_Methods
);


CREATE TABLE Membership_Tiers (
    tier_id INT PRIMARY KEY IDENTITY(1,1),
    tier_name VARCHAR(50),              -- Tên loại thành viên (e.g., Silver, Gold, Platinum)
    required_points INT,                -- Điểm tích lũy cần để đạt đến hạng này (e.g., 1000 cho Silver)
    discount_rate DECIMAL(3,2) DEFAULT 0 -- Tỉ lệ giảm giá tương ứng (e.g., 0.10 cho 10%)
);

-- Thêm dữ liệu vào bảng Membership_Tiers
INSERT INTO Membership_Tiers (tier_name, required_points, discount_rate)
VALUES
('Silver', 1000, 0.10),  -- Silver với 10% giảm giá
('Gold', 2000, 0.20),    -- Gold với 20% giảm giá
('Platinum', 3000, 0.30); -- Platinum với 30% giảm giá

CREATE TABLE User_Memberships (
    user_id INT PRIMARY KEY,                        -- Liên kết đến bảng Users
    tier_id INT,                                    -- Liên kết đến bảng Membership_Tiers
    accumulated_points INT DEFAULT 0,               -- Số điểm thành viên tích lũy
    discount_granted_date DATETIME NULL,            -- Ngày discount được cấp
    last_discount_used DATETIME NULL,               -- Thời điểm discount được sử dụng lần cuối
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (tier_id) REFERENCES Membership_Tiers(tier_id)
);





