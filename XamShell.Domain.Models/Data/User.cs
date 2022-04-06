using SQLite;

namespace XamShell.Domain.Models.Data
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Count { get; set; }
    }
}