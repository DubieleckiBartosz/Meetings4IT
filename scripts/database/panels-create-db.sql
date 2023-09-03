sp_configure 'user options', 256
RECONFIGURE WITH OVERRIDE;
GO

IF NOT EXISTS(SELECT*FROM sys.databases WHERE name = 'Meetings4IT')
BEGIN
	CREATE DATABASE Meetings4IT
END
GO 
 
