using System;
using System.Data;
using System.Data.SqlClient;
using SurveySystem.Models;

namespace SurveySystem.DAL
{
    public class SurveyDAL
    {
        public static int CreateSurvey(Survey survey)
        {
            string sql = @"INSERT INTO Surveys(SurveyTitle, Description, CreatorID, StartDate, EndDate, Status, AllowAnonymous) 
                          VALUES(@SurveyTitle, @Description, @CreatorID, @StartDate, @EndDate, @Status, @AllowAnonymous);
                          SELECT SCOPE_IDENTITY();";
            SqlParameter[] parameters = {
                new SqlParameter("@SurveyTitle", survey.SurveyTitle),
                new SqlParameter("@Description", survey.Description ?? (object)DBNull.Value),
                new SqlParameter("@CreatorID", survey.CreatorID),
                new SqlParameter("@StartDate", survey.StartDate ?? (object)DBNull.Value),
                new SqlParameter("@EndDate", survey.EndDate ?? (object)DBNull.Value),
                new SqlParameter("@Status", survey.Status),
                new SqlParameter("@AllowAnonymous", survey.AllowAnonymous)
            };

            object result = DBHelper.ExecuteScalar(sql, parameters);
            return Convert.ToInt32(result);
        }

        public static DataTable GetSurveyList()
        {
            string sql = @"SELECT s.*, u.FullName as CreatorName, 
                          (SELECT COUNT(*) FROM Responses WHERE SurveyID=s.SurveyID) as ResponseCount
                          FROM Surveys s 
                          JOIN Users u ON s.CreatorID = u.UserID 
                          WHERE s.Status='Active' 
                          ORDER BY s.CreatedDate DESC";
            return DBHelper.ExecuteQuery(sql);
        }

        public static DataTable GetUserSurveys(int userID)
        {
            string sql = @"SELECT s.*, 
                          (SELECT COUNT(*) FROM Responses WHERE SurveyID=s.SurveyID) as ResponseCount
                          FROM Surveys s 
                          WHERE s.CreatorID=@UserID 
                          ORDER BY s.CreatedDate DESC";
            SqlParameter[] parameters = {
                new SqlParameter("@UserID", userID)
            };
            return DBHelper.ExecuteQuery(sql, parameters);
        }

        public static DataTable GetSurveyDetail(int surveyID)
        {
            string sql = "SELECT * FROM Surveys WHERE SurveyID=@SurveyID";
            SqlParameter[] parameters = {
                new SqlParameter("@SurveyID", surveyID)
            };
            return DBHelper.ExecuteQuery(sql, parameters);
        }

        public static bool UpdateSurvey(Survey survey)
        {
            string sql = @"UPDATE Surveys SET SurveyTitle=@SurveyTitle, Description=@Description, 
                          StartDate=@StartDate, EndDate=@EndDate, Status=@Status, AllowAnonymous=@AllowAnonymous 
                          WHERE SurveyID=@SurveyID";
            SqlParameter[] parameters = {
                new SqlParameter("@SurveyTitle", survey.SurveyTitle),
                new SqlParameter("@Description", survey.Description ?? (object)DBNull.Value),
                new SqlParameter("@StartDate", survey.StartDate ?? (object)DBNull.Value),
                new SqlParameter("@EndDate", survey.EndDate ?? (object)DBNull.Value),
                new SqlParameter("@Status", survey.Status),
                new SqlParameter("@AllowAnonymous", survey.AllowAnonymous),
                new SqlParameter("@SurveyID", survey.SurveyID)
            };
            return DBHelper.ExecuteNonQuery(sql, parameters) > 0;
        }

        public static bool DeleteSurvey(int surveyID)
        {
            string sql = "DELETE FROM Surveys WHERE SurveyID=@SurveyID";
            SqlParameter[] parameters = {
                new SqlParameter("@SurveyID", surveyID)
            };
            return DBHelper.ExecuteNonQuery(sql, parameters) > 0;
        }
    }
}