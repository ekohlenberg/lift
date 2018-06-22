using LiftCommon;

using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;

namespace liftprayer
{

    public sealed class LiftRoleProvider : RoleProvider
    {

        #region -- Declarations --

        private string _applicationName = string.Empty;
        private string _connectionString = string.Empty;

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

        #endregion

        #region -- Methods --

        public override void AddUsersToRoles(string[] userNames, string[] roleNames)
        {

            foreach (string roleName in roleNames)
            {
                if (!RoleExists(roleName))
                {
                    string message = string.Format("Role name '{0}' not found.", roleName);
                    Logger.log(Logger.Level.ERROR, this, message);
                    throw new ProviderException(message);
                }
            }

            foreach (string userName in userNames)
            {

                if (userName.Contains(","))
                {
                    string message = "User names cannot contain commas.";
                    Logger.log(Logger.Level.ERROR, this, message);
                    throw new ArgumentException(message);
                }

                foreach (string roleName in roleNames)
                {
                    if (IsUserInRole(userName, roleName))
                    {
                        string message = string.Format("User '{0}' is already in role '{1}'.", userName, roleName);
                        Logger.log(Logger.Level.ERROR, this, message);
                        throw new ProviderException(message);
                    }
                }

            }

            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                cn.Open();

                using (SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable))
                {

                    try
                    {

                        using (SqlCommand cm = cn.CreateCommand())
                        {
                            
                            cm.Transaction = tr;
                            cm.CommandType = CommandType.Text;
                            cm.CommandText = "INSERT INTO [dbo].[roles_users] ([user_id], role_id, created_at) SELECT  [u].[id], [r].[title], GETDATE() FROM [dbo].[users] [u] INNER JOIN [dbo].[roles] [r] ON [r].[title] = @roleName WHERE [u].[login] = @userName";

                            SqlParameter userParm = cm.Parameters.Add("@userName", SqlDbType.VarChar);
                            SqlParameter roleParm = cm.Parameters.Add("@roleName", SqlDbType.VarChar);

                            foreach (string userName in userNames)
                            {
                                foreach (string roleName in roleNames)
                                {
                                    userParm.Value = userName;
                                    roleParm.Value = roleName;
                                    cm.ExecuteNonQuery();
                                }
                            }

                        } // using IDbCommand

                        tr.Commit();

                    }
                    catch (SqlException ex)
                    {
                        // tr.Rollback(); using block will automatically rollback when exception is thrown?
                        Logger.log(Logger.Level.ERROR, this, ex.Message);
                        throw;
                    }

                } // IDbTransaction

            } // IDbConnection

        }

        public override void CreateRole(string roleName)
        {

            if (roleName.Contains(","))
            {
                string message = "Role names cannot contain commas.";
                Logger.log(Logger.Level.ERROR, this, message);
                throw new ArgumentException(message);
            }

            if (RoleExists(roleName))
            {
                string message = string.Format("Role name '{0}' already exists.");
                Logger.log(Logger.Level.ERROR, this, message);
                throw new ProviderException(message);
            }

            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                cn.Open();

                using (SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable))
                {

                    try
                    {

                        using (SqlCommand cm = cn.CreateCommand())
                        {

                            cm.Transaction = tr;
                            cm.CommandType = CommandType.Text;
                            cm.CommandText = "INSERT INTO [dbo].[roles] ([created_at], [title]) VALUES (@roleName, GETDATE())";
                            cm.Parameters.AddWithValue("@roleName", roleName);

                            cm.ExecuteNonQuery();

                        } // using IDbCommand

                        tr.Commit();

                    }
                    catch (SqlException ex)
                    {
                        // tr.Rollback(); using block will automatically rollback when exception is thrown?
                        Logger.log(Logger.Level.ERROR, this, ex.Message);
                        throw;
                    }

                } // IDbTransaction

            } // IDbConnection

        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {

            bool result = true;

            if (!RoleExists(roleName))
            {
                string message = string.Format("Role '{0}' does not exist.", roleName);
                Logger.log(Logger.Level.ERROR, this, message);
                throw new ProviderException(message);
            }

            if (throwOnPopulatedRole && GetUsersInRole(roleName).Length > 0)
            {
                string message = "Cannot delete a populated role.";
                Logger.log(Logger.Level.ERROR, this, message);
                throw new ProviderException(message);
            }

            using (SqlConnection cn = new SqlConnection(_connectionString))
            {

                cn.Open();

                using (SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable))
                {

                    try
                    {

                        using (SqlCommand cm = cn.CreateCommand())
                        {

                            cm.Transaction = tr;
                            cm.CommandType = CommandType.Text;
                            cm.CommandText = "DELETE [dbo].[roles_users] WHERE [role_id] IN (SELECT [id] FROM [dbo].[roles] WHERE [title] = @roleName); DELETE [dbo].[roles] WHERE [title] = @roleName";
                            cm.Parameters.AddWithValue("@roleName", roleName);

                            cm.ExecuteNonQuery();

                        } // using IDbCommand

                        tr.Commit();

                    }
                    catch (SqlException ex)
                    {
                        tr.Rollback();
                        Logger.log(Logger.Level.ERROR, this, ex.Message);
                        result = false;
                    }

                } // IDbTransaction

            } // IDbConnection

            return result;

        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {

            string[] result = new string[0];
            string users = string.Empty;

            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                cn.Open();

                using (SqlTransaction tr = cn.BeginTransaction(IsolationLevel.ReadCommitted))
                {

                    try
                    {

                        using (SqlCommand cm = cn.CreateCommand())
                        {

                            cm.Transaction = tr;
                            cm.CommandType = CommandType.Text;
                            cm.CommandText = "SELECT [u].[login] FROM [dbo].[roles] [r] INNER JOIN [dbo].[roles_users] [ru] ON [r].[id] = [ru].[role_id] INNER JOIN [dbo].[users] [u] ON [ru].[user_id] = [u].[id] WHERE [r].[title] = @roleName AND [u].[login] LIKE @userNameSearch";
                            cm.Parameters.AddWithValue("@userNameSearch", usernameToMatch);
                            cm.Parameters.AddWithValue("@roleName", roleName);

                            using (SqlDataReader dr = cm.ExecuteReader())
                            {

                                while (dr.Read())
                                {
                                    users += dr.GetString(0) + ",";
                                }

                            } // IDbReader

                        } // using IDbCommand

                        tr.Commit();

                    }
                    catch (SqlException ex)
                    {
                        // tr.Rollback(); using block will automatically rollback when exception is thrown?
                        Logger.log(Logger.Level.ERROR, this, ex.Message);
                        throw;
                    }

                } // IDbTransaction

            } // IDbConnection

            if (!string.IsNullOrEmpty(users))
            {
                // Remove trailing comma.
                users = users.Substring(0, users.Length - 1);
                result = users.Split(',');
            }

            return result;

        }

        public override string[] GetAllRoles()
        {
            string[] result = new string[0];

            LiftDomain.Role r = new LiftDomain.Role();

            DataSet roleSet = r.doQuery("se;ect");

            if (roleSet != null)
            {
                if (roleSet.Tables.Count == 1)
                {
                    result = new string[roleSet.Tables[0].Rows.Count];
                    int i = 0;
                    foreach (DataRow rr in roleSet.Tables[0].Rows)
                    {
                        result[i] = rr["title"].ToString();
                        i++;
                    }
                }
            }

            return result;
        }

        public override string[] GetRolesForUser(string userName)
        {
            string[] result = new string[0];

            LiftDomain.Role r = new LiftDomain.Role();
            r["username"] = userName;

            DataSet roleSet = r.doQuery("get_roles_for_user");

            if (roleSet != null)
            {
                if (roleSet.Tables.Count == 1)
                {
                    result = new string[roleSet.Tables[0].Rows.Count];
                    int i = 0;
                    foreach (DataRow rr in roleSet.Tables[0].Rows)
                    {
                        result[i] = rr["title"].ToString();
                        i++;
                    }
                }
            }

            return result;
        }

        public override string[] GetUsersInRole(string roleName)
        {

            string[] result = new string[0];

            LiftDomain.Role r = new LiftDomain.Role();
            r["rolename"] = roleName;

            DataSet roleSet = r.doQuery("get_users_in_roles");

            if (roleSet != null)
            {
                if (roleSet.Tables.Count == 1)
                {
                    result = new string[roleSet.Tables[0].Rows.Count];
                    int i = 0;
                    foreach (DataRow rr in roleSet.Tables[0].Rows)
                    {
                        result[i] = rr["login"].ToString();
                        i++;
                    }
                }
            }

            return result;

        }

        public override void Initialize(string name, NameValueCollection config)
        {

            if (config == null)
            {
                config = new NameValueCollection();
                config.Add("applicationName", "LiftPrayer");
            }

            if (name == null || name.Length == 0)
            {
                name = "LiftRoleProvider";
            }

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Lift Role Provider");
            }

            // Initialize the abstract base class.
            base.Initialize(name, config);

            if (config["applicationName"] == null || config["applicationName"].Trim() == "")
            {
                _applicationName = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;
            }
            else
            {
                _applicationName = config["applicationName"];
            }

            _connectionString = ConfigurationManager.AppSettings["defaultconnection"];

            if (string.IsNullOrEmpty(_connectionString))
            {
                Logger.log(Logger.Level.ERROR, this, "Connection string cannot be blank.");
                throw new ProviderException("Connection string cannot be blank.");
            }
            else
            {
                _connectionString = _connectionString.ToUpper().Replace("PROVIDER=SQLOLEDB;", string.Empty);
            }

        }

        public override bool IsUserInRole(string userName, string roleName)
        {

            bool result = false;

            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                cn.Open();

                using (SqlTransaction tr = cn.BeginTransaction(IsolationLevel.ReadCommitted))
                {

                    try
                    {

                        using (SqlCommand cm = cn.CreateCommand())
                        {

                            cm.Transaction = tr;
                            cm.CommandType = CommandType.Text;
                            cm.CommandText = "SELECT COUNT(*) FROM [dbo].[roles] [r] INNER JOIN [dbo].[roles_users] [ru] ON [r].[id] = [ru].[role_id] INNER JOIN [dbo].[users] [u] ON [ru].[user_id] = [u].[id] WHERE [r].[title] = @roleName AND [u].[login] = @userName";
                            cm.Parameters.AddWithValue("@userName", userName);
                            cm.Parameters.AddWithValue("@roleName", roleName);

                            result = ((int) cm.ExecuteScalar() >= 1);

                        } // using IDbCommand

                        tr.Commit();

                    }
                    catch (SqlException ex)
                    {
                        // tr.Rollback(); using block will automatically rollback when exception is thrown?
                        Logger.log(Logger.Level.ERROR, this, ex.Message);
                        throw;
                    }

                } // IDbTransaction

            } // IDbConnection

            return result;

        }

        public override void RemoveUsersFromRoles(string[] userNames, string[] roleNames)
        {
            
            foreach (string roleName in roleNames)
            {
                if (!RoleExists(roleName))
                {
                    string message = string.Format("Role name '{0}' not found.", roleName);
                    Logger.log(Logger.Level.ERROR, this, message);
                    throw new ProviderException(message);
                }
            }

            foreach (string userName in userNames)
            {
                foreach (string roleName in roleNames)
                {
                    if (!IsUserInRole(userName, roleName))
                    {
                        string message = string.Format("User '{0}' is not in role '{1}'.", userName, roleName);
                        Logger.log(Logger.Level.ERROR, this, message);
                        throw new ProviderException(message);
                    }
                }
            }

            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                cn.Open();

                using (SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable))
                {

                    try
                    {

                        using (SqlCommand cm = cn.CreateCommand())
                        {

                            cm.Transaction = tr;
                            cm.CommandType = CommandType.Text;
                            cm.CommandText = "DELETE [dbo].[roles_users] WHERE [role_id] IN (SELECT [id] FROM [dbo].[roles] WHERE [title] = @roleName) AND [user_id] IN (SELECT [id] FROM [dbo].[users] WHERE [login] = @userName)";

                            SqlParameter userParm = cm.Parameters.Add("@userName", SqlDbType.VarChar);
                            SqlParameter roleParm = cm.Parameters.Add("@roleName", SqlDbType.VarChar);

                            foreach (string userName in userNames)
                            {
                                foreach (string roleName in roleNames)
                                {
                                    userParm.Value = userName;
                                    roleParm.Value = roleName;
                                    cm.ExecuteNonQuery();
                                }
                            }

                        } // using IDbCommand

                        tr.Commit();

                    }
                    catch (SqlException ex)
                    {
                        // tr.Rollback(); using block will automatically rollback when exception is thrown?
                        Logger.log(Logger.Level.ERROR, this, ex.Message);
                        throw;
                    }

                } // IDbTransaction

            } // IDbConnection

        }

        public override bool RoleExists(string roleName)
        {

            bool result = false;

            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                cn.Open();

                using (SqlTransaction tr = cn.BeginTransaction(IsolationLevel.ReadCommitted))
                {

                    try
                    {

                        using (SqlCommand cm = cn.CreateCommand())
                        {

                            cm.Transaction = tr;
                            cm.CommandType = CommandType.Text;
                            cm.CommandText = "SELECT COUNT(*) FROM [dbo].[roles] [r] WHERE [r].[title] = @roleName";
                            cm.Parameters.AddWithValue("@roleName", roleName);

                            result = ((int)cm.ExecuteScalar() >= 1);

                        } // using IDbCommand

                        tr.Commit();

                    }
                    catch (SqlException ex)
                    {
                        // tr.Rollback(); using block will automatically rollback when exception is thrown?
                        Logger.log(Logger.Level.ERROR, this, ex.Message);
                        throw;
                    }

                } // IDbTransaction

            } // IDbConnection

            return result;

        }

        #endregion

    } // class LiftRoleProvider

} // namespace liftprayer
