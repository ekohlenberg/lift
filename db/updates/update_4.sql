update group_relationship_types set name='grouptype.member' where id=1
update group_relationship_types set name='grouptype.nonmember' where id=2

insert into phrases ([id], [language_id], [label],  [phrase] ) values (45, 1, 'grouptype.member', 'Member')
insert into phrases ([id], [language_id], [label],  [phrase] ) values (46, 2, 'grouptype.member', 'Miembro')

insert into phrases ([id], [language_id], [label],  [phrase] ) values (47, 2, 'grouptype.nonmember', 'No miembro')
insert into phrases ([id], [language_id], [label],  [phrase] ) values (48, 1, 'grouptype.nonmember', 'Non-member')

insert into updates (revision, update_date ) values (4, getdate() )
