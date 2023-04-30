using RLNET;
using RogueSharp;
using RogueSharp1.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueSharp1.Core
{   
    //Отрисовка карты
    public class DungeonMap : Map
    {
        public List<Rectangle> Rooms;

        public DungeonMap()
        {
            Rooms = new List<Rectangle>();
        }
        public void Draw(RLConsole mapConsole)
        {
            mapConsole.Clear();
            foreach (Cell cell in GetAllCells())
            {
                SetConsoleSymbolForCell(mapConsole, cell);
            }
        }
        private void SetConsoleSymbolForCell(RLConsole console, Cell cell)
        {
            // Ничего не отрисовывать, если клетка не исследована
            if (!cell.IsExplored)
            {
                return;
            }

            // Если клетка в поле зрения
            if (IsInFov(cell.X, cell.Y))
            {
                // Символы для стен и пола
                if (cell.IsWalkable)
                {
                    console.Set(cell.X, cell.Y, Colors.FloorFov, Colors.FloorBackgroundFov, '.');
                }
                else
                {
                    console.Set(cell.X, cell.Y, Colors.WallFov, Colors.WallBackgroundFov, '#');
                }
            }
            // Если клетка за границей поля зрения
            else
            {
                if (cell.IsWalkable)
                {
                    console.Set(cell.X, cell.Y, Colors.Floor, Colors.FloorBackground, '.');
                }
                else
                {
                    console.Set(cell.X, cell.Y, Colors.Wall, Colors.WallBackground, '#');
                }
            }
        }

        public bool SetActorOnPosition(Actor actor, int x, int y)
        {
            if (GetCell(x, y).IsWalkable)
            {
                SetCellIsWalkable(actor.X, actor.Y, true);
                actor.X = x;
                actor.Y = y;
                SetCellIsWalkable(actor.X, actor.Y, false);
                if(actor is Player)
                {
                    UpdatePlayerFieldOfView();
                }
                return true;
            }
            return false;
        }
        public void SetCellIsWalkable(int x, int y, bool isWalkable)
        {
            ICell cell = GetCell( x, y);
            SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, cell.IsExplored);
        }
        public void UpdatePlayerFieldOfView()
        {
            Player player = Game.Player;
            // Рассчёт поля зрения от положения и видимости
            ComputeFov(player.X, player.Y, player.Awaraness, true);
            // изученные клетки будут подсвечиваться
            foreach (Cell cell in GetAllCells())
            {
                if (IsInFov(cell.X, cell.Y))
                {
                    SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
        }
        public void AddPlayer(Player player)
        {
            Game.Player = player;
            SetCellIsWalkable(player.X, player.Y, false);
            UpdatePlayerFieldOfView();
        }
    }
}
