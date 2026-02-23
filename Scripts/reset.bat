@echo off

echo Deleting database...
sqlcmd -S localhost\SQLEXPRESS -C -i reset.sql

echo Setting up database...
sqlcmd -S localhost\SQLEXPRESS -C -i GameDB_DDL.sql

echo Seeding data...
sqlcmd -S localhost\SQLEXPRESS -d GameDB -C -i seed_db.sql

echo Database reset!
pause
