using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi.Game.Models
{
    public class FoodInventory
    {
        public FoodInventory()
        {
            this.FoodList = new List<Food>();

        }

        public List<Food> FoodList { get; set; }
    }
}
