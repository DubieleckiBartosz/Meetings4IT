IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'logs')
BEGIN
    EXEC('CREATE SCHEMA [logs] AUTHORIZATION [dbo];')
END

CREATE TABLE [logs].[IntegrationEventLogs](
	EventId UNIQUEIDENTIFIER NOT NULL,
	[State] INT NOT NULL,
	TimesSent INT NOT NULL,
	CreationTime DATETIME NOT NULL,
	EventTypeName VARCHAR(MAX) NOT NULL, 
	EventTypeShortName VARCHAR(MAX) NOT NULL, 
	Content VARCHAR(MAX) NOT NULL
)
