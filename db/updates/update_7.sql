CREATE TABLE org_emails 
(
	id INT IDENTITY, 
	organization_id INT NULL, 
	title VARCHAR(255) NULL, 
	smtp_server VARCHAR(255) NULL, 
	smtp_username VARCHAR(255) NULL, 
	smtp_password VARCHAR(255) NULL, 
	smtp_port INT NULL, 
	email_reply_to VARCHAR(255) NULL, 
	email_from VARCHAR(255) NULL
)
GO

INSERT INTO updates (revision, update_date) VALUES (7, GETDATE())
GO
