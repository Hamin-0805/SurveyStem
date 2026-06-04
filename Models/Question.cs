using System;

namespace SurveySystem.Models
{
    public class Question
    {
        public int QuestionID { get; set; }
        public int SurveyID { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public int OrderNum { get; set; }
        public bool IsRequired { get; set; }
    }
}