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