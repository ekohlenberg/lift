using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using LiftCommon;

namespace LiftDomain
{
    public class Language : BaseLiftDomain
    {
        public StringProperty dateformat = new StringProperty();
        public StringProperty datetimeformat = new StringProperty();
        public IntProperty id = new IntProperty();
        public StringProperty timeformat = new StringProperty();
        public StringProperty title = new StringProperty();
        public StringProperty iso = new StringProperty();

        protected static Hashtable languagesById = new Hashtable();
        protected static Hashtable languagesByTitle = new Hashtable();
        protected static object langSync = new object();

        // language phrases...
        public PhraseProperty LOGIN_HEADING = new PhraseProperty();
        public PhraseProperty LOGIN_REGISTER = new PhraseProperty();
        public PhraseProperty LOGIN_REGISTER_LINK = new PhraseProperty();
        public PhraseProperty LOGIN_LEGEND = new PhraseProperty();
        public PhraseProperty LOGIN_USER = new PhraseProperty();
        public PhraseProperty LOGIN_USER_HELP = new PhraseProperty();
        public PhraseProperty LOGIN_PASSWORD = new PhraseProperty();
        public PhraseProperty LOGIN_PASSWORD_HELP = new PhraseProperty();
        public PhraseProperty LOGIN_PASSWORD_ERROR = new PhraseProperty();
        public PhraseProperty LOGIN_USER_ERROR = new PhraseProperty();
        public PhraseProperty LOGIN_BUTTON_TEXT = new PhraseProperty();
        public PhraseProperty LOGIN_RECOVERY = new PhraseProperty();
        public PhraseProperty LOGIN_RECOVERY_LINK = new PhraseProperty();
        public PhraseProperty LOGIN_FORGOT_YOUR_PASSWORD = new PhraseProperty();
        public PhraseProperty LOGIN_REQUEST_A_NEW_ONE = new PhraseProperty();
        public PhraseProperty LOGIN_PASSWORD_NOT_RESET = new PhraseProperty();
        public PhraseProperty LOGIN_THE_NEW_PASSWORD = new PhraseProperty();
        public PhraseProperty LOGIN_YOUR_USERNAME = new PhraseProperty();
        public PhraseProperty LOGIN_SIGN_UP = new PhraseProperty();
        public PhraseProperty LOGIN_PLEASE_ENTER_EMAIL = new PhraseProperty();
        public PhraseProperty LOGIN_WELCOME = new PhraseProperty();
        public PhraseProperty LOGIN_COME_ALONGSIDE = new PhraseProperty();
        public PhraseProperty LOGIN_IF_YOU_HAVE_AN_ACCOUNT = new PhraseProperty();




        public PhraseProperty TIMEFRAME_DAY = new PhraseProperty();
        public PhraseProperty TIMEFRAME_WEEK = new PhraseProperty();
        public PhraseProperty TIMEFRAME_MONTH = new PhraseProperty();
        public PhraseProperty TIMEFRAME_YEAR = new PhraseProperty();
        public PhraseProperty TIMEFRAME_100DAYS = new PhraseProperty();
        

        public PhraseProperty REQUESTTYPES_BIRTH = new PhraseProperty();
        public PhraseProperty REQUESTTYPES_CHURCH_STAFF = new PhraseProperty();
        public PhraseProperty REQUESTTYPES_CHURCH_VISION_STRATEGY = new PhraseProperty();
        public PhraseProperty REQUESTTYPES_DEATH = new PhraseProperty();
        public PhraseProperty REQUESTTYPES_FINANCIAL = new PhraseProperty();
        public PhraseProperty REQUESTTYPES_GOVERNMENT = new PhraseProperty();
        public PhraseProperty REQUESTTYPES_HOSTPITALIZED = new PhraseProperty();
        public PhraseProperty REQUESTTYPES_MEDICAL = new PhraseProperty();
        public PhraseProperty REQUESTTYPES_MILITARY = new PhraseProperty();
        public PhraseProperty REQUESTTYPES_MISSIONARY = new PhraseProperty();
        public PhraseProperty REQUESTTYPES_PERSONAL = new PhraseProperty();
        public PhraseProperty REQUESTTYPES_PRAISE = new PhraseProperty();
        public PhraseProperty REQUESTTYPES_RELATIONSHIPS = new PhraseProperty();
        public PhraseProperty REQUESTTYPES_SALVATION = new PhraseProperty();
        public PhraseProperty REQUESTTYPES_OTHER = new PhraseProperty();
        public PhraseProperty REQUESTTYPES_ALL = new PhraseProperty();



        public PhraseProperty SHARED_SUBMIT = new PhraseProperty();
        public PhraseProperty SHARED_CONTACT_US = new PhraseProperty();
        public PhraseProperty SHARED_ALL_FIELDS_REQUIRED = new PhraseProperty();
        public PhraseProperty SHARED_CHARACTERS_LEFT = new PhraseProperty();
        public PhraseProperty SHARED_SEARCH = new PhraseProperty();
        public PhraseProperty SHARED_MY_ACCOUNT = new PhraseProperty();
        public PhraseProperty SHARED_CHANGE = new PhraseProperty();
        public PhraseProperty SHARED_EDIT = new PhraseProperty();
        public PhraseProperty SHARED_ROLES = new PhraseProperty();
        public PhraseProperty SHARED_DELETE = new PhraseProperty();
        public PhraseProperty SHARED_BACK = new PhraseProperty();
        public PhraseProperty SHARED_UPLOAD = new PhraseProperty();
        public PhraseProperty SHARED_ADD = new PhraseProperty();
        public PhraseProperty SHARED_REQUIRED_FIELD = new PhraseProperty();
        public PhraseProperty SHARED_PASSWORDS_DO_NOT_MATCH = new PhraseProperty();
        public PhraseProperty SHARED_MUST_BE_A_VALID_EMAIL_ADDRESS = new PhraseProperty();



        public PhraseProperty CONTACTUS_YOUR_NAME = new PhraseProperty();
        public PhraseProperty CONTACTUS_YOUR_EMAIL = new PhraseProperty();
        public PhraseProperty CONTACTUS_YOUR_EMAIL_REQUIRED = new PhraseProperty();
        public PhraseProperty CONTACTUS_SUBJECT = new PhraseProperty();
        public PhraseProperty CONTACTUS_PLEASE_ENTER_SUBJECT = new PhraseProperty();
        public PhraseProperty CONTACTUS_MESSAGE = new PhraseProperty();
        public PhraseProperty CONTACTUS_PLEASE_ENTER_MESSAGE = new PhraseProperty();

        public PhraseProperty REQUESTS_VISUALIZE_PRAYER_REQUESTS = new PhraseProperty();
        public PhraseProperty REQUESTS_DURING_THE_LAST = new PhraseProperty();
        public PhraseProperty REQUEST_REQUEST_PRAYER = new PhraseProperty();
        public PhraseProperty REQUEST_DETAILS = new PhraseProperty();
        public PhraseProperty REQUEST_ALL_FIELDS_REQUIRED = new PhraseProperty();
        public PhraseProperty REQUEST_SUBJECT = new PhraseProperty();
        public PhraseProperty REQUEST_TO_BE_PRAYED_FOR = new PhraseProperty();
        public PhraseProperty REQUEST_PERSONS_RELATIONSHIP = new PhraseProperty();
        public PhraseProperty REQUEST_PERSON_BEING_PRAYED_FOR = new PhraseProperty();
        public PhraseProperty REQUEST_REQUEST_CATEGORY = new PhraseProperty();
        public PhraseProperty REQUEST_SELECT_CATEGORY = new PhraseProperty();
        public PhraseProperty REQUEST_DETAIL = new PhraseProperty();
        public PhraseProperty REQUEST_DESCRIPTION = new PhraseProperty();
        public PhraseProperty REQUEST_SUBMITTED_BY = new PhraseProperty();
        public PhraseProperty REQUEST_YOUR_NAME = new PhraseProperty();
        public PhraseProperty REQUEST_YOUR_EMAIL = new PhraseProperty();
        public PhraseProperty REQUEST_EMAIL_IS_REQUIRED = new PhraseProperty();
        public PhraseProperty REQUEST_ENCOURAGEMENT = new PhraseProperty();
        public PhraseProperty REQUEST_SEND_ENCOURAGEMENT = new PhraseProperty();
        public PhraseProperty REQUEST_ADDRESS = new PhraseProperty();
        public PhraseProperty REQUEST_RECIPIENT_ADDRESS = new PhraseProperty();
        public PhraseProperty REQUEST_PHONE = new PhraseProperty();
        public PhraseProperty REQUEST_YOUR_PHONE_NUMBER = new PhraseProperty();
        public PhraseProperty REQUEST_LIST_REQUEST = new PhraseProperty();
        public PhraseProperty REQUEST_PRINT_REQUESTS = new PhraseProperty();
        public PhraseProperty REQUEST_UPDATES_COMMENTS = new PhraseProperty();
        public PhraseProperty REQUEST_KEEP_ME_POSTED = new PhraseProperty();
        public PhraseProperty REQUEST_PRAY = new PhraseProperty();
        public PhraseProperty REQUEST_PRAYERS = new PhraseProperty();
        public PhraseProperty REQUEST_PRAYER = new PhraseProperty();
        public PhraseProperty REQUEST_ADD_YOUR_UPDATE_OR_COMMENT = new PhraseProperty();
        public PhraseProperty REQUEST_UPDATE_OR_COMMENT = new PhraseProperty();
        public PhraseProperty REQUEST_DESCRIPTION_LABEL = new PhraseProperty();
        public PhraseProperty REQUEST_TYPE_YOUR_DESCRIPTION_HERE = new PhraseProperty();
        public PhraseProperty REQUEST_REQUESTER = new PhraseProperty();
        public PhraseProperty REQUEST_PLEASE_ENTER_INFO = new PhraseProperty();
        public PhraseProperty REQUEST_PRAISE = new PhraseProperty();
        public PhraseProperty REQUEST_PRAISES_UPDATES_COMMENTS = new PhraseProperty();
        public PhraseProperty REQUEST_IS_PUBLIC = new PhraseProperty();
        public PhraseProperty REQUEST_IS_PRIVATE = new PhraseProperty();
        public PhraseProperty REQUEST_PUBLIC_OR_PRIVATE = new PhraseProperty();

        public PhraseProperty REQUEST_EDIT = new PhraseProperty();
        public PhraseProperty REQUEST_UPDATE = new PhraseProperty();
        public PhraseProperty REQUEST_COMMENT = new PhraseProperty();
        public PhraseProperty REQUEST_REPORT = new PhraseProperty();
        public PhraseProperty REQUEST_SAID = new PhraseProperty();
        public PhraseProperty REQUEST_EDIT_TITLE = new PhraseProperty();
        public PhraseProperty REQUEST_AWAITING_APPROVAL = new PhraseProperty();
        public PhraseProperty REQUEST_APPROVE = new PhraseProperty();
        public PhraseProperty REQUEST_APPROVE_TITLE = new PhraseProperty();
        public PhraseProperty REQUEST_UPDATE_NOT_SUCCESSFUL = new PhraseProperty();
        public PhraseProperty REQUEST_PRAYER_SESSION_COMPLETE = new PhraseProperty();
        public PhraseProperty REQUEST_INAPPROPRIATE = new PhraseProperty();
        public PhraseProperty REQUEST_INAPPROPRIATE_TITLE = new PhraseProperty();
        public PhraseProperty REQUEST_REPORT_ABUSE = new PhraseProperty();
        public PhraseProperty REQUEST_RECYCLE_BIN = new PhraseProperty();
        public PhraseProperty REQUEST_CONFIRM_DELETE = new PhraseProperty();
        public PhraseProperty REQUEST_DELETE = new PhraseProperty();
        public PhraseProperty REQUEST_RESTORE = new PhraseProperty();
        public PhraseProperty REQUEST_CONFIRM_RESTORE = new PhraseProperty();
        public PhraseProperty REQUEST_CONFIRM_RECYCLE = new PhraseProperty();
        public PhraseProperty REQUEST_RECYCLE = new PhraseProperty();




        public PhraseProperty LAYOUT_ADMIN = new PhraseProperty();
        public PhraseProperty LAYOUT_LOGIN = new PhraseProperty();
        public PhraseProperty LAYOUT_LOGOUT = new PhraseProperty();
        public PhraseProperty LAYOUT_HELP = new PhraseProperty();
        public PhraseProperty LAYOUT_CONTACT_US = new PhraseProperty();
        public PhraseProperty LAYOUT_ADD_PRAYER_REQUEST = new PhraseProperty();
        public PhraseProperty LAYOUT_PRAYER_REQUESTS = new PhraseProperty();
        public PhraseProperty LAYOUT_PRAISE_REPORT = new PhraseProperty();
        public PhraseProperty LAYOUT_PRAYER_SESSION = new PhraseProperty();
        public PhraseProperty LAYOUT_WATCHMAN_WALL = new PhraseProperty();
        public PhraseProperty LAYOUT_JOIN_NOW = new PhraseProperty();

        public PhraseProperty DATETIME_JUST_NOW = new PhraseProperty();
        public PhraseProperty DATETIME_ABOUT_MINUTES = new PhraseProperty();
        public PhraseProperty DATETIME_ABOUT_DAYS = new PhraseProperty();
        public PhraseProperty DATETIME_ABOUT_WEEKS = new PhraseProperty();
        public PhraseProperty DATETIME_ABOUT_MONTHS = new PhraseProperty();
        public PhraseProperty DATETIME_ABOUT_YEARS = new PhraseProperty();
        public PhraseProperty DATETIME_ABOUT_HOURS = new PhraseProperty();
        public PhraseProperty DATETIME_ABOUT_MINUTE = new PhraseProperty();
        public PhraseProperty DATETIME_ABOUT_DAY = new PhraseProperty();
        public PhraseProperty DATETIME_ABOUT_WEEK = new PhraseProperty();
        public PhraseProperty DATETIME_ABOUT_YEAR = new PhraseProperty();
        public PhraseProperty DATETIME_ABOUT_MONTH = new PhraseProperty();
        public PhraseProperty DATETIME_ABOUT_HOUR = new PhraseProperty();

        public PhraseProperty SUBSCRIPTION_COMMENT = new PhraseProperty();
        public PhraseProperty SUBSCRIPTION_COMMENTS = new PhraseProperty();

        public PhraseProperty MY_ACCOUNT_CHANGE_MY_ACCOUNT_INFORMATION = new PhraseProperty();
        public PhraseProperty MY_ACCOUNT_ENTER_NEW_INFORMATION = new PhraseProperty();
        public PhraseProperty MY_ACCOUNT_LIFT_MEMBER_SINCE = new PhraseProperty();
        public PhraseProperty MY_ACCOUNT_CHANGE_MY_PASSWORD = new PhraseProperty();
        public PhraseProperty MY_ACCOUNT_ENTER_NEW_PASSWORD = new PhraseProperty();
        public PhraseProperty MY_ACCOUNT_CHANGE_MY_TIME_ZONE = new PhraseProperty();
        public PhraseProperty MY_ACCOUNT_MY_PRAYER_REQUESTS = new PhraseProperty();
        public PhraseProperty MY_ACCOUNT_CLICK_X_TO_DELETE = new PhraseProperty();
        public PhraseProperty MY_ACCOUNT_MY_PRAYER_REQUEST_SUBSCRIPTIONS = new PhraseProperty();
        public PhraseProperty MY_ACCOUNT_CLICK_X_TO_UNSUBSCRIBE = new PhraseProperty();
        public PhraseProperty MY_ACCOUNT_MY_PRAYER_SESSIONS = new PhraseProperty();
        public PhraseProperty MY_ACCOUNT_YOU_HAVE_FULFILLED = new PhraseProperty();
        public PhraseProperty MY_ACCOUNT_PRAYER_REQUESTS_DURING = new PhraseProperty();
        public PhraseProperty MY_ACCOUNT_HOURS_OF_PRAYER_SESSIONS = new PhraseProperty();
        public PhraseProperty MY_ACCOUNT_PRAYER_SESSION_START_TIME = new PhraseProperty();
        public PhraseProperty MY_ACCOUNT_PRAYER_SESSION_TOTAL_PRAYER_REQUESTS = new PhraseProperty();
        public PhraseProperty MY_ACCOUNT_PRAYER_SESSION_NOTES = new PhraseProperty();
        public PhraseProperty MY_ACCOUNT_PRAYER_SESSION_TOTAL_TIME = new PhraseProperty();
        public PhraseProperty MY_ACCOUNT_DELETE_REQUEST_CONFIRMATION = new PhraseProperty();
        public PhraseProperty MY_ACCOUNT_DELETE_REQUEST_CAPTION = new PhraseProperty();
        public PhraseProperty MY_ACCOUNT_UNSUBSCRIBE_SUBSCRIPTION_CONFIRMATION = new PhraseProperty();
        public PhraseProperty MY_ACCOUNT_UNSUBSCRIBE_SUBSCRIPTION_CAPTION = new PhraseProperty();
        public PhraseProperty MY_ACCOUNT_SUBSCRIPTION_LAST_ACTIVITY = new PhraseProperty();
        public PhraseProperty MY_ACCOUNT_YOU_HAVE_NO_REQUESTS = new PhraseProperty();
        public PhraseProperty MY_ACCOUNT_YOU_HAVE_NO_SUBSCRIPTIONS = new PhraseProperty();
        public PhraseProperty MY_ACCOUNT_YOU_HAVE_NO_SESSIONS = new PhraseProperty();

        public PhraseProperty USER_FIRST_NAME = new PhraseProperty();
        public PhraseProperty USER_LAST_NAME = new PhraseProperty();
        public PhraseProperty USER_EMAIL = new PhraseProperty();
        public PhraseProperty USER_ADDRESS = new PhraseProperty();
        public PhraseProperty USER_CITY = new PhraseProperty();
        public PhraseProperty USER_STATE_PROVINCE = new PhraseProperty();
        public PhraseProperty USER_ZIP_POSTAL_CODE = new PhraseProperty();
        public PhraseProperty USER_PHONE = new PhraseProperty();
        public PhraseProperty USER_LANGUAGE = new PhraseProperty();
        public PhraseProperty USER_PASSWORD = new PhraseProperty();
        public PhraseProperty USER_PASSWORD_CONFIRMATION = new PhraseProperty();
        public PhraseProperty USER_TIME_ZONE = new PhraseProperty();
        public PhraseProperty USER_USER_NAME = new PhraseProperty();
        public PhraseProperty USER_LOGIN = new PhraseProperty();
        public PhraseProperty USER_STATUS = new PhraseProperty();
        public PhraseProperty USER_DELETE_USER_CONFIRMATION = new PhraseProperty();
        public PhraseProperty USER_DELETE_USER_CAPTION = new PhraseProperty();
        public PhraseProperty USER_CREATE_A_NEW_USER = new PhraseProperty();
        public PhraseProperty USER_EDIT_USER = new PhraseProperty();
        public PhraseProperty USER_EDITING_USER = new PhraseProperty();
        public PhraseProperty USER_LEAVE_PASSWORD_EMPTY = new PhraseProperty();
        public PhraseProperty USER_ANY_STATUS = new PhraseProperty();


        public PhraseProperty SIGNUP_USER_NEW_USER_REGISTRATION = new PhraseProperty();
        public PhraseProperty SIGNUP_USER_ALL_FIELDS_REQUIRED = new PhraseProperty();
        public PhraseProperty SIGNUP_USER_DESIRED_USER_NAME = new PhraseProperty();
        public PhraseProperty SIGNUP_USER_THIS_USED_TO_LOGIN = new PhraseProperty();
        public PhraseProperty SIGNUP_USER_YOUR_FIRST_NAME = new PhraseProperty();
        public PhraseProperty SIGNUP_USER_YOUR_LAST_NAME = new PhraseProperty();
        public PhraseProperty SIGNUP_USER_YOUR_ADDRESS = new PhraseProperty();
        public PhraseProperty SIGNUP_USER_YOUR_CITY = new PhraseProperty();
        public PhraseProperty SIGNUP_USER_YOUR_STATE_PROVINCE = new PhraseProperty();
        public PhraseProperty SIGNUP_USER_YOUR_ZIP_POSTAL_CODE = new PhraseProperty();
        public PhraseProperty SIGNUP_USER_YOUR_PHONE = new PhraseProperty();
        public PhraseProperty SIGNUP_USER_YOUR_EMAIL = new PhraseProperty();
        public PhraseProperty SIGNUP_USER_EMAIL_SENT_TO_COMPLETE = new PhraseProperty();
        public PhraseProperty SIGNUP_USER_YOUR_TIME_ZONE = new PhraseProperty();
        public PhraseProperty SIGNUP_USER_YOUR_LANGUAGE = new PhraseProperty();
        public PhraseProperty SIGNUP_USER_YOUR_PASSWORD = new PhraseProperty();
        public PhraseProperty SIGNUP_USER_CHOOSE_NEW_PASSWORD = new PhraseProperty();
        public PhraseProperty SIGNUP_USER_PASSWORD_CONFIRMATION = new PhraseProperty();
        public PhraseProperty SIGNUP_USER_ENTER_NEW_PASSWORD_AGAIN = new PhraseProperty();
        public PhraseProperty SIGNUP_USER_TYPE_CODE_FROM_IMAGE = new PhraseProperty();
        public PhraseProperty SIGNUP_USER_SIGN_ME_UP = new PhraseProperty();
        public PhraseProperty SIGNUP_USER_USER_REGISTRATION_FAILED = new PhraseProperty();
        public PhraseProperty SIGNUP_USER_USERNAME_TAKEN = new PhraseProperty();
        public PhraseProperty SIGNUP_USER_EMAIL_ADDRESS_IN_USE = new PhraseProperty();
        public PhraseProperty SIGNUP_ACCT_EXISTS1 = new PhraseProperty();
        public PhraseProperty SIGNUP_ACCT_EXISTS2 = new PhraseProperty();
        public PhraseProperty SIGNUP_RETRIEVE_YOUR_PASSWORD = new PhraseProperty();
        public PhraseProperty SIGNUP_PHONE_REQUIRED = new PhraseProperty();
        public PhraseProperty SIGNUP_USER_THANKYOU = new PhraseProperty();




        public PhraseProperty USER_LIST_CURRENT_USERS = new PhraseProperty();
        public PhraseProperty USER_LIST_ADD_USER = new PhraseProperty();
        public PhraseProperty USER_LIST_NO_MATCHING_RECORDS = new PhraseProperty();
        public PhraseProperty USER_LIST_ENTER_VALUE_TO_MATCH = new PhraseProperty();

        public PhraseProperty USER_STATUS_UNCONFIRMED = new PhraseProperty();
        public PhraseProperty USER_STATUS_CONFIRMED = new PhraseProperty();
        public PhraseProperty USER_STATUS_LOCKED = new PhraseProperty();
        public PhraseProperty USER_STATUS_DELETED = new PhraseProperty();
        public PhraseProperty USER_STATUS_UNKNOWN = new PhraseProperty();

        public PhraseProperty ROLES_ADMIN = new PhraseProperty();
        public PhraseProperty ROLES_POWER_USER = new PhraseProperty();
        public PhraseProperty ROLES_MODERATOR = new PhraseProperty();
        public PhraseProperty ROLES_WALL_LEADER = new PhraseProperty();
        public PhraseProperty ROLES_WATCHMAN = new PhraseProperty();
        public PhraseProperty ROLES_SYSTEM_ADMIN = new PhraseProperty();
        public PhraseProperty ROLES_ORGANIZATION_ADMIN = new PhraseProperty();

        public PhraseProperty ORGANIZATION_TITLE = new PhraseProperty();
        public PhraseProperty ORGANIZATION_ADDRESS = new PhraseProperty();
        public PhraseProperty ORGANIZATION_CITY = new PhraseProperty();
        public PhraseProperty ORGANIZATION_STATE_PROVINCE = new PhraseProperty();
        public PhraseProperty ORGANIZATION_ZIP_POSTAL_CODE = new PhraseProperty();
        public PhraseProperty ORGANIZATION_PHONE = new PhraseProperty();
        public PhraseProperty ORGANIZATION_SUBDOMAIN = new PhraseProperty();
        public PhraseProperty ORGANIZATION_TIME_ZONE = new PhraseProperty();
        public PhraseProperty ORGANIZATION_LANGUAGE = new PhraseProperty();
        public PhraseProperty ORGANIZATION_DELETE_ORGANIZATION_CONFIRMATION = new PhraseProperty();
        public PhraseProperty ORGANIZATION_DELETE_ORGANIZATION_CAPTION = new PhraseProperty();
        public PhraseProperty ORGANIZATION_CREATE_A_NEW_ORGANIZATION = new PhraseProperty();
        public PhraseProperty ORGANIZATION_EDIT_ORGANIZATION = new PhraseProperty();
        public PhraseProperty ORGANIZATION_EDITING_ORGANIZATION = new PhraseProperty();
        public PhraseProperty ORGANIZATION_EMAIL_TO_WEBMASTER = new PhraseProperty();
        public PhraseProperty ORGANIZATION_EMAIL_TO_CONTACT_US = new PhraseProperty();
        public PhraseProperty ORGANIZATION_EMAIL_TO_ENCOURAGER = new PhraseProperty();
        public PhraseProperty ORGANIZATION_STATUS = new PhraseProperty();
        public PhraseProperty ORGANIZATION_STATUS_UNAPPROVED = new PhraseProperty();
        public PhraseProperty ORGANIZATION_STATUS_APPROVED = new PhraseProperty();
        public PhraseProperty ORGANIZATION_GENERAL_INFORMATION = new PhraseProperty();
        public PhraseProperty ORGANIZATION_EMAILS = new PhraseProperty();
        public PhraseProperty ORGANIZATION_APPEARANCE = new PhraseProperty();
        public PhraseProperty ORGANIZATION_LOGO = new PhraseProperty();
        public PhraseProperty ORGANIZATION_UPLOAD_LOGO_FILE = new PhraseProperty();
        public PhraseProperty ORGANIZATION_CUSTOM_STYLESHEET = new PhraseProperty();
        public PhraseProperty ORGANIZATION_IMAGES = new PhraseProperty();
        public PhraseProperty ORGANIZATION_UPLOAD_IMAGE_FILES = new PhraseProperty();
        public PhraseProperty ORGANIZATION_IMAGE_FILE_NAME_LIST = new PhraseProperty();
        public PhraseProperty ORGANIZATION_IMAGE_FILE_NAME = new PhraseProperty();
        public PhraseProperty DELETE_ORGANIZATION_IMAGE_CONFIRMATION = new PhraseProperty();
        public PhraseProperty DELETE_ORGANIZATION_IMAGE_CAPTION = new PhraseProperty();
        public PhraseProperty ORGANIZATION_IMAGES_ADD_TO_LIST = new PhraseProperty();
        public PhraseProperty ORGANIZATION_IMAGES_REMOVE_FROM_LIST = new PhraseProperty();
        public PhraseProperty ORGANIZATION_IMAGES_UPLOAD_TO_SERVER = new PhraseProperty();

        public PhraseProperty ORGANIZATION_LIST_CURRENT_ORGANIZATIONS = new PhraseProperty();
        public PhraseProperty ORGANIZATION_LIST_ADD_ORGANIZATION = new PhraseProperty();
        public PhraseProperty ORGANIZATION_LIST_NO_MATCHING_RECORDS = new PhraseProperty();
        public PhraseProperty ORGANIZATION_LIST_ENTER_VALUE_TO_MATCH = new PhraseProperty();
        public PhraseProperty ORGANIZATION_FROM_EMAIL_ADDRESS = new PhraseProperty();
        public PhraseProperty ORGANIZATION_FOOTER = new PhraseProperty();
        public PhraseProperty ORG_WHEN_REQUESTS_ENTERED = new PhraseProperty();
        public PhraseProperty ORG_REQUESTS_AUTOMATICALLY_APPROVED = new PhraseProperty();
        public PhraseProperty ORG_REQUESTS_MANUALLY_APPROVED = new PhraseProperty();
        public PhraseProperty ORGANIZATION_NEW_USER_SIGNUP = new PhraseProperty();
        public PhraseProperty ORGANIZATION_NEW_USERS_CREATE_ACCOUNTS = new PhraseProperty();
        public PhraseProperty ORGANIZATION_NEW_USERS_REQUIRE_APPROVAL = new PhraseProperty();





        public PhraseProperty SIGNUP_ORGANIZATION_TITLE = new PhraseProperty();
        public PhraseProperty SIGNUP_ORGANIZATION_TERMS_OF_USE = new PhraseProperty();
        public PhraseProperty SIGNUP_ORGANIZATION_APPROVAL_REQUEST_SUBJECT = new PhraseProperty();
        public PhraseProperty SIGNUP_ORGANIZATION_APPROVAL_REQUEST_MESSAGE = new PhraseProperty();
        public PhraseProperty SIGNUP_ORGANIZATION_APPROVAL_RESPONSE_SUBJECT = new PhraseProperty();
        public PhraseProperty SIGNUP_ORGANIZATION_APPROVAL_RESPONSE_MESSAGE = new PhraseProperty();



        public PhraseProperty PS_WELCOME = new PhraseProperty();
        public PhraseProperty PS_SENTENCE1 = new PhraseProperty();
        public PhraseProperty PS_SENTENCE2 = new PhraseProperty();
        public PhraseProperty PS_SENTENCE3 = new PhraseProperty();
        public PhraseProperty PS_SENTENCE4 = new PhraseProperty();
        public PhraseProperty PS_SENTENCE5 = new PhraseProperty();
        public PhraseProperty PS_SENTENCE6 = new PhraseProperty();
        public PhraseProperty PS_SENTENCE7 = new PhraseProperty();
        public PhraseProperty PS_NEW_PRAYER_SESSION = new PhraseProperty();
        public PhraseProperty PS_NOTES = new PhraseProperty();
        public PhraseProperty PS_YOUR_NOTES = new PhraseProperty();
        public PhraseProperty PS_PLAY_PAUSE = new PhraseProperty();
        public PhraseProperty PS_NEXT = new PhraseProperty();
        public PhraseProperty PS_PREVIOUS = new PhraseProperty();
        public PhraseProperty PS_END_SESSION = new PhraseProperty();
        public PhraseProperty PS_CURRENTLY_VIEWING_REQUEST = new PhraseProperty();
        public PhraseProperty PS_OF = new PhraseProperty();

		public PhraseProperty ADMIN_ADMIN_PANEL = new PhraseProperty();
        public PhraseProperty ADMIN_ADMIN_PANEL_DESCRIPTION1 = new PhraseProperty();
        public PhraseProperty ADMIN_ADMIN_PANEL_DESCRIPTION2 = new PhraseProperty();
        public PhraseProperty ADMIN_HELP_GUIDE = new PhraseProperty();
        public PhraseProperty ADMIN_HELP_GUIDE_DESCRIPTION = new PhraseProperty();
        public PhraseProperty ADMIN_CONFIGURATION = new PhraseProperty();
        public PhraseProperty ADMIN_CONFIGURATION_DESCRIPTION = new PhraseProperty();
        public PhraseProperty ADMIN_COMMUNITY_EMAILS = new PhraseProperty();
        public PhraseProperty ADMIN_COMMUNITY_EMAILS_DESCRIPTION = new PhraseProperty();
        public PhraseProperty ADMIN_MANAGE_ROLES = new PhraseProperty();
        public PhraseProperty ADMIN_MANAGE_ROLES_DESCRIPTION = new PhraseProperty();
        public PhraseProperty ADMIN_MANAGE_ORGANIZATIONS = new PhraseProperty();
        public PhraseProperty ADMIN_MANAGE_ORGANIZATIONS_DESCRIPTION = new PhraseProperty();
        public PhraseProperty ADMIN_MANAGE_USERS = new PhraseProperty();
        public PhraseProperty ADMIN_MANAGE_USERS_DESCRIPTION = new PhraseProperty();
        public PhraseProperty ADMIN_MANAGE_WALLS = new PhraseProperty();
        public PhraseProperty ADMIN_MANAGE_WALLS_DESCRIPTION = new PhraseProperty();

        public PhraseProperty WALL_ALREADY_SUBSCRIBED = new PhraseProperty();
        public PhraseProperty WALL_ALREADY_SUBSCRIBED_TEXT = new PhraseProperty();
        public PhraseProperty WALL_UNSUBSCRIBE_TEXT = new PhraseProperty();
        public PhraseProperty WALL_YES = new PhraseProperty();
        public PhraseProperty WALL_NO = new PhraseProperty();
        public PhraseProperty WALL_UNSUBSCRIBE_FROM_THIS_TIME = new PhraseProperty();
        public PhraseProperty WALL_WALLS_OPEN = new PhraseProperty();
        public PhraseProperty WALL_SUBSCRIBE_TO_THIS_SLOT = new PhraseProperty();
        public PhraseProperty WALL_MY_TIME = new PhraseProperty();
        public PhraseProperty WALL_OPEN = new PhraseProperty();
        public PhraseProperty WALL_FULL = new PhraseProperty();
        public PhraseProperty WALL_PRAYER_TIME_SELECTION = new PhraseProperty();
        public PhraseProperty WALL_SENTENCE2 = new PhraseProperty();
        public PhraseProperty WALL_SENTENCE3 = new PhraseProperty();
        public PhraseProperty WALL_SOME_WALLS_OPEN = new PhraseProperty();
        public PhraseProperty WALL_PRAYER_TIMES = new PhraseProperty();
        public PhraseProperty WALL_MY_PRAYER_TIME = new PhraseProperty();
        public PhraseProperty WALL_ARE_YOU_SURE = new PhraseProperty();
        public PhraseProperty WALL_MANAGE_WALLS = new PhraseProperty();
        public PhraseProperty WALL_ADD_A_NEW_WALL = new PhraseProperty();
        public PhraseProperty WALL_TITLE = new PhraseProperty();
        public PhraseProperty WALL_YOU_CAN_CHANGE_THIS_LATER = new PhraseProperty();



        public PhraseProperty SHARED_SUNDAY = new PhraseProperty();
        public PhraseProperty SHARED_MONDAY = new PhraseProperty();
        public PhraseProperty SHARED_TUESDAY = new PhraseProperty();
        public PhraseProperty SHARED_WEDNESDAY = new PhraseProperty();
        public PhraseProperty SHARED_THURSDAY = new PhraseProperty();
        public PhraseProperty SHARED_FRIDAY = new PhraseProperty();
        public PhraseProperty SHARED_SATURDAY = new PhraseProperty();

        public PhraseProperty FORGOT_PASSWORD_INSTRUCTIONS = new PhraseProperty();
        public PhraseProperty FORGOT_PASSWORD_NOTIFICATION_SUBJECT = new PhraseProperty();
        public PhraseProperty FORGOT_PASSWORD_NOTIFICATION_MESSAGE = new PhraseProperty();

        public PhraseProperty EMAIL_ALL_USERS_CAPTION = new PhraseProperty();
        
        public PhraseProperty REQUEST_HOW_INAPPROPRIATE = new PhraseProperty();
        public PhraseProperty REQUEST_FIELDS_OPTIONAL = new PhraseProperty();

        protected const string ENGLISH = "English";

        public Language()
        {
            BaseTable = "languages";
            AutoIdentity = true;
            PrimaryKey = "id";

            attach("dateformat", dateformat);
            attach("datetimeformat", datetimeformat);
            attach("id", id);
            attach("timeformat", timeformat);
            attach("title", title);
            attach("iso", iso);

            attach("login.heading", LOGIN_HEADING);
            attach("login.register", LOGIN_REGISTER);
            attach("login.register_link", LOGIN_REGISTER_LINK);
            attach("login.legend", LOGIN_LEGEND);
            attach("login.user", LOGIN_USER);
            attach("login.user_help", LOGIN_USER_HELP);
            attach("login.user_error", LOGIN_USER_ERROR);
            attach("login.password", LOGIN_PASSWORD);
            attach("login.password_help", LOGIN_PASSWORD_HELP);
            attach("login.password_error", LOGIN_PASSWORD_ERROR);
            attach("login.button_text", LOGIN_BUTTON_TEXT);
            attach("login.recovery", LOGIN_RECOVERY);
            attach("login.recovery_link", LOGIN_RECOVERY_LINK);
            attach("login.forgot_your_password", LOGIN_FORGOT_YOUR_PASSWORD);
            attach("login.request_a_new_one", LOGIN_REQUEST_A_NEW_ONE);
            attach("login.password_not_reset", LOGIN_PASSWORD_NOT_RESET);
            attach("login.the_new_password", LOGIN_THE_NEW_PASSWORD);
            attach("login.your_username", LOGIN_YOUR_USERNAME);
            attach("login.sign_up", LOGIN_SIGN_UP);
            attach("login.please_enter_email", LOGIN_PLEASE_ENTER_EMAIL);
            attach("login.welcome", LOGIN_WELCOME);
            attach("login.come_alongside", LOGIN_COME_ALONGSIDE);
            attach("login.if_you_have_an_account", LOGIN_IF_YOU_HAVE_AN_ACCOUNT);


            attach("timeframe.day", TIMEFRAME_DAY);
            attach("timeframe.week", TIMEFRAME_WEEK);
            attach("timeframe.month", TIMEFRAME_MONTH);
            attach("timeframe.year", TIMEFRAME_YEAR);
            attach("timeframe.100days", TIMEFRAME_100DAYS);

            attach("requesttypes.birth", REQUESTTYPES_BIRTH);
            attach("requesttypes.church_staff", REQUESTTYPES_CHURCH_STAFF);
            attach("requesttypes.church_vision_strategy", REQUESTTYPES_CHURCH_VISION_STRATEGY);
            attach("requesttypes.death", REQUESTTYPES_DEATH);
            attach("requesttypes.financial", REQUESTTYPES_FINANCIAL);
            attach("requesttypes.government", REQUESTTYPES_GOVERNMENT);
            attach("requesttypes.hostpitalized", REQUESTTYPES_HOSTPITALIZED);
            attach("requesttypes.medical", REQUESTTYPES_MEDICAL);
            attach("requesttypes.military", REQUESTTYPES_MILITARY);
            attach("requesttypes.missionary", REQUESTTYPES_MISSIONARY);
            attach("requesttypes.personal", REQUESTTYPES_PERSONAL);
            attach("requesttypes.praise", REQUESTTYPES_PRAISE);
            attach("requesttypes.relationships", REQUESTTYPES_RELATIONSHIPS);
            attach("requesttypes.salvation", REQUESTTYPES_SALVATION);
            attach("requesttypes.other", REQUESTTYPES_OTHER);
            attach("requesttypes.all", REQUESTTYPES_ALL);

            attach("shared.submit", SHARED_SUBMIT);
            attach("shared.contact_us", SHARED_CONTACT_US);
            attach("shared.all_fields_required", SHARED_ALL_FIELDS_REQUIRED);
            attach("shared.characters_left", SHARED_CHARACTERS_LEFT);
            attach("shared.search", SHARED_SEARCH);
            attach("shared.my_account", SHARED_MY_ACCOUNT);
            attach("shared.change", SHARED_CHANGE);
            attach("shared.edit", SHARED_EDIT);
            attach("shared.roles", SHARED_ROLES);
            attach("shared.delete", SHARED_DELETE);
            attach("shared.back", SHARED_BACK);
            attach("shared.upload", SHARED_UPLOAD);
            attach("shared.add", SHARED_ADD);
            attach("shared.required_field", SHARED_REQUIRED_FIELD);
            attach("shared.passwords_do_not_match", SHARED_PASSWORDS_DO_NOT_MATCH);
            attach("shared.must_be_a_valid_email_address", SHARED_MUST_BE_A_VALID_EMAIL_ADDRESS);

            attach("contactus.your_name", CONTACTUS_YOUR_NAME);
            attach("contactus.your_email", CONTACTUS_YOUR_EMAIL);
            attach("contactus.your_email_required", CONTACTUS_YOUR_EMAIL_REQUIRED);
            attach("contactus.subject", CONTACTUS_SUBJECT);
            attach("contactus.please_enter_subject", CONTACTUS_PLEASE_ENTER_SUBJECT);
            attach("contactus.message", CONTACTUS_MESSAGE);
            attach("contactus.please_enter_message", CONTACTUS_PLEASE_ENTER_MESSAGE);

            attach("requests.visualize_prayer_requests", REQUESTS_VISUALIZE_PRAYER_REQUESTS);
            attach("requests.during_the_last", REQUESTS_DURING_THE_LAST);
            attach("request.request_prayer", REQUEST_REQUEST_PRAYER);
            attach("request.details", REQUEST_DETAILS);
            attach("request.all_fields_required", REQUEST_ALL_FIELDS_REQUIRED);
            attach("request.subject", REQUEST_SUBJECT);
            attach("request.to_be_prayed_for", REQUEST_TO_BE_PRAYED_FOR);
            attach("request.persons_relationship", REQUEST_PERSONS_RELATIONSHIP);
            attach("request.person_being_prayed_for", REQUEST_PERSON_BEING_PRAYED_FOR);
            attach("request.request_category", REQUEST_REQUEST_CATEGORY);
            attach("request.select_category", REQUEST_SELECT_CATEGORY);
            attach("request.detail", REQUEST_DETAIL);
            attach("request.description", REQUEST_DESCRIPTION);
            attach("request.submitted_by", REQUEST_SUBMITTED_BY);
            attach("request.your_name", REQUEST_YOUR_NAME);
            attach("request.your_email", REQUEST_YOUR_EMAIL);
            attach("request.email_is_required", REQUEST_EMAIL_IS_REQUIRED);
            attach("request.encouragement", REQUEST_ENCOURAGEMENT);
            attach("request.send_encouragement", REQUEST_SEND_ENCOURAGEMENT);
            attach("request.address", REQUEST_ADDRESS);
            attach("request.recipient_address", REQUEST_RECIPIENT_ADDRESS);
            attach("request.phone", REQUEST_PHONE);
            attach("request.your_phone_number", REQUEST_YOUR_PHONE_NUMBER);
            attach("request.list_request", REQUEST_LIST_REQUEST);
            attach("request.print_requests", REQUEST_PRINT_REQUESTS);
            attach("request.updates_comments", REQUEST_UPDATES_COMMENTS);
            attach("request.keep_me_posted", REQUEST_KEEP_ME_POSTED);
            attach("request.pray", REQUEST_PRAY);
            attach("request.prayers", REQUEST_PRAYERS);
            attach("request.prayer", REQUEST_PRAYER);

            attach("request.edit", REQUEST_EDIT);
            attach("request.edit_title", REQUEST_EDIT_TITLE);
            attach("request.update", REQUEST_UPDATE);
            attach("request.comment", REQUEST_COMMENT);
            attach("request.report", REQUEST_REPORT);
            attach("request.said", REQUEST_SAID);

            attach("request.awaiting_approval", REQUEST_AWAITING_APPROVAL);
            attach("request.approve", REQUEST_APPROVE);
            attach("request.approve_title", REQUEST_APPROVE_TITLE);
            attach("request.update_not_successful", REQUEST_UPDATE_NOT_SUCCESSFUL);
            attach("request.prayer_session_complete", REQUEST_PRAYER_SESSION_COMPLETE);
            attach("request.praise", REQUEST_PRAISE);
            attach("request.praises_updates_comments", REQUEST_PRAISES_UPDATES_COMMENTS);
            attach("request.is_private", REQUEST_IS_PRIVATE);
            attach("request.is_public", REQUEST_IS_PUBLIC );
            attach("request.public_or_private", REQUEST_PUBLIC_OR_PRIVATE);
            attach("request.inappropriate", REQUEST_INAPPROPRIATE);
            attach("request.inappropriate_title", REQUEST_INAPPROPRIATE_TITLE);
            attach("request.report_abuse", REQUEST_REPORT_ABUSE);
            attach("request.recycle_bin", REQUEST_RECYCLE_BIN);
            attach("request.confirm_delete", REQUEST_CONFIRM_DELETE);
            attach("request.delete", REQUEST_DELETE);
            attach("request.restore", REQUEST_RESTORE);
            attach("request.confirm_restore", REQUEST_CONFIRM_RESTORE);
            attach("request.confirm_recycle", REQUEST_CONFIRM_RECYCLE);
            attach("request.recycle", REQUEST_RECYCLE);


            attach("layout.admin", LAYOUT_ADMIN);

            attach("request.add_your_update_or_comment", REQUEST_ADD_YOUR_UPDATE_OR_COMMENT);
            attach("request.update_or_comment", REQUEST_UPDATE_OR_COMMENT);
            attach("request.description_label", REQUEST_DESCRIPTION_LABEL);
            attach("request.type_your_description_here", REQUEST_TYPE_YOUR_DESCRIPTION_HERE);
            attach("request.requester", REQUEST_REQUESTER);
            attach("request.please_enter_info", REQUEST_PLEASE_ENTER_INFO);


            attach("layout.login", LAYOUT_LOGIN);
            attach("layout.logout", LAYOUT_LOGOUT);
            attach("layout.help", LAYOUT_HELP);
            attach("layout.contact_us", LAYOUT_CONTACT_US);
            attach("layout.add_prayer_request", LAYOUT_ADD_PRAYER_REQUEST);
            attach("layout.prayer_requests", LAYOUT_PRAYER_REQUESTS);
            attach("layout.praise_report", LAYOUT_PRAISE_REPORT);
            attach("layout.prayer_session", LAYOUT_PRAYER_SESSION);
            attach("layout.watchman_wall", LAYOUT_WATCHMAN_WALL);
            attach("layout.join_now", LAYOUT_JOIN_NOW);

            attach("datetime.just_now", DATETIME_JUST_NOW);
            attach("datetime.about_minutes", DATETIME_ABOUT_MINUTES);
            attach("datetime.about_days", DATETIME_ABOUT_DAYS);
            attach("datetime.about_weeks", DATETIME_ABOUT_WEEKS);
            attach("datetime.about_months", DATETIME_ABOUT_MONTHS);
            attach("datetime.about_years", DATETIME_ABOUT_YEARS);
            attach("datetime.about_hours", DATETIME_ABOUT_HOURS);
            attach("datetime.about_minute", DATETIME_ABOUT_MINUTE);
            attach("datetime.about_day", DATETIME_ABOUT_DAY);
            attach("datetime.about_week", DATETIME_ABOUT_WEEK);
            attach("datetime.about_year", DATETIME_ABOUT_YEAR);
            attach("datetime.about_month", DATETIME_ABOUT_MONTH);
            attach("datetime.about_hour", DATETIME_ABOUT_HOUR);

            attach("subscription.comment", SUBSCRIPTION_COMMENT);
            attach("subscription.comments", SUBSCRIPTION_COMMENTS);

            attach("my_account.change_my_account_information", MY_ACCOUNT_CHANGE_MY_ACCOUNT_INFORMATION);
            attach("my_account.enter_new_information", MY_ACCOUNT_ENTER_NEW_INFORMATION);
            attach("my_account.lift_member_since", MY_ACCOUNT_LIFT_MEMBER_SINCE);
            attach("my_account.change_my_password", MY_ACCOUNT_CHANGE_MY_PASSWORD);
            attach("my_account.enter_new_password", MY_ACCOUNT_ENTER_NEW_PASSWORD);
            attach("my_account.change_my_time_zone", MY_ACCOUNT_CHANGE_MY_TIME_ZONE);
            attach("my_account.my_prayer_requests", MY_ACCOUNT_MY_PRAYER_REQUESTS);
            attach("my_account.click_x_to_delete", MY_ACCOUNT_CLICK_X_TO_DELETE);
            attach("my_account.my_prayer_request_subscriptions", MY_ACCOUNT_MY_PRAYER_REQUEST_SUBSCRIPTIONS);
            attach("my_account.click_x_to_unsubscribe", MY_ACCOUNT_CLICK_X_TO_UNSUBSCRIBE);
            attach("my_account.my_prayer_sessions", MY_ACCOUNT_MY_PRAYER_SESSIONS);
            attach("my_account.you_have_fulfilled", MY_ACCOUNT_YOU_HAVE_FULFILLED);
            attach("my_account.prayer_requests_during", MY_ACCOUNT_PRAYER_REQUESTS_DURING);
            attach("my_account.hours_of_prayer_sessions", MY_ACCOUNT_HOURS_OF_PRAYER_SESSIONS);
            attach("my_account.prayer_session_start_time", MY_ACCOUNT_PRAYER_SESSION_START_TIME);
            attach("my_account.prayer_session_total_prayer_requests", MY_ACCOUNT_PRAYER_SESSION_TOTAL_PRAYER_REQUESTS);
            attach("my_account.prayer_session_notes", MY_ACCOUNT_PRAYER_SESSION_NOTES);
            attach("my_account.prayer_session_total_time", MY_ACCOUNT_PRAYER_SESSION_TOTAL_TIME);
            attach("my_account.delete_request_confirmation", MY_ACCOUNT_DELETE_REQUEST_CONFIRMATION);
            attach("my_account.delete_request_caption", MY_ACCOUNT_DELETE_REQUEST_CAPTION);
            attach("my_account.unsubscribe_subscription_confirmation", MY_ACCOUNT_UNSUBSCRIBE_SUBSCRIPTION_CONFIRMATION);
            attach("my_account.unsubscribe_subscription_caption", MY_ACCOUNT_UNSUBSCRIBE_SUBSCRIPTION_CAPTION);
            attach("my_account.subscription_last_activity", MY_ACCOUNT_SUBSCRIPTION_LAST_ACTIVITY);
            attach("my_account.you_have_no_requests", MY_ACCOUNT_YOU_HAVE_NO_REQUESTS);
            attach("my_account.you_have_no_subscriptions", MY_ACCOUNT_YOU_HAVE_NO_SUBSCRIPTIONS);
            attach("my_account.you_have_no_sessions", MY_ACCOUNT_YOU_HAVE_NO_SESSIONS);

            attach("user.first_name", USER_FIRST_NAME);
            attach("user.last_name", USER_LAST_NAME);
            attach("user.email", USER_EMAIL);
            attach("user.address", USER_ADDRESS);
            attach("user.city", USER_CITY);
            attach("user.state_province", USER_STATE_PROVINCE);
            attach("user.zip_postal_code", USER_ZIP_POSTAL_CODE);
            attach("user.phone", USER_PHONE);
            attach("user.language", USER_LANGUAGE);
            attach("user.password", USER_PASSWORD);
            attach("user.password_confirmation", USER_PASSWORD_CONFIRMATION);
            attach("user.time_zone", USER_TIME_ZONE);
            attach("user.user_name", USER_USER_NAME);
            attach("user.login", USER_LOGIN);
            attach("user.status", USER_STATUS);
            attach("user.delete_user_confirmation", USER_DELETE_USER_CONFIRMATION);
            attach("user.delete_user_caption", USER_DELETE_USER_CAPTION);
            attach("user.create_a_new_user", USER_CREATE_A_NEW_USER);
            attach("user.edit_user", USER_EDIT_USER);
            attach("user.editing_user", USER_EDITING_USER);
            attach("user.leave_password_empty", USER_LEAVE_PASSWORD_EMPTY);
            attach("user.any_status", USER_ANY_STATUS);


            attach("signup_user.new_user_registration", SIGNUP_USER_NEW_USER_REGISTRATION);
            attach("signup_user.all_fields_required", SIGNUP_USER_ALL_FIELDS_REQUIRED);
            attach("signup_user.desired_user_name", SIGNUP_USER_DESIRED_USER_NAME);
            attach("signup_user.this_used_to_login", SIGNUP_USER_THIS_USED_TO_LOGIN);
            attach("signup_user.your_first_name", SIGNUP_USER_YOUR_FIRST_NAME);
            attach("signup_user.your_last_name", SIGNUP_USER_YOUR_LAST_NAME);
            attach("signup_user.your_address", SIGNUP_USER_YOUR_ADDRESS);
            attach("signup_user.your_city", SIGNUP_USER_YOUR_CITY);
            attach("signup_user.your_state_province", SIGNUP_USER_YOUR_STATE_PROVINCE);
            attach("signup_user.your_zip_postal_code", SIGNUP_USER_YOUR_ZIP_POSTAL_CODE);
            attach("signup_user.your_phone", SIGNUP_USER_YOUR_PHONE);
            attach("signup_user.your_email", SIGNUP_USER_YOUR_EMAIL);
            attach("signup_user.email_sent_to_complete", SIGNUP_USER_EMAIL_SENT_TO_COMPLETE);
            attach("signup_user.your_time_zone", SIGNUP_USER_YOUR_TIME_ZONE);
            attach("signup_user.your_language", SIGNUP_USER_YOUR_LANGUAGE);
            attach("signup_user.your_password", SIGNUP_USER_YOUR_PASSWORD);
            attach("signup_user.choose_new_password", SIGNUP_USER_CHOOSE_NEW_PASSWORD);
            attach("signup_user.password_confirmation", SIGNUP_USER_PASSWORD_CONFIRMATION);
            attach("signup_user.enter_new_password_again", SIGNUP_USER_ENTER_NEW_PASSWORD_AGAIN);
            attach("signup_user.type_code_from_image", SIGNUP_USER_TYPE_CODE_FROM_IMAGE);
            attach("signup_user.sign_me_up", SIGNUP_USER_SIGN_ME_UP);
            attach("signup_user.user_registration_failed", SIGNUP_USER_USER_REGISTRATION_FAILED);
            attach("signup_user.username_taken", SIGNUP_USER_USERNAME_TAKEN);
            attach("signup_user.email_address_in_use", SIGNUP_USER_EMAIL_ADDRESS_IN_USE);
            attach("signup.acct_exists1", SIGNUP_ACCT_EXISTS1);
            attach("signup.acct_exists2", SIGNUP_ACCT_EXISTS2);
            attach("signup.retrieve_your_password", SIGNUP_RETRIEVE_YOUR_PASSWORD);
            attach("signup.phone_required", SIGNUP_PHONE_REQUIRED);
            attach("signup_user.thankyou", SIGNUP_USER_THANKYOU);


            attach("user_list.current_users", USER_LIST_CURRENT_USERS);
            attach("user_list.add_user", USER_LIST_ADD_USER);
            attach("user_list.no_matching_records", USER_LIST_NO_MATCHING_RECORDS);
            attach("user_list.enter_value_to_match", USER_LIST_ENTER_VALUE_TO_MATCH);

            attach("user_status.unconfirmed", USER_STATUS_UNCONFIRMED);
            attach("user_status.confirmed", USER_STATUS_CONFIRMED);
            attach("user_status.locked", USER_STATUS_LOCKED);
            attach("user_status.deleted", USER_STATUS_DELETED);
            attach("user_status.unknown", USER_STATUS_UNKNOWN);

            attach("roles.admin", ROLES_ADMIN);
            attach("roles.power_user", ROLES_POWER_USER);
            attach("roles.moderator", ROLES_MODERATOR);
            attach("roles.wall_leader", ROLES_WALL_LEADER);
            attach("roles.watchman", ROLES_WATCHMAN);
            attach("roles.organization_admin", ROLES_ORGANIZATION_ADMIN );
            attach("roles.system_admin", ROLES_SYSTEM_ADMIN);

            attach("organization.title", ORGANIZATION_TITLE);
            attach("organization.address", ORGANIZATION_ADDRESS);
            attach("organization.city", ORGANIZATION_CITY);
            attach("organization.state_province", ORGANIZATION_STATE_PROVINCE);
            attach("organization.zip_postal_code", ORGANIZATION_ZIP_POSTAL_CODE);
            attach("organization.phone", ORGANIZATION_PHONE);
            attach("organization.subdomain", ORGANIZATION_SUBDOMAIN);
            attach("organization.time_zone", ORGANIZATION_TIME_ZONE);
            attach("organization.language", ORGANIZATION_LANGUAGE);
            attach("organization.delete_organization_confirmation", ORGANIZATION_DELETE_ORGANIZATION_CONFIRMATION);
            attach("organization.delete_organization_caption", ORGANIZATION_DELETE_ORGANIZATION_CAPTION);
            attach("organization.create_a_new_organization", ORGANIZATION_CREATE_A_NEW_ORGANIZATION);
            attach("organization.edit_organization", ORGANIZATION_EDIT_ORGANIZATION);
            attach("organization.editing_organization", ORGANIZATION_EDITING_ORGANIZATION);
            attach("organization.email_to_webmaster", ORGANIZATION_EMAIL_TO_WEBMASTER);
            attach("organization.email_to_contact_us", ORGANIZATION_EMAIL_TO_CONTACT_US);
            attach("organization.email_to_encourager", ORGANIZATION_EMAIL_TO_ENCOURAGER);
            attach("organization.status", ORGANIZATION_STATUS);
            attach("organization.status_unapproved", ORGANIZATION_STATUS_UNAPPROVED);
            attach("organization.status_approved", ORGANIZATION_STATUS_APPROVED);
            attach("organization.general_information", ORGANIZATION_GENERAL_INFORMATION);
            attach("organization.emails", ORGANIZATION_EMAILS);
            attach("organization.appearance", ORGANIZATION_APPEARANCE);
            attach("organization.logo", ORGANIZATION_LOGO);
            attach("organization.upload_logo_file", ORGANIZATION_UPLOAD_LOGO_FILE);
            attach("organization.custom_stylesheet", ORGANIZATION_CUSTOM_STYLESHEET);
            attach("organization.images", ORGANIZATION_IMAGES);
            attach("organization.upload_image_files", ORGANIZATION_UPLOAD_IMAGE_FILES);
            attach("organization.image_file_name_list", ORGANIZATION_IMAGE_FILE_NAME_LIST);
            attach("organization.image_file_name", ORGANIZATION_IMAGE_FILE_NAME);
            attach("organization.delete_org_image_confirmation", DELETE_ORGANIZATION_IMAGE_CONFIRMATION);
            attach("organization.delete_org_image_caption", DELETE_ORGANIZATION_IMAGE_CAPTION);
            attach("organization.images_add_to_list", ORGANIZATION_IMAGES_ADD_TO_LIST);
            attach("organization.images_remove_from_list", ORGANIZATION_IMAGES_REMOVE_FROM_LIST);
            attach("organization.images_upload_to_server", ORGANIZATION_IMAGES_UPLOAD_TO_SERVER);

            attach("organization_list.current_organizations", ORGANIZATION_LIST_CURRENT_ORGANIZATIONS);
            attach("organization_list.add_organization", ORGANIZATION_LIST_ADD_ORGANIZATION);
            attach("organization_list.no_matching_records", ORGANIZATION_LIST_NO_MATCHING_RECORDS);
            attach("organization_list.enter_value_to_match", ORGANIZATION_LIST_ENTER_VALUE_TO_MATCH);
            attach("organization.from_email_address", ORGANIZATION_FROM_EMAIL_ADDRESS);
            attach("organization.footer", ORGANIZATION_FOOTER);

            attach("org.when_requests_entered", ORG_WHEN_REQUESTS_ENTERED);
            attach("org.requests_automatically_approved", ORG_REQUESTS_AUTOMATICALLY_APPROVED);
            attach("org.requests_manually_approved", ORG_REQUESTS_MANUALLY_APPROVED);

            attach("organization.new_user_signup", ORGANIZATION_NEW_USER_SIGNUP);
            attach("organization.new_users_create_accounts", ORGANIZATION_NEW_USERS_CREATE_ACCOUNTS);
            attach("organization.new_users_require_approval", ORGANIZATION_NEW_USERS_REQUIRE_APPROVAL);



            attach("signup_organization.title", SIGNUP_ORGANIZATION_TITLE);
            attach("signup_organization.terms_of_use", SIGNUP_ORGANIZATION_TERMS_OF_USE);
            attach("signup_organization.approval_request_subject", SIGNUP_ORGANIZATION_APPROVAL_REQUEST_SUBJECT);
            attach("signup_organization.approval_request_message", SIGNUP_ORGANIZATION_APPROVAL_REQUEST_MESSAGE);
            attach("signup_organization.approval_response_subject", SIGNUP_ORGANIZATION_APPROVAL_RESPONSE_SUBJECT);
            attach("signup_organization.approval_response_message", SIGNUP_ORGANIZATION_APPROVAL_RESPONSE_MESSAGE);

            attach("ps.welcome", PS_WELCOME);
            attach("ps.sentence1", PS_SENTENCE1);
            attach("ps.sentence2", PS_SENTENCE2);
            attach("ps.sentence3", PS_SENTENCE3);
            attach("ps.sentence4", PS_SENTENCE4);
            attach("ps.sentence5", PS_SENTENCE5);
            attach("ps.sentence6", PS_SENTENCE6);
            attach("ps.sentence7", PS_SENTENCE7);
            attach("ps.new_prayer_session", PS_NEW_PRAYER_SESSION);
            attach("ps.notes", PS_NOTES);
            attach("ps.your_notes", PS_YOUR_NOTES);
            attach("ps.play_pause", PS_PLAY_PAUSE);
            attach("ps.next", PS_NEXT);
            attach("ps.previous", PS_PREVIOUS);
            attach("ps.end_session", PS_END_SESSION);
            attach("ps.currently_viewing_request", PS_CURRENTLY_VIEWING_REQUEST);
            attach("ps.of", PS_OF);

			attach("admin.admin_panel", ADMIN_ADMIN_PANEL);
            attach("admin.admin_panel_description1", ADMIN_ADMIN_PANEL_DESCRIPTION1);
            attach("admin.admin_panel_description2", ADMIN_ADMIN_PANEL_DESCRIPTION2);
            attach("admin.help_guide", ADMIN_HELP_GUIDE);
            attach("admin.help_guide_description", ADMIN_HELP_GUIDE_DESCRIPTION);
            attach("admin.configuration", ADMIN_CONFIGURATION);
            attach("admin.configuration_description", ADMIN_CONFIGURATION_DESCRIPTION);
            attach("admin.community_emails", ADMIN_COMMUNITY_EMAILS);
            attach("admin.community_emails_description", ADMIN_COMMUNITY_EMAILS_DESCRIPTION);
            attach("admin.manage_roles", ADMIN_MANAGE_ROLES);
            attach("admin.manage_roles_description", ADMIN_MANAGE_ROLES_DESCRIPTION);
            attach("admin.manage_organizations", ADMIN_MANAGE_ORGANIZATIONS);
            attach("admin.manage_organizations_description", ADMIN_MANAGE_ORGANIZATIONS_DESCRIPTION);
            attach("admin.manage_users", ADMIN_MANAGE_USERS);
            attach("admin.manage_users_description", ADMIN_MANAGE_USERS_DESCRIPTION);
            attach("admin.manage_walls", ADMIN_MANAGE_WALLS);
            attach("admin.manage_walls_description", ADMIN_MANAGE_WALLS_DESCRIPTION);

            attach("wall.already_subscribed", WALL_ALREADY_SUBSCRIBED);
            attach("wall.already_subscribed_text", WALL_ALREADY_SUBSCRIBED_TEXT);
            attach("wall.unsubscribe_text", WALL_UNSUBSCRIBE_TEXT);
            attach("wall.yes", WALL_YES);
            attach("wall.no", WALL_NO);
            attach("wall.unsubscribe_from_this_time", WALL_UNSUBSCRIBE_FROM_THIS_TIME);
            attach("wall.walls_open", WALL_WALLS_OPEN);
            attach("wall.subscribe_to_this_slot", WALL_SUBSCRIBE_TO_THIS_SLOT);
            attach("wall.my_time", WALL_MY_TIME);
            attach("wall.open", WALL_OPEN);
            attach("wall.full", WALL_FULL);
            attach("wall.prayer_time_selection", WALL_PRAYER_TIME_SELECTION);
            attach("wall.sentence2", WALL_SENTENCE2);
            attach("wall.sentence3", WALL_SENTENCE3);
            attach("wall.some_walls_open", WALL_SOME_WALLS_OPEN);
            attach("wall.prayer_times", WALL_PRAYER_TIMES);
            attach("wall.my_prayer_time", WALL_MY_PRAYER_TIME);
            attach("wall.are_you_sure", WALL_ARE_YOU_SURE);
            attach("wall.manage_walls", WALL_MANAGE_WALLS);
            attach("wall.add_a_new_wall", WALL_ADD_A_NEW_WALL);
            attach("wall.title", WALL_TITLE);
            attach("wall.you_can_change_this_later", WALL_YOU_CAN_CHANGE_THIS_LATER);

            attach("shared.sunday", SHARED_SUNDAY);
            attach("shared.monday", SHARED_MONDAY);
            attach("shared.tuesday", SHARED_TUESDAY);
            attach("shared.wednesday", SHARED_WEDNESDAY);
            attach("shared.thursday", SHARED_THURSDAY);
            attach("shared.friday", SHARED_FRIDAY);
            attach("shared.saturday", SHARED_SATURDAY);

            attach("forgot_password.instructions", FORGOT_PASSWORD_INSTRUCTIONS);
            attach("forgot_password.notification_subject", FORGOT_PASSWORD_NOTIFICATION_SUBJECT);
            attach("forgot_password.notification_message", FORGOT_PASSWORD_NOTIFICATION_MESSAGE);

            attach("email_all_users.caption", EMAIL_ALL_USERS_CAPTION);

            attach("request.how_inappropriate", REQUEST_HOW_INAPPROPRIATE );
            attach("request.fields_optional", REQUEST_FIELDS_OPTIONAL);
        }

        protected void init()
        {
            string englishPhraseValue = string.Empty;
            string translatedPhraseValue = string.Empty;

            //-------------------------------------------------------------------------
            //-- query the database for all of the phrase values for the current language
            //-------------------------------------------------------------------------
            Phrase queryPhrase = new Phrase();
            queryPhrase["language_id"] = this.id.Value;
            List<Phrase> queryPhraseList = queryPhrase.doQuery<Phrase>("select");

            //-------------------------------------------------------------------------
            //-- map the retrieved phrase values into this object's corresponding properties
            //-------------------------------------------------------------------------
            foreach (Phrase currentPhrase in queryPhraseList)
            {
                string propertyKey = currentPhrase.label.Value;
                string propertyValue = currentPhrase.phrase.Value;
                this[propertyKey] = propertyValue;
            }

            //-------------------------------------------------------------------------
            //-- if the current language is NOT English, then make sure the current  
            //-- language phrase list has values for all of the equivalent loaded English phrases
            //-------------------------------------------------------------------------
            if (this.title != ENGLISH)
            {
                Language englishLanguageObject = getLanguage(ENGLISH);

                //-------------------------------------------------------------------------
                //-- loop through each English label entry...
                //-------------------------------------------------------------------------
                foreach (string englishLabelKey in englishLanguageObject.Keys)
                {
                    //-- KLUDGE:  leveraging the dot separator in the property name to designate it as a phrase label
                    if (englishLabelKey.Contains("."))
                    {
                        //-------------------------------------------------------------------------
                        //-- if the current language phrase list does NOT contain this label, 
                        //-- then query the translator to get this language's version of the English phrase
                        //-------------------------------------------------------------------------
                        if (!this.ContainsKey(englishLabelKey))
                        {
                            englishPhraseValue = (string)englishLanguageObject.getString(englishLabelKey);

                            //-------------------------------------------------------------------------
                            //-- if the English version of the phrase is not blank
                            //-------------------------------------------------------------------------
                            if (!String.IsNullOrEmpty(englishPhraseValue))
                            {
                                translatedPhraseValue = Translator.TranslateText(englishPhraseValue, "en", this.iso);

                                if (String.IsNullOrEmpty(translatedPhraseValue))
                                {
                                    //-------------------------------------------------------------------------
                                    //-- if there was an English phrase but NO translated phrase,
                                    //-- then use the English version of the phrase
                                    //-------------------------------------------------------------------------
                                    translatedPhraseValue = englishPhraseValue;
                                }

                                //-------------------------------------------------------------------------
                                //-- add translated phrase to internal collection
                                //-------------------------------------------------------------------------
                                this[englishLabelKey] = translatedPhraseValue;

                                //-------------------------------------------------------------------------
                                //-- insert the translated phrase into database
                                //-------------------------------------------------------------------------
                                LiftDomain.Phrase newPhrase = new LiftDomain.Phrase();
                                newPhrase.language_id.Value = this.id;
                                newPhrase.label.Value = englishLabelKey;
                                newPhrase.phrase.Value = translatedPhraseValue;
                                newPhrase.doCommand("insert");
                            }
                        }
                    }
                }
            }
        }

        public string phrase(string label)
        {
            string result = "phrase not found";

            string subdomain = Organization.Current.subdomain.Value;

            string orgLabel = label + "." + subdomain;

            if (ContainsKey(orgLabel))
            {
                result = getString(orgLabel);
            }
            else if (ContainsKey(label))
            {
                result = getString(label);
            }

            return result;
        }

        
        public string translate( string english )
        {
            string result = string.Empty;

            try
            {
                result = Translator.TranslateText(english, "en", this.iso);
            }
            catch
            {
                result = english;
            }

            return result;
        }
        

        public static Language Current
        {
            get
            {
                Language lang = null;

                User u = User.Current;

                if (u != null)
                {
                    if (u.id > 0)
                    {
                        lang = getLanguage(u.language_id.Value);
                    }
                }

                if (lang == null)
                {
                    Organization org = Organization.Current;
                    if (org != null)
                    {
                        lang = getLanguage(org.language_id.Value);
                    }
                }

                if (lang == null)
                {
                    lang = getLanguage(ENGLISH);
                }

                return lang;
            }
        }

        public static Language getLanguage(int language_id)
        {
            Language lang = null;
            lock (langSync)
            {
                if (!languagesById.ContainsKey(language_id))
                {
                    //loadAll();

                    //-------------------------------------------------------------------------
                    //-- we really do not want to load _all_ languages with the loadAll() 
                    //-- from above, do we???  trying do just load the requested language (by language ID)...
                    //-------------------------------------------------------------------------
                    Language queryLanguage = new Language();
                    queryLanguage.id.Value = language_id;
                    List<Language> LangList = queryLanguage.doQuery<Language>("select");

                    if (LangList.Count > 0)
                    {
                        Language newLanguage = LangList[0];
                        newLanguage.init();
                        languagesById.Add(newLanguage.id.Value, newLanguage);
                        languagesByTitle.Add(newLanguage.title.Value, newLanguage);
                    }
                }

                lang = (Language)languagesById[language_id];
            }

            return lang;
        }

        public static Language getLanguage(string title)
        {
            Language lang = null;
            lock (langSync)
            {
                if (!languagesByTitle.ContainsKey(title))
                {
                    //loadAll();

                    //-------------------------------------------------------------------------
                    //-- we really do not want to load _all_ languages with the loadAll() 
                    //-- from above, do we???  trying do just load the requested language (by language name)...
                    //-------------------------------------------------------------------------
                    Language queryLanguage = new Language();
                    queryLanguage.title.Value = title;
                    List<Language> LangList = queryLanguage.doQuery<Language>("select");

                    if (LangList.Count > 0)
                    {
                        Language newLanguage = LangList[0];
                        newLanguage.init();
                        languagesById.Add(newLanguage.id.Value, newLanguage);
                        languagesByTitle.Add(newLanguage.title.Value, newLanguage);
                    }
                }
                lang = (Language)languagesByTitle[title];
            }

            return lang;
        }

        protected static void loadAll()
        {
            languagesById.Clear();
            languagesByTitle.Clear();

            Language L = new Language();
            List<Language> LangList = L.doQuery<Language>("select");

            foreach (Language lang in LangList)
            {
                lang.init();
                languagesById.Add(lang.id.Value, lang);
                languagesByTitle.Add(lang.title.Value, lang);
            }
        }

    }
}
