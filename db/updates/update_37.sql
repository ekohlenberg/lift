
delete from phrases where label = 'requesttypes.hostpitalized'
delete from phrases where label = 'requesttypes.missionary'
delete from phrases where label = 'requesttypes.government'

insert into requesttypes (title) values ( 'requesttypes.spiritualrenewal' )

INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'requesttypes.missionary', 'Missions' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'requesttypes.hostpitalized', 'Hospital' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'requesttypes.government', 'Community, Government, and Schools' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'requesttypes.spiritualrenewal', 'Spiritual Renewal' from phrases



INSERT  INTO [dbo].[updates]
VALUES  ( 37, GETDATE() )
