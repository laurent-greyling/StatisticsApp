using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticsApp.Entities
{
    public class AddToFav
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique]
        public string SurveId { get; set; }
        public bool IsFavourite { get; set; }

        public string Icon { get; set; }
        public AddToFav()
        {

        }
    }
}
