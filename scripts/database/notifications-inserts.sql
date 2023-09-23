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
                    </body>
                    </html>', 3, GETDATE(), GETDATE())
                    
        INSERT INTO notifications.Templates (Body, [Type], Created, Modified)
        VALUES ('<!DOCTYPE html>
                    <html>
                    <head>
                        <meta charset="UTF-8"> 
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
                                border-radius: 5px;
                                box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
                            }
                            h1 {
                                color: #333;
                                text-align: center;
                            }
                            p {
                                color: #555;
                                font-size: 16px;
                                line-height: 1.4;
                            }
                            .button {
                                display: inline-block;
                                padding: 10px 20px;
                                background-color: #007BFF;
                                color: #ffffff;
                                text-decoration: none;
                                border-radius: 5px;
                                margin-top: 20px;
                            }
                            .button:hover {
                                background-color: #0056b3;
                            }
                        </style>
                    </head>
                    <body>
                        <div class="container"> 
                            <p>Hello,</p>
                            <p>Unfortunately, the organizer <strong>{MeetingOrganizer}</strong> canceled the meeting and with it your invitation was canceled.</p>
                            <p>We apologize for any inconvenience this may cause.</p> 
                            <p>Warm regards,</p> 
                            <p>Meetings4IT team</p>
                            <a class="button" href="{MeetingLink}">View Details</a>
                        </div>
                    </body>
                    </html>', 4, GETDATE(), GETDATE())
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
                       'You have a new invitation to the <a href="{MeetingLink}">meeting</a> from {MeetingOrganizer} that will expire soon. Go to the 
                       <a href="{InvitationLink}">invitation</a>, take the code, and accept or decline the invitation.'
            , GETDATE())
        
            INSERT INTO notifications.AlertDetails (AlertDetailsId, Title, MessageTemplate, Created)
                VALUES (2, 'You have a new guest',
                           '{NameInvitationRecipient} accepted your invitation to the <a href="{MeetingLink}">meeting</a>!'
            , GETDATE())
            
            INSERT INTO notifications.AlertDetails (AlertDetailsId, Title, MessageTemplate, Created)
                VALUES (3, 'Your invitation has been rejected',
                           '{RejectedBy} has been rejected you invitation to the <a href="{MeetingLink}">meeting</a>'
            , GETDATE())
            
            INSERT INTO notifications.AlertDetails (AlertDetailsId, Title, MessageTemplate, Created)
                VALUES (4, 'Your invitation has been canceled',
                           '{MeetingOrganizer} canceled meeting. You can check the reason by clicking <a href="{MeetingLink}">link</a>'
            , GETDATE())
        END
END
GO 
