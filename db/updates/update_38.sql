
delete from phrases where label = 'request.inappropriate';
delete from phrases where label = 'request.inappropriate_title';


INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'request.inappropriate', 'inappropriate' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'requesttypes.inappropriate_title', 'Click if this request is in any way inappropriate' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'request.report', 'REPORT' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'request.how_inappropriate', 'Please describe the problem' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'request.fields_optional', 'All fields are optional' from phrases
    

INSERT  INTO [dbo].[updates]
VALUES  ( 38, GETDATE() )
