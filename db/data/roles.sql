SET NOCOUNT ON

SET IDENTITY_INSERT [roles] ON
PRINT 'INSERTING DATA INTO TABLE roles'
ALTER TABLE [roles] NOCHECK CONSTRAINT ALL
INSERT INTO [roles] ([id],[created_at],[updated_at],[title]) VALUES (2,'2006/11/22 04:22:14 PM','2006/11/22 04:22:14 PM','Admin')
INSERT INTO [roles] ([id],[created_at],[updated_at],[title]) VALUES (7,'2006/12/06 12:24:37 AM','2007/10/25 06:08:06 PM','Moderator')
INSERT INTO [roles] ([id],[created_at],[updated_at],[title]) VALUES (8,'2006/12/06 12:24:51 AM','2007/01/03 11:02:47 AM','Wall Leader')
INSERT INTO [roles] ([id],[created_at],[updated_at],[title]) VALUES (10,'2006/12/06 05:53:20 PM','2006/12/06 05:53:20 PM','Watchman')
ALTER TABLE [roles] CHECK CONSTRAINT ALL
SET IDENTITY_INSERT [roles] OFF
