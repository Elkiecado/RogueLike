using RogueSharp1.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueSharp1.Systems
{
    public class SystemOfCommands
    {
        public bool PlayerMovement(Directions direction)
        {
            int x = Game.Player.X;
            int y = Game.Player.Y;
            switch (direction)
            {
                case Directions.Left:
                    {
                        x = Game.Player.X - 1;
                        break;
                    }
                case Directions.Right:
                    {
                        x = Game.Player.X + 1;
                        break;
                    }
                case Directions.Up:
                    {
                        y = Game.Player.Y - 1;
                        break;
                    }
                case Directions.Down:
                    {
                        y = Game.Player.Y + 1;
                        break;
                    }
                default:
                    {
                        return false;
                    }
            }
            if(Game.DungeonMap.SetActorOnPosition(Game.Player, x, y))
            {
                return true;
            }
            return false;
        }
    }
}
