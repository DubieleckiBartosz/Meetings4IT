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
                
        INSERT INTO notifications.Templates (Body, [Type], Created, Modified)
        VALUES ('<!DOCTYPE html>
                    <html lang="en">
                    <head>
                        <meta charset="UTF-8">
                        <meta name="viewport" content="width=device-width, initial-scale=1.0"> 
                        <style>
                            body {
                                font-family: Arial, sans-serif;
                                background-color: #f4f4f4;
                                margin: 0;
                                padding: 0;
                            }
                            .container {
                                max-width: 600px;
                                margin: 0 auto;
                                padding: 20px;
                                background-color: #ffffff;
                                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                            }
                            .header {
                                text-align: center;
                                background-color: #e1e7f0;
                                color: #ffffff;
                                padding: 10px 0;
                            }
                            .content {
                                padding: 20px;  
                            }
                            h1 {
                                color: #333;
                                font-size: 24px;
                            }
                            p {
                                color: #555;
                                font-size: 16px;
                                line-height: 1.5;
                            }
                            .special-link {
                                color: #007bff;
                                text-decoration: none;
                                font-weight: bold;
                            } 
                        </style>
                    </head>
                    <body>
                        <div class="container">
                            <div class="header">
                                <h1>New invitation</h1>
                            </div>
                            <div class="content">
                                <p>Hello,</p>
                                <p>You have just received an invitation to a <a class="special-link" href="{MeetingLink}">meeting</a> organized by {MeetingOrganizer}.</p>
                                <p>If you want to participate, go to the <a class="special-link" href="{InvitationLink}">invitation</a> and just accept! if not, please inform the organizer </p>
                                <p>Thank you for your attention.</p>
                            </div>
                        </div>
                    </body>', 3, GETDATE(), GETDATE())
    END 
END
GO


IF EXISTS (SELECT * FROM sysobjects WHERE name='AlertDetails' and xtype='U')
BEGIN 
    PRINT('Insert alert details...')

        IF NOT EXISTS(SELECT 1 FROM notifications.AlertDetails)
        BEGIN 
            INSERT INTO notifications.AlertDetails (AlertDetailsId, Title, MessageTemplate, Created)
            VALUES (1, 'You have a new invitation from {MeetingOrganizer}',
                       'You have a new {MeetingLink} invitation from {MeetingOrganizer} that is about to expire. Go to the {InvitationLink}, take the code, and accept or decline the invitation.'
        , GETDATE())
        END
END
GO 
