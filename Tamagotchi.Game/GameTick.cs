using System;
using System.Collections.Generic;
using System.Text;

namespace Tamagotchi.Game
{
    public class GameTick
    {
        public GameTick(GameEngine game)
        {
            this.TickPeriod = TimeSpan.FromHours(3);
            this.TimeBetweenChecks = TimeSpan.FromMinutes(15);
            this.Game = game;
        }

        private TimeSpan TickPeriod { get; set; }
        private TimeSpan TimeBetweenChecks { get; set; }
        private GameEngine Game { get; set; }
        
        public void TickGame()
        {
            DateTime currentTime = DateTime.Now;
            DateTime lastTime = this.Game.LastTime;
            TimeSpan timeDiff = currentTime - lastTime;
            if (timeDiff > TickPeriod)
            {
                int gameTicksPassed = (int)Math.Floor(timeDiff.Ticks / (double)this.TickPeriod.Ticks);
                TimeSpan leftoverTime = TimeSpan.FromTicks(timeDiff.Ticks - gameTicksPassed * this.TickPeriod.Ticks);
                this.Game.LastTime = currentTime - leftoverTime;
                if (!this.Game.Tama.DetermineSickness())
                {
                    this.Game.Tama.IncrementHappiness(-gameTicksPassed);
                    this.Game.Tama.IncrementHunger(-gameTicksPassed);
                }
                if (this.Game.Tama.SickCounter >= 3)
                {
                    this.Game.GameState = States.GameState.Gameover;
                }
            }

            int tama_id = this.Game.Query.GetTamaId();
            int owner_id = this.Game.Query.GetOwnerId();
            int game_id = this.Game.Query.GetGameId();

            this.Game.Query.UpdateTama(tama_id, this.Game.Tama.Hunger, this.Game.Tama.Happiness, this.Game.Tama.IsSick, this.Game.Tama.SickCounter);
            this.Game.Query.UpdateGame(game_id, tama_id, owner_id, this.Game.GameState.ToString(), currentTime);
            this.Game.Query.UpdateOwner(owner_id, this.Game.TamaOwner.Credits);
        }

        public void StartTicking() //call this when the app is open if the game is running
        {
            this.TickGame();
            System.Threading.Timer timer = new System.Threading.Timer((e) =>
            {
                this.TickGame();
            }, null, TimeSpan.Zero, this.TimeBetweenChecks);
        }
    }
}
