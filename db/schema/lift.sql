/****** Object:  Table [dbo].[railmail_deliveries]    Script Date: 06/23/2009 19:10:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[railmail_deliveries](
	[id] [int] IDENTITY,
	[recipients] [varchar](1024) NULL,
	[from] [varchar](255) NULL,
	[subject] [varchar](1024) NULL,
	[sent_at] [datetime] NULL,
	[read_at] [datetime] NULL,
	[raw] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[requests]    Script Date: 06/23/2009 19:11:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[requests](
	[id] [int] IDENTITY,
	[user_id] [numeric](10, 0) NULL,
	[title] [varchar](100) NOT NULL,
	[description] [text] NOT NULL,
	[requesttype_id] [int] NOT NULL,
	[post_date] [datetime] NULL,
	[total_requests] [real] NOT NULL,
	[organization_id] [int] NOT NULL,
	[total_comments] [numeric](10, 0) NULL,
	[age] [varchar](20) NULL,
	[is_for] [varchar](100) NULL,
	[is_approved] [numeric](10, 0) NULL,
	[last_action] [datetime] NULL,
	[from] [varchar](100) NULL,
	[from_email] [varchar](255) NULL,
	[encouragement_address] [text] NULL,
	[encouragement_email] [varchar](255) NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[group_relationship_type_id] [int] NULL,
	[listed] [int] NULL,
	[total_comments_needing_approval] [int] NULL,
	[total_private_comments] [int] NULL,
	[encouragement_phone] [varchar](20) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[requesttypes]    Script Date: 06/23/2009 19:11:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[requesttypes](
	[id] [int] IDENTITY,
	[title] [varchar](100) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[roles]    Script Date: 06/23/2009 19:11:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[roles](
	[id] [int] IDENTITY,
	[created_at] [datetime] NOT NULL,
	[updated_at] [datetime] NOT NULL,
	[title] [nvarchar](100) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[roles_static_permissions]    Script Date: 06/23/2009 19:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[roles_static_permissions](
	[role_id] [numeric](10, 0) NOT NULL,
	[static_permission_id] [numeric](10, 0) NOT NULL,
	[created_at] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[roles_users]    Script Date: 06/23/2009 19:11:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[roles_users](
	[id] [int] identity,
	[user_id] [int] NOT NULL,
	[role_id] [int] NOT NULL,
	[created_at] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[schema_migrations]    Script Date: 06/23/2009 19:11:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[schema_migrations](
	[version] [varchar](255) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[sessions]    Script Date: 06/23/2009 19:11:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sessions](
	[id] [int] IDENTITY,
	[session_id] [varchar](255) NULL,
	[data] [text] NULL,
	[updated_at] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[simple_captcha_data]    Script Date: 06/23/2009 19:11:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[simple_captcha_data](
	[id] [int] IDENTITY,
	[key] [varchar](40) NULL,
	[value] [varchar](6) NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[subscriptions]    Script Date: 06/23/2009 19:11:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[subscriptions](
	[id] [int] IDENTITY,
	[user_id] [numeric](10, 0) NOT NULL,
	[request_id] [numeric](10, 0) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 06/23/2009 19:12:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY,
	[created_at] [datetime] NOT NULL,
	[updated_at] [datetime] NOT NULL,
	[last_logged_in_at] [datetime] NOT NULL,
	[login_failure_count] [numeric](10, 0) NOT NULL,
	[login] [nvarchar](100) NULL,
	[email] [nvarchar](200) NULL,
	[crypted_password] [nvarchar](100) NULL,
	[password_hash_type] [nvarchar](20) NOT NULL,
	[password_salt] [nvarchar](10) NOT NULL,
	[state] [int] NOT NULL,
	[first_name] [nvarchar](100) NULL,
	[last_name] [nvarchar](100) NULL,
	[organization_id] [numeric](10, 0) NULL,
	[address] [nvarchar](200) NULL,
	[city] [nvarchar](100) NULL,
	[state_provience] [nvarchar](100) NULL,
	[postal_code] [nvarchar](50) NULL,
	[phone] [nvarchar](100) NULL,
	[previous_increment_id] [int] NULL,
	[time_zone] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[static_permissions]    Script Date: 06/23/2009 19:11:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[static_permissions](
	[id] [int] IDENTITY,
	[title] [nvarchar](200) NOT NULL,
	[created_at] [datetime] NOT NULL,
	[updated_at] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_registrations]    Script Date: 06/23/2009 19:11:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_registrations](
	[id] [int] IDENTITY,
	[user_id] [numeric](10, 0) NOT NULL,
	[token] [ntext] NOT NULL,
	[created_at] [datetime] NOT NULL,
	[expires_at] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[walls]    Script Date: 06/23/2009 19:12:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[walls](
	[id] [int] IDENTITY,
	[title] [varchar](100) NOT NULL,
	[organization_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[increment_option] [varchar](255) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[group_relationship_types]    Script Date: 06/23/2009 19:09:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[group_relationship_types](
	[id] [int] IDENTITY,
	[name] [varchar](50) NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[days]    Script Date: 06/23/2009 19:09:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[days](
	[id] [int] IDENTITY,
	[title] [varchar](100) NOT NULL,
	[wall_id] [int] NOT NULL,
	[user_id] [int] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[encouragements]    Script Date: 06/23/2009 19:09:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[encouragements](
	[id] [int] IDENTITY,
	[user_id] [int] NULL,
	[request_id] [numeric](10, 0) NOT NULL,
	[Note] [text] NULL,
	[post_date] [datetime] NOT NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[approved_at] [datetime] NULL,
	[from] [varchar](100) NULL,
	[listed] [int] NULL,
	[encouragement_type] [int] NULL,
	[from_email] [varchar](255) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[engine_schema_info]    Script Date: 06/23/2009 19:09:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[engine_schema_info](
	[engine_name] [varchar](255) NULL,
	[version] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[groups]    Script Date: 06/23/2009 19:09:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[groups](
	[id] [int] IDENTITY,
	[created_at] [datetime] NOT NULL,
	[updated_at] [datetime] NOT NULL,
	[title] [nvarchar](200) NOT NULL,
	[parent_id] [numeric](10, 0) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[groups_users]    Script Date: 06/23/2009 19:09:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[groups_users](
	[group_id] [numeric](10, 0) NOT NULL,
	[user_id] [numeric](10, 0) NOT NULL,
	[created_at] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[groups_roles]    Script Date: 06/23/2009 19:09:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[groups_roles](
	[group_id] [numeric](10, 0) NOT NULL,
	[role_id] [numeric](10, 0) NOT NULL,
	[created_at] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[header_images]    Script Date: 06/23/2009 19:09:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[header_images](
	[id] [int] IDENTITY,
	[parent_id] [int] NULL,
	[content_type] [varchar](255) NULL,
	[filename] [varchar](255) NULL,
	[thumbnail] [varchar](255) NULL,
	[size] [int] NULL,
	[width] [int] NULL,
	[height] [int] NULL,
	[activated] [smallint] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[increments]    Script Date: 06/23/2009 19:10:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[increments](
	[id] [int] IDENTITY,
	[day_id] [numeric](10, 0) NOT NULL,
	[title] [varchar](200) NOT NULL,
	[start_time] [real] NOT NULL,
	[end_time] [real] NOT NULL,
	[user_id] [numeric](10, 0) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[organizations]    Script Date: 06/23/2009 19:10:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[organizations](
	[id] [int] IDENTITY,
	[title] [varchar](100) NOT NULL,
	[user_id] [int] NOT NULL,
	[address] [varchar](200) NOT NULL,
	[city] [varchar](100) NOT NULL,
	[state_province] [varchar](200) NOT NULL,
	[postal_code] [varchar](100) NOT NULL,
	[phone] [varchar](50) NOT NULL,
	[created_at] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[prayersessions]    Script Date: 06/23/2009 19:10:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[prayersessions](
	[id] [int] IDENTITY,
	[user_id] [int] NOT NULL,
	[note] [text] NOT NULL,
	[start_time] [datetime] NOT NULL,
	[end_time] [datetime] NOT NULL,
	[total_requests] [numeric](10, 0) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[updates]    Script Date: 06/23/2009 20:38:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[updates](
	[revision] [int] NULL,
	[update_date] [datetime] NULL
) ON [PRIMARY]
GO
CREATE VIEW [dbo].[appver] as
select max(revision) [build] from updates
go
insert into updates (revision, update_date) values (0, getdate())
go
