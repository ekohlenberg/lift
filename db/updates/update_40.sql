alter table requests add active int
go

update requests set active=1

delete from phrases where label='request.recycle_bin'
delete from phrases where label='request.confirm_delete'
delete from phrases where label='request.delete'
delete from phrases where label='request.restore'
delete from phrases where label='request.confirm_restore'
delete from phrases where label='request.confirm_recycle'
delete from phrases where label='request.recycle'


INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'request.recycle_bin', 'Recycle Bin' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'request.confirm_delete', 'Are you sure you want to delete this request?  This action is permanent and cannot be reversed.' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'request.delete', 'delete' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'request.restore', 'restore' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'request.confirm_restore', 'Are you sure you want to restore this request?' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'request.confirm_recycle', 'Are you sure you want to send this request to the recycle bin?' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'request.recycle', 'recycle' from phrases

   
INSERT  INTO [dbo].[updates]
VALUES  ( 40, GETDATE() )
