using ExamenMuttual.Shared.Abstraction;
using ExamenMuttual.Shared.Models.SQLite;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ExamenMuttual.Shared.Helpers
{
    public class SQLiteHelper: ISQLite
    {
        SQLiteAsyncConnection db;
      
        public SQLiteHelper()
        {
            if (db == null)
            {
                 db = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "XamarinSQLite.db3"));
            }
 
       
            db.CreateTableAsync<Usuarios>().Wait();
        }
       
        public Task<List<Usuarios>> GetItemsAsync()
        {
            return db.Table<Usuarios>().ToListAsync();
        }
        public Task<Usuarios> GetItemAsync(int personId)
        {
            return db.Table<Usuarios>().Where(i => i.PersonID == personId).FirstOrDefaultAsync();
        }
        public Task<int> SaveItemAsync(Usuarios person)
        {
            if (person.PersonID != 0)
            {
                return db.UpdateAsync(person);
            }
            else
            {
                return db.InsertAsync(person);
            }
        }
        public Task<int> DeleteItemAsync(Usuarios person)
        {
            return db.DeleteAsync(person);
        }

    }
}