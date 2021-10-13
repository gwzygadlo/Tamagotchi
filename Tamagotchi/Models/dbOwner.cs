using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Tamagotchi.Models
{
    public class dbOwner
    {
        [PrimaryKey, AutoIncrement]
        public int owner_id { get; set; }
        public string name { get; set; }
        public int credits { get; set; }
    }
}
