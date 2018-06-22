

delete from phrases where label='signup_user.thankyou.englewood'

INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 
'signup_user.thankyou.englewood', 
'Thank you for signing up to be a Watchman on the Wall.  Please allow us 12-24 hours to confirm your request, and then try to log on again.  If you are unable to log in after that time, please contact the church office.' from phrases

   
INSERT  INTO [dbo].[updates]
VALUES  ( 42, GETDATE() )
