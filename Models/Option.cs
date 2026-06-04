using System;

namespace SurveySystem.Models
{
    public class Option
    {
        public int OptionID { get; set; }
        public int QuestionID { get; set; }
        public string OptionText { get; set; }
        public int OrderNum { get; set; }
    }
}