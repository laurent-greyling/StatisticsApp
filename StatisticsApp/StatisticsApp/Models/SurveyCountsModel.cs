﻿namespace StatisticsApp.Models
{
    public class SurveyCountsModel
    {
        public string SurveyId { get; set; }
        public int? SuccessfulCount { get; set; }
        public int? ScreenedOutCount { get; set; }
        public int? DroppedOutCount { get; set; }
        public int? RejectedCount { get; set; }
        public QuotaLevel QuotaCounts { get; set; }
        public int? ActiveLiveCount { get; set; }
        public int? ActiveTestCount { get; set; }
    }
}
