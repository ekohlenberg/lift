--------------------------------------------------------------------------
-- Shared phrases and Contact us phrases
--------------------------------------------------------------------------
INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'shared.submit', 'Submit' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 2, 'shared.submit', 'Enviar' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'shared.contact_us', 'Contact Us' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 2, 'shared.contact_us', 'Contáctenos' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'shared.all_fields_required', '(All fields are required.)' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 2, 'shared.all_fields_required', '(Todos los campos son obligatorios.)' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'shared.characters_left', 'Characters Left' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 2, 'shared.characters_left', 'Caracteres Restante' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'contactus.your_name', 'Your Name' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 2, 'contactus.your_name', 'Su Nombre' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'contactus.your_email', 'Your Email' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 2, 'contactus.your_email', 'Su Dirección de Correo Electrónico' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'contactus.your_email_required', '(Email is required in case of questions.)' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 2, 'contactus.your_email_required', '(El correo electrónico es necesaria en el caso de las preguntas.)' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'contactus.subject', 'Subject' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 2, 'contactus.subject', 'Asunto' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'contactus.please_enter_subject', '(Please enter a subject.)' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 2, 'contactus.please_enter_subject', '(Por favor, introduzca un asunto.)' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'contactus.message', 'Message' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 2, 'contactus.message', 'Mensaje' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'contactus.please_enter_message', '(Please enter your message.)' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 2, 'contactus.please_enter_message', '(Por favor, escribe tu mensaje.)' FROM phrases


INSERT INTO updates (revision, update_date) VALUES (9, GETDATE())
GO

