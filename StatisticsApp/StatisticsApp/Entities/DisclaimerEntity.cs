using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsApp.Entities
{
    public class DisclaimerEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public bool IsRead { get; set; }

        public DisclaimerEntity()
        {

        }
    }
}
