using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi.Game.Models
{
    public class Shop
    {
        public Shop(GameEngine game)
        {
            this.FoodStock = new List<Food>();
            this.SnackStock = new List<Snack>();
            this.Game = game;
        }

        public GameEngine Game { get; set; }
        public List<Food> FoodStock { get; set; }
        public List<Snack> SnackStock { get; set; }

        public void InitializeShop()
        {
            if (this.SnackStock == null || this.SnackStock.Count != 0)
            {
                this.SnackStock.Add(new Snack("Apple", -1)); // -1 means infinite supply or stock
                this.SnackStock.Add(new Snack("Ice Cream", -1));
                this.SnackStock.Add(new Snack("Pineapple", -1));
            }
            else
            {
                this.SnackStock = new List<Snack>() { new Snack("Apple", -1), new Snack("Ice Cream", -1), new Snack("Pineapple", -1) };
            }

            if (this.FoodStock == null || this.FoodStock.Count != 0)
            {
                this.FoodStock.Add(new Food("Bread", -1));
                this.FoodStock.Add(new Food("Apple", -1));
                this.FoodStock.Add(new Food("Cereal", -1));
            }
            else
            {
                this.FoodStock = new List<Food>() { new Food("Apple", -1), new Food("Bread", -1), new Food("Cereal", -1) };
            }
        }

        public bool PurchaseSnack(string selectedSnack)
        {
            Snack selectedSnackItem = this.SnackStock.Find(x => x.SnackName.Equals(selectedSnack));

            int ownerCredits = this.Game.TamaOwner.Credits;
            int snackCreditValue = selectedSnackItem.CreditValue;

            if (selectedSnackItem == null)
            {
                return false;
            }
            else
            {
                if (ownerCredits >= snackCreditValue)
                {
                    this.Game.TamaOwner.Credits -= snackCreditValue;

                    Snack purchasedSnack = this.Game.SnackInventory.SnackList.Find(x => x.SnackName.Equals(selectedSnack));
                    if (purchasedSnack == null)
                    {
                        this.Game.SnackInventory.SnackList.Add(selectedSnackItem);
                    }
                    else
                    {
                        this.Game.SnackInventory.SnackList.Find(x => x.SnackName.Equals(selectedSnack)).SnackQuantity++;
                    }

                }
                return true;
            }
        }

        public bool PurchaseFood(string selectedFood)
        {
            Food selectedFoodItem = this.FoodStock.Find(x => x.FoodName.Equals(selectedFood));

            int ownerCredits = this.Game.TamaOwner.Credits;
            int foodCreditValue = selectedFoodItem.CreditValue;

            if (selectedFoodItem == null)
            {
                return false;
            }
            else
            {
                if (ownerCredits >= foodCreditValue)
                {
                    this.Game.TamaOwner.Credits -= foodCreditValue;

                    Food purchasedFood = this.Game.FoodInventory.FoodList.Find(x => x.FoodName.Equals(selectedFood));
                    if (purchasedFood == null)
                    {
                        this.Game.FoodInventory.FoodList.Add(selectedFoodItem);
                    }
                    else
                    {
                        this.Game.FoodInventory.FoodList.Find(x => x.FoodName.Equals(selectedFood)).FoodQuantity++;
                    }
                }
                return true;
            }
        }

    }
}
