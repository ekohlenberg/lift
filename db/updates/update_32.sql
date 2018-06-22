
alter table organizations add footer varchar(200)
go
	update organizations set footer=''
	

INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'shared.required_field', 'Required Field' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'shared.passwords_do_not_match', 'Passwords do not match' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'shared.must_be_a_valid_email_address', 'Must be a valid email address' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'organization.footer', 'Footer' from phrases
-- ran to here
 
    INSERT  INTO [dbo].[updates]
    VALUES  ( 32, GETDATE() )

    



