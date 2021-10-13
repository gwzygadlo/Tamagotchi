using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagotchi.Game.Models
{
    public class Food
    {
        public Food(string food_name, int food_quantity)
        {
            this.FoodName = food_name;
            this.FoodQuantity = food_quantity;
            this.CreditValue = 100;
        }

        public string FoodName { get; set; }
        public int FoodQuantity { get; set; }
        public int CreditValue { get; set; }
    }
}
