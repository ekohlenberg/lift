SET NOCOUNT ON

SET IDENTITY_INSERT [users] ON
PRINT 'INSERTING DATA INTO TABLE users'
ALTER TABLE [users] NOCHECK CONSTRAINT ALL
INSERT INTO [users] ([id],[created_at],[updated_at],[last_logged_in_at],[login_failure_count],[login],[email],[crypted_password],[password_hash_type],[password_salt],[state],[first_name],[last_name],[organization_id],[address],[city],[state_provience],[postal_code],[phone],[previous_increment_id],[time_zone]) VALUES (1,getdate(),getdate(),getdate(),0,'Admin','youremail@yourdomain.com','7d056726781e24e984d23929dac79410','md5','/a5B88H6af',2,'System','Administrator',1,'','','','','',0,NULL)

ALTER TABLE [users] CHECK CONSTRAINT ALL

SET IDENTITY_INSERT [users] OFF
