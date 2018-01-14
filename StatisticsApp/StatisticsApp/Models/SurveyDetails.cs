namespace StatisticsApp.Models
{
    public class SurveyDetails
    {
        public string SurveyId { get; set; }
        public string SurveyName { get; set; }
        public string ClientName { get; set; }
        public string SurveyType { get; set; }
        public string Description { get; set; }
        public string QuestionnaireMD5 { get; set; }
        public string InterviewerInstruction { get; set; }

        public string Icon { get; set; }
    }
}
