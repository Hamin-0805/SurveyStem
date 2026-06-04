using System;
using System.Data;
using System.Data.SqlClient;
using SurveySystem.Models;

namespace SurveySystem.DAL
{
    public class QuestionDAL
    {
        public static int AddQuestion(Question question)
        {
            string sql = @"INSERT INTO Questions(SurveyID, QuestionText, QuestionType, OrderNum, IsRequired) 
                          VALUES(@SurveyID, @QuestionText, @QuestionType, @OrderNum, @IsRequired);
                          SELECT SCOPE_IDENTITY();";
            SqlParameter[] parameters = {
                new SqlParameter("@SurveyID", question.SurveyID),
                new SqlParameter("@QuestionText", question.QuestionText),
                new SqlParameter("@QuestionType", question.QuestionType),
                new SqlParameter("@OrderNum", question.OrderNum),
                new SqlParameter("@IsRequired", question.IsRequired)
            };

            object result = DBHelper.ExecuteScalar(sql, parameters);
            return Convert.ToInt32(result);
        }

        public static DataTable GetQuestionsBySurvey(int surveyID)
        {
            string sql = "SELECT * FROM Questions WHERE SurveyID=@SurveyID ORDER BY OrderNum";
            SqlParameter[] parameters = {
                new SqlParameter("@SurveyID", surveyID)
            };
            return DBHelper.ExecuteQuery(sql, parameters);
        }

        public static DataTable GetQuestion(int questionID)
        {
            string sql = "SELECT * FROM Questions WHERE QuestionID=@QuestionID";
            SqlParameter[] parameters = {
                new SqlParameter("@QuestionID", questionID)
            };
            return DBHelper.ExecuteQuery(sql, parameters);
        }

        public static bool UpdateQuestion(Question question)
        {
            string sql = @"UPDATE Questions SET QuestionText=@QuestionText, QuestionType=@QuestionType, 
                          OrderNum=@OrderNum, IsRequired=@IsRequired WHERE QuestionID=@QuestionID";
            SqlParameter[] parameters = {
                new SqlParameter("@QuestionText", question.QuestionText),
                new SqlParameter("@QuestionType", question.QuestionType),
                new SqlParameter("@OrderNum", question.OrderNum),
                new SqlParameter("@IsRequired", question.IsRequired),
                new SqlParameter("@QuestionID", question.QuestionID)
            };
            return DBHelper.ExecuteNonQuery(sql, parameters) > 0;
        }

        public static bool DeleteQuestion(int questionID)
        {
            string sql = "DELETE FROM Questions WHERE QuestionID=@QuestionID";
            SqlParameter[] parameters = {
                new SqlParameter("@QuestionID", questionID)
            };
            return DBHelper.ExecuteNonQuery(sql, parameters) > 0;
        }
    }
}