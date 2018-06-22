INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'organization.images', 'Images' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'organization.upload_image_files', 'Click "Browse" to select an image file from your computer.  Click "Add" to add the file to the list of files to be uploaded.  Select a file and click "Delete" to remove that file from the list.  Click "Upload" to copy the files to the server.' FROM phrases;
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'shared.add', 'Add' FROM phrases;
GO

INSERT INTO updates (revision, update_date) VALUES (23, GETDATE())
GO
