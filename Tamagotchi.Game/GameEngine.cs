using System;
using System.Collections.Generic;
using System.Text;
using Tamagotchi.Game.Models;
using Tamagotchi.Game.States;
using Tamagotchi.Models;

namespace Tamagotchi.Game
{
    public class GameEngine
    {
        public GameEngine()
        {
        }

        public void InitializeGame()
        {
            this.GameState = GameState.Uninitialized;
            this.Runner = new GameRunner(this);
            this.Builder = new MenuBuilder(this.Runner);
            this.TamaOwner = new Models.Owner();
            this.SnackInventory = new Models.SnackInventory();
            this.FoodInventory = new Models.FoodInventory();
            this.LastTime = DateTime.Now;
            this.Query = new Queries();
            this.dbGame = Queries.CreateGame(game_state: this.GameState.ToString(), last_time: this.LastTime);
        }


        public void InitializeOwner(string name)
        {
            this.TamaOwner.Name = name;
            this.dbOwner = Queries.CreateOwner(name: this.TamaOwner.Name, credits: this.TamaOwner.Credits);
            Queries.UpdateGame(game_id: this.dbGame.game_id, owner_id: this.dbOwner.owner_id);
        }

        public List<Snack> InitializeSnackList(List<Snack> snackList)
        {
            if (snackList == null || snackList.Count != 0)
            {
                snackList.Add(new Snack("Apple", 5));
                dbSnack apple = Queries.CreateSnack("Apple", 100);
                dbSnackInventory snackInventoryApple = Queries.CreateSnackInventory(apple.snack_id, this.dbGame.game_id, 5);
                snackList.Add(new Snack("Ice Cream", 5));
                Tamagotchi.Models.dbSnack iceCream = Queries.CreateSnack("Ice Cream", 100);
                Tamagotchi.Models.dbSnackInventory snackInventoryIceCream = Queries.CreateSnackInventory(iceCream.snack_id, this.dbGame.game_id, 5);
                snackList.Add(new Snack("Pineapple", 5));
                Tamagotchi.Models.dbSnack pineapple = Queries.CreateSnack("Pineapple", 100);
                Tamagotchi.Models.dbSnackInventory snackInventoryPineapple = Queries.CreateSnackInventory(pineapple.snack_id, this.dbGame.game_id, 5);
                return snackList;
            }
            else
            {
                Tamagotchi.Models.dbSnack apple = Queries.CreateSnack("Apple", 100);
                Tamagotchi.Models.dbSnackInventory snackInventoryApple = Queries.CreateSnackInventory(apple.snack_id, this.dbGame.game_id, 5);
                Tamagotchi.Models.dbSnack iceCream = Queries.CreateSnack("Ice Cream", 100);
                Tamagotchi.Models.dbSnackInventory snackInventoryIceCream = Queries.CreateSnackInventory(iceCream.snack_id, this.dbGame.game_id, 5);
                Tamagotchi.Models.dbSnack pineapple = Queries.CreateSnack("Pineapple", 100);
                Tamagotchi.Models.dbSnackInventory snackInventoryPineapple = Queries.CreateSnackInventory(pineapple.snack_id, this.dbGame.game_id, 5);

                return new List<Snack>() { new Snack("Apple", 5), new Snack("Ice Cream", 5), new Snack("Pineapple", 5) };
            }
        }

        public List<Models.Food> InitializeFoodList(List<Models.Food> foodList)
        {
            if (foodList == null || foodList.Count != 0)
            {
                foodList.Add(new Food("Bread", 5));
                Tamagotchi.Models.dbFood bread = Queries.CreateFood("Bread", 100);
                Tamagotchi.Models.dbFoodInventory foodInventoryBread = Queries.CreateFoodInventory(bread.food_id, this.dbGame.game_id, 5);
                foodList.Add(new Food("Cereal", 5));
                Tamagotchi.Models.dbFood cereal = Queries.CreateFood("Cereal", 100);
                Tamagotchi.Models.dbFoodInventory foodInventoryCereal = Queries.CreateFoodInventory(cereal.food_id, this.dbGame.game_id, 5);
                return foodList;
            }
            else
            {
                Tamagotchi.Models.dbFood bread = Queries.CreateFood("Bread", 100);
                Tamagotchi.Models.dbFoodInventory foodInventoryBread = Queries.CreateFoodInventory(bread.food_id, this.dbGame.game_id, 5);
                Tamagotchi.Models.dbFood cereal = Queries.CreateFood("Cereal", 100);
                Tamagotchi.Models.dbFoodInventory foodInventoryCereal = Queries.CreateFoodInventory(cereal.food_id, this.dbGame.game_id, 5);

                return new List<Food>() { new Food("Bread", 5), new Food("Cereal", 5) };
            }
        }

        public void InitializeTama(string tamaName, string tamaSpecies)
        {
            this.Tama = new Models.Tama();
            this.Tama.Hunger = 8;
            this.Tama.Happiness = 8;

            this.dbTama = Queries.CreateTama(tamaName, tamaSpecies, 8, 8, false, 0);
            Queries.UpdateGame(this.dbGame.game_id, dbTama.tama_id);
        }

        public void InitializeMenus()
        {
            this.HomeMenu = this.Builder.BuildHomeMenu();
            this.UninitializedMenu = this.Builder.BuildUninitializedMenu();
            this.GameOverMenu = this.Builder.BuildGameoverMenu();

        }

        public bool SelectSnack(string snackSelection)
        {
            if (this.Tama.Happiness < 8)
            {
                Snack selectedSnack = this.SnackInventory.SnackList.Find(x => x.SnackName.Equals(snackSelection));
                if (selectedSnack.SnackQuantity >= 1)
                {
                    this.SnackInventory.SnackList.Find(x => x.SnackName.Equals(snackSelection)).SnackQuantity--;
                    int snack_inventory_id = this.Queries.FindSnackInInventory(selectedSnack.SnackName);
                    Queries.UpdateSnackInventory(snack_inventory_id, -1);
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

        public bool SelectFood(string foodSelection)
        {
            if (this.Tama.Hunger < 8)
            {
                Food selectedFood = this.FoodInventory.FoodList.Find(x => x.FoodName.Equals(foodSelection));
                if (selectedFood.FoodQuantity >= 1)
                {
                    this.FoodInventory.FoodList.Find(x => x.FoodName.Equals(foodSelection)).FoodQuantity--;
                    int food_inventory_id = this.Queries.FindFoodInInventory(selectedFood.FoodName);
                    Queries.UpdateFoodInventory(food_inventory_id, -1);
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
        public bool MedicateTama()
        {
            if (this.Tama.IsSick && (this.Tama.SickCounter == 1 || this.Tama.SickCounter == 0))
            {
                this.Tama.IsSick = false;
                if (this.Tama.SickCounter > 0)
                {
                    this.Tama.SickCounter--;
                }
                Queries.UpdateTama(tama_id: this.dbTama.tama_id, is_sick: false, sick_counter: this.Tama.SickCounter);
                return true;
            }
            if (this.Tama.IsSick && this.Tama.SickCounter > 1)
            {
                this.Tama.IsSick = true;
                this.Tama.SickCounter--;
                Queries.UpdateTama(tama_id: this.dbTama.tama_id, is_sick: true, sick_counter: this.Tama.SickCounter);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool FeedSnackTama(string snackSelection)
        {
            bool success = SelectSnack(snackSelection);
            if (success)
            {
                this.Tama.IncrementHappiness(1);
                Queries.UpdateTama(this.dbTama.tama_id, this.Tama.Happiness);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool FeedFoodTama(string foodSelection)
        {
            bool success = SelectFood(foodSelection);
            if (success)
            {
                this.Tama.IncrementHunger(1);
                Queries.UpdateTama(tama_id: this.dbTama.tama_id, hunger: this.Tama.Hunger);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DoAction(dbSelectedAction action, string name)
        {
            switch (action)
            {
                case dbSelectedAction.Medicate:
                    this.MedicateTama();
                    break;
                case dbSelectedAction.FeedFood:
                    this.FeedFoodTama(name);
                    break;
                case dbSelectedAction.FeedSnack:
                    this.FeedSnackTama(name);
                    break;
                case dbSelectedAction.StartOver:
                    this.Runner.StartOver("Tama", "Species");
                    break;
                case dbSelectedAction.GameOver:
                    this.Runner.GameOver();
                    break;
                default:
                    break;
            }
        }

        public GameState GameState { get; set; }
        public MenuNode HomeMenu { get; set; }
        public MenuNode UninitializedMenu { get; set; }
        public MenuNode GameOverMenu { get; set; }
        public MenuBuilder Builder { get; set; }
        public MenuNavigation Navigation { get; set; }
        public Models.Tama Tama { get; set; }
        public Models.Owner TamaOwner { get; set; }
        public DateTime LastTime { get; internal set; }
        public Queries Query { get; set; }
        public FoodInventory FoodInventory { get; set; }
        public SnackInventory SnackInventory { get; set; }
        public Queries Queries { get; set; }
        public Tamagotchi.Models.dbGame dbGame { get; set; }
        public Tamagotchi.Models.dbOwner dbOwner { get; set; }
        public Tamagotchi.Models.dbTama dbTama { get; set; }
        public GameRunner Runner { get; set; }
    }
}
