using System;

namespace SurveySystem.Models
{
    public class Answer
    {
        public int AnswerID { get; set; }
        public int ResponseID { get; set; }
        public int QuestionID { get; set; }
        public string AnswerText { get; set; }
        public int? SelectedOptionID { get; set; }
    }
}