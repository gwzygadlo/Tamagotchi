using System;
using SQLite;


namespace Tamagotchi.Models
{
    public class dbFood
    {
        [PrimaryKey, AutoIncrement]
        public int food_id { get; set; }
        public string name { get; set; }
        public int credit_value { get; set; }
        public dbSelectedAction selected_action { get; set; }
    }
}
