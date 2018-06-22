SET NOCOUNT ON


PRINT 'INSERTING DATA INTO TABLE group_relationship_types'
SET IDENTITY_INSERT [group_relationship_types] ON
ALTER TABLE [group_relationship_types] NOCHECK CONSTRAINT ALL
INSERT INTO [group_relationship_types] ([id],[name],[created_at],[updated_at]) VALUES (1,'Member','2007/05/08 10:46:20 AM','2007/05/08 10:46:20 AM')
INSERT INTO [group_relationship_types] ([id],[name],[created_at],[updated_at]) VALUES (2,'Non-Member','2007/05/08 10:46:20 AM','2007/05/08 10:46:20 AM')
ALTER TABLE [group_relationship_types] CHECK CONSTRAINT ALL
SET NOCOUNT ON

SET IDENTITY_INSERT [group_relationship_types] OFF
