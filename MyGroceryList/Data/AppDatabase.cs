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
    }
}
