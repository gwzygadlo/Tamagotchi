using System;
using SQLite;

namespace Tamagotchi.Models
{
    public class dbShop
    {
        [PrimaryKey, AutoIncrement]
        public int shop_id { get; set; }
        public int game_id { get; set; }
        public int food_id { get; set; }
        public int snack_id { get; set; }
        public string name { get; set; }
    }
}
