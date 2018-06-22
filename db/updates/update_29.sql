IF ( EXISTS ( SELECT    *
          FROM      [dbo].[updates]
          WHERE     [revision] = 28 ) ) 
BEGIN
	BEGIN TRANSACTION

	INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.already_subscribed', 'Already Subscribed' from phrases
	INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.already_subscribed_text', 'You are already subscribed to another time slot.' from phrases
	INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.unsubscribe_text', 'Would you like to unsubscribe from that slot and subscribe to this one?' from phrases
	INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.yes', 'yes' from phrases
	INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.no', 'no' from phrases

	INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.unsubscribe_from_this_time', 'unsubscribe from this time' from phrases

	INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.walls_open', 'Walls Open' from phrases
	INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.subscribe_to_this_slot', 'subscribe to this slot' from phrases

	INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'shared.sunday', 'Sunday' from phrases
	INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'shared.monday', 'Monday' from phrases
	INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'shared.tuesday', 'Tuesday' from phrases
	INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'shared.wednesday', 'Wednesday' from phrases
	INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'shared.thursday', 'Thursday' from phrases
	INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'shared.friday', 'Friday' from phrases
	INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'shared.saturday', 'Saturday' from phrases

	INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.my_time', 'My Time' from phrases
	INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.open', 'Open' from phrases
	INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.full', 'Full' from phrases

	INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.prayer_time_selection', 'Prayer Time Selection' from phrases
	INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.sentence2', 'This view represents all of the walls combined.' from phrases
	INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.sentence3', 'Click the brick for the time slot/prayer hour you would like, provided it is not labeled "Full."' from phrases

	INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.some_walls_open', 'Some Walls Open' from phrases
	INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.prayer_times', 'Prayer Times' from phrases

	INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.my_prayer_time', 'My Prayer Time' from phrases

	INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'wall.are_you_sure', 'Are you sure you want to unsubscribe from this time?' from phrases

    IF ( @@ERROR <> 0 ) 
        BEGIN
            ROLLBACK TRANSACTION
            RETURN
        END

    INSERT  INTO [dbo].[updates]
    VALUES  ( 29, GETDATE() )

    COMMIT TRANSACTION
END



