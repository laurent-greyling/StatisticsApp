using SQLite;

namespace Nfield.Stats.Entities
{
    public class ServerEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique]
        public string NfieldServer { get; set; }
        [Unique]
        public string ServerName { get; set; }
    }
}
