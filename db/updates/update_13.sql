 IF ( EXISTS ( SELECT   *
               FROM     [dbo].[updates]
               WHERE    [revision] = 12 ) ) 
    BEGIN
   	
        BEGIN TRANSACTION
        
        ALTER TABLE dbo.users DROP CONSTRAINT DF_users_IsApproved 
 
        ALTER TABLE dbo.users DROP CONSTRAINT DF_users_IsLockedOut
 
        CREATE TABLE dbo.Tmp_users
            (
              id INT NOT NULL
                     IDENTITY(1, 1),
              created_at DATETIME NOT NULL,
              updated_at DATETIME NOT NULL,
              last_logged_in_at DATETIME NOT NULL,
              login_failure_count NUMERIC(10, 0) NOT NULL,
              login NVARCHAR(100) NULL,
              email NVARCHAR(200) NULL,
              crypted_password NVARCHAR(100) NULL,
              password_hash_type NVARCHAR(20) NOT NULL,
              password_salt NVARCHAR(10) NOT NULL,
              state INT NOT NULL,
              first_name NVARCHAR(100) NULL,
              last_name NVARCHAR(100) NULL,
              organization_id NUMERIC(10, 0) NULL,
              address NVARCHAR(200) NULL,
              city NVARCHAR(100) NULL,
              state_provience NVARCHAR(100) NULL,
              postal_code NVARCHAR(50) NULL,
              phone NVARCHAR(100) NULL,
              previous_increment_id INT NULL,
              time_zone NVARCHAR(255) NULL,
              isapproved BIT NOT NULL,
              islockedout BIT NOT NULL
            )
        ON  [PRIMARY]
        /* ALTER TABLE dbo.Tmp_users SET ( LOCK_ESCALATION = TABLE */
        ALTER TABLE dbo.Tmp_users
        ADD CONSTRAINT DF_users_IsApproved DEFAULT ( (1) ) FOR isapproved
        ALTER TABLE dbo.Tmp_users
        ADD CONSTRAINT DF_users_IsLockedOut DEFAULT ( (0) ) FOR islockedout
        SET IDENTITY_INSERT dbo.Tmp_users ON
        IF EXISTS ( SELECT  *
                    FROM    dbo.users ) 
            EXEC
                ( 'INSERT INTO dbo.Tmp_users (id, created_at, updated_at, last_logged_in_at, login_failure_count, login, email, crypted_password, password_hash_type, password_salt, state, first_name, last_name, organization_id, address, city, state_provience, postal_code, phone, previous_increment_id, time_zone, isapproved, islockedout)
		SELECT CONVERT(int, id), created_at, updated_at, last_logged_in_at, login_failure_count, login, email, crypted_password, password_hash_type, password_salt, state, first_name, last_name, organization_id, address, city, state_provience, postal_code, phone, previous_increment_id, time_zone, isapproved, islockedout FROM dbo.users WITH (HOLDLOCK TABLOCKX)'
                )
        SET IDENTITY_INSERT dbo.Tmp_users OFF
        DROP TABLE dbo.users
        EXECUTE sp_rename N'dbo.Tmp_users', N'users', 'OBJECT' 
       /* ALTER TABLE dbo.users
        ADD CONSTRAINT PK_users PRIMARY KEY CLUSTERED ( id )
                WITH ( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF,
                       ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON )
                ON [PRIMARY]
		*/
        CREATE UNIQUE NONCLUSTERED INDEX IX_users_login ON dbo.users ( login )
            WITH (
                 STATISTICS_NORECOMPUTE = OFF,
                 IGNORE_DUP_KEY = OFF,
                 ALLOW_ROW_LOCKS = ON,
                 ALLOW_PAGE_LOCKS = ON)
        ON  [PRIMARY]
        CREATE UNIQUE NONCLUSTERED INDEX IX_users_email ON dbo.users ( email )
            WITH (
                 STATISTICS_NORECOMPUTE = OFF,
                 IGNORE_DUP_KEY = OFF,
                 ALLOW_ROW_LOCKS = ON,
                 ALLOW_PAGE_LOCKS = ON)
        ON  [PRIMARY]
        
        ALTER TABLE [dbo].[users]
        ADD last_password_changed_date DATETIME NULL,
            last_locked_out_date DATETIME NULL,
            failed_password_attempt_window_start DATETIME NULL,
            failed_password_answer_attempt_count INT,
            failed_password_answer_attempt_window_start DATETIME NULL,
            password_question NVARCHAR(255) NULL,
            password_answer NVARCHAR(255) NULL,
            comment NVARCHAR(255) NULL

        IF ( @@ERROR <> 0 ) 
            BEGIN
                ROLLBACK TRANSACTION
                RETURN
            END

        INSERT  INTO [dbo].[updates]
        VALUES  ( 13, GETDATE() )

        COMMIT TRANSACTION
    END
