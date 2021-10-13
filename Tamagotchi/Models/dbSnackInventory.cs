using System;
using SQLite;

namespace Tamagotchi.Models
{
    public class dbSnackInventory
    {
        [PrimaryKey, AutoIncrement]
        public int snack_inventory_id { get; set; }
        public int snack_id { get; set; }
        public int game_id { get; set; }
        public int quantity { get; set; }
    }
}
