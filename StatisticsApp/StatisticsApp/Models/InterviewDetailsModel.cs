using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsApp.Models
{
    public class InterviewDetailsModel
    {
        public string Id { get; set; }
        public InterviewQuality InterviewQuality { get; set; }

        public DateTime StartDate { get; set; }

        public string InterviewId { get; set; }
        public string SamplingPointId { get; set; }
        public string OfficeId { get; set; }
    }
}
