using System;

namespace SurveySystem.Models
{
    public class Response
    {
        public int ResponseID { get; set; }
        public int SurveyID { get; set; }
        public int? RespondentID { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string IPAddress { get; set; }
    }
}