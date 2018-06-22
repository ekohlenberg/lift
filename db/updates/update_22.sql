ALTER TABLE org_emails ADD webmaster_email_to VARCHAR(255)
go
ALTER TABLE org_emails ADD contact_us_email_to VARCHAR(255)
go
ALTER TABLE org_emails ADD encourager_email_to VARCHAR(255)
go
ALTER TABLE organizations ALTER COLUMN subdomain VARCHAR(20) NOT NULL
go
ALTER TABLE organizations ADD CONSTRAINT ak_subdomain UNIQUE(subdomain)
go
/****** Object:  Index [IX_organizations_subdomain]    Script Date: 06/01/2009 11:45:54 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_organizations_subdomain] ON [dbo].organizations 
(
	subdomain ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
go
-- organizations.status:  0 = unapproved; 1 = approved
ALTER TABLE organizations ADD [status] INTEGER 
go
UPDATE organizations SET [status] = 1

INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'shared.upload', 'Upload' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'signup_organization.title', 'Organization Signup' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'signup_organization.terms_of_use', 'Terms of Use' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'signup_organization.approval_request_subject', 'LiftPrayer - New Organization Approval Request' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'signup_organization.approval_request_message', 'A new LiftPrayer organization has been created and is awaiting approval.  The new organization name is:' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'signup_organization.approval_response_subject', 'LiftPrayer - New Organization Approval Response' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'signup_organization.approval_response_message', 'A new LiftPrayer organization has been created and has been approved.  The new organization name is:' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'organization.email_to_webmaster', 'Webmaster Email' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'organization.email_to_contact_us', 'Contact Us Email' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'organization.email_to_encourager', 'Encourager Email' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'organization.status', 'Status' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'organization.status_unapproved', 'Unapproved' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'organization.status_approved', 'Approved' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'organization.general_information', 'General Information' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'organization.emails', 'Emails' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'organization.appearance', 'Appearance' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'organization.logo', 'Logo' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'organization.upload_logo_file', 'Click "Browse" to select your logo .gif file.  The image should be 155 pixels wide x 85 pixels high.  Click "Upload" to copy the file to the server.' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'organization.custom_stylesheet', 'Custom Stylesheet' FROM phrases;

INSERT INTO updates (revision, update_date) VALUES (22, GETDATE())
GO

