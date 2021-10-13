using System;
using SQLite;

namespace Tamagotchi.Models
{
    public class dbTama
    {
        [PrimaryKey, AutoIncrement]
        public int tama_id { get; set; }
        public string name { get; set; }
        public string species { get; set; }
        public int hunger { get; set; }
        public int happiness { get; set; }
        public bool is_sick { get; set; }
        public int sick_counter { get; set; }
    }
}
