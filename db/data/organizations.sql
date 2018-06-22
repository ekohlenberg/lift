SET NOCOUNT ON


SET IDENTITY_INSERT organizations ON


PRINT 'INSERTING DATA INTO TABLE organizations'
ALTER TABLE [organizations] NOCHECK CONSTRAINT ALL
INSERT INTO [organizations] ([id],[title],[user_id],[address],[city],[state_province],[postal_code],[phone],[created_at]) VALUES (1,'System Org',0,'','','','','', getdate() )
ALTER TABLE [organizations] CHECK CONSTRAINT ALL
SET NOCOUNT ON


SET IDENTITY_INSERT organizations OFF
