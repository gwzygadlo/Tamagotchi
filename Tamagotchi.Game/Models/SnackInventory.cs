using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi.Game.Models
{
    public class SnackInventory
    {
        public SnackInventory()
        {
            this.SnackList = new List<Snack>();

        }

        public List<Snack> SnackList { get; set; }
    }
}
