SET NOCOUNT ON


PRINT 'INSERTING DATA INTO TABLE requesttypes'
SET IDENTITY_INSERT [requesttypes] ON
ALTER TABLE [requesttypes] NOCHECK CONSTRAINT ALL
INSERT INTO [requesttypes] ([id],[title]) VALUES (1,'Birth')
INSERT INTO [requesttypes] ([id],[title]) VALUES (2,'Church Staff')
INSERT INTO [requesttypes] ([id],[title]) VALUES (3,'Church Vision/Strategy')
INSERT INTO [requesttypes] ([id],[title]) VALUES (4,'Death')
INSERT INTO [requesttypes] ([id],[title]) VALUES (5,'Financial')
INSERT INTO [requesttypes] ([id],[title]) VALUES (6,'Government')
INSERT INTO [requesttypes] ([id],[title]) VALUES (7,'Hospitalized')
INSERT INTO [requesttypes] ([id],[title]) VALUES (8,'Medical')
INSERT INTO [requesttypes] ([id],[title]) VALUES (9,'Military')
INSERT INTO [requesttypes] ([id],[title]) VALUES (10,'Missionary')
INSERT INTO [requesttypes] ([id],[title]) VALUES (11,'Personal')
INSERT INTO [requesttypes] ([id],[title]) VALUES (12,'Praise')
INSERT INTO [requesttypes] ([id],[title]) VALUES (13,'Relationships')
INSERT INTO [requesttypes] ([id],[title]) VALUES (14,'Salvation')
INSERT INTO [requesttypes] ([id],[title]) VALUES (15,'Other')
ALTER TABLE [requesttypes] CHECK CONSTRAINT ALL
SET IDENTITY_INSERT [requesttypes] OFF
