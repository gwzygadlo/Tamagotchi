using System;
using SQLite;

namespace Tamagotchi.Models
{
    public class dbGame
    {
        [PrimaryKey, AutoIncrement]
        public int game_id { get; set; }
        public int tama_id { get; set; }
        public int owner_id { get; set; }
        public string game_state { get; set; }
        public DateTime last_time { get; set; }
    }
}
