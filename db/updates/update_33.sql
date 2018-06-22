INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'forgot_password.instructions', 'Your new password will be sent to the e-mail address below' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'forgot_password.notification_subject', 'LiftPrayer - Password Reset' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'forgot_password.notification_message', 'Your LiftPrayer password has been reset.' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'email_all_users.caption', 'Email All Users' from phrases

INSERT  INTO [dbo].[updates]
VALUES  ( 33, GETDATE() )



