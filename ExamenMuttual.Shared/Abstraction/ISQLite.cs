using ExamenMuttual.Shared.Models.SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamenMuttual.Shared.Abstraction
{
    public interface ISQLite
    {
        Task<List<Usuarios>> GetItemsAsync();
        Task<Usuarios> GetItemAsync(int personId);
        Task<int> SaveItemAsync(Usuarios person);
        Task<int> DeleteItemAsync(Usuarios person);

    }
}
