 IF ( EXISTS ( SELECT    *
              FROM      [dbo].[updates]
              WHERE     [revision] = 24 ) ) 
    BEGIN
   	
        BEGIN TRANSACTION

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'login.legend', 'Login Details' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'login.user', 'User Name' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'login.user_help', 'Please enter your user name.' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'login.password', 'Password' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'login.password_help', 'Please enter your password.' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'login.button_text', 'Log In' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'login.user_error', 'User Name is required.' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'login.password_error', 'Password is required.' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'login.recovery', 'If you have an account with us and forgot your login please' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'login.recovery_link', 'contact us' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'login.heading', 'Login to Your Account' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'login.register', 'Don''t have an account yet? It only takes a minute.' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'login.register_link', 'Join now' FROM phrases

        IF ( @@ERROR <> 0 ) 
            BEGIN
                ROLLBACK TRANSACTION
                RETURN
            END

        INSERT  INTO [dbo].[updates]
        VALUES  ( 25, GETDATE() )

        COMMIT TRANSACTION
    END



