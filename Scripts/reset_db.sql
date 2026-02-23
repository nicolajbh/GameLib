USE master;
GO

IF DB_ID ('GameDB') IS NOT NULL
BEGIN
-- Forcefully kick out any active connections (like if you left SSMS open)
ALTER DATABASE GameDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE ;
DROP DATABASE GameDB ;
END ;
GO
