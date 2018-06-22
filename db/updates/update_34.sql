INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'login.forgot_your_password', 'Forgot your password?' from phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'login.request_a_new_one', 'Request a new one' from phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'login.password_not_reset', 'We are sorry but your password could not be reset.' from phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'login.the_new_password', 'The new password is:' from phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'login.your_username', 'Your username is:' from phrases


INSERT  INTO [dbo].[updates]
VALUES  ( 34, GETDATE() )
