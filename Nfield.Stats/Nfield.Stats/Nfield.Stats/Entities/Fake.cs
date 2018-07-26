using SQLite;

namespace Nfield.Stats.Entities
{
    class Fake
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique]
        public string SurveId { get; set; }
        public bool IsFavourite { get; set; }

        public string Icon { get; set; }
        public Fake()
        {

        }
    }
}
