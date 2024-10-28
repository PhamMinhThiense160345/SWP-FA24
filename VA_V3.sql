-- Tạo database
CREATE DATABASE VegetariansAssistantV3;
GO

-- Sử dụng database
USE VegetariansAssistantV3;
GO

-- Bảng Dietary_Preferences: Lưu trữ các tùy chọn dinh dưỡng
CREATE TABLE Dietary_Preferences (
    id INT PRIMARY KEY IDENTITY(1,1),
    preference_name VARCHAR(50) NOT NULL
);

-- Dữ liệu mẫu cho Dietary_Preferences
INSERT INTO Dietary_Preferences (preference_name)
VALUES
('Vegan'),
('Lacto'),
('Ovo'),
('Lacto-Ovo'),
('Pescatarian');

-- Bảng Roles
CREATE TABLE Roles (
    role_id INT PRIMARY KEY IDENTITY(1,1),
    role_name VARCHAR(50) NOT NULL
);

-- Dữ liệu mẫu cho Roles
INSERT INTO Roles (role_name)
VALUES
('Admin'),
('Staff'),
('Customer'),
('Moderator'),
('Nutritionist');

-- Bảng Users
CREATE TABLE Users (
    user_id INT PRIMARY KEY IDENTITY(1,1),
    username NVARCHAR(50),
    password VARCHAR(50) NOT NULL,
    email VARCHAR(100),
    phone_number VARCHAR(15),
    address VARCHAR(255),
    image_url VARCHAR(255),
    height INT,
    weight INT,
    age INT,
    gender VARCHAR(10),
    profession NVARCHAR(MAX),
    dietary_preference_id INT,
    status VARCHAR(20) DEFAULT 'active',
    role_id INT,
    activity_level NVARCHAR(10),
	goal NVARCHAR(20),
    is_phone_verified BIT DEFAULT 0,
    is_email_verified BIT DEFAULT 0,
    FOREIGN KEY (dietary_preference_id) REFERENCES Dietary_Preferences(id),
    FOREIGN KEY (role_id) REFERENCES Roles(role_id)
);

-- Bảng Dishes
CREATE TABLE Dishes (
    dish_id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(100) NOT NULL,
    dish_type NVARCHAR(50),
    description NVARCHAR(MAX),
    recipe NVARCHAR(MAX),
    image_url NVARCHAR(255),
    dietary_preference_id INT,
    price DECIMAL(10,2),
    status VARCHAR(20) DEFAULT 'active',
    FOREIGN KEY (dietary_preference_id) REFERENCES Dietary_Preferences(id)
);

-- Bảng Ingredients
CREATE TABLE Ingredients (
    ingredient_id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(100) NOT NULL,
	weight DECIMAL(10,2),
    calories DECIMAL(10,2),
    protein DECIMAL(10,2),
    carbs DECIMAL(10,2),
    fat DECIMAL(10,2),
    fiber DECIMAL(10,2),
    vitamin_A DECIMAL(10,2),
    vitamin_B DECIMAL(10,2),
    vitamin_C DECIMAL(10,2),
    vitamin_D DECIMAL(10,2),
    vitamin_E DECIMAL(10,2),
    calcium DECIMAL(10,2),
    iron DECIMAL(10,2),
    magnesium DECIMAL(10,2),
    omega_3 DECIMAL(10,2),
    sugars DECIMAL(10,2),
    cholesterol DECIMAL(10,2),
    sodium DECIMAL(10,2)
);

-- Bảng Dish_Ingredients (N-N)
CREATE TABLE Dish_Ingredients (
    dish_id INT,
    ingredient_id INT,
    weight DECIMAL(10,2),
    PRIMARY KEY (dish_id, ingredient_id),
    FOREIGN KEY (dish_id) REFERENCES Dishes(dish_id),
    FOREIGN KEY (ingredient_id) REFERENCES Ingredients(ingredient_id)
);

-- Bảng Cart
CREATE TABLE Cart (
    user_id INT,
    dish_id INT,
    quantity INT,
    PRIMARY KEY (user_id, dish_id),
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (dish_id) REFERENCES Dishes(dish_id)
);

-- Bảng Orders
CREATE TABLE Orders (
    order_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT,
    total_price DECIMAL(10,2),
    order_date DATETIME,
    delivery_address NVARCHAR(MAX),
	status VARCHAR(255),                            --pending ,processing ,delivering,delivered,canceled,failed (giao hang that bai vi ly do nao do => phai ghi vao phi giao hang that bai)
    delivery_fee DECIMAL(10,2),
	delivery_failed_fee DECIMAL(10,2),
    completed_time DATETIME NULL,
    note TEXT,
    FOREIGN KEY (user_id) REFERENCES Users(user_id)
);

-- Bảng Payment_Details
CREATE TABLE Payment_Details (
    payment_id INT PRIMARY KEY IDENTITY(1,1),
    order_id INT,
    payment_method VARCHAR(20),
    payment_status VARCHAR(20),
    transaction_id VARCHAR(100) NULL,
    payment_date DATETIME,
    amount DECIMAL(10,2),
	refund_amount DECIMAL(10,2),
    FOREIGN KEY (order_id) REFERENCES Orders(order_id)
);

-- Bảng Nutrition_Criteria
CREATE TABLE Nutrition_Criteria (
    criteria_id INT PRIMARY KEY IDENTITY(1,1),
    gender NVARCHAR(MAX),
    age_range VARCHAR(20),
    bmi_range VARCHAR(20),
    profession NVARCHAR(MAX),
    activity_level NVARCHAR(MAX),
    goal NVARCHAR(MAX),
    calories DECIMAL(10,2),
    protein DECIMAL(10,2),
    carbs DECIMAL(10,2),
    fat DECIMAL(10,2),      -- chat beo
    fiber DECIMAL(10,2),     --chat xo
    vitamin_A DECIMAL(10,2),
    vitamin_B DECIMAL(10,2),
    vitamin_C DECIMAL(10,2),
    vitamin_D DECIMAL(10,2),
    vitamin_E DECIMAL(10,2),
    calcium DECIMAL(10,2), --canxi
    iron DECIMAL(10,2),    -- sat
    magnesium DECIMAL(10,2),  --magie
    omega_3 DECIMAL(10,2),
    sugars DECIMAL(10,2),
    cholesterol DECIMAL(10,2),
    sodium DECIMAL(10,2)
);

-- Bảng Users_Nutrition_Criteria (1-1 giữa Users và Nutrition_Criteria)
CREATE TABLE Users_Nutrition_Criteria (
    user_id INT,
    criteria_id INT,
    PRIMARY KEY (user_id, criteria_id),
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (criteria_id) REFERENCES Nutrition_Criteria(criteria_id)
);

-- Bảng Fixed_Menus
CREATE TABLE Fixed_Menus (
    fixed_menu_id INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(100),
    description NVARCHAR(MAX)
);

-- Bảng Fixed_Menu_Items (N-N giữa Fixed_Menus và Dishes)
CREATE TABLE Fixed_Menu_Items (
    id INT PRIMARY KEY IDENTITY(1,1),
    fixed_menu_id INT,
    dish_id INT,
    FOREIGN KEY (fixed_menu_id) REFERENCES Fixed_Menus(fixed_menu_id),
    FOREIGN KEY (dish_id) REFERENCES Dishes(dish_id)
);


-- Bảng Articles: Lưu trữ các bài viết về ăn chay
CREATE TABLE Articles (
    article_id INT PRIMARY KEY IDENTITY(1,1),
    title VARCHAR(255),
    content NVARCHAR(MAX),
	status VARCHAR(20) DEFAULT 'pending',        -- Tên trạng thái (pending, rejected, accepted)
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
    content NVARCHAR(MAX),                         -- Nội dung comment
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
    feedback_content NVARCHAR(MAX),                -- Nội dung feedback
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
    notification_type_name NVARCHAR(MAX) NOT NULL  
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
    notification_type_id INT, 
    content NVARCHAR(MAX),
    sent_date DATETIME DEFAULT GETDATE(),
    status VARCHAR(20) DEFAULT 'unread', -- Các giá trị: 'unread', 'read'
    device_token VARCHAR(255), 
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
    follow_notification BIT DEFAULT 1, -- 1: Bật, 0: Tắt thông báo follow mới
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




CREATE TABLE Membership_Tiers (
    tier_id INT PRIMARY KEY IDENTITY(1,1),
    tier_name VARCHAR(50),              -- Tên loại thành viên (e.g., Silver, Gold, Platinum)
    required_points INT,                -- Điểm tích lũy cần để đạt đến hạng này (e.g., 1000 cho Silver)
    discount_rate DECIMAL(3,2) DEFAULT 0 -- Tỉ lệ giảm giá tương ứng (e.g., 0.10 cho 10%)
);

-- Thêm dữ liệu vào bảng Membership_Tiers
INSERT INTO Membership_Tiers (tier_name, required_points, discount_rate)
VALUES
('Silver', 100, 0.05),  -- Silver với 10% giảm giá
('Gold', 500, 0.10),    -- Gold với 20% giảm giá
('Platinum', 1000, 0.20), -- Platinum với 30% giảm giá
('Diamond', 2000, 0.30); -- Platinum với 30% giảm giá

CREATE TABLE User_Memberships (
    user_id INT PRIMARY KEY,                        -- Liên kết đến bảng Users
    tier_id INT,                                    -- Liên kết đến bảng Membership_Tiers
    accumulated_points INT DEFAULT 0,               -- Số điểm thành viên tích lũy
    discount_granted_date DATETIME NULL,            -- Ngày discount được cấp
    last_discount_used DATETIME NULL,               -- Thời điểm discount được sử dụng lần cuối
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (tier_id) REFERENCES Membership_Tiers(tier_id)
);





