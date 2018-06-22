alter table encouragements add is_approved int
go

update encouragements set is_approved = 1 where approved_at is not null
go

INSERT INTO updates (revision, update_date) VALUES (15, GETDATE())
GO