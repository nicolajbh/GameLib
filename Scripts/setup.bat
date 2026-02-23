@echo off
echo Setting up database...

sqlcmd -S localhost\SQLEXPRESS -C -i GameDB_DDL.sql

echo Database setup complete
pause
