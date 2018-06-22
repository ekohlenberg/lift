alter table organizations add default_approval int
go


delete from phrases where label = 'org.when_requests_entered'
delete from phrases where label = 'org.requests_automatically_approved'
delete from phrases where label = 'org.requests_manually_approved'
delete from phrases where label = 'request.report_abuse'

INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'org.when_requests_entered', 'When prayer requests are entered:' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'org.requests_automatically_approved', 'They are automatically approved and the community moderates' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'org.requests_manually_approved', 'They must be approved manually by a moderator' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'request.report_abuse', 'Report Abuse' from phrases

   

update organizations set default_approval=0   
   
INSERT  INTO [dbo].[updates]
VALUES  ( 39, GETDATE() )
