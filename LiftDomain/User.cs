using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Security;

using LiftCommon;

namespace LiftDomain
{
    public enum UserState
    {
        unknown = 0,
        unconfirmed = 1,
        confirmed = 2,
        locked = 3,
        deleted = 4
    }

    public class User : BaseLiftDomain
    {
       

        public  const UserState UNKNOWN = UserState.unknown;
        public  const UserState UNCONFIRMED = UserState.unconfirmed;
        public  const UserState CONFIRMED = UserState.confirmed;
        public  const UserState LOCKED = UserState.locked;
        public  const UserState DELETED = UserState.deleted;

        public StringProperty address = new StringProperty();
        public StringProperty city = new StringProperty();
        public UserTimeProperty created_at = new UserTimeProperty();
        public StringProperty crypted_password = new StringProperty();
        public StringProperty email = new StringProperty();
        public StringProperty first_name = new StringProperty();
        public IntProperty id = new IntProperty();
        public BoolProperty isapproved = new BoolProperty();
        public BoolProperty islockedout = new BoolProperty();
        public IntProperty language_id = new IntProperty();
        public UserTimeProperty last_logged_in_at = new UserTimeProperty();
        public StringProperty last_name = new StringProperty();
        public StringProperty login = new StringProperty();
        public IntProperty login_failure_count = new IntProperty();
        public IntProperty organization_id = new IntProperty();
        public StringProperty password_hash_type = new StringProperty();
        public StringProperty password_salt = new StringProperty();
        public StringProperty phone = new StringProperty();
        public StringProperty postal_code = new StringProperty();
        public IntProperty previous_increment_id = new IntProperty();
        public IntProperty state = new IntProperty();
        public StringProperty state_province = new StringProperty();
        public StringProperty time_zone = new StringProperty();
        public UserTimeProperty updated_at = new UserTimeProperty();
        public UserTimeProperty last_password_changed_date = new UserTimeProperty();
        public UserTimeProperty last_locked_out_date = new UserTimeProperty();
        public UserTimeProperty failed_password_attempt_window_start = new UserTimeProperty();
        public IntProperty failed_password_answer_attempt_count = new IntProperty();
        public UserTimeProperty failed_password_answer_attempt_window_start = new UserTimeProperty();
        public StringProperty password_question = new StringProperty();
        public StringProperty password_answer = new StringProperty();
        public StringProperty comment = new StringProperty();

        public IntProperty pageindex = new IntProperty();
        public IntProperty pagesize = new IntProperty();

        public static User empty = new User();
        protected static Hashtable users = new Hashtable();
        protected static object usersSync = new object();

        protected static object wordSync = new object();
        protected static ArrayList words = null;
        protected static Random randWord = new Random();

             string notify_webmaster_email = @"A new user account has been requested for $(USERNAME).  Please logon to the $(ORGNAME) prayer site and approve or reject the new account.";

            string approved_user_email =
@"Thank you for joining the $(ORGNAME) prayer site. Below please find your username and link to the site. Your password is the same as when you requested the account. You will need to sign in each time you visit in order to begin your Prayer Session. 

Link: http://$(SUBDOMAIN).liftprayer.cc

Username: $(USERNAME) 

Please log in and visit the WATCHMEN WALL to select the hour you wish to commit to pray.  Then, begin praying by clicking on the Prayer Request Link at the top of the page. 

If you have any questions, please contact $(WEBMASTER).";

            string rejected_user_email = 
@"Thank you for your interest in the $(ORGNAME) prayer site.  Unfortunately we are unable to honor your request at this time.  
            If you have any questions or concerns about this decision, contact $(WEBMASTER). ";

        public User()
        {
            BaseTable = "users";
            AutoIdentity = true;
            PrimaryKey = "id";

            attach("address", address);
            attach("city", city);
            attach("created_at", created_at);
            attach("crypted_password", crypted_password);
            attach("email", email);
            attach("first_name", first_name);
            attach("id", id);
            attach("isapproved", isapproved);
            attach("islockedout", islockedout);
            attach("language_id", language_id);
            attach("last_logged_in_at", last_logged_in_at);
            attach("last_name", last_name);
            attach("login", login);
            attach("login_failure_count", login_failure_count);
            attach("organization_id", organization_id);
            attach("password_hash_type", password_hash_type);
            attach("password_salt", password_salt);
            attach("phone", phone);
            attach("postal_code", postal_code);
            attach("previous_increment_id", previous_increment_id);
            attach("state", state);
            attach("state_province", state_province);
            attach("time_zone", time_zone);
            attach("updated_at", updated_at);
            attach("last_password_changed_date", last_password_changed_date);
            attach("last_locked_out_date", last_locked_out_date);
            attach("failed_password_attempt_window_start", failed_password_attempt_window_start);
            attach("failed_password_answer_attempt_count", failed_password_answer_attempt_count);
            attach("failed_password_answer_attempt_window_start", failed_password_answer_attempt_window_start);
            attach("password_question", password_question);
            attach("password_answer", password_answer);
            attach("comment", comment);

            attach("pageindex", pageindex);
            attach("pagesize", pagesize);

            orgProperty = organization_id;


       


        }

        /// <summary>
        /// This property returns the current user based on the http context.  If there is no current user,
        /// then this property returns an "empty" object so that we do not have to if/else in situations where there
        /// can be anonymous users.
        /// </summary>
        public static User Current
        {
            get
            {
                User result = empty;

                HttpContext ctx = HttpContext.Current;

                if (ctx != null)
                {
                    string login = HttpContext.Current.User.Identity.Name;
                    bool authenticated = HttpContext.Current.User.Identity.IsAuthenticated;

                    if (authenticated)
                    {
                        lock (usersSync)
                        {
                            if (users.ContainsKey(login))
                            {
                                result = (User)users[login];
                            }
                            else
                            {
                                User u = new User();

                                if (login.Contains("@"))
                                {
                                    u.email.Value = login;
                                }
                                else
                                {
                                    u.login.Value = login;
                                }

                                u = u.doSingleObjectQuery<User>("select");
                                if (u != null)
                                {
                                    users[login] = u;
                                    result = u;
                                }



                            }
                        }
                    }
                }
                return result;
            }
        }

        public string FullName
        {
            get
            {
                string n = first_name.Value;
                n += " ";
                n += last_name.Value;
                return n;
            }
        }

        public static bool IsLoggedIn
        {
            get
            {
                bool result = false;

                if (HttpContext.Current.User != null)
                {
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        result = true;
                    }
                }
                return result;
            }
        }

        public bool IsInRole(string rolename)
        {
            return HttpContext.Current.User.IsInRole(rolename);
        }

        public override string ToString()
        {
            return first_name + " " + last_name;
        }

        public static string hash(string password, string salt)
        {
            return hash(password + salt);
        }

        public static string hash(string passwordAndSalt)
        {
            string result = string.Empty;

            MD5 hash = MD5CryptoServiceProvider.Create();
            result = Utilities.ByteToHex(hash.ComputeHash(Encoding.ASCII.GetBytes(passwordAndSalt)));

            return result;
        }

        public static string generateRandomSalt()
        {
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();

            byte[] saltInBytes = new byte[10];

            crypto.GetBytes(saltInBytes);

            return Convert.ToBase64String(saltInBytes).Substring(0, 10);
        }

        public bool canSeePrivateRequests
        {
            get 
            {
                Organization org = Organization.Current;
                return (IsInRole(Role.MODERATOR) || org.default_signup_mode.Value == (int)UserSignupMode.user_requires_approval ); 
            }
        }

        public bool canApproveRequests
        {
            get { return IsInRole(Role.MODERATOR); }
        }

        public bool canEditRequest(object request_user_id_obj)
        {
            bool result = false;

            if (IsInRole(Role.MODERATOR))
            {
                result = true;
            }
            else if (request_user_id_obj != null)
            {
                if (!request_user_id_obj.GetType().Equals(typeof(System.DBNull)))
                {
                    int request_user_id = Convert.ToInt32(request_user_id_obj);
                    if ((request_user_id == this.id.Value) && (request_user_id != 0))
                        result = true;
                }
            }

            return result;
        }

        public bool isAdmin
        {
            get
            {
                bool result = false;

                if (IsInRole(Role.MODERATOR) ||
                    IsInRole(Role.WALL_LEADER) ||
                    IsInRole(Role.ORG_ADMIN) ||
                    IsInRole(Role.SYS_ADMIN))
                {
                    result = true;
                }

                return result;
            }
        }

        public bool isSysAdmin
        {
            get
            {
                bool result = IsInRole(Role.SYS_ADMIN);
                return result;
            }
        }

        public long create_account()
        {
            long userId = doCommand("save");
            long result = 0;


            if (userId > 0)
            {
                this.id.Value = (int) userId;

                result = (long)Organization.Current.default_signup_mode.Value;

                if (result == (long)UserSignupMode.user_requires_approval)
                {
                    notify_webmaster_new_account();
                }

            }
            return userId;
        }

        protected void notify_webmaster_new_account()
        {
            User u = new User();
            u.id.Value = this.id.Value;
            u = (User)u.doSingleObjectQuery(typeof(User), "select");

            Organization o = Organization.Current;

            OrgEmail oe = new OrgEmail();
            oe.organization_id.Value = o.id.Value;                
            oe = oe.doSingleObjectQuery<LiftDomain.OrgEmail>("select");
                       

            string notification_msg = notify_webmaster_email;
            notification_msg = notification_msg.Replace("$(ORGNAME)", o.title);
            notification_msg = notification_msg.Replace("$(USERNAME)", u.email.Value);
            
            Email email = new Email();
            email.subject = "New Account Request";
            email.Body = notification_msg;
            email.addTo(oe.webmaster_email_to.Value);
            email.from = Organization.Current.getFromEmail();
            email.send();
        }

        public long update_status()
        {
            long result = doCommand("update");

            if (result > 0)
            {
                result = (long)Organization.Current.default_signup_mode.Value;

                if (result == (long)UserSignupMode.user_requires_approval)
                {
                    if (state.Value == (int)User.CONFIRMED)
                    {
                        notify_user_account_status(approved_user_email);
                    }
                    else if (state.Value == (int)User.LOCKED)
                    {
                        notify_user_account_status(rejected_user_email);
                    }
                }

            }
            return result;
        }

        protected void notify_user_account_status(string msgTemplate)
        {
            User u = new User();
            u.id.Value = this.id.Value;
            u = (User) u.doSingleObjectQuery(typeof(User), "select");

            Organization o = Organization.Current;

            OrgEmail oe = new OrgEmail();
            oe.organization_id.Value = o.id.Value;
            oe = oe.doSingleObjectQuery<OrgEmail>("select");

            string notificationMsg = msgTemplate;
            notificationMsg = notificationMsg.Replace("$(ORGNAME)", o.title.Value);
            notificationMsg = notificationMsg.Replace("$(USERNAME)", u.email.Value);
            notificationMsg = notificationMsg.Replace("$(WEBMASTER)", oe.webmaster_email_to.Value);
            notificationMsg = notificationMsg.Replace("$(SUBDOMAIN)", o.subdomain.Value);


            Email email = new Email();
            email.subject = "New Account Request";
            email.Body = notificationMsg;
            email.addTo(u.email.Value);
            email.from = Organization.Current.getFromEmail();
            email.send();
        }

        public long save_current()
        {
            long result = doCommand("save");
            reloadCurrent();
            return result;
        }

        protected static void reloadCurrent()
        {
            User u = Current;
            lock (usersSync)
            {
                User u2 = new User();

                u2.id.Value = u.id.Value;

                u2 = u2.doSingleObjectQuery<User>("select");

                if (u2 != null)
                {
                    string email = u2.email.Value;
                    users[email] = u2;
                }

            }
        }

        protected static void populateWords()
        {
            if (words != null) return;

            #region word list
            words = new ArrayList();
            words.Add("absent");
            words.Add("accept");
            words.Add("acts");
            words.Add("actual");
            words.Add("adding");
            words.Add("admit");
            words.Add("advice");
            words.Add("affect");
            words.Add("affirm");
            words.Add("after");
            words.Add("agree");
            words.Add("alone");
            words.Add("also");
            words.Add("alter");
            words.Add("among");
            words.Add("armies");
            words.Add("arming");
            words.Add("army");
            words.Add("arts");
            words.Add("ballot");
            words.Add("become");
            words.Add("been");
            words.Add("before");
            words.Add("being");
            words.Add("best");
            words.Add("bill");
            words.Add("bills");
            words.Add("blair");
            words.Add("born");
            words.Add("borrow");
            words.Add("both");
            words.Add("bound");
            words.Add("branch");
            words.Add("broom");
            words.Add("butler");
            words.Add("call");
            words.Add("called");
            words.Add("cannot");
            words.Add("care");
            words.Add("case");
            words.Add("cases");
            words.Add("census");
            words.Add("chief");
            words.Add("choice");
            words.Add("chosen");
            words.Add("choose");
            words.Add("civil");
            words.Add("claim");
            words.Add("claims");
            words.Add("class");
            words.Add("clear");
            words.Add("clymer");
            words.Add("coin");
            words.Add("common");
            words.Add("compel");
            words.Add("concur");
            words.Add("corpus");
            words.Add("court");
            words.Add("courts");
            words.Add("credit");
            words.Add("daniel");
            words.Add("david");
            words.Add("days");
            words.Add("dayton");
            words.Add("death");
            words.Add("debate");
            words.Add("debts");
            words.Add("deem");
            words.Add("defend");
            words.Add("define");
            words.Add("delay");
            words.Add("demand");
            words.Add("deputy");
            words.Add("desire");
            words.Add("direct");
            words.Add("dobbs");
            words.Add("dock");
            words.Add("done");
            words.Add("drawn");
            words.Add("during");
            words.Add("duties");
            words.Add("duty");
            words.Add("each");
            words.Add("effect");
            words.Add("eight");
            words.Add("eighty");
            words.Add("either");
            words.Add("emit");
            words.Add("engage");
            words.Add("enjoy");
            words.Add("enter");
            words.Add("equal");
            words.Add("equity");
            words.Add("ever");
            words.Add("every");
            words.Add("exceed");
            words.Add("except");
            words.Add("expel");
            words.Add("expire");
            words.Add("extend");
            words.Add("fact");
            words.Add("facto");
            words.Add("faith");
            words.Add("fill");
            words.Add("first");
            words.Add("five");
            words.Add("fled");
            words.Add("flee");
            words.Add("forces");
            words.Add("form");
            words.Add("formed");
            words.Add("forth");
            words.Add("forts");
            words.Add("found");
            words.Add("four");
            words.Add("fourth");
            words.Add("free");
            words.Add("from");
            words.Add("full");
            words.Add("george");
            words.Add("gilman");
            words.Add("give");
            words.Add("given");
            words.Add("giving");
            words.Add("going");
            words.Add("gold");
            words.Add("good");
            words.Add("gorham");
            words.Add("grant");
            words.Add("grants");
            words.Add("habeas");
            words.Add("happen");
            words.Add("having");
            words.Add("heads");
            words.Add("held");
            words.Add("herein");
            words.Add("high");
            words.Add("hold");
            words.Add("honor");
            words.Add("house");
            words.Add("houses");
            words.Add("hugh");
            words.Add("insure");
            words.Add("into");
            words.Add("island");
            words.Add("issue");
            words.Add("jacob");
            words.Add("james");
            words.Add("jared");
            words.Add("jersey");
            words.Add("john");
            words.Add("judge");
            words.Add("judges");
            words.Add("jury");
            words.Add("keep");
            words.Add("kind");
            words.Add("king");
            words.Add("labour");
            words.Add("laid");
            words.Add("land");
            words.Add("lands");
            words.Add("large");
            words.Add("laws");
            words.Add("least");
            words.Add("life");
            words.Add("like");
            words.Add("list");
            words.Add("longer");
            words.Add("lord");
            words.Add("made");
            words.Add("make");
            words.Add("manner");
            words.Add("marque");
            words.Add("meet");
            words.Add("member");
            words.Add("miles");
            words.Add("mode");
            words.Add("monday");
            words.Add("money");
            words.Add("more");
            words.Add("morris");
            words.Add("most");
            words.Add("naval");
            words.Add("navy");
            words.Add("nays");
            words.Add("next");
            words.Add("nine");
            words.Add("ninth");
            words.Add("north");
            words.Add("number");
            words.Add("oath");
            words.Add("office");
            words.Add("once");
            words.Add("only");
            words.Add("open");
            words.Add("ordain");
            words.Add("order");
            words.Add("other");
            words.Add("over");
            words.Add("overt");
            words.Add("paid");
            words.Add("part");
            words.Add("parts");
            words.Add("party");
            words.Add("pass");
            words.Add("passed");
            words.Add("peace");
            words.Add("people");
            words.Add("period");
            words.Add("person");
            words.Add("pierce");
            words.Add("place");
            words.Add("places");
            words.Add("ports");
            words.Add("post");
            words.Add("power");
            words.Add("powers");
            words.Add("prince");
            words.Add("prior");
            words.Add("profit");
            words.Add("proper");
            words.Add("proved");
            words.Add("public");
            words.Add("punish");
            words.Add("quorum");
            words.Add("raise");
            words.Add("read");
            words.Add("recess");
            words.Add("remain");
            words.Add("repel");
            words.Add("return");
            words.Add("rhode");
            words.Add("right");
            words.Add("roads");
            words.Add("robert");
            words.Add("roger");
            words.Add("rufus");
            words.Add("rule");
            words.Add("rules");
            words.Add("safety");
            words.Add("said");
            words.Add("saint");
            words.Add("same");
            words.Add("samuel");
            words.Add("sealed");
            words.Add("seas");
            words.Add("seat");
            words.Add("seats");
            words.Add("second");
            words.Add("secure");
            words.Add("senate");
            words.Add("sent");
            words.Add("seven");
            words.Add("ships");
            words.Add("should");
            words.Add("sign");
            words.Add("signed");
            words.Add("silver");
            words.Add("sixth");
            words.Add("soul");
            words.Add("south");
            words.Add("speech");
            words.Add("square");
            words.Add("stated");
            words.Add("swear");
            words.Add("take");
            words.Add("taken");
            words.Add("taxed");
            words.Add("taxes");
            words.Add("tender");
            words.Add("term");
            words.Add("test");
            words.Add("than");
            words.Add("that");
            words.Add("their");
            words.Add("them");
            words.Add("then");
            words.Add("there");
            words.Add("they");
            words.Add("thing");
            words.Add("think");
            words.Add("third");
            words.Add("thirty");
            words.Add("this");
            words.Add("thomas");
            words.Add("those");
            words.Add("three");
            words.Add("time");
            words.Add("times");
            words.Add("title");
            words.Add("treaty");
            words.Add("trial");
            words.Add("tribes");
            words.Add("tried");
            words.Add("troops");
            words.Add("trust");
            words.Add("twenty");
            words.Add("under");
            words.Add("union");
            words.Add("unless");
            words.Add("until");
            words.Add("upon");
            words.Add("useful");
            words.Add("valid");
            words.Add("value");
            words.Add("vest");
            words.Add("vested");
            words.Add("vice");
            words.Add("vote");
            words.Add("voted");
            words.Add("votes");
            words.Add("voting");
            words.Add("water");
            words.Add("well");
            words.Add("what");
            words.Add("when");
            words.Add("where");
            words.Add("whole");
            words.Add("whom");
            words.Add("whose");
            words.Add("will");
            words.Add("wilson");
            words.Add("with");
            words.Add("within");
            words.Add("work");
            words.Add("writ");
            words.Add("writs");
            words.Add("yards");
            words.Add("year");
            words.Add("years");
            words.Add("yeas");
            words.Add("york");
            #endregion

            randWord = new Random(randWord.Next());

        }

        public static string generatePassword()
        {
            string result = string.Empty;

            lock (wordSync)
            {
                populateWords();

                int offset = randWord.Next(words.Count);

                result = words[offset].ToString();

                result += randWord.Next(1000).ToString();

            }


            return result;
        }

        public static bool checkEmailExists(string email)
        {
            bool result = false;
            DataSet userListSet;

            try
            {
                LiftDomain.User thisUserList = new LiftDomain.User();
                thisUserList["search"] = email;
                userListSet = thisUserList.doQuery("SearchUsersByEmail");
                if (userListSet != null)
                {
                    if (userListSet.Tables[0].Rows.Count > 0)
                    {
                        result = true;
                    }
                }
            }
            catch
            { }

            return result;
        }

        public static bool checkUsernameExists(string username)
        {
            bool result = false;
            DataSet userListSet;

            try
            {
                LiftDomain.User thisUserList = new LiftDomain.User();
                thisUserList["search"] = username;
                userListSet = thisUserList.doQuery("SearchUsersByUsername");
                if (userListSet != null)
                {
                    if (userListSet.Tables[0].Rows.Count > 0)
                    {
                        result = true;
                    }
                }
            }
            catch
            { }

            return result;

        }

        public static string getUserStatusDescription(int userState)
        {
            string userStatus = string.Empty;

            switch (userState)
            {
                case (int)User.UNCONFIRMED:
                    userStatus = LiftDomain.Language.Current.USER_STATUS_UNCONFIRMED.Value;
                    break;

                case (int)User.CONFIRMED:
                    userStatus = LiftDomain.Language.Current.USER_STATUS_CONFIRMED.Value;
                    break;

                case (int)User.LOCKED:
                    userStatus = LiftDomain.Language.Current.USER_STATUS_LOCKED.Value;
                    break;

                case (int)User.DELETED:
                    userStatus = LiftDomain.Language.Current.USER_STATUS_DELETED.Value;
                    break;

                default:
                    userStatus = LiftDomain.Language.Current.USER_STATUS_UNKNOWN.Value + " (" + userState.ToString() + ")";
                    break;
            }
            return userStatus;
        }
       

    }
}
