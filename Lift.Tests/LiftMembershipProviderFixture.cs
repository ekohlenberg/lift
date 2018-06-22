using liftprayer;

using System.Collections.Specialized;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lift.Tests
{
    
    [TestClass()]
    public class LiftMembershipProviderFixture : FixtureBase
    {

        #region -- Declarations --

        private LiftMembershipProvider _provider;

        #endregion

        #region -- Setup / Teardown --

        [TestInitialize()]
        public void TestInitialize()
        {
            base.Initialize();

            HttpContext.Current = (new MockHttpContext(false)).Context;
            HttpContext.Current.Session["org"] = "sugarcreek";

            _provider = new LiftMembershipProvider();

            MembershipSection section = (MembershipSection)ConfigurationManager.GetSection("system.web/membership");
            ProviderSettingsCollection settings = section.Providers;
            NameValueCollection membershipParams = settings[section.DefaultProvider].Parameters;
            _provider.Initialize(section.DefaultProvider, membershipParams);

        }

        #endregion

        #region -- Test Methods --

        [TestMethod()]
        public void ApplicationName()
        {
            Assert.AreEqual("LiftPrayer", _provider.ApplicationName);
        }

        [TestMethod()]
        public void ChangePassword()
        {
            bool result1 = _provider.ChangePassword("jeffreyprice", "pray4me", "prayhard");
            bool result2 = _provider.ChangePassword("jeffreyprice", "prayhard", "pray4me");

            Assert.IsTrue(result1 || result2);
        }

        [TestMethod()]
        public void CreateUser()
        {
            MembershipCreateStatus status;
            _provider.CreateUser("test1", "test1", "test@test.com", "who am i?", "me", true, null, out status);
            Assert.AreEqual(MembershipCreateStatus.Success, status);
        }

        [TestMethod()]
        public void CreateUserCancel()
        {
            _provider.ValidatingPassword += new MembershipValidatePasswordEventHandler(CreateUserCancel_ValidatingPassword);
            MembershipCreateStatus status;
            _provider.CreateUser("test1", "test1", "test@test.com", "who am i?", "me", true, null, out status);
            Assert.AreEqual(MembershipCreateStatus.InvalidPassword, status);
            _provider.ValidatingPassword -= new MembershipValidatePasswordEventHandler(CreateUserCancel_ValidatingPassword);
        }

        private void CreateUserCancel_ValidatingPassword(object sender, ValidatePasswordEventArgs e)
        {
            e.Cancel = true;
        }

        [TestMethod()]
        public void CreateUserDuplicateEmail()
        {
            MembershipCreateStatus status;
            _provider.CreateUser("test1", "test1", "jeff@thepricefam.org", "who am i?", "me", true, null, out status);
            Assert.AreEqual(MembershipCreateStatus.DuplicateEmail, status);
        }

        [TestMethod()]
        public void GetAllUsers()
        {

            int totalRecords = 0;
            MembershipUserCollection users = _provider.GetAllUsers(1, 10, out totalRecords);
            
            Assert.IsTrue(users.Count <= 10);   // crude test of paging
            Assert.IsTrue(totalRecords > 1);   // crude test of something/anything being returned 

        }

        [TestMethod()]
        public void GetAllUsersAspNetCongifuration()
        {

            HttpContext.Current.Session.Remove("org");

            int totalRecords = 0;
            MembershipUserCollection users = _provider.GetAllUsers(0, int.MaxValue, out totalRecords);

            Assert.AreEqual(users.Count, totalRecords);

        }

        [TestMethod()]
        [Ignore()]  // TODO: Re-enable test when login can be tested prior to this test
        public void GetNumberOfUsersOnline()
        {
            int count = _provider.GetNumberOfUsersOnline();
            Assert.AreEqual(0, count);
        }

        [TestMethod()]
        public void GetUserByID()
        {
            MembershipUser user = _provider.GetUser(415, false);
            Assert.IsNotNull(user);
            Assert.AreEqual("jeff@thepricefam.org", user.Email);
        }

        [TestMethod()]
        public void GetUserByLogin()
        {
            MembershipUser user = _provider.GetUser("jeffreyprice", false);
            Assert.IsNotNull(user);
            Assert.AreEqual("jeff@thepricefam.org", user.Email);
        }

        [TestMethod()]
        public void GetUserNameByEmail()
        {
            string userName = _provider.GetUserNameByEmail("jeff@thepricefam.org");
            Assert.AreEqual("jeffreyprice", userName);
        }

        [TestMethod()]
        public void EnablePasswordReset()
        {
            Assert.IsTrue(_provider.EnablePasswordReset);
        }

        [TestMethod()]
        public void EnablePasswordRetrieval()
        {
            Assert.IsTrue(_provider.EnablePasswordRetrieval);
        }

        [TestMethod()]
        public void FindUsersByEmail()
        {

            int totalRecords = 0;
            MembershipUserCollection users = _provider.FindUsersByEmail("jeff@thepricefam.org", 1, 10, out totalRecords);

            Assert.IsTrue(users.Count <= 10);   // crude test of paging
            Assert.IsTrue(totalRecords > 1);    // crude test of something/anything being returned 

        }

        [TestMethod()]
        public void FindUsersByName()
        {

            int totalRecords = 0;
            MembershipUserCollection users = _provider.FindUsersByName("jeffreyprice", 1, 10, out totalRecords);

            Assert.IsTrue(users.Count <= 10);   // crude test of paging
            Assert.IsTrue(totalRecords > 1);    // crude test of something/anything being returned 

        }

        [TestMethod()]
        public void MaxInvalidPasswordAttempts()
        {
            Assert.AreEqual(5, _provider.MaxInvalidPasswordAttempts);
        }

        [TestMethod()]
        public void MinRequiredNonAlphanumericCharacters()
        {
            Assert.AreEqual(1, _provider.MinRequiredNonAlphanumericCharacters);
        }

        [TestMethod()]
        public void MinRequiredPasswordLength()
        {
            Assert.AreEqual(7, _provider.MinRequiredPasswordLength);
        }

        [TestMethod()]
        public void PasswordAttemptWindow()
        {
            Assert.AreEqual(10, _provider.PasswordAttemptWindow);
        }

        [TestMethod()]
        public void PasswordFormat()
        {
            Assert.AreEqual(MembershipPasswordFormat.Hashed, _provider.PasswordFormat);
        }

        [TestMethod()]
        public void RequiresUniqueEmail()
        {
            Assert.IsTrue(_provider.RequiresUniqueEmail);
        }

        [TestMethod()]
        public void RequiresQuestionAndAnswer()
        {
            Assert.IsFalse(_provider.RequiresQuestionAndAnswer);
        }

        [TestMethod()]
        public void ValidateUser()
        {
            bool result1 = _provider.ValidateUser("jeffreyprice", "pray4me");
            bool result2 = _provider.ValidateUser("jeffreyprice", "prayhard");

            Assert.IsTrue(result1 || result2);
        }

        #endregion

    }

}
