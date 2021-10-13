using System;
using SQLite;

namespace Tamagotchi.Models
{
    public class dbFoodInventory
    {
        [PrimaryKey, AutoIncrement]
        public int food_inventory_id { get; set; }
        public int food_id { get; set; }
        public int game_id { get; set; }
        public int quantity { get; set; }
    }
}
