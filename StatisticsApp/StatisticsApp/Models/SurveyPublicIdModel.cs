using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsApp.Models
{
    public class SurveyPublicIdModel
    {
        public string Id { get; set; }
        public string LinkType { get; set; }
        public bool Active { get; set; }
        public string Url { get; set; }
    }
}
