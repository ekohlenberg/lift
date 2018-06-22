
alter table organizations add default_signup_mode int  -- 0 = non-moderated, 1 = moderated
go

update organizations set default_signup_mode = 0 -- initialize all orgs to non-moderated

update users set state=2 -- set all existing users to confirmed

delete from phrases where label='user.any_status'
delete from phrases where label='organization.new_user_signup'
delete from phrases where label='organization.new_users_create_accounts'
delete from phrases where label='organization.new_users_require_approval'
delete from phrases where label='signup_user.thankyou'

INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'user.any_status', 'Any Status' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'organization.new_user_signup', 'New user signup:' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'organization.new_users_create_accounts', 'New users may create their own accounts' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'organization.new_users_require_approval', 'New users must be approved' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'signup_user.thankyou', 'Thank you for sigining up.  An email will be sent to the address provided when your account is approved.' from phrases

   
INSERT  INTO [dbo].[updates]
VALUES  ( 41, GETDATE() )
