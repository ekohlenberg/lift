 IF ( EXISTS ( SELECT    *
              FROM      [dbo].[updates]
              WHERE     [revision] = 9 ) ) 
    BEGIN
   	
        BEGIN TRANSACTION
        
		-- removes duplicate users by login (leaves related records orphaned)
        DELETE  FROM u1
        FROM    users u1
                INNER JOIN ( SELECT MAX(id) AS id,
                                    [login]
                             FROM   users
                             GROUP BY login
                             HAVING COUNT(*) > 1
                           ) u2 ON ( u1.login = u2.login
                                     AND u1.id <> u2.id
                                   )
                                   
		-- removes duplicate users by email (leaves related records orphaned)
        DELETE  FROM u1
        FROM    users u1
                INNER JOIN ( SELECT MAX(id) AS id,
                                    [email]
                             FROM   users
                             GROUP BY email
                             HAVING COUNT(*) > 1
                           ) u2 ON ( u1.email = u2.email
                                     AND u1.id <> u2.id
                                   )                                   
                                   
        -- adds a unique index on login column
        CREATE UNIQUE NONCLUSTERED INDEX [IX_users_login] ON [dbo].[users] ( [login] ASC )
            WITH (
                 STATISTICS_NORECOMPUTE  = OFF,
                 SORT_IN_TEMPDB = OFF,
                 IGNORE_DUP_KEY = OFF,
                 DROP_EXISTING = OFF,
                 ONLINE = OFF,
                 ALLOW_ROW_LOCKS  = ON,
                 ALLOW_PAGE_LOCKS  = ON)
        ON  [PRIMARY]
        
        -- adds a unique index on email column
        CREATE UNIQUE NONCLUSTERED INDEX [IX_users_email] ON [dbo].[users] ( [email] ASC )
            WITH (
                 STATISTICS_NORECOMPUTE  = OFF,
                 SORT_IN_TEMPDB = OFF,
                 IGNORE_DUP_KEY = OFF,
                 DROP_EXISTING = OFF,
                 ONLINE = OFF,
                 ALLOW_ROW_LOCKS  = ON,
                 ALLOW_PAGE_LOCKS  = ON)
        ON  [PRIMARY]

        IF ( @@ERROR <> 0 ) 
            BEGIN
                ROLLBACK TRANSACTION
                RETURN
            END

        INSERT  INTO [dbo].[updates]
        VALUES  ( 10, GETDATE() )

        COMMIT TRANSACTION
    END