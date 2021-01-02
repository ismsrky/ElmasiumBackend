namespace Mh.Db.Tables
{
    public class Dictionary
    {
        public int Id { get; set; } // int, not null
        public string Key { get; set; } // nvarchar(255), not null, unique
        public string Tr { get; set; } // nvarchar(max), null
        public string En { get; set; } // nvarchar(max), null
    }
}