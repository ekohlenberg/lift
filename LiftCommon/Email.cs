using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;

namespace LiftCommon
{
    public class Email
    {
        /// <summary>
        /// Regular expression pattern for validating e-mail addresses.
        /// </summary>
        private const string PATTERN_EMAIL_ADDRESS = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
           + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
             + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
             + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

        protected SmtpClient smtpClient;
        public string server;
        protected string username;
        public string password;
        public int port;
        public string from;
        public string subject;
        protected ArrayList toRecipients;
        protected StringBuilder body;

        public Email()
        {
            smtpClient = new SmtpClient();
            server = ConfigReader.getString("smtp_server", ""); // smtp_server;
            username = ConfigReader.getString("smtp_username", ""); // smtp_username;
            password = ConfigReader.getString("smtp_password", ""); // smtp_password;
            port = ConfigReader.getInt("smtp_port", 25); // smtp_port;

            toRecipients = new ArrayList();
            from = string.Empty;
            subject = string.Empty;
            body = new StringBuilder();
            
        }

        public string Body
        {
            get
            {
                return body.ToString();
            }
            set
            {
                body.Remove(0, body.Length);
                body.Append(value);
            }
        }

        public void addTo(string to)
        {
            toRecipients.Add(to);
        }

        public bool send()
        {
            bool result = true;

            string recipients = getRecipients();

            try
            {
                smtpClient.Host = server;
                // TODO: hack warning - need to fix username handling.  "from" and "username" must always be the same
                username = from;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(username, password); 
                smtpClient.Port = port;

                smtpClient.Send(from, recipients, subject, body.ToString());
            }
            catch (Exception x)
            {
                Logger.log(this, x, "Error sending email.");
                Logger.log(Logger.Level.ERROR, "server=" + server);
                Logger.log(Logger.Level.ERROR, "username=" + username);
                Logger.log(Logger.Level.ERROR, "from=" + from);
                Logger.log(Logger.Level.ERROR, "recipients=" + recipients);
                Logger.log(Logger.Level.ERROR, "subject=" + subject);
                //throw;
            }

            return result;
        }

        protected string getRecipients()
        {
            StringBuilder result = new StringBuilder();

            foreach (string to in toRecipients)
            {
                if (result.Length > 0) result.Append(",");
                result.Append(to);
            }

            return result.ToString();
        }

        public void clearRecipients()
        {
            toRecipients.Clear();
        }

        /// <summary>
        /// Checks whether the given string is a valid e-mail address.
        /// </summary>
        /// <param name="emailAddressToCheck">Parameter string that represents an email address.</param>
        /// <returns><b>True</b> when email address is valid, else <b>false</b>.</returns>
        public static bool IsValidEmailAddress(string emailAddressToCheck)
        {
            if (emailAddressToCheck != null)
            {
                return Regex.IsMatch(emailAddressToCheck, PATTERN_EMAIL_ADDRESS);
            }
            else
            {
                return false;
            }
        }

    }
}
