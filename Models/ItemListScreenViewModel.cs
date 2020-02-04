using System;
using System.Collections.Generic;

namespace AWSDotnetCoreHandsOnLab.Models
{
    public class ItemListScreenViewModel
    {
        public List<Item> Items { get; set; }
        public string Random { get; set; }
    }

    public class Item
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
}