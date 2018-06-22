INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'signup_user.user_registration_failed', 'User registration was not successful.  Please check your information and try again.' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'signup_user.username_taken', 'The username is already taken.' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'signup_user.email_address_in_use', 'That email address is already in use.' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'request.update_not_successful', 'Your update was not successful.  Please check your information and try again.' from phrases
-- ran to here

INSERT  INTO [dbo].[updates]
VALUES  ( 35, GETDATE() )
