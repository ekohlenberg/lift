 IF ( EXISTS ( SELECT    *
              FROM      [dbo].[updates]
              WHERE     [revision] = 25 ) ) 
    BEGIN
   	
        BEGIN TRANSACTION

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'layout.logout', 'Logout' FROM phrases

        IF ( @@ERROR <> 0 ) 
            BEGIN
                ROLLBACK TRANSACTION
                RETURN
            END

        INSERT  INTO [dbo].[updates]
        VALUES  ( 26, GETDATE() )

        COMMIT TRANSACTION
    END



