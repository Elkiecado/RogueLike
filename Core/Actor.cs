using RLNET;
using RogueSharp;
using RogueSharp1.Assets;
using RogueSharp1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueSharp1.Core
{
    public class Actor : IActor, IDrawable
    {
        public string Name { get; set; }
        public int Awaraness { get; set; }

        public RLColor Color { get; set; }
        public char Symbol { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public void Draw(RLConsole console, IMap map)
        {
            if (!map.GetCell(X, Y).IsExplored)
            {
                return;
            }

            // отрисовка только если в поле зрения
            if (map.IsInFov(X, Y))
            {
                console.Set(X, Y, Color, Colors.FloorBackgroundFov, Symbol);
            }
            else
            {
                // Не изученные клетки вне полезрения
                console.Set(X, Y, Colors.Floor, Colors.FloorBackground, '.');
            }
        }
    }
}
