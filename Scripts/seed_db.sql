USE GameDB;
GO

-- 1. Users (IDs will automatically be 1, 2, 3)
INSERT INTO users (name) VALUES
('Utorian'), ('NoobMaster69'), ('GamerGirl99') ;

-- 2. Wallet (Perfectly matches user_id 1, 2, 3)
INSERT INTO wallet (user_id, balance) VALUES
(1, 250.00), (2, 15.50), (3, 100.00) ;

-- 3. Game (IDs will be 1, 2, 3)
INSERT INTO game (title, price, rating) VALUES
('Elden Ring', 59.99, 9.5),
('Counter-Strike 2', 0.00, 8.2),
('Helldivers 2', 39.99, 9.0) ;

-- 4. Category (IDs will be 1, 2, 3, 4)
INSERT INTO category (name) VALUES
('Action'), ('RPG'), ('Shooter'), ('Co-op') ;

-- 5. Order
INSERT INTO [order] (user_id, game_id, [date]) VALUES
(1, 2, GETDATE ()) ;

-- 6. Library
INSERT INTO library (user_id, game_id) VALUES
(1, 2) ;

-- 7. Game Category
INSERT INTO game_category (game_id, category_id) VALUES
(1, 1), (1, 2), (2, 3), (3, 1), (3, 3), (3, 4) ;
