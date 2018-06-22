------------------------------------------------------------------------------------
-- LAYOUT
------------------------------------------------------------------------------------
INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'layout.join_now', 'Join Now' FROM phrases

------------------------------------------------------------------------------------
-- SIGNUP USER
------------------------------------------------------------------------------------
INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'signup_user.new_user_registration', 'New User Registration' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'signup_user.all_fields_required', 'All fields are required.' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'signup_user.desired_user_name', 'Desired User Name' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'signup_user.this_used_to_login', 'This is what you will use to login.' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'signup_user.your_first_name', 'Your First Name' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'signup_user.your_last_name', 'Your Last Name' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'signup_user.your_address', 'Your Address' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'signup_user.your_city', 'Your City' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'signup_user.your_state_province', 'Your State/Province' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'signup_user.your_zip_postal_code', 'Your Postal Code' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'signup_user.your_phone', 'Your Phone Number' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'signup_user.your_email', 'Your Email' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'signup_user.email_sent_to_complete', 'An email will be sent to this address to complete the registration process.' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'signup_user.your_time_zone', 'Your Time Zone' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'signup_user.your_language', 'Your Language' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'signup_user.your_password', 'Your Password' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'signup_user.choose_new_password', 'Choose a new password for your account.' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'signup_user.password_confirmation', 'Confirm Your Password' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'signup_user.enter_new_password_again', 'Enter your new password again.' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'signup_user.type_code_from_image', 'type the code from the image' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'signup_user.sign_me_up', 'Sign Me Up' FROM phrases

------------------------------------------------------------------------------------
-- ORGANIZATION
------------------------------------------------------------------------------------
INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'organization.title', 'Title' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'organization.address', 'Address' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'organization.city', 'City' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'organization.state_province', 'State/Province' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'organization.zip_postal_code', 'ZIP/Postal Code' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'organization.phone', 'Phone' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'organization.subdomain', 'Subdomain' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'organization.time_zone', 'Time Zone' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'organization.language', 'Language' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'organization.delete_user_confirmation', 'Are you sure you want to delete this organization?' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'organization.delete_user_caption', 'Delete this organization' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'organization.create_a_new_organization', 'Create a New Organization' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'organization.edit_organization', 'Edit Organization' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'organization.editing_organization', 'Editing organization' FROM phrases

------------------------------------------------------------------------------------
-- ORGANIZATION LIST
------------------------------------------------------------------------------------
INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'organization_list.current_organizations', 'Current Organizations' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'organization_list.add_organization', 'Add Organization' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'organization_list.no_matching_records', 'No matching records found' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'organization_list.enter_value_to_match', 'Enter a value to match on and click Search' FROM phrases



INSERT INTO updates (revision, update_date) VALUES (19, GETDATE())
GO