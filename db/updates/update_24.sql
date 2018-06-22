CREATE TABLE appts
( 
	id int NOT NULL  IDENTITY(1, 1),
	wall_id	int,
	user_id	int,
	tod_utc datetime
)
GO

insert into appts (wall_id, tod_utc, user_id)
select d.wall_id, 
dateadd( hour, 6, convert(datetime, '03/01/2009 ' + convert( varchar(10), i.start_time) + ':00')) [tod_utc],
  u.id user_id--, d.title, i.title, u.first_name, u.last_name
from increments i, days d, users u
where i.user_id=u.id
and i.day_id=d.id
and d.title='Sunday'
union
select d.wall_id, 
dateadd( hour, 6, convert(datetime, '03/02/2009 ' + convert( varchar(10), i.start_time) + ':00')) [tod_utc],
 u.id user_id--, d.title, i.title, u.first_name, u.last_name
from increments i, days d, users u
where i.user_id=u.id
and i.day_id=d.id
and d.title='Monday'
union
select d.wall_id, 
dateadd( hour, 6, convert(datetime, '03/03/2009 ' + convert( varchar(10), i.start_time) + ':00')) [tod_utc],
 u.id user_id--, d.title, i.title, u.first_name, u.last_name
from increments i, days d, users u
where i.user_id=u.id
and i.day_id=d.id
and d.title='Tuesday'
union
select d.wall_id, 
dateadd( hour, 6, convert(datetime, '03/04/2009 ' + convert( varchar(10), i.start_time) + ':00')) [tod_utc],
 u.id user_id--, d.title, i.title, u.first_name, u.last_name
from increments i, days d, users u
where i.user_id=u.id
and i.day_id=d.id
and d.title='Wednesday'
union
select d.wall_id, 
dateadd( hour, 6, convert(datetime, '03/05/2009 ' + convert( varchar(10), i.start_time) + ':00')) [tod_utc],
 u.id user_id--, d.title, i.title, u.first_name, u.last_name
from increments i, days d, users u
where i.user_id=u.id
and i.day_id=d.id
and d.title='Thursday'
union
select d.wall_id, 
dateadd( hour, 6, convert(datetime, '03/06/2009 ' + convert( varchar(10), i.start_time) + ':00')) [tod_utc],
 u.id user_id--, d.title, i.title, u.first_name, u.last_name
from increments i, days d, users u
where i.user_id=u.id
and i.day_id=d.id
and d.title='Friday'
union
select d.wall_id, 
dateadd( hour, 6, convert(datetime, '03/07/2009 ' + convert( varchar(10), i.start_time) + ':00')) [tod_utc],
 u.id user_id--, d.title, i.title, u.first_name, u.last_name
from increments i, days d, users u
where i.user_id=u.id
and i.day_id=d.id
and d.title='Saturday'
order by 1,2

INSERT INTO updates (revision, update_date) VALUES (24, GETDATE())
GO
