using SQLite;
namespace ExamenMuttual.Shared.Models.SQLite
{
    public class Usuarios
    {
        [PrimaryKey, AutoIncrement]
        public int PersonID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
