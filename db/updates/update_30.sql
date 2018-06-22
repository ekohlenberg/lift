IF ( EXISTS ( SELECT    *
          FROM      [dbo].[updates]
          WHERE     [revision] = 29 ) ) 
BEGIN
	BEGIN TRANSACTION


INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.manage_walls', 'Manage Walls' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.add_a_new_wall', 'Add a new wall' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.title', 'Title' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.you_can_change_this_later', 'you can change this later' from phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.wall_leader', 'Wall Leader' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.change', 'change' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.manage_this_wall', 'Manage This Wall' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.cancel', 'cancel' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.start_typing', 'Start typing the name of the user above.' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.once_we_find_them', 'Once we find them, we''ll display them here, then just click the name.' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.are_you_sure_you_want_to_delete', 'Are you sure you want to delete this wall?' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.all_users_unsubscribed', 'All users on this wall will be unsubscribed.' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.delete', 'delete' from phrases

	
    IF ( @@ERROR <> 0 ) 
        BEGIN
            ROLLBACK TRANSACTION
            RETURN
        END

    INSERT  INTO [dbo].[updates]
    VALUES  ( 30, GETDATE() )

    COMMIT TRANSACTION
END



