using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamagotchi.Game.Models;

namespace Tamagotchi.Game
{
    public class MenuNavigation
    {
        public MenuNavigation(MenuNode rootNode, GameEngine game)
        {
            this.RootNode = rootNode;
            this.CurrentNode = this.RootNode;
            this.SelectedNode = this.CurrentNode.ChildNodes[0];
            this.SelectedNodeIndex = 0;
            this.Game = game;
        }

        public MenuNode RootNode { get; set; }
        public MenuNode CurrentNode { get; set; }
        public MenuNode SelectedNode { get; set; }
        public int SelectedNodeIndex { get; set; }
        public GameEngine Game { get;set; }

        public void EscapeToPreviousNode()
        {
            if (this.CurrentNode.ParentMenuNode != null)
            {
                this.CurrentNode = this.CurrentNode.ParentMenuNode;
                this.SelectedNode = this.CurrentNode.ChildNodes[0];
                this.SelectedNodeIndex = 0;
            }
        }
        public void SelectNextNode()
        {
            //TODO have the UI grey this out
            if (this.CurrentNode.ChildNodes.Count - 1 > this.SelectedNodeIndex)
            {
                this.SelectedNode = this.CurrentNode.ChildNodes[this.SelectedNodeIndex + 1];
                this.SelectedNodeIndex++;
            }
            else
            {
                this.SelectedNode = this.CurrentNode.ChildNodes[0];
                this.SelectedNodeIndex = 0;
            }
        }
        public void CurrentSelectedAction()
        {
            if (this.SelectedNode.ChildNodes.Count > 0)
            {
                this.CurrentNode = this.SelectedNode;
                this.SelectedNode = this.CurrentNode.ChildNodes[0];
            }
            else
            {
                this.Game.DoAction(this.SelectedNode.SelectedNodeAction, this.SelectedNode.NodeText);
                this.ResetNavigation();
            }
        }
        public void ResetNavigation()
        {
            this.CurrentNode = this.RootNode;
            this.SelectedNode = this.CurrentNode.ChildNodes[0];
            this.SelectedNodeIndex = 0;
        }
    }
}
