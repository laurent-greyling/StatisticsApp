﻿using System.Collections.Generic;

namespace StatisticsApp.Models
{
    public class QuotaGroup: List<QuotaLevel>
    {
        public string LevelName { get; set; }

        public QuotaGroup(string name)
        {
            LevelName = name;
        }
    }
}
