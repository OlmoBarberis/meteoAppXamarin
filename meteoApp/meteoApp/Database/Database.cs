using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace meteoApp.Database
{
    public class MeteoDatabase
    {
        SQLiteAsyncConnection database;

        public MeteoDatabase()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Database.db3");
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Entry>().Wait();
        }

        public Task<List<Entry>> GetEntryAsync()
        {
            return database.Table<Entry>().ToListAsync();
        }

        public Task<List<Entry>> GetEntryWithWhere(int id)
        {
            return database.QueryAsync<Entry>("SELECT * FROM [Entry] WHERE [ID] =?", id);
        }
        public Task<Entry> GetEntryAsync(int id)
        {
            return database.Table<Entry>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveEntryAsync(Entry entry)
        {
            return database.InsertAsync(entry);
        }

        public Task<int> UpdateEntryAsync(Entry entry)
        {
            return database.UpdateAsync(entry);
        }

        public Task<int> DeleteEntryAsync(Entry entry)
        {
            return database.DeleteAsync(entry);
        }
    }
}
