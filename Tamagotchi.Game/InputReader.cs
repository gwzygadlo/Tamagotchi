using System;
using Tamagotchi.Game.Models;
using Tamagotchi.Game.States;

namespace Tamagotchi.Game
{
    public class InputReader
    {
        public InputReader(GameEngine game)
        {
            this.Game = game;
            this.MenuDeterminer(this.Game.GameState);
        }

        public void MenuDeterminer(GameState gameState)
        {
            switch (gameState)
            {
                case States.GameState.Running:
                    this.Game.Navigation = new MenuNavigation(this.Game.HomeMenu, this.Game);
                    break;
                case States.GameState.Uninitialized:
                    this.Game.Navigation = new MenuNavigation(this.Game.UninitializedMenu, this.Game);
                    break;
                case States.GameState.Gameover:
                    this.Game.Navigation = new MenuNavigation(this.Game.GameOverMenu, this.Game);
                    break;
                case States.GameState.Minigame:
                    //TODO wait for minigame menu
                    //this.Game.Navigation = new MenuNavigation(this.Game.MinigameMenu, this.Game);
                    break;
                default:
                    break;
            }
        
        }

        public void CommandLeftButton(MenuNavigation currentMenuNavigation)
        {
            currentMenuNavigation.SelectNextNode();
        }

        public void CommandRightButton(MenuNavigation currentMenuNavigation)
        {
            currentMenuNavigation.EscapeToPreviousNode();
        }

        public void CommandMiddleButton(MenuNavigation currentMenuNavigation)
        {
            currentMenuNavigation.CurrentSelectedAction();
        }

        public GameEngine Game { get; set; }
    }
}
