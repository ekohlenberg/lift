ALTER TABLE languages ADD iso VARCHAR(10);
GO

UPDATE languages SET iso = 'en' WHERE id = 1;
UPDATE languages SET iso = 'es' WHERE id = 2;
INSERT INTO updates (revision, update_date) VALUES (12, GETDATE());
