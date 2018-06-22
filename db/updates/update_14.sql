INSERT INTO phrases ([id], [language_id], [label], [phrase])  
SELECT COUNT(*) + 1, 1, 'shared.my_account', 'My Account' FROM phrases

EXEC sp_rename 'users.state_provience','state_province','COLUMN';
GO

INSERT INTO updates (revision, update_date) VALUES (14, GETDATE())
GO