using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagotchi.Game.Models
{
    public class Snack
    {
        public Snack(string snack_name, int snack_quantity)
        {
            this.SnackName = snack_name;
            this.SnackQuantity = snack_quantity;
            this.CreditValue = 100;
        }

        public string SnackName { get; set; }
        public int SnackQuantity { get; set; }
        public int CreditValue { get; set; }
    }
}
