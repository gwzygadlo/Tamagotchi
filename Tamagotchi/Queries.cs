using System;
using System.Collections.Generic;
using SQLite;
using Tamagotchi.Models;

namespace Tamagotchi
{
    public class Queries
    {
        public Queries()
        {
        }

        public bool Insert<T>(ref T input)
        {
            using (SQLiteConnection sqlConn = new SQLiteConnection(App._dbFilePath))
            {
                if(sqlConn.Insert(input) > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void CreateTables() // because Professor Mansfield will be mad if we call CreateTable everywhere
        {
            using (SQLiteConnection sqlConn = new SQLiteConnection(App._dbFilePath))
            {
                sqlConn.CreateTable<dbFood>();
                sqlConn.CreateTable<dbFoodInventory>();
                sqlConn.CreateTable<dbGame>();
                sqlConn.CreateTable<dbShop>();
                sqlConn.CreateTable<dbSnack>();
                sqlConn.CreateTable<dbSnackInventory>();
                sqlConn.CreateTable<dbTama>();
            }
        }

        public void UpdateTama(int tama_id, int? hunger = null, int? happiness = null, bool? is_sick = null, int? sick_counter = null)
        {
            using (SQLiteConnection sqlConn = new SQLiteConnection(App._dbFilePath))
            {
                if (hunger != null)
                {
                    sqlConn.Table<dbTama>().Where(x => x.tama_id == tama_id).FirstOrDefault().hunger = (int)hunger;
                }
                if (happiness != null)
                {
                    sqlConn.Table<dbTama>().Where(x => x.tama_id == tama_id).FirstOrDefault().happiness = (int)happiness;
                }
                if (is_sick != null)
                {
                    sqlConn.Table<dbTama>().Where(x => x.tama_id == tama_id).FirstOrDefault().is_sick = (bool)is_sick;
                }
                if (sick_counter != null)
                {
                    sqlConn.Table<dbTama>().Where(x => x.tama_id == tama_id).FirstOrDefault().sick_counter = (int)sick_counter;
                }
            }
        }

        public void UpdateOwner(int owner_id, int? credits = null)
        {
            using (SQLiteConnection sqlConn = new SQLiteConnection(App._dbFilePath))
            {
                if (credits != null)
                {
                    sqlConn.Table<dbOwner>().Where(x => x.owner_id == owner_id).FirstOrDefault().credits = (int)credits;
                }
            }
        }

        public void UpdateSnackInventory(int snack_inventory_id, int? additionQuantity = null)
        {
            using (SQLiteConnection sqlConn = new SQLiteConnection(App._dbFilePath))
            {
                if (additionQuantity != null)
                {
                    int currentQuantity = sqlConn.Table<dbSnackInventory>().Where(x => x.snack_inventory_id == snack_inventory_id).FirstOrDefault().quantity;
                    if (currentQuantity >= 0 && Math.Abs((int)additionQuantity) <= currentQuantity)
                    {
                        sqlConn.Table<dbSnackInventory>().Where(x => x.snack_inventory_id == snack_inventory_id).FirstOrDefault().quantity = currentQuantity + (int)additionQuantity;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        public int FindSnackInInventory(string snack_name)
        {
            using (SQLiteConnection sqlConn = new SQLiteConnection(App._dbFilePath))
            {
                if (!string.IsNullOrEmpty(snack_name))
                {
                    int snack_inventory_id = -1;
                    int snack_id_found = sqlConn.Table<dbSnack>().Where(x => x.name == snack_name).FirstOrDefault().snack_id;
                    if (snack_id_found >= 0)
                    {
                        snack_inventory_id = sqlConn.Table<dbSnackInventory>().Where(x => x.snack_id == snack_id_found).FirstOrDefault().snack_inventory_id;
                    }
                    return snack_inventory_id;
                }
                else
                {
                    return -1;
                }
            }
        }

        public int FindFoodInInventory(string food_name)
        {
            using (SQLiteConnection sqlConn = new SQLiteConnection(App._dbFilePath))
            {
                if (string.IsNullOrEmpty(food_name))
                {
                    int food_inventory_id = -1;
                    int food_id_found = sqlConn.Table<dbFood>().Where(x => x.name == food_name).FirstOrDefault().food_id;
                    if (food_id_found >= 0)
                    {
                        food_inventory_id = sqlConn.Table<dbFoodInventory>().Where(x => x.food_id == food_id_found).FirstOrDefault().food_inventory_id;
                    }
                    return food_inventory_id;
                }
                else
                {
                    return -1;
                }
            }
        }

        public void UpdateFoodInventory(int food_inventory_id, int? additionQuantity = null)
        {
            using (SQLiteConnection sqlConn = new SQLiteConnection(App._dbFilePath))
            {
                if (additionQuantity != null)
                {
                    int currentQuantity = sqlConn.Table<dbFoodInventory>().Where(x => x.food_inventory_id == food_inventory_id).FirstOrDefault().quantity;
                    if (currentQuantity >= 0 && Math.Abs((int)additionQuantity) <= currentQuantity)
                    {
                        sqlConn.Table<dbFoodInventory>().Where(x => x.food_inventory_id == food_inventory_id).FirstOrDefault().quantity = currentQuantity + (int)additionQuantity;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        public void UpdateGame(int game_id, int? tama_id = null, int? owner_id = null, string game_state = null, DateTime? last_time = null)
        {
            using (SQLiteConnection sqlConn = new SQLiteConnection(App._dbFilePath))
            {
                if (tama_id != null)
                {
                    sqlConn.Table<dbGame>().Where(x => x.game_id == game_id).FirstOrDefault().tama_id = (int)tama_id;
                }
                if (owner_id != null)
                {
                    sqlConn.Table<dbGame>().Where(x => x.game_id == game_id).FirstOrDefault().owner_id = (int)owner_id;
                }
                if (!String.IsNullOrEmpty(game_state))
                {
                    sqlConn.Table<dbGame>().Where(x => x.game_id == game_id).FirstOrDefault().game_state = game_state;
                }
                if (last_time != null)
                {
                    sqlConn.Table<dbGame>().Where(x => x.game_id == game_id).FirstOrDefault().last_time = (DateTime)last_time;
                }
                else
                {
                    return;
                }
            }
        }

        public void UpdateShop(int shop_id, string name = null, int? game_id = null, int? food_id = null, int? snack_id = null)
        {
            using (SQLiteConnection sqlConn = new SQLiteConnection(App._dbFilePath))
            {
                if (name != null)
                {
                    sqlConn.Table<dbShop>().Where(x => x.shop_id == shop_id).FirstOrDefault().name = name;
                }
                if (game_id != null)
                {
                    sqlConn.Table<dbShop>().Where(x => x.shop_id == shop_id).FirstOrDefault().game_id = (int)game_id;
                }
                if (food_id != null)
                {
                    sqlConn.Table<dbShop>().Where(x => x.shop_id == shop_id).FirstOrDefault().food_id = (int)food_id;
                }
                if (snack_id != null)
                {
                    sqlConn.Table<dbShop>().Where(x => x.shop_id == shop_id).FirstOrDefault().snack_id = (int)snack_id;
                }
            }
        }

        public dbOwner CreateOwner(string name = null, int? credits = null)
        {
            return new dbOwner
            {
                name = name,
                credits = (int)credits
            };
        }

        public dbFood CreateFood(string name = null, int? creditValue = null)
        {
            return new dbFood
            {
                name = name,
                credit_value = (int)creditValue
            };
        }

        public dbFoodInventory CreateFoodInventory(int? food_id = null, int? game_id = null, int? quantity = null)
        {
            return new dbFoodInventory
            {
                food_id = (int)food_id,
                game_id = (int)game_id,
                quantity = (int)quantity
            };
        }

        public dbGame CreateGame(int? tama_id = null, string game_state = null, DateTime? last_time = null)
        {
            return new dbGame
            {
                tama_id = (int)tama_id,
                game_state = game_state,
                last_time = (DateTime)last_time
            };
        }

        public dbSnack CreateSnack(string name = null, int? creditValue = null)
        {
            return new dbSnack
            {
                name = name,
                credit_value = (int)creditValue
            };
        }

        public dbSnackInventory CreateSnackInventory(int? snack_id = null, int? game_id = null, int? quantity = null)
        {
            return new dbSnackInventory
            {
                snack_id = (int)snack_id,
                game_id = (int)game_id,
                quantity = (int)quantity
            };
        }

        public dbTama CreateTama(string name = null, string species = null, int? hunger = null, int? happiness = null, bool? is_sick = null, int? sick_counter = null)
        {
            return new dbTama
            {
                name = name,
                species = species,
                hunger = (int)hunger,
                happiness = (int)happiness,
                is_sick = (bool)is_sick,
                sick_counter = (int)sick_counter
            };
        }

        public dbShop CreateShop(string name = null, int? game_id = null, int? food_id = null, int? snack_id = null)
        {
            return new dbShop
            {
                name = name,
                game_id = (int)game_id,
                food_id = (int)food_id,
                snack_id = (int)snack_id
            };
        }

        public List<dbSnack> GetSnack()
        {
            using (SQLiteConnection sqlConn = new SQLiteConnection(App._dbFilePath))
            {
                List<dbSnack> snackList = sqlConn.Table<dbSnack>().ToList();
                return snackList;
            }
        }

        public List<dbFood> GetFood()
        {
            using (SQLiteConnection sqlConn = new SQLiteConnection(App._dbFilePath))
            {
                List<dbFood> foodList = sqlConn.Table<dbFood>().ToList();
                return foodList;
            }
        }

        public int GetTamaId()
        {
            using (SQLiteConnection sqlConn = new SQLiteConnection(App._dbFilePath))
            {
                dbTama tama = sqlConn.Table<dbTama>().FirstOrDefault();
                return tama.tama_id;
            }
        }

        public int GetOwnerId()
        {
            using (SQLiteConnection sqlConn = new SQLiteConnection(App._dbFilePath))
            {
                dbOwner owner = sqlConn.Table<dbOwner>().FirstOrDefault();
                return owner.owner_id;
            }
        }

        public int GetGameId()
        {
            using (SQLiteConnection sqlConn = new SQLiteConnection(App._dbFilePath))
            {
                dbGame game = sqlConn.Table<dbGame>().FirstOrDefault();
                return game.game_id;
            }
        }
    }
}
