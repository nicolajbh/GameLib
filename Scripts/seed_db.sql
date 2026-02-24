USE GameDB;
GO

-- 1. Users
-- Adding more diverse users for testing
INSERT INTO users (name) VALUES
('Utorian'),       -- User 1
('NoobMaster69'),  -- User 2
('GamerGirl99'),   -- User 3
('SpeedRunner'),   -- User 4
('CasualGamer') ;  -- User 5

-- 2. Wallet
-- Different balance levels to test the Stored Procedure logic
INSERT INTO wallet (user_id, balance) VALUES
(1, 250.00), -- Rich
(2, 15.50),  -- Poor (can't afford Elden Ring)
(3, 100.00), -- Average
(4, 500.00), -- High balance
(5, 5.00) ;  -- Very low balance

-- 3. Developer
INSERT INTO developer (studio_name, studio_size) VALUES
("CD PROJECT RED", 350),
("XBOX", 512),
("Valve", 220),
("FromSoftware", 400),
("Pocketpair", 150),
("Dave's Studio", 1) ;

-- 3. Game
-- Expanded library with various prices
INSERT INTO game (title, price, rating, developer_id) VALUES
('Elden Ring', 59.99, 9.5, 4),      -- Game 1
('Counter-Strike 2', 0.00, 8.2, 3), -- Game 2
('Helldivers 2', 39.99, 9.0, 2),    -- Game 3
('Palworld', 29.99, 8.5, 5),        -- Game 4
('Cyberpunk 2077', 49.99, 8.8, 1),  -- Game 5
('Stardew Valley', 14.99, 9.8, 6) ; -- Game 6

-- 4. Category
INSERT INTO category (name) VALUES
('Action'), ('RPG'), ('Shooter'), ('Co-op'), ('Indie'), ('Survival') ;

-- 5. Game Category Junction
-- Linking the new games to categories
INSERT INTO game_category (game_id, category_id) VALUES
(1, 1), (1, 2), -- Elden Ring: Action, RPG
(2, 3),         -- CS2: Shooter
(3, 1), (3, 3), (3, 4), -- Helldivers: Action, Shooter, Co-op
(4, 6), (4, 4), -- Palworld: Survival, Co-op
(5, 2), (5, 1), -- Cyberpunk: RPG, Action
(6, 5), (6, 2) ; -- Stardew: Indie, RPG

-- 6. Initial Library (Ownership)
-- Let's give users some starting games
INSERT INTO library (user_id, game_id) VALUES
(1, 2), -- Utorian owns CS2
(4, 1), -- SpeedRunner owns Elden Ring
(4, 5), -- SpeedRunner owns Cyberpunk
(3, 6) ; -- GamerGirl owns Stardew

-- 7. Initial Orders (Receipts for the games above)
INSERT INTO [order] (user_id, game_id, [date]) VALUES
(1, 2, GETDATE ()),
(4, 1, GETDATE ()),
(4, 5, GETDATE ()),
(3, 6, GETDATE ()) ;
