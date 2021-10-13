using System;
using Tamagotchi.Game.Models;
using Tamagotchi.Game.States;
using Tamagotchi.Game;

namespace Tamagotchi.Game
{
    public class GameRunner
    {
        public GameRunner(GameEngine game)
        {
            this.Game = game;
            this.Reader = new InputReader(this.Game);
        }

        public void StartGame(string ownerName, string tamaName, string tamaSpecies)
        {
            this.Reader.Game.GameState = GameState.Uninitialized;
            this.Reader.Game.Queries.UpdateGame(this.Reader.Game.dbGame.game_id, game_state: "Uninitialized");
            this.Reader.Game.InitializeTama(tamaName, tamaSpecies);
            this.Reader.Game.InitializeMenus();
            this.Reader.Game.InitializeOwner(ownerName);
            this.Reader.Game.InitializeFoodList(this.Reader.Game.FoodInventory.FoodList);
            this.Reader.Game.InitializeSnackList(this.Reader.Game.SnackInventory.SnackList);
            this.Reader.MenuDeterminer(this.Reader.Game.GameState);
        }

        public void RunGame()
        {
            this.Reader.Game.GameState = GameState.Running;
            this.Reader.Game.Queries.UpdateGame(this.Reader.Game.dbGame.game_id, game_state: "Running");
            this.Reader.MenuDeterminer(this.Reader.Game.GameState);
        }

        public void MiniGame()
        {
            this.Reader.Game.GameState = GameState.Minigame;
            this.Reader.Game.Queries.UpdateGame(this.Reader.Game.dbGame.game_id, game_state: "Minigame");
            this.Reader.MenuDeterminer(this.Reader.Game.GameState);
        }

        public void GameOver()
        {
            this.Reader.Game.GameState = GameState.Gameover;
            this.Reader.Game.Queries.UpdateGame(this.Reader.Game.dbGame.game_id, game_state: "GameOver");
            this.Reader.MenuDeterminer(this.Reader.Game.GameState);
        }

        public void StartOver(string tamaName, string tamaSpecies)
        {
            this.Reader.Game.GameState = GameState.Uninitialized;
            this.Reader.Game.Queries.UpdateGame(this.Reader.Game.dbGame.game_id, game_state: "Uninitialized");
            this.Reader.Game.InitializeTama(tamaName, tamaSpecies);
            this.Reader.Game.InitializeMenus();
            this.Reader.Game.InitializeFoodList(this.Reader.Game.FoodInventory.FoodList);
            this.Reader.Game.InitializeSnackList(this.Reader.Game.SnackInventory.SnackList);
            this.Reader.MenuDeterminer(this.Reader.Game.GameState);
        }

        public InputReader Reader { get; set; }
        public GameEngine Game { get; set; }
    }
}
