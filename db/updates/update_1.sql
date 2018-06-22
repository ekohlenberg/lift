alter table organizations add subdomain varchar(20)
go
update organizations
set subdomain='liftadmin' where id=1
go
insert into updates (revision, update_date ) values (1, getdate() )
go
