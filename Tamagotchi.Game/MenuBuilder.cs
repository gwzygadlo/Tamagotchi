using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamagotchi.Models;

namespace Tamagotchi.Game.Models
{
    public class MenuBuilder
    {
        public MenuBuilder(GameRunner runner)
        {
            this.Runner = runner;
            this.Queries = new Queries();
        }

        public GameRunner Runner { get; set; }
        public Queries Queries { get; set; }

        public MenuNode BuildHomeMenu()
        {
            MenuNode rootNode = this.BuildNewNode("Root", new List<MenuNode>());
            rootNode.AddChildNode(this.BuildNewNode("Inventory", new List<MenuNode>()));
            rootNode.AddChildNode(this.BuildNewNode("Medicine", null, dbSelectedAction.Medicate));

            MenuNode inventoryMenu = rootNode.ChildNodes[0];
            inventoryMenu.AddChildNode(this.BuildNewNode("Food", new List<MenuNode>()));
            inventoryMenu.AddChildNode(this.BuildNewNode("Snacks", new List<MenuNode>()));

            MenuNode food = inventoryMenu.ChildNodes[0];
            List<dbFood> foodList = this.Queries.GetFood();
            foreach (dbFood item in foodList)
            {
                food.AddChildNode(this.BuildNewNode(item.name, new List<MenuNode>(), item.selected_action));
            }

            MenuNode snack = inventoryMenu.ChildNodes[1];
            List<dbSnack> snackList = this.Queries.GetSnack();
            foreach (dbSnack item in snackList)
            {
                snack.AddChildNode(this.BuildNewNode(item.name, new List<MenuNode>(), item.selected_action));
            }

            return rootNode;
        }

        //TODO make methods for uninitialized menus
        public MenuNode BuildUninitializedMenu()
        {
            MenuNode rootNode = this.BuildNewNode("Root", new List<MenuNode>());
            for (int i = 65; i < 91; i++)
            {
                rootNode.AddChildNode(this.BuildNewNode(Convert.ToChar(i).ToString(), new List<MenuNode>()));//, this.Game.test)); //need alphabet method for each one and then a tama name string builder
            }
            rootNode.AddChildNode(this.BuildNewNode("Space", new List<MenuNode>()));//, this.Game.test)); //filler
            rootNode.AddChildNode(this.BuildNewNode("Backspace", new List<MenuNode>()));//, this.Game.test)) ; //filler
            rootNode.AddChildNode(this.BuildNewNode("Done", new List<MenuNode>()));//, this.Game.test)); //filler

            return rootNode;
        }

        public MenuNode BuildGameoverMenu()
        {
            MenuNode rootNode = this.BuildNewNode("Root", new List<MenuNode>());
            rootNode.AddChildNode(this.BuildNewNode("Yes", new List<MenuNode>(), dbSelectedAction.StartOver));
            rootNode.AddChildNode(this.BuildNewNode("No", new List<MenuNode>(), dbSelectedAction.GameOver));
            return rootNode;
        }

        //TODO make a minigame menu

        public MenuNode BuildNewNode(string nodeText, List<MenuNode> childNodes, dbSelectedAction? action = null)
        {
            MenuNode node = new MenuNode(this.Runner.Game)
            {
                NodeText = nodeText,
                ChildNodes = childNodes,
                SelectedNodeAction = (dbSelectedAction)action
            };
            return node;
        }
    }
}
