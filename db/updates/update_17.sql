------------------------------------------------------------------------------------
-- SHARED
------------------------------------------------------------------------------------
INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'shared.change', 'Change' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'shared.edit', 'Edit' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'shared.roles', 'Roles' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'shared.delete', 'Delete' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'shared.back', 'Back' FROM phrases

------------------------------------------------------------------------------------
-- PRAYER SUBSCRIPTION
------------------------------------------------------------------------------------
INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'subscription.comment', 'comment' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'subscription.comments', 'comments' FROM phrases

------------------------------------------------------------------------------------
-- MY ACCOUNT
------------------------------------------------------------------------------------
INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.change_my_account_information', 'Change My Account Information' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.enter_new_information', 'Enter new information and click the Submit button' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.lift_member_since', 'Lift Member since' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.change_my_password', 'Change My Password' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.enter_new_password', 'Enter a new password and click the Change button' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.change_my_time_zone', 'Change My Time Zone' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.my_prayer_requests', 'My Prayer Requests' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.click_x_to_delete', 'Click on the red ''X'' to delete that request' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.my_prayer_request_subscriptions', 'My Prayer Request Subscriptions' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.click_x_to_unsubscribe', 'Click on the red ''X'' to unsubscribe' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.my_prayer_sessions', 'My Prayer Sessions' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.you_have_fulfilled', 'You have fulfilled' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.prayer_requests_during', 'prayer request(s) during' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.hours_of_prayer_sessions', 'hour(s) of prayer sessions' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.prayer_session_start_time', 'Start Time' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.prayer_session_total_prayer_requests', 'Total Prayer Requests' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.prayer_session_notes', 'Notes' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.prayer_session_total_time', 'Total Time' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.delete_request_confirmation', 'Are you sure you want to delete this request?' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.delete_request_caption', 'Delete this prayer request' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.unsubscribe_subscription_confirmation', 'Are you sure you want to unsubscribe?' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.unsubscribe_subscription_caption', 'Unsubscribe from this prayer request' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.subscription_last_activity', 'Last Activity' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.you_have_no_requests', 'You have no prayer requests' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.you_have_no_subscriptions', 'You have not subscribed to any prayer requests' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'my_account.you_have_no_sessions', 'You have no prayer sessions' FROM phrases

------------------------------------------------------------------------------------
-- USER
------------------------------------------------------------------------------------
INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user.first_name', 'First Name' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user.last_name', 'Last Name' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user.email', 'Email' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user.address', 'Address' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user.city', 'City' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user.state_province', 'State/Province' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user.zip_postal_code', 'ZIP/Postal Code' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user.phone', 'Phone' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user.language', 'Language' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user.password', 'Password' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user.password_confirmation', 'Password Confirmation' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user.time_zone', 'Time Zone' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user.user_name', 'User Name' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user.login', 'Login' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user.status', 'User Status' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user.delete_user_confirmation', 'Are you sure you want to delete this user?' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user.delete_user_caption', 'Delete this user' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user.create_a_new_user', 'Create a New User' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user.edit_user', 'Edit User' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user.editing_user', 'Editing user' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user.leave_password_empty', 'Leave empty to keep the password unchanged' FROM phrases

------------------------------------------------------------------------------------
-- USER LIST
------------------------------------------------------------------------------------
INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user_list.current_users', 'Current Users' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user_list.add_user', 'Add User' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user_list.no_matching_records', 'No matching records found' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user_list.enter_value_to_match', 'Enter a value to match on and click Search' FROM phrases

------------------------------------------------------------------------------------
-- USER STATUS
------------------------------------------------------------------------------------
INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user_status.unconfirmed', 'Unconfirmed' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user_status.confirmed', 'Confirmed' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user_status.locked', 'Locked' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user_status.deleted', 'Deleted' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'user_status.unknown', 'Unknown' FROM phrases

------------------------------------------------------------------------------------
-- ROLES
------------------------------------------------------------------------------------
INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'roles.admin', 'Admin' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'roles.power_user', 'Power User' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'roles.moderator', 'Moderator' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'roles.wall_leader', 'Wall Leader' FROM phrases

INSERT INTO phrases ([id], [language_id], [label], [phrase]) 
SELECT COUNT(*) + 1, 1, 'roles.watchman', 'Watchman' FROM phrases



INSERT INTO updates (revision, update_date) values (17, GETDATE())
GO