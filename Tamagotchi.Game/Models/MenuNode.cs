using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamagotchi.Models;

namespace Tamagotchi.Game.Models
{
    public class MenuNode
    {
        public MenuNode(GameEngine game)
        {
            this.Game = game;
        }

        public GameEngine Game { get; set; }
        public string NodeText { get; set; }
        public MenuNode ParentMenuNode { get; set; }
        public List<MenuNode> ChildNodes { get; set; }
        public dbSelectedAction SelectedNodeAction { get; set; }

        public void AddChildNode(MenuNode node)
        {
            node.ParentMenuNode = this;
            this.ChildNodes.Add(node);
        }
    }
}