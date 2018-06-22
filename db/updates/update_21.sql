INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'ps.welcome', 'Welcome to your prayer session' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'ps.sentence1', 'You will be presented with a new prayer request every 60 seconds for a maximum of 60 prayer requests.' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'ps.sentence2', 'If at anytime you want to pause the session, you may do so with the button below.              ' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'ps.sentence3', 'You may also manually navigate through the requests using the buttons below.' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'ps.sentence4', 'You can create notes about this session in the box provided below.' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'ps.sentence5', 'These notes will be automatically saved to your session for later review.' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'ps.sentence6', 'When you are ready to begin, press the play button below.' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'ps.sentence7', 'When you have finished, simply click "End Session". Thank you.' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'ps.new_prayer_session', 'New prayer session' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'ps.notes', 'Notes' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'ps.your_notes', 'Your Notes' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'ps.play_pause', 'Play/Pause' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'ps.next', 'Next' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'ps.previous', 'Previous' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'ps.end_session', 'End Session' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'ps.currently_viewing_request', 'Currently viewing request' from phrases
INSERT INTO phrases ([id], [language_id], [label], [phrase]) SELECT COUNT(*) + 1, 1, 'ps.of', 'of' from phrases




INSERT INTO updates (revision, update_date) VALUES (21, GETDATE())
GO

