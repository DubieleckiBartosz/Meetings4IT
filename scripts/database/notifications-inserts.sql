PRINT 'Insert templates...'

IF EXISTS (SELECT * FROM sysobjects WHERE name='Templates' and xtype='U')
BEGIN 
    
    IF NOT EXISTS(SELECT 1 FROM notifications.Templates)
    BEGIN
        INSERT INTO notifications.Templates (Body, [Type], Created, Modified)
        VALUES ('<!DOCTYPE html><html><body><h2>Hi {UserName}</h2></br><p><strong>
                Confirm your registration:<strong><a href={VerificationUri}>
                confirmation</a></p></body></html>', 1, GETDATE(), GETDATE())

        INSERT INTO notifications.Templates (Body, [Type], Created, Modified)
        VALUES ('<!DOCTYPE html><html><body><h4>Reset Password Email</h4>
                <p>Please use the below token to reset your password with the <code>
                {Path}</code> api route: </p>
                <p><code>{ResetToken}</code></p></body></html>', 2, GETDATE(), GETDATE())
    END

END
    
GO 