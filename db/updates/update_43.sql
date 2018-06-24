
/*
 attach("login.welcome", LOGIN_WELCOME);
 attach("login.come_alongside", LOGIN_COME_ALONGSIDE);
 attach("login.if_you_have_an_account", LOGIN_IF_YOU_HAVE_AN_ACCOUNT);
*/

create table phrases_43
(
	id int,
	language_id int,
	label varchar(50),
	phrase varchar(512)
)
go

insert into phrases_43
select id, language_id, label, phrase from phrases
go

exec sp_rename 'phrases', 'phrases_42'

exec sp_rename 'phrases_43', 'phrases'

delete from phrases where label='login.welcome.upward'
delete from phrases where label='login.come_alongside.upward'
delete from phrases where label='login.if_you_have_an_account.upward'
delete from phrases where label='timeframe.100days'


/*
login.welcome	Welcome to our online house of prayer.  
login.come_alongside	and come alongside other prayer warriors in our watchman prayer ministry. 
login.if_you_have_an_account	 if you already have an account.
*/

/*
The Upward Basketball and Cheerleading at Sugar Creek program is about to begin for the 2013/2014 season.  We need your help!  

Will you pray? 

Will you partner with us through prayer as we seek God in every aspect of our Upward Basketball and Cheerleading League? 

We are in need of people who will be committed to praying each day of our Upward Basketball season for lives to be changed. 

Time Commitment: 5 minutes a day for 100 days 
*/


INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 
'login.welcome.upward', 
'The Upward Basketball and Cheerleading at Sugar Creek program is about to begin for the 2013/2014 season.  We need your help!</br/><br/><b>Will you pray?</b><br/><br/>' from phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 
'login.come_alongside.upward', 
' or ' from phrases


INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 
'login.if_you_have_an_account.upward', 
'to partner with us in prayer as we seek God in every aspect of our Upward Basketball and Cheerleading League.<br/><br/> We are in need of people who will be committed to praying each day of our Upward Basketball season for lives to be changed.<br/><br/><b>Time Commitment: 5 minutes a day for 100 days</b>' from phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 
'timeframe.100days', 
'100 Days' from phrases


delete from updates where revision=43
   
INSERT  INTO [dbo].[updates]
VALUES  ( 43, GETDATE() )
