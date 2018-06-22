create table languages (
	[id]		int,
	[title]		varchar(50),
	[dateformat]	varchar(50),
	[timeformat]	varchar(50),
	[datetimeformat]varchar(50)
	)
go

create table phrases (
	[id]		int,
	[language_id]	int,
	[label]		varchar(50),
	[phrase]	varchar(255)
	)
go

insert into languages ( [id], [title], [dateformat], [timeformat], [datetimeformat] )
values (1, 'English', 'MM/dd/yyyy', 'hh:mm:ss', 'MM/dd/yyyy hh:mm:ss')

insert into languages ( [id], [title], [dateformat], [timeformat], [datetimeformat] )
values (2,  'Español', 'yyyy MM-dd', 'hh:mm:ss', 'yyyy MM-dd hh:mm:ss')

insert into phrases ([id], [language_id], [label], [phrase] ) values (1, 1, 'timeframe.day', 'day')
insert into phrases ([id], [language_id], [label],  [phrase] ) values (2, 1, 'timeframe.week', 'week')
insert into phrases ([id], [language_id], [label],  [phrase] ) values (3, 1, 'timeframe.month', 'month')
insert into phrases ([id], [language_id], [label],  [phrase] ) values (4, 1, 'timeframe.year', 'year')
-- 	día semana mes año
insert into phrases ([id], [language_id], [label],  [phrase] ) values (5, 2, 'timeframe.day', 'día')
insert into phrases ([id], [language_id], [label],  [phrase] ) values (6, 2, 'timeframe.week', 'semana')
insert into phrases ([id], [language_id], [label],  [phrase] ) values (7, 2, 'timeframe.month', 'mes')
insert into phrases ([id], [language_id], [label],  [phrase] ) values (8, 2, 'timeframe.year', 'año')

delete from requesttypes
SET IDENTITY_INSERT [requesttypes] ON
insert into requesttypes ([id], [title]) values (1, 'requesttypes.birth')
insert into requesttypes ([id], [title]) values (2, 'requesttypes.church_staff')
insert into requesttypes ([id], [title]) values (3, 'requesttypes.church_vision_strategy')
insert into requesttypes ([id], [title]) values (4, 'requesttypes.death')
insert into requesttypes ([id], [title]) values (5, 'requesttypes.financial')
insert into requesttypes ([id], [title]) values (6, 'requesttypes.government')
insert into requesttypes ([id], [title]) values (7, 'requesttypes.hostpitalized')
insert into requesttypes ([id], [title]) values (8, 'requesttypes.medical')
insert into requesttypes ([id], [title]) values (9, 'requesttypes.military')
insert into requesttypes ([id], [title]) values (10, 'requesttypes.missionary')
insert into requesttypes ([id], [title]) values (11, 'requesttypes.personal')
insert into requesttypes ([id], [title]) values (12, 'requesttypes.praise')
insert into requesttypes ([id], [title]) values (13, 'requesttypes.relationships')
insert into requesttypes ([id], [title]) values (14, 'requesttypes.salvation')
insert into requesttypes ([id], [title]) values (15, 'requesttypes.other')
SET IDENTITY_INSERT [requesttypes] OFF






insert into phrases ( [id], [language_id], [label], [phrase] ) values (9, 2, 'requesttypes.birth', 'Nacimiento')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (10, 2, 'requesttypes.church_staff', 'Iglesia Mayor')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (11, 2, 'requesttypes.church_vision_strategy', 'Iglesia Visión / Estrategia')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (12, 2, 'requesttypes.death', 'Muerte')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (13, 2, 'requesttypes.financial', 'Financieros')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (14, 2, 'requesttypes.government', 'Gobierno')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (15, 2, 'requesttypes.hostpitalized', 'Hospitalizado')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (16, 2, 'requesttypes.medical', 'Médicos')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (17, 2, 'requesttypes.military', 'Militares')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (18, 2, 'requesttypes.missionary', 'Misionero')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (19, 2, 'requesttypes.personal', 'Personales')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (20, 2, 'requesttypes.praise', 'Alabanza')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (21, 2, 'requesttypes.relationships', 'Relaciones')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (22, 2, 'requesttypes.salvation', 'La salvación')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (23, 2, 'requesttypes.other', 'Otro')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (24, 1, 'requesttypes.birth', 'Birth')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (25, 1, 'requesttypes.church_staff', 'Church Staff')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (26, 1, 'requesttypes.church_vision_strategy', 'Church Vision/Strategy')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (27, 1, 'requesttypes.death', 'Death')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (28, 1, 'requesttypes.financial', 'Financial')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (29, 1, 'requesttypes.government', 'Government')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (30, 1, 'requesttypes.hostpitalized', 'Hospitalized')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (31, 1, 'requesttypes.medical', 'Medical')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (32, 1, 'requesttypes.military', 'Military')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (33, 1, 'requesttypes.missionary', 'Missionary')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (34, 1, 'requesttypes.personal', 'Personal')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (35, 1, 'requesttypes.praise', 'Praise')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (36, 1, 'requesttypes.relationships', 'Relationships')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (37, 1, 'requesttypes.salvation', 'Salvation')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (38, 1, 'requesttypes.other', 'Other')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (39, 1, 'requesttypes.all', 'All')
insert into phrases ( [id], [language_id], [label], [phrase] ) values (40, 2, 'requesttypes.all', 'Todo')


alter table organizations add language_id int
go

update organizations set language_id=1

insert into updates (revision, update_date ) values (2, getdate() )
go
