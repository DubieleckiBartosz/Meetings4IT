IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'panels')
BEGIN
    EXEC('CREATE SCHEMA [panels] AUTHORIZATION [dbo];')
END