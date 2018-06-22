select T.wall_id, T.tod, T.title,
isnull( sun.user_id, 0) [sunday_user_id],
isnull( mon.user_id, 0) [monday_user_id],
isnull( tue.user_id, 0) [tuesday_user_id],
isnull( wed.user_id, 0) [wednesday_user_id],
isnull( thu.user_id, 0) [thursday_user_id],
isnull( fri.user_id, 0) [friday_user_id],
isnull( sat.user_id, 0) [saturday_user_id],
isnull( sun.appt, '') [sunday],
isnull( mon.appt, '') [monday],
isnull( tue.appt, '') [tuesday],
isnull( wed.appt, '') [wednesday],
isnull( thu.appt, '') [thursday],
isnull( fri.appt, '') [friday],
isnull( sat.appt, '') [saturday]
from
(
select 0 as tod, '12:00 AM' [title], w.id [wall_id] from walls w where w.organization_id=1 and w.id=1
union
select 1 as tod, '1:00 AM' [title], w.id [wall_id] from walls w where w.organization_id=1 and w.id=1
union
select 2 as tod, '2:00 AM' [title], w.id [wall_id] from walls w where w.organization_id=1 and w.id=1
union
select 3 as tod, '3:00 AM' [title], w.id [wall_id] from walls w where w.organization_id=1 and w.id=1
union
select 4 as tod, '4:00 AM' [title], w.id [wall_id] from walls w where w.organization_id=1 and w.id=1
union
select 5 as tod, '5:00 AM' [title], w.id [wall_id] from walls w where w.organization_id=1 and w.id=1
union
select 6 as tod, '6:00 AM' [title], w.id [wall_id] from walls w where w.organization_id=1 and w.id=1
union
select 7 as tod, '7:00 AM' [title], w.id [wall_id] from walls w where w.organization_id=1 and w.id=1
union
select 8 as tod, '8:00 AM' [title], w.id [wall_id] from walls w where w.organization_id=1 and w.id=1
union
select 9 as tod, '9:00 AM' [title], w.id [wall_id] from walls w where w.organization_id=1 and w.id=1
union
select 10 as tod, '10:00 AM' [title], w.id [wall_id] from walls w where w.organization_id=1 and w.id=1
union
select 11 as tod, '11:00 AM' [title], w.id [wall_id] from walls w where w.organization_id=1 and w.id=1
union
select 12 as tod, '12:00 PM' [title], w.id [wall_id] from walls w where w.organization_id=1 and w.id=1
union
select 13 as tod, '1:00 PM' [title], w.id [wall_id] from walls w where w.organization_id=1 and w.id=1
union
select 14 as tod, '2:00 PM' [title], w.id [wall_id] from walls w where w.organization_id=1 and w.id=1
union
select 15 as tod, '3:00 PM' [title], w.id [wall_id] from walls w where w.organization_id=1 and w.id=1
union
select 16 as tod, '4:00 PM' [title], w.id [wall_id] from walls w where w.organization_id=1 and w.id=1
union
select 17 as tod, '5:00 PM' [title], w.id [wall_id] from walls w where w.organization_id=1 and w.id=1
union
select 18 as tod, '6:00 PM' [title], w.id [wall_id] from walls w where w.organization_id=1 and w.id=1
union
select 19 as tod, '7:00 PM' [title], w.id [wall_id] from walls w where w.organization_id=1 and w.id=1
union
select 20 as tod, '8:00 PM' [title], w.id [wall_id] from walls w where w.organization_id=1 and w.id=1
union
select 21 as tod, '9:00 PM' [title], w.id [wall_id] from walls w where w.organization_id=1 and w.id=1
union
select 22 as tod, '10:00 PM' [title], w.id [wall_id] from walls w where w.organization_id=1 and w.id=1
union
select 23 as tod, '11:00 PM' [title], w.id [wall_id] from walls w where w.organization_id=1 and w.id=1
) T
left outer join (select  a.[wall_id], a.[user_id], datepart( hour, dateadd( hour, -6, a.tod_utc)) [tod], /*'Sunday ' + convert( varchar(10), datepart( hour, dateadd( hour, -6, a.tod_utc)))+':00 '+*/   left( u.first_name, 1)+ '. ' + u.last_name [appt]
				from appts a, users u
				where a.user_id=u.id
				and datepart( weekday, dateadd( hour, -6, a.tod_utc))=1
				and a.wall_id=1
				--and d.wall_id=1 and d.title='Sunday'
				) sun on T.tod=sun.tod and T.wall_id=sun.wall_id
left outer join (select  a.[wall_id],  a.[user_id], datepart( hour, dateadd( hour, -6, a.tod_utc)) [tod], /*'Monday ' + convert( varchar(10), datepart( hour, dateadd( hour, -6, a.tod_utc)))+':00 '+*/   left( u.first_name, 1) + '. ' + u.last_name [appt]
				from appts a, users u
				where a.user_id=u.id
				and datepart( weekday, dateadd( hour, -6, a.tod_utc))=2
				and a.wall_id=1
				--and d.wall_id=1 and d.title='Sunday'
				) mon on T.tod=mon.tod and T.wall_id=mon.wall_id
left outer join (select  a.[wall_id],  a.[user_id], datepart( hour, dateadd( hour, -6, a.tod_utc)) [tod], /*'Tuesday ' + convert( varchar(10), datepart( hour, dateadd( hour, -6, a.tod_utc)))+':00 '+ */  left( u.first_name, 1) + '. ' + u.last_name [appt]
				from appts a, users u
				where a.user_id=u.id
				and datepart( weekday, dateadd( hour, -6, a.tod_utc))=3
				and a.wall_id=1
				--and d.wall_id=1 and d.title='Sunday'
				) tue on T.tod=tue.tod and T.wall_id=tue.wall_id
left outer join (select  a.[wall_id],  a.[user_id], datepart( hour, dateadd( hour, -6, a.tod_utc)) [tod], /* 'Wednesday ' + convert( varchar(10), datepart( hour, dateadd( hour, -6, a.tod_utc)))+':00 '+ */  left( u.first_name, 1) + '. ' + u.last_name [appt]
				from appts a, users u
				where a.user_id=u.id
				and datepart( weekday, dateadd( hour, -6, a.tod_utc))=4
				and a.wall_id=1
				--and d.wall_id=1 and d.title='Sunday'
				) wed on T.tod=wed.tod and T.wall_id=wed.wall_id
left outer join (select  a.[wall_id],  a.[user_id], datepart( hour, dateadd( hour, -6, a.tod_utc)) [tod], /* 'Thursday ' + convert( varchar(10), datepart( hour, dateadd( hour, -6, a.tod_utc)))+':00 '+ */  left( u.first_name, 1) + '. ' + u.last_name [appt]
				from appts a, users u
				where a.user_id=u.id
				and datepart( weekday, dateadd( hour, -6, a.tod_utc))=5
				and a.wall_id=1
				--and d.wall_id=1 and d.title='Sunday'
				) thu on T.tod=thu.tod and T.wall_id=thu.wall_id
left outer join (select  a.[wall_id],  a.[user_id], datepart( hour, dateadd( hour, -6, a.tod_utc)) [tod], /*'Friday ' + convert( varchar(10), datepart( hour, dateadd( hour, -6, a.tod_utc)))+':00 '+ */ left( u.first_name, 1) + '. ' + u.last_name [appt]
				from appts a, users u
				where a.user_id=u.id
				and datepart( weekday, dateadd( hour, -6, a.tod_utc))=6
				and a.wall_id=1
				--and d.wall_id=1 and d.title='Sunday'
				) fri on T.tod=fri.tod and T.wall_id=fri.wall_id
left outer join (select  a.[wall_id],  a.[user_id], datepart( hour, dateadd( hour, -6, a.tod_utc)) [tod], /*'Saturday ' + convert( varchar(10), datepart( hour, dateadd( hour, -6, a.tod_utc)))+':00 '+ */ left( u.first_name, 1) + '. ' + u.last_name [appt]
				from appts a, users u
				where a.user_id=u.id
				and datepart( weekday, dateadd( hour, -6, a.tod_utc))=7
				and a.wall_id=1
				--and d.wall_id=1 and d.title='Sunday'
				) sat on T.tod=sat.tod and T.wall_id=sat.wall_id
order by 1,2