using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MyGroceryList.Data;
using MyGroceryList.Models;


namespace MyGroceryList.ViewModels
{
    public class GroceryListViewModel : BindableObject
    {
        private readonly AppDatabase _database;

        public ObservableCollection<GroceryItem> Items { get; } = new();

        private string _newItemName;

        public string NewItemName
        {
            get => _newItemName;
            set
            {
                _newItemName = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddItemCommand { get; }
        public ICommand ToggleItemCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand LoadItemsCommand { get; }

        public GroceryListViewModel(AppDatabase database)
        {
            _database = database;

            AddItemCommand = new Command(async () => await AddItem());
            ToggleItemCommand = new Command<GroceryItem>(async item => await ToggleItem(item));
            DeleteItemCommand = new Command<GroceryItem>(async item => await DeleteItem(item));
            LoadItemsCommand = new Command(async () => await LoadItems() );

        }

        private async Task LoadItems()
        {
            Items.Clear();
            var items = await _database.GetItemsAsync();
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }

        private async Task AddItem()
        {
            if (string.IsNullOrWhiteSpace(NewItemName))
                return;

            var newItem = new GroceryItem
            {
                Name = NewItemName,
                IsCompleted = false,
                SortOrder = Items.Count
            };

            await _database.AddItemAsync(newItem);
            Items.Add(newItem);

            NewItemName = string.Empty;

        }

        private async Task ToggleItem(GroceryItem item)
        {
            item.IsCompleted = !item.IsCompleted;
            await _database.UpdateItemAsync(item);
        }

        private async Task DeleteItem(GroceryItem item)
        {
            await _database.DeleteItemAsync(item);
            Items.Remove(item);
        }

    }
}
