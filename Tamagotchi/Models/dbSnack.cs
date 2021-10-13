using System;
using SQLite;

namespace Tamagotchi.Models
{
    public class dbSnack
    {
        [PrimaryKey, AutoIncrement]
        public int snack_id { get; set; }
        public string name { get; set; }
        public int credit_value { get; set; }
        public dbSelectedAction selected_action { get; set; }
    }
}
