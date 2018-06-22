
alter table [users] alter column [login] nvarchar(255)
go
alter table [users] alter column [email] nvarchar(255)
go


INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'login.sign_up', 'Sign-Up' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'signup.acct_exists1', 'The email address you supplied has already been registered.' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'signup.acct_exists2', 'Please choose another, or try retrieving your password if that is your email address.' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'signup.retrieve_your_password', 'Retrieve your password' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'login.please_enter_email', 'Please enter your email address' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'signup.phone_required', 'Phone number is required for participation in the watchman wall.' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'request.keep_me_posted', 'Keep me posted' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'login.welcome', 'Welcome to our online house of prayer.  ' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'login.come_alongside', 'and come alongside other prayer warriors in our watchman prayer ministry. ' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'login.if_you_have_an_account', ' if you already have an account.' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'request.prayer_session_complete', 'Prayer session complete.' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'request.praise', 'PRAISE' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'request.praises_updates_comments', 'praises, updates, or comments' from phrases



INSERT  INTO [dbo].[updates]
VALUES  ( 36, GETDATE() )
