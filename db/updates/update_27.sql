IF ( EXISTS ( SELECT    *
          FROM      [dbo].[updates]
          WHERE     [revision] = 26 ) ) 
BEGIN
	BEGIN TRANSACTION

	INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
	SELECT COUNT(*) + 1, 1, 'organization.image_file_name', 'Image File Name' FROM phrases;
	INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
	SELECT COUNT(*) + 1, 1, 'organization.image_file_name_list', 'Image Files On Server:' FROM phrases;
	INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
	SELECT COUNT(*) + 1, 1, 'organization.delete_org_image_confirmation', 'Are you sure you want to delete this organization image?' FROM phrases;
	INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
	SELECT COUNT(*) + 1, 1, 'organization.delete_org_image_caption', 'Delete this organization image' FROM phrases;

    IF ( @@ERROR <> 0 ) 
        BEGIN
            ROLLBACK TRANSACTION
            RETURN
        END

    INSERT  INTO [dbo].[updates]
    VALUES  ( 27, GETDATE() )

    COMMIT TRANSACTION
END



