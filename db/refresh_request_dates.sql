update requests set 
last_action=dateadd(day, datediff( day,(select max(last_action) from requests), getdate()), last_action),
post_date=dateadd(day, datediff( day,(select max(last_action) from requests), getdate()), post_date)
where id > (select max(id) - 100 from requests)