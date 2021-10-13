using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagotchi.Game.Models
{
    public class Tama
    {
        public Tama()
        {

        }

        public string Name { get; set; }
        public string Species { get; set; }
        public int Hunger { get; set; } // max 8
        public int Happiness { get; set; } //max 8
        public bool IsSick { get; set; }
        public int SickCounter { get; set; }

        public void IncrementHunger(int increment)
        {
            this.Hunger += increment;
            this.DetermineSickness();
        }

        public void IncrementHappiness(int increment)
        {
            this.Happiness += increment;
            this.DetermineSickness();
        }

        public bool DetermineSickness()
        {
            if (!this.IsSick && this.SickCounter < 3)
            {
                if (this.Hunger == 0 && this.Happiness == 0)
                {
                    this.IsSick = true;
                    this.SickCounter++;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }



    }
}
