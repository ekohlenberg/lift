using LiftDomain;

using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Security.Cryptography;
using System.Data;
using System.Text;
using System.Web.Hosting;
using System.Web.Security;

namespace liftprayer
{
    
    public sealed class LiftMembershipProvider : MembershipProvider
    {

        #region -- Declarations --

        private bool _enablePasswordReset = false;
        private bool _enablePasswordRetrieval = false;
        private bool _requiresUniqueEmail = false;
        private bool _requiresQuestionAndAnswer = false;

        private int _maxInvalidPasswordAttempts = 0;
        private int _minRequiredNonAlphanumericCharacters = 0;
        private int _minRequiredPasswordLength = 0;
        private int _passwordAttemptWindow = 0;

        private MembershipPasswordFormat _passwordFormat = MembershipPasswordFormat.Hashed;

        private string _applicationName = string.Empty;
        private string _passwordStrengthRegularExpression = string.Empty;

        #endregion

        #region -- Properties --

        public override string ApplicationName
        {
            get
            {
                return _applicationName;
            }
            set
            {
                _applicationName = value;
            }
        }

        public override bool EnablePasswordReset
        {
            get
            {
                return _enablePasswordReset;
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                return _enablePasswordRetrieval;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get 
            {
                return _maxInvalidPasswordAttempts;
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get 
            {
                return _minRequiredNonAlphanumericCharacters;
            }
        }

        public override int MinRequiredPasswordLength
        {
            get 
            {
                return _minRequiredPasswordLength;
            }
        }

        public override int PasswordAttemptWindow
        {
            get 
            {
                return _passwordAttemptWindow;
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get 
            {
                return _passwordFormat;
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get 
            {
                return _passwordStrengthRegularExpression;
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get 
            {
                return _requiresQuestionAndAnswer;
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                return _requiresUniqueEmail;
            }
        }

        #endregion

        #region -- Methods --

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {

            if (!ValidateUser(username, oldPassword))
            {
                return false;
            }

            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, newPassword, true);

            OnValidatingPassword(args);

            if (args.Cancel)
            {
                if (args.FailureInformation != null)
                {
                    throw args.FailureInformation;
                }
                else
                {
                    throw new MembershipPasswordException("Change password canceled due to new password validation failure.");
                }
            }

            User user = new User();
            user["login"] = username;
            user = user.doSingleObjectQuery<User>("select");

            user.crypted_password.Value = EncodePassword(newPassword + user.password_salt.Value);
            //TODO: ???Update LastPasswordChangeDate

            long rowsAffected = user.doCommand("update");

            if (rowsAffected > 0)
            {
                return true;
            }

            return false;

        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {

            MembershipUserCollection result = new MembershipUserCollection();

            User user = new User();
            user["email"] = emailToMatch;
            user["pageindex"] = pageIndex;
            user["pagesize"] = pageSize;
            DataSet ds = user.doQuery("FindUsersByEmail");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string login = dr["login"].ToString();
                string id = dr["id"].ToString();
                string email = dr["email"].ToString();
                DateTime lastLogin = DateTime.Parse(dr["last_logged_in_at"].ToString());
                DateTime created = DateTime.Parse(dr["created_at"].ToString());
                DateTime updated = DateTime.Parse(dr["updated_at"].ToString());

                MembershipUser mu = new MembershipUser("LiftMembershipProvider", login, id, email, string.Empty, string.Empty, true, false, created, lastLogin, updated, DateTime.MinValue, DateTime.MinValue);
                result.Add(mu);
            }

            totalRecords = (int)ds.Tables[1].Rows[0][0];

            return result;

        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {

            MembershipUserCollection result = new MembershipUserCollection();

            User user = new User();
            user["login"] = usernameToMatch;
            user["pageindex"] = pageIndex;
            user["pagesize"] = pageSize;
            DataSet ds = user.doQuery("FindUsersByName");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string login = dr["login"].ToString();
                string id = dr["id"].ToString();
                string email = dr["email"].ToString();
                DateTime lastLogin = DateTime.Parse(dr["last_logged_in_at"].ToString());
                DateTime created = DateTime.Parse(dr["created_at"].ToString());
                DateTime updated = DateTime.Parse(dr["updated_at"].ToString());

                MembershipUser mu = new MembershipUser("LiftMembershipProvider", login, id, email, string.Empty, string.Empty, true, false, created, lastLogin, updated, DateTime.MinValue, DateTime.MinValue);
                result.Add(mu);
            }

            totalRecords = (int)ds.Tables[1].Rows[0][0];

            return result;

        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {

            MembershipUserCollection result = new MembershipUserCollection();
            
            User user = new User();
            user.pageindex.Value = Math.Max(pageIndex, 1);
            user.pagesize.Value = Math.Max(pageSize, 1);
            DataSet ds = user.doQuery("GetAllUsers");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string login = dr["login"].ToString();
                string id = dr["id"].ToString();
                string email = dr["email"].ToString();
                DateTime lastLogin = DateTime.Parse(dr["last_logged_in_at"].ToString());
                DateTime created = DateTime.Parse(dr["created_at"].ToString());
                DateTime updated = DateTime.Parse(dr["updated_at"].ToString());

                MembershipUser mu = new MembershipUser("LiftMembershipProvider", login, id, email, string.Empty, string.Empty, true, false, created, lastLogin, updated, DateTime.MinValue, DateTime.MinValue);
                result.Add(mu);
            }

            totalRecords = (int) ds.Tables[1].Rows[0][0];
            
            return result;

        }

        public override int GetNumberOfUsersOnline()
        {

            int result = 0;
            
            TimeSpan onlineSpan = new TimeSpan(0, Membership.UserIsOnlineTimeWindow, 0);
            DateTime compareTime = DateTime.Now.ToUniversalTime().Subtract(onlineSpan);

            User user = new User();
            user["last_logged_in_at"] = compareTime;
            DataSet ds = user.doQuery("GetNumberOfUsersOnline");

            result = (int)ds.Tables[0].Rows[0][0];

            return result;

        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {

            MembershipUser result;

            User user = new User();
            user["login"] = username;
            user = (User) user.doSingleObjectQuery<User>("select");

            result = new MembershipUser("LiftMembershipProvider", user.login, user.id, user.email, string.Empty, string.Empty, true, false, user.created_at, user.last_logged_in_at, user.updated_at, DateTime.MinValue, DateTime.MinValue);

            return result;

        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {

            MembershipUser result;

            User user = new User();
            user["id"] = providerUserKey;
            user = (User)user.doSingleObjectQuery<User>("select");

            result = new MembershipUser("LiftMembershipProvider", user.login, user.id, user.email, string.Empty, string.Empty, true, false, user.created_at, user.last_logged_in_at, user.updated_at, DateTime.MinValue, DateTime.MinValue);

            return result;

        }

        public override string GetUserNameByEmail(string email)
        {
            
            string result = string.Empty;

            User user = new User();
            user["email"] = email;
            List<User> users = user.doQuery<User>("select");
            result = users[0].login;

            return result;

        }

        public override void Initialize(string name, NameValueCollection config)
        {

            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            if (name == null || name.Length == 0)
            {
                name = "LiftMembershipProvider";
            }

            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Lift Membership Provider");
            }

            // initialize the abstract base class
            base.Initialize(name, config);

            _applicationName = GetConfigValue(config["applicationName"], HostingEnvironment.ApplicationVirtualPath);
            _enablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
            _enablePasswordRetrieval = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "true"));
            _maxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            _minRequiredNonAlphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredNonAlphanumericCharacters"], "1"));
            _minRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "7"));
            _passwordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
            _passwordStrengthRegularExpression = GetConfigValue(config["passwordStrengthRegularExpression"], string.Empty);
            _requiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "true"));
            _requiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "false"));
            
            string temp_format = config["passwordFormat"];
            if (temp_format == null)
            {
                temp_format = "hashed";
            }

            switch (temp_format.ToLower())
            {
                case "hashed":
                    _passwordFormat = MembershipPasswordFormat.Hashed;
                    break;
                case "encrypted":
                    _passwordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "clear":
                    _passwordFormat = MembershipPasswordFormat.Clear;
                    break;
                default:
                    throw new ProviderException("Password format not supported.");
            }

        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {

            bool result = false;

            User user = new User();
            user.login.Value = username;
            user.islockedout.Value = false;
            
            UserState state =  User.UNCONFIRMED;
            string pwd = string.Empty;

            ArrayList users = user.doQueryObjects(typeof(LiftDomain.User), "select");

            if (users.Count == 0)
            {
                user = new User();
                user.email.Value = username;
                user.islockedout.Value = false;
                user = user.doSingleObjectQuery<User>("select");
            }
            else
            {
                user = (LiftDomain.User)users[0];
            }


            if (user != null)
            {
                pwd = user.crypted_password.Value;
                state = (UserState) user.state.Value;
            }
            else
            {
                return false;
            }

            if (CheckPassword(password + user.password_salt, pwd))
            {
                if (state == User.CONFIRMED)
                {
                    result = true;
                    User updatedUser = new User();
                    updatedUser.id.Value = user.id.Value;
                    updatedUser.last_logged_in_at.Value = DateTime.Now.ToUniversalTime();
                    updatedUser.doCommand("update");

                }
            }
            else
            {
                //TODO: ???UpdateFailureCount(userName, "result");
            }

            return result;

        }

        #endregion

        #region -- Helper Methods --


        /// <summary>
        /// Compares result values based on the MembershipPasswordFormat.
        /// </summary>
        private bool CheckPassword(string password, string dbpassword)
        {

            string pass1 = password;
            string pass2 = dbpassword;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Encrypted:
                    pass2 = UnEncodePassword(dbpassword);
                    break;
                case MembershipPasswordFormat.Hashed:
                    pass1 = EncodePassword(password);
                    break;
                default:
                    break;
            }

            if (pass1 == pass2)
            {
                return true;
            }

            return false;

        }

        /// <summary>
        /// Encrypts, Hashes, or leaves the result clear based on the PasswordFormat.
        /// </summary>
        private string EncodePassword(string password)
        {
            string result = password;

            switch (PasswordFormat)
            {
                
                case MembershipPasswordFormat.Clear:
                    break;

                case MembershipPasswordFormat.Encrypted:
                    result = LiftCommon.Utilities.ByteToHex(EncryptPassword(Encoding.ASCII.GetBytes(password)));
                    break;

                case MembershipPasswordFormat.Hashed:
                    result = LiftDomain.User.hash(password);
                    break;

                default:
                    throw new ProviderException("Unsupported result format.");

            }

            return result;

        }

        /// <summary>
        /// Decrypts or leaves the result clear based on the PasswordFormat.
        /// </summary>
        private string UnEncodePassword(string encodedPassword)
        {
            
            string result = encodedPassword;

            switch (PasswordFormat)
            {
                
                case MembershipPasswordFormat.Clear:
                    break;

                case MembershipPasswordFormat.Encrypted:
                    result = Encoding.Unicode.GetString(DecryptPassword(Convert.FromBase64String(result)));
                    break;
                
                case MembershipPasswordFormat.Hashed:
                    throw new ProviderException("Cannot unencode a hashed result.");

                default:
                    throw new ProviderException("Unsupported result format.");

            }

            return result;

        }

        /// <summary>
        /// A helper function to retrieve config values from the configuration file.
        /// </summary>
        private string GetConfigValue(string configValue, string defaultValue)
        {

            string result = defaultValue;

            if (!string.IsNullOrEmpty(configValue))
            {
                result = configValue;
            }

            return result;

        }

        /// <summary>
        /// Converts a hexadecimal string to a byte array. Used to convert encryption
        /// key values from the configuration.
        /// </summary>
        private byte[] HexToByte(string hexString)
        {

            byte[] result = new byte[hexString.Length / 2];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return result;
        }

        
        #endregion

    } // class LiftMembershipProvider

} // namespace liftprayer
