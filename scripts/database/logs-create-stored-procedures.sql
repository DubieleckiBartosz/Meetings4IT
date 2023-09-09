CREATE OR ALTER PROCEDURE [logs].[integration_saveEventLog_I]
    @eventId UNIQUEIDENTIFIER,
    @creationTime DATETIME,
    @eventTypeName VARCHAR(MAX),
    @content VARCHAR(MAX),
    @state INT,
    @timesSent INT,
    @eventTypeShortName VARCHAR(MAX)
AS
BEGIN
    INSERT INTO [logs].[IntegrationEventLogs] (
        EventId,
        [State],
        TimesSent,
        CreationTime,
        EventTypeName,
        EventTypeShortName, 
        Content
    )
    VALUES (
        @eventId,
        @state,
        @timesSent,
        @creationTime,
        @eventTypeName,
        @eventTypeShortName, 
        @content
    )
END
GO

CREATE OR ALTER PROCEDURE [logs].[integration_updateEventLog_U]
    @eventId UNIQUEIDENTIFIER,
    @newStatus INT,
	@sent BIT
AS
BEGIN
	UPDATE IntegrationEventLogs
		SET [State] = @newStatus, TimesSent = IIF(@sent = 1, TimesSent + 1, TimesSent)
	WHERE EventId = @eventId
END
GO