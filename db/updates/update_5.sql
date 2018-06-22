alter table organizations add time_zone varchar(120)
go
update organizations set time_zone='Central Standard Time'
update users set time_zone='Central Standard Time'
insert into updates (revision, update_date ) values (5, getdate() )
