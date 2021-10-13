using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi.Game.Models
{
    public class Owner
    {
        public Owner(string name = null)
        {
            this.Credits = 0;
            this.Name = name;

        }
        public int Credits { get; set; }
        public string Name { get; set; }
    }
}
