ALTER TABLE users ADD language_id INT
GO

UPDATE users SET language_id = (SELECT language_id FROM organizations o WHERE o.id = users.organization_id)
GO

/*
DECLARE @role_id INT
DECLARE @user_id INT
DECLARE @id INT

SET @id = 1

DECLARE db_cursor CURSOR FOR 
SELECT role_id, user_id   
FROM roles_users

OPEN db_cursor  
FETCH NEXT FROM db_cursor INTO @role_id, @user_id  

WHILE @@FETCH_STATUS = 0  
BEGIN  
	UPDATE roles_users SET id = @id WHERE role_id = @role_id AND user_id = @user_id 
	SET @id = @id + 1
	FETCH NEXT FROM db_cursor INTO @role_id, @user_id  
END  

CLOSE db_cursor  
DEALLOCATE db_cursor
*/

INSERT INTO updates (revision, update_date) VALUES (16, GETDATE())
GO
