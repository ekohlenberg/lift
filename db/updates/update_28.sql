IF ( EXISTS ( SELECT    *
          FROM      [dbo].[updates]
          WHERE     [revision] = 27 ) ) 
BEGIN
	BEGIN TRANSACTION

	UPDATE phrases SET phrase = 'Click "Browse" to select a local file.  Click "Add To List" include the file to be uploaded.  Select a file and click "Remove From List" to exclude the file.  Click "Upload To Server" to copy the files to the server.' 
	WHERE language_id = 1 AND label = 'organization.upload_image_files';

	INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
	SELECT COUNT(*) + 1, 1, 'organization.images_add_to_list', 'Add To List' FROM phrases;
	INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
	SELECT COUNT(*) + 1, 1, 'organization.images_remove_from_list', 'Remove From List' FROM phrases;
	INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
	SELECT COUNT(*) + 1, 1, 'organization.images_upload_to_server', 'Upload To Server' FROM phrases;

    IF ( @@ERROR <> 0 ) 
        BEGIN
            ROLLBACK TRANSACTION
            RETURN
        END

    INSERT  INTO [dbo].[updates]
    VALUES  ( 28, GETDATE() )

    COMMIT TRANSACTION
END



