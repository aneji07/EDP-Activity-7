using MySql.Data.MySqlClient;
using BankManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BankManagementSystem.Services
{
    public static class AuthService
    {
        private static User currentUser = null;

        public static User CurrentUser
        {
            get { return currentUser; }
        }

        public static bool IsLoggedIn
        {
            get { return currentUser != null; }
        }

        public static bool IsAdmin
        {
            get { return currentUser != null && currentUser.Role == "Admin"; }
        }

        public static bool Login(string username, string password)
        {
            string query = "SELECT * FROM users WHERE username = @username AND password_hash = @password AND is_active = TRUE";

            var parameters = new Dictionary<string, object>();
            parameters.Add("@username", username);
            parameters.Add("@password", password);

            try
            {
                using (var reader = DatabaseManager.ExecuteReader(query, parameters))
                {
                    if (reader.Read())
                    {
                        currentUser = new User();
                        currentUser.UserId = reader.GetInt32("user_id");
                        currentUser.Username = reader.GetString("username");
                        currentUser.PasswordHash = reader.GetString("password_hash");
                        currentUser.Email = reader.GetString("email");
                        currentUser.FullName = reader.GetString("full_name");
                        currentUser.SecurityQuestion = reader.GetString("security_question");
                        currentUser.SecurityAnswer = reader.GetString("security_answer");
                        currentUser.IsActive = reader.GetBoolean("is_active");
                        currentUser.Role = reader.GetString("role");
                        currentUser.CreatedAt = reader.GetDateTime("created_at");
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static void Logout()
        {
            currentUser = null;
        }

        public static bool ResetPassword(string username, string securityAnswer, string newPassword)
        {
            string verifyQuery = "SELECT user_id FROM users WHERE username = @username AND security_answer = @answer";

            var verifyParams = new Dictionary<string, object>();
            verifyParams.Add("@username", username);
            verifyParams.Add("@answer", securityAnswer);

            var result = DatabaseManager.ExecuteScalar(verifyQuery, verifyParams);

            if (result != null)
            {
                string updateQuery = "UPDATE users SET password_hash = @newPassword WHERE username = @username";
                var updateParams = new Dictionary<string, object>();
                updateParams.Add("@newPassword", newPassword);
                updateParams.Add("@username", username);

                DatabaseManager.ExecuteNonQuery(updateQuery, updateParams);
                return true;
            }

            return false;
        }

        public static string GetSecurityQuestion(string username)
        {
            string query = "SELECT security_question FROM users WHERE username = @username";
            var parameters = new Dictionary<string, object>();
            parameters.Add("@username", username);

            var result = DatabaseManager.ExecuteScalar(query, parameters);
            return result == null ? null : result.ToString();
        }

        public static bool UserExists(string username)
        {
            string query = "SELECT COUNT(*) FROM users WHERE username = @username";
            var parameters = new Dictionary<string, object>();
            parameters.Add("@username", username);

            var result = DatabaseManager.ExecuteScalar(query, parameters);
            return Convert.ToInt32(result) > 0;
        }

        public static bool VerifyCurrentPassword(string password)
        {
            if (currentUser == null) return false;
            string query = "SELECT COUNT(*) FROM users WHERE user_id = @id AND password_hash = @pwd";
            var parameters = new Dictionary<string, object>();
            parameters.Add("@id", currentUser.UserId);
            parameters.Add("@pwd", password);
            var result = DatabaseManager.ExecuteScalar(query, parameters);
            return Convert.ToInt32(result) > 0;
        }
    }
}