USE GameDB;
GO

-- 1. Users
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'users')
BEGIN
DELETE FROM users ;
DBCC CHECKIDENT ('users', RESEED, 0) ;

INSERT INTO users (name) VALUES
('Utorian'), ('NoobMaster69'), ('GamerGirl99') ;
END ;

-- 2. Wallet
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'wallet')
BEGIN
DELETE FROM wallet ;
DBCC CHECKIDENT ('wallet', RESEED, 0) ;

INSERT INTO wallet (user_id, balance) VALUES
(1, 250.00), (2, 15.50), (3, 100.00) ;
END ;

-- 3. Game
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'game')
BEGIN
DELETE FROM game ;
DBCC CHECKIDENT ('game', RESEED, 0) ;

INSERT INTO game (title, price, rating) VALUES
('Elden Ring', 59.99, 9.5),
('Counter-Strike 2', 0.00, 8.2),
('Helldivers 2', 39.99, 9.0) ;
END ;

-- 4. Category
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'category')
BEGIN
DELETE FROM category ;
DBCC CHECKIDENT ('category', RESEED, 0) ;

INSERT INTO category (name) VALUES
('Action'), ('RPG'), ('Shooter'), ('Co-op') ;
END ;

-- 5. Order
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'order')
BEGIN
DELETE FROM [order] ;
DBCC CHECKIDENT ('order', RESEED, 0) ;

INSERT INTO [order] (user_id, game_id, [date]) VALUES
(1, 2, GETDATE ()) ;
END ;

-- 6. Library
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'library')
BEGIN
DELETE FROM library ;
INSERT INTO library (user_id, game_id) VALUES
(1, 2) ;
END ;

-- 7. Game Category
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'game_category')
BEGIN
DELETE FROM game_category ;
INSERT INTO game_category (game_id, category_id) VALUES
(1, 1), (1, 2), (2, 3), (3, 1), (3, 3), (3, 4) ;
END ;
