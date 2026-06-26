using System;
using System.Collections.Generic;
using System.Text;

namespace MyGroceryList.Models
{
    public class GroceryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public int SortOrder { get; set; }
    }
}
