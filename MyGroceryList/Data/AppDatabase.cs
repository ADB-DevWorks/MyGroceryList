using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using MyGroceryList.Models;

namespace MyGroceryList.Data
{
    public class AppDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public AppDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<GroceryItem>().Wait();
        }

        // Loads items in correct order
        public Task<List<GroceryItem>> GetItemsAsync()
        {
            return _database.Table<GroceryItem>()
                .OrderBy(i => i.SortOrder)
                .ToListAsync();
        }

        // Add new item
        public Task<int> AddItemAsync(GroceryItem item)
        {
            return _database.InsertAsync(item);
        }

        public Task<int> UpdateItemAsync(GroceryItem item)
        {
            return _database.UpdateAsync(item);
        }

        public Task<int> DeleteItemAsync(GroceryItem item)
        {
            return _database.DeleteAsync(item);
        }

    }
}
