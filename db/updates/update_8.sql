alter table org_emails add email_to varchar(255)
go

delete from org_emails
insert into org_emails ( organization_id, title, smtp_server, smtp_username, smtp_password,
smtp_port, email_to, email_reply_to, email_from)
values(  1, 'email.contact_us', 'smtp.liftprayer.cc', 'sugarcreek-contact@liftprayer.cc', 'liftprayer',
25, 'eric.kohlenberg@gmail.com', '', 'sugarcreek-contact@liftprayer.cc')

INSERT INTO updates (revision, update_date) VALUES (8, GETDATE())
GO

