select  t.wall_id, t.tod,
isnull(sun.appt,'') [sunday], 
isnull(mon.appt,'') [monday] ,
isnull(tue.appt,'') [tuesday] ,
isnull(wed.appt,'') [wednesday],
isnull(thu.appt,'') [thursday],
isnull(fri.appt,'') [friday],
isnull(sat.appt,'') [saturday]
from
(
select 0 as tod, w.id [wall_id] from walls w where w.organization_id=1
union
select 1 as tod, w.id [wall_id] from walls w where w.organization_id=1
union
select 2 as tod, w.id [wall_id] from walls w where w.organization_id=1
union
select 3 as tod, w.id [wall_id] from walls w where w.organization_id=1
union
select 4 as tod, w.id [wall_id] from walls w where w.organization_id=1
union
select 5 as tod, w.id [wall_id] from walls w where w.organization_id=1
union
select 6 as tod, w.id [wall_id] from walls w where w.organization_id=1
union
select 7 as tod, w.id [wall_id] from walls w where w.organization_id=1
union
select 8 as tod, w.id [wall_id] from walls w where w.organization_id=1
union
select 9 as tod, w.id [wall_id] from walls w where w.organization_id=1
union
select 10 as tod, w.id [wall_id] from walls w where w.organization_id=1
union
select 11 as tod, w.id [wall_id] from walls w where w.organization_id=1
union
select 12 as tod, w.id [wall_id] from walls w where w.organization_id=1
union
select 13 as tod, w.id [wall_id] from walls w where w.organization_id=1
union
select 14 as tod, w.id [wall_id] from walls w where w.organization_id=1
union
select 15 as tod, w.id [wall_id] from walls w where w.organization_id=1
union
select 16 as tod, w.id [wall_id] from walls w where w.organization_id=1
union
select 17 as tod, w.id [wall_id] from walls w where w.organization_id=1
union
select 18 as tod, w.id [wall_id] from walls w where w.organization_id=1
union
select 19 as tod, w.id [wall_id] from walls w where w.organization_id=1
union
select 20 as tod, w.id [wall_id] from walls w where w.organization_id=1
union
select 21 as tod, w.id [wall_id] from walls w where w.organization_id=1
union
select 22 as tod, w.id [wall_id] from walls w where w.organization_id=1
union
select 23 as tod, w.id [wall_id] from walls w where w.organization_id=1
) T
left outer join (select  d.[wall_id], i.start_time, d.title + ' ' + i.title+ ' ' + u.first_name+ ' ' + u.last_name [appt]
				from increments i, days d, users u
				where i.user_id=u.id
				and i.day_id=d.id
				--and d.wall_id=1
				and d.title='Sunday') sun on T.tod=sun.start_time and T.wall_id=sun.wall_id

left outer join (select d.[wall_id], i.start_time, d.title + ' ' + i.title+ ' ' + u.first_name+ ' ' + u.last_name [appt]
				from increments i, days d, users u
				where i.user_id=u.id
				and i.day_id=d.id
				--and d.wall_id=1
				and d.title='Monday') mon on T.tod=mon.start_time and T.wall_id=mon.wall_id
left outer join (select d.[wall_id], i.start_time, d.title + ' ' + i.title+ ' ' + u.first_name+ ' ' + u.last_name [appt]
				from increments i, days d, users u
				where i.user_id=u.id
				and i.day_id=d.id
				--and d.wall_id=1
				and d.title='Tuesday') tue on T.tod=tue.start_time and T.wall_id=tue.wall_id
left outer join (select d.[wall_id], i.start_time, d.title + ' ' + i.title+ ' ' + u.first_name+ ' ' + u.last_name [appt]
				from increments i, days d, users u
				where i.user_id=u.id
				and i.day_id=d.id
				--and d.wall_id=1
				and d.title='Wednesday') wed on T.tod=wed.start_time and T.wall_id=wed.wall_id
left outer join (select d.[wall_id], i.start_time, d.title + ' ' + i.title+ ' ' + u.first_name+ ' ' + u.last_name [appt]
				from increments i, days d, users u
				where i.user_id=u.id
				and i.day_id=d.id
				--and d.wall_id=1
				and d.title='Thursday') thu on T.tod=thu.start_time and T.wall_id=thu.wall_id
left outer join (select d.[wall_id], i.start_time, d.title + ' ' + i.title+ ' ' + u.first_name+ ' ' + u.last_name [appt]
				from increments i, days d, users u
				where i.user_id=u.id
				and i.day_id=d.id
				--and d.wall_id=1
				and d.title='Friday') fri on T.tod=fri.start_time and T.wall_id=fri.wall_id
left outer join (select d.[wall_id], i.start_time, d.title + ' ' + i.title+ ' ' + u.first_name+ ' ' + u.last_name [appt]
				from increments i, days d, users u
				where i.user_id=u.id
				and i.day_id=d.id
				--and d.wall_id=1
				and d.title='Saturday') sat on T.tod=sat.start_time and T.wall_id=sat.wall_id
order by 1,2
