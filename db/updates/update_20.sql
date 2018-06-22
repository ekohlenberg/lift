
SET IDENTITY_INSERT [roles] ON
INSERT INTO roles (id, created_at, updated_at, title) VALUES (13, GETDATE(), GETDATE(), 'System Admin');
INSERT INTO roles (id, created_at, updated_at, title) VALUES (14, GETDATE(), GETDATE(), 'Organization Admin');
SET IDENTITY_INSERT [roles] OFF

insert into roles_users (role_id, user_id, created_at) select id, 1, getdate() from roles where title='Admin'
insert into roles_users (role_id, user_id, created_at) select id, 1, getdate() from roles where title='Moderator'
insert into roles_users (role_id, user_id, created_at) select id, 1, getdate() from roles where title='Watchman'
insert into roles_users (role_id, user_id, created_at) select id, 1, getdate() from roles where title='System Admin'
insert into roles_users (role_id, user_id, created_at) select id, 1, getdate() from roles where title='Organization Admin'

INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'roles.system_admin', 'System Admin' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'roles.organization_admin', 'Organization Admin' FROM phrases;

INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'admin.admin_panel', 'Administration Panel' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'admin.admin_panel_description1', 'The Administration Panel is the main set of controls, tools, and information that permit you to administer each aspect of your ministry.' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'admin.admin_panel_description2', 'If you are not yet familiar with the features, please be sure to review the <strong>"Help Guide"</strong> which is a good starting point for new users.' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'admin.help_guide', 'Help Guide' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'admin.help_guide_description', 'Access helpful documentation.' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'admin.configuration', 'Configuration' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'admin.configuration_description', 'Manage configuration settings here.' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'admin.community_emails', 'Community Emails' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'admin.community_emails_description', 'Send out email to the community here.' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'admin.manage_roles', 'Manage Roles' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'admin.manage_roles_description', 'Add, edit and delete user roles here.' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'admin.manage_organizations', 'Manage Organization' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'admin.manage_organizations_description', 'Manage your organization''s identity, appearance, and communication settings.' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'admin.manage_users', 'Manage Users' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'admin.manage_users_description', 'Add, edit and delete users here.' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'admin.manage_walls', 'Manage Walls' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'admin.manage_walls_description', 'Add, edit and delete walls here.' FROM phrases;

INSERT INTO updates (revision, update_date) VALUES (20, GETDATE())
GO

