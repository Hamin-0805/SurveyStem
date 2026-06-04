using System;

namespace SurveySystem.Models
{
    public class Survey
    {
        public int SurveyID { get; set; }
        public string SurveyTitle { get; set; }
        public string Description { get; set; }
        public int CreatorID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public bool AllowAnonymous { get; set; }
    }
}