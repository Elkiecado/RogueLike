using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueSharp1.Assets
{
    public class Colors
    {
        public static RLColor FloorBackground = RLColor.Black;//цвет для  клеток вне поле зрения
        public static RLColor Floor = Canvas.AlternateDarkest;
        public static RLColor FloorBackgroundFov = Canvas.DbDark; //цвет для клеток в поле зрения
        public static RLColor FloorFov = Canvas.Alternate;

        public static RLColor WallBackground = Canvas.SecondaryDarkest;
        public static RLColor Wall = Canvas.Secondary;
        public static RLColor WallBackgroundFov = Canvas.SecondaryDarker;
        public static RLColor WallFov = Canvas.SecondaryLighter;

        public static RLColor TextHeading = Canvas.DbLight;
        public static RLColor Player = Canvas.DbLight;
    }
}
