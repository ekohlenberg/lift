IF ( EXISTS ( SELECT    *
              FROM      [dbo].[updates]
              WHERE     [revision] = 5 ) ) 
    BEGIN
   	
        BEGIN TRANSACTION

        ALTER TABLE [dbo].[users]
        ADD isapproved BIT NOT NULL
                           CONSTRAINT DF_users_IsApproved DEFAULT 1,
            islockedout BIT NOT NULL
                            CONSTRAINT DF_users_IsLockedOut DEFAULT 0

        IF ( @@ERROR <> 0 ) 
            BEGIN
                ROLLBACK TRANSACTION
                RETURN
            END

        INSERT  INTO [dbo].[updates]
        VALUES  ( 6, GETDATE() )

        COMMIT TRANSACTION
    END
