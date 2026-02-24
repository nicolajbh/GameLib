IF DB_ID ('GameDB') IS NULL
BEGIN
CREATE DATABASE GameDB ;
END
GO

USE GameDB ;
GO

-- Create tables only if they do not exist

-- Users
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'users')
BEGIN
CREATE TABLE users (
user_id INT PRIMARY KEY IDENTITY (1, 1),
name NVARCHAR (50)
) ;
END ;

-- Wallet
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'wallet')
BEGIN
CREATE TABLE wallet (
wallet_id INT PRIMARY KEY IDENTITY (1, 1),
user_id INT,
balance DECIMAL (10, 2),
CONSTRAINT fk_wallet_user FOREIGN KEY (user_id) REFERENCES users (user_id) ON DELETE CASCADE
) ;
END ;

-- Developer
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'developer')
BEGIN
CREATE TABLE developer (
developer_id INTEGER IDENTITY (1, 1) PRIMARY KEY NOT NULL,
studio_name NVARCHAR (50) NOT NULL,
studio_size INTEGER NOT NULL
) ;
END ;

-- Game
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'game')
BEGIN
CREATE TABLE game (
game_id INT PRIMARY KEY IDENTITY (1, 1),
title NVARCHAR (50),
price DECIMAL (10, 2),
rating FLOAT,
developer_id INT,
CONSTRAINT fk_game_developer FOREIGN KEY (developer_id) REFERENCES developer (developer_id) ON DELETE CASCADE
) ;
END ;


-- Category
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'category')
BEGIN
CREATE TABLE category (
category_id INT PRIMARY KEY IDENTITY (1, 1),
name NVARCHAR (50)
) ;
END ;


-- Order
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'order')
BEGIN
CREATE TABLE [order] (
order_id INT PRIMARY KEY IDENTITY (1, 1),
user_id INT,
game_id INT,
[date] DATETIME,
CONSTRAINT fk_order_user FOREIGN KEY (user_id) REFERENCES users (user_id) ON DELETE CASCADE,
CONSTRAINT fk_order_game FOREIGN KEY (game_id) REFERENCES game (game_id) ON DELETE CASCADE
) ;
END ;


-- Library
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'library')
BEGIN
CREATE TABLE library (
user_id INT,
game_id INT,
PRIMARY KEY (user_id, game_id),
CONSTRAINT fk_library_user FOREIGN KEY (user_id) REFERENCES users (user_id) ON DELETE CASCADE,
CONSTRAINT fk_library_game FOREIGN KEY (game_id) REFERENCES game (game_id) ON DELETE CASCADE
) ;
END ;

-- Game Category Junction 
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'game_category')
BEGIN
CREATE TABLE game_category (
game_id INT,
category_id INT,
PRIMARY KEY (game_id, category_id),
CONSTRAINT fk_game_category FOREIGN KEY (game_id) REFERENCES game (game_id) ON DELETE CASCADE,
CONSTRAINT fk_category_game FOREIGN KEY (category_id) REFERENCES category (category_id) ON DELETE CASCADE
) ;
END ;


-- Views
-- User Game Libary
GO
CREATE OR ALTER VIEW vw_UserGameLibrary AS
SELECT
u.user_id,
g.game_id,
g.title AS [GameTitle],
g.rating AS [Rating],
c.category_id,
c.name AS [CategoryName]
FROM users u
JOIN library l ON u.user_id = l.user_id
JOIN game g ON l.game_id = g.game_id
LEFT JOIN game_category gc ON g.game_id = gc.game_id
LEFT JOIN category c ON gc.category_id = c.category_id ;
GO

-- User
GO
CREATE OR ALTER VIEW vw_UserDetails AS
SELECT u.user_id AS user_id,
u.name AS name,
w.wallet_id AS wallet_id,
w.balance AS balance
FROM dbo.users u
JOIN dbo.wallet w
ON u.user_id = w.user_id ;
GO

-- Stored Procedures
