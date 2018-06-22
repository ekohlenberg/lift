select my.my_dow, my.my_tod, thewall.* from
(select wc.total, cal.tod, cal.title,
(wc.total - cal.sunday) [Sunday],
(wc.total - cal.monday) [Monday],
(wc.total - cal.tuesday) [Tuesday],
(wc.total - cal.wednesday) [Wednesday],
(wc.total - cal.thursday) [Thursday],
(wc.total - cal.friday) [Friday],
(wc.total - cal.saturday) [Saturday]

--select wc.*, cal.* 

 from
(
select  T.tod, T.title,
sum(isnull( sun.appt, 0)) [sunday],
sum(isnull( mon.appt, 0)) [monday],
sum(isnull( tue.appt, 0)) [tuesday],
sum(isnull( wed.appt, 0)) [wednesday],
sum(isnull( thu.appt, 0)) [thursday],
sum(isnull( fri.appt, 0)) [friday],
sum(isnull( sat.appt, 0)) [saturday]
from
(
select 0 as tod, '12:00 AM' as title, w.id [wall_id] from walls w where w.organization_id=1
union
select 1 as tod, '1:00 AM' as title, w.id [wall_id] from walls w where w.organization_id=1
union
select 2 as tod, '2:00 AM' as title, w.id [wall_id] from walls w where w.organization_id=1
union
select 3 as tod, '3:00 AM' as title, w.id [wall_id] from walls w where w.organization_id=1
union
select 4 as tod, '4:00 AM' as title, w.id [wall_id] from walls w where w.organization_id=1
union
select 5 as tod, '5:00 AM' as title, w.id [wall_id] from walls w where w.organization_id=1
union
select 6 as tod, '6:00 AM' as title, w.id [wall_id] from walls w where w.organization_id=1
union
select 7 as tod, '7:00 AM' as title, w.id [wall_id] from walls w where w.organization_id=1
union
select 8 as tod, '8:00 AM' as title, w.id [wall_id] from walls w where w.organization_id=1
union
select 9 as tod, '9:00 AM' as title, w.id [wall_id] from walls w where w.organization_id=1
union
select 10 as tod, '10:00 AM' as title, w.id [wall_id] from walls w where w.organization_id=1
union
select 11 as tod, '11:00 AM' as title, w.id [wall_id] from walls w where w.organization_id=1
union
select 12 as tod, '12:00 PM' as title, w.id [wall_id] from walls w where w.organization_id=1
union
select 13 as tod, '1:00 PM' as title, w.id [wall_id] from walls w where w.organization_id=1
union
select 14 as tod, '2:00 PM' as title, w.id [wall_id] from walls w where w.organization_id=1
union
select 15 as tod, '3:00 PM' as title, w.id [wall_id] from walls w where w.organization_id=1
union
select 16 as tod, '4:00 PM' as title, w.id [wall_id] from walls w where w.organization_id=1
union
select 17 as tod, '5:00 PM' as title, w.id [wall_id] from walls w where w.organization_id=1
union
select 18 as tod, '6:00 PM' as title, w.id [wall_id] from walls w where w.organization_id=1
union
select 19 as tod, '7:00 PM' as title, w.id [wall_id] from walls w where w.organization_id=1
union
select 20 as tod, '8:00 PM' as title, w.id [wall_id] from walls w where w.organization_id=1
union
select 21 as tod, '9:00 PM' as title, w.id [wall_id] from walls w where w.organization_id=1
union
select 22 as tod, '10:00 PM' as title, w.id [wall_id] from walls w where w.organization_id=1
union
select 23 as tod, '11:00 PM' as title, w.id [wall_id] from walls w where w.organization_id=1
) T
left outer join (select  a.[wall_id], datepart( hour, dateadd( hour, -6, a.tod_utc)) [tod], 1 [appt]
				from appts a, users u
				where a.user_id=u.id
				and datepart( weekday, dateadd( hour, -6, a.tod_utc))=1
				--and d.wall_id=1 and d.title='Sunday'
				) sun on T.tod=sun.tod and T.wall_id=sun.wall_id
left outer join (select  a.[wall_id], datepart( hour, dateadd( hour, -6, a.tod_utc)) [tod], 1 [appt]
				from appts a, users u
				where a.user_id=u.id
				and datepart( weekday, dateadd( hour, -6, a.tod_utc))=2
				--and d.wall_id=1 and d.title='Sunday'
				) mon on T.tod=mon.tod and T.wall_id=mon.wall_id
left outer join (select  a.[wall_id], datepart( hour, dateadd( hour, -6, a.tod_utc)) [tod], 1 [appt]
				from appts a, users u
				where a.user_id=u.id
				and datepart( weekday, dateadd( hour, -6, a.tod_utc))=3
				--and d.wall_id=1 and d.title='Sunday'
				) tue on T.tod=tue.tod and T.wall_id=tue.wall_id
left outer join (select  a.[wall_id], datepart( hour, dateadd( hour, -6, a.tod_utc)) [tod], 1 [appt]
				from appts a, users u
				where a.user_id=u.id
				and datepart( weekday, dateadd( hour, -6, a.tod_utc))=4
				--and d.wall_id=1 and d.title='Sunday'
				) wed on T.tod=wed.tod and T.wall_id=wed.wall_id
left outer join (select  a.[wall_id], datepart( hour, dateadd( hour, -6, a.tod_utc)) [tod], 1 [appt]
				from appts a, users u
				where a.user_id=u.id
				and datepart( weekday, dateadd( hour, -6, a.tod_utc))=5
				--and d.wall_id=1 and d.title='Sunday'
				) thu on T.tod=thu.tod and T.wall_id=thu.wall_id
left outer join (select  a.[wall_id], datepart( hour, dateadd( hour, -6, a.tod_utc)) [tod], 1 [appt]
				from appts a, users u
				where a.user_id=u.id
				and datepart( weekday, dateadd( hour, -6, a.tod_utc))=6
				--and d.wall_id=1 and d.title='Sunday'
				) fri on T.tod=fri.tod and T.wall_id=fri.wall_id
left outer join (select  a.[wall_id], datepart( hour, dateadd( hour, -6, a.tod_utc)) [tod], 1 [appt]
				from appts a, users u
				where a.user_id=u.id
				and datepart( weekday, dateadd( hour, -6, a.tod_utc))=7
				--and d.wall_id=1 and d.title='Sunday'
				) sat on T.tod=sat.tod and T.wall_id=sat.wall_id
group by T.tod, T.title
) cal,
(select count(*) [total] from walls where organization_id=1) wc
) thewall,
(
 select max(my_dow) [my_dow], max(my_tod) [my_tod] 
	from (
		select datepart( weekday, dateadd( hour, -6, a.tod_utc)) [my_dow], datepart( hour, dateadd( hour, -6, a.tod_utc)) [my_tod]  from appts a where user_id=726
		union 
		--select distinct -1 [my_dow], -1 [my_tod] from appts a where 800 not in (select user_id from appts)
		select -1 [my_dow], -1 [my_tod] from users left outer join appts on appts.user_id=user_id
		) my_max
) my
order by 4