using RLNET;
using RogueSharp.Random;
using RogueSharp1.Core;
using RogueSharp1.Systems;
using System;

namespace RogueSharp1
{
    public class Game
    {

        private static bool _renderRequired = true;

        public static SystemOfCommands CommandSystem { get; private set; }

        //Основаное поле
        private static readonly int _screenWidth = 100; //Ширина
        private static readonly int _screenHeight = 70; //Высота

        private static RLRootConsole? rootConsole;

        //Поле для карты
        private static readonly int _mapWidth = 80;
        private static readonly int _mapHeight = 48;

        private static RLConsole? mapConsole;
        
        //Поле событий
        private static readonly int _messageWidth = 80;
        private static readonly int _messageHeight = 11;

        private static RLConsole? newsFeedConsole;

        //Характеристики
        private static readonly int _statWidth = 20;
        private static readonly int _statHeight = 70;

        private static RLConsole? statConsole;

        //Инвентарь
        private static readonly int _inventoryWidth = 80;
        private static readonly int _inventoryHeight = 11;

        private static RLConsole? inventoryConsole;
        
        public static DungeonMap? DungeonMap { get; private set; }

        public static Player? Player { get; set; }
        public static IRandom Random { get; private set; }
        public static void Main()
        {
            int seed  = (int) DateTime.UtcNow.Ticks;
            Random = new DotNetRandom(seed);
            string bitmapFile = "ascii_8x8.png";
            string consoleTitle = $"RougeSharp  - Level - Seed {seed}";
            CommandSystem = new SystemOfCommands();
            rootConsole = new RLRootConsole(bitmapFile, _screenWidth, _screenHeight, 8, 8, 1f, consoleTitle);
            mapConsole = new RLConsole(_mapWidth, _mapHeight);
            newsFeedConsole = new RLConsole(_messageWidth, _messageHeight);
            statConsole = new RLConsole(_statWidth, _statHeight);
            inventoryConsole = new RLConsole(_inventoryWidth, _inventoryHeight);
            MapGenerator mapGenerator = new MapGenerator(_mapWidth, _mapHeight, 20, 13 ,7);
            DungeonMap = mapGenerator.CreateMap();
            DungeonMap.UpdatePlayerFieldOfView();
            rootConsole.Render += rootConsole_Render;
            rootConsole.Update += rootConsole_Update;
            rootConsole.Run();

        }

        private static void rootConsole_Update(object sender, UpdateEventArgs e)
        {
            mapConsole.SetBackColor(0, 0, _mapWidth, _mapHeight, RLColor.Black);
            mapConsole.Print(1, 1, "Map", RLColor.White);

            newsFeedConsole.SetBackColor(0, 0, _messageWidth, _messageHeight, RLColor.Gray);
            newsFeedConsole.Print(1, 1, "Messages", RLColor.White);

            statConsole.SetBackColor(0, 0, _statWidth, _statHeight, RLColor.Brown);
            statConsole.Print(1, 1, "Stats", RLColor.White);

            inventoryConsole?.SetBackColor(0, 0, _inventoryWidth, _inventoryHeight, RLColor.Cyan);
            inventoryConsole?.Print(1, 1, "Inventory", RLColor.White);

            bool didPlayerAct = false;
            RLKeyPress? keyPress = rootConsole?.Keyboard.GetKeyPress();

            if(keyPress != null)
            {
                if(keyPress.Key == RLKey.Up)
                {
                    didPlayerAct = CommandSystem.PlayerMovement(Directions.Up);

                }
                else if(keyPress.Key == RLKey.Down)
                {
                    didPlayerAct = CommandSystem.PlayerMovement(Directions.Down);

                }
                else if(keyPress.Key == RLKey.Left)
                {
                    didPlayerAct = CommandSystem.PlayerMovement(Directions.Left);

                }
                else if(keyPress.Key == RLKey.Right)
                {
                    didPlayerAct = CommandSystem.PlayerMovement(Directions.Right);

                }
            }
            if(didPlayerAct)
            {
                _renderRequired = true;
            }

        }

        private static void rootConsole_Render(object sender, UpdateEventArgs e)
        {

            if (_renderRequired)
            {
                DungeonMap.Draw(mapConsole);
                Player.Draw(mapConsole, DungeonMap);

                RLConsole.Blit(mapConsole, 0, 0, _mapWidth, _mapHeight,
                  rootConsole, 0, _inventoryHeight);
                RLConsole.Blit(statConsole, 0, 0, _statWidth, _statHeight,
                  rootConsole, _mapWidth, 0);
                RLConsole.Blit(newsFeedConsole, 0, 0, _messageWidth, _messageHeight,
                  rootConsole, 0, _screenHeight - _messageHeight);
                RLConsole.Blit(inventoryConsole, 0, 0, _inventoryWidth, _inventoryHeight,
                  rootConsole, 0, 0);
                rootConsole.Draw();

                _renderRequired = false;
            }
            
        }
    }
}