using System;
using System.Data;
using System.Data.SqlClient;
using SurveySystem.Models;

namespace SurveySystem.DAL
{
    public class UserDAL
    {
        public static User Login(string username, string password)
        {
            string sql = "SELECT * FROM Users WHERE Username=@Username AND Password=@Password AND IsActive=1";
            SqlParameter[] parameters = {
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", password)
            };

            DataTable dt = DBHelper.ExecuteQuery(sql, parameters);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                User user = new User
                {
                    UserID = (int)row["UserID"],
                    Username = row["Username"].ToString(),
                    Email = row["Email"].ToString(),
                    FullName = row["FullName"].ToString(),
                    Role = row["Role"].ToString(),
                    IsActive = (bool)row["IsActive"],
                    CreatedDate = (DateTime)row["CreatedDate"]
                };
                return user;
            }
            return null;
        }

        public static bool Register(string username, string email, string password, string fullName)
        {
            string sql = "INSERT INTO Users(Username, Email, Password, FullName, Role, IsActive) VALUES(@Username, @Email, @Password, @FullName, 'User', 1)";
            SqlParameter[] parameters = {
                new SqlParameter("@Username", username),
                new SqlParameter("@Email", email),
                new SqlParameter("@Password", password),
                new SqlParameter("@FullName", fullName)
            };

            return DBHelper.ExecuteNonQuery(sql, parameters) > 0;
        }

        public static bool UsernameExists(string username)
        {
            string sql = "SELECT COUNT(*) FROM Users WHERE Username=@Username";
            SqlParameter[] parameters = {
                new SqlParameter("@Username", username)
            };

            object result = DBHelper.ExecuteScalar(sql, parameters);
            return (int)result > 0;
        }

        public static User GetUser(int userID)
        {
            string sql = "SELECT * FROM Users WHERE UserID=@UserID";
            SqlParameter[] parameters = {
                new SqlParameter("@UserID", userID)
            };

            DataTable dt = DBHelper.ExecuteQuery(sql, parameters);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                User user = new User
                {
                    UserID = (int)row["UserID"],
                    Username = row["Username"].ToString(),
                    Email = row["Email"].ToString(),
                    FullName = row["FullName"].ToString(),
                    Role = row["Role"].ToString()
                };
                return user;
            }
            return null;
        }

        public static bool UpdateUser(User user)
        {
            string sql = "UPDATE Users SET Email=@Email, FullName=@FullName WHERE UserID=@UserID";
            SqlParameter[] parameters = {
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@FullName", user.FullName),
                new SqlParameter("@UserID", user.UserID)
            };

            return DBHelper.ExecuteNonQuery(sql, parameters) > 0;
        }
    }
}