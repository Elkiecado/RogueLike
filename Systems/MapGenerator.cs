using RogueSharp;
using RogueSharp1.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueSharp1.Systems
{
    public class MapGenerator
    {
        private readonly int _width;
        private readonly int _height;
        private readonly int _maxRooms;
        private readonly int _roomMaxSize;
        private readonly int _roomMinSize;


        private readonly DungeonMap _map;

        //Область создания карты
        public MapGenerator(int width, int height,int maxRooms, int roomMaxSize, int roomMinSize)
        {
            _width = width;
            _height = height;
            _maxRooms = maxRooms;
            _roomMaxSize = roomMaxSize;
            _roomMinSize = roomMinSize;
            _map = new DungeonMap();
        }

        // Создание карты
        public DungeonMap CreateMap()
        { 
            // Все характеристики карты на true
            _map.Initialize(_width, _height);
            for (int r = _maxRooms; r > 0; r--)
            {
                // Рандомный размер и координаты комнаты
                int roomWidth = Game.Random.Next(_roomMinSize, _roomMaxSize);
                int roomHeight = Game.Random.Next(_roomMinSize, _roomMaxSize);
                int roomXPosition = Game.Random.Next(0, _width - roomWidth - 1);
                int roomYPosition = Game.Random.Next(0, _height - roomHeight - 1);

                // Комнаты - прямоугольники
                var newRoom = new Rectangle(roomXPosition, roomYPosition,
                  roomWidth, roomHeight);

                bool newRoomIntersects = _map.Rooms.Any(room => newRoom.Intersects(room));

                // Добавлять новые комнаты если не пересекаются с другими
                if (!newRoomIntersects)
                {
                    _map.Rooms.Add(newRoom);
                }
            }
            foreach (Rectangle room in _map.Rooms)
            {
                CreateRoom(room);
            }
            for (int r = 1; r < _map.Rooms.Count; r++)
            {
                // центр текущей и прошлой комнаты
                int previousRoomCenterX = _map.Rooms[r - 1].Center.X;
                int previousRoomCenterY = _map.Rooms[r - 1].Center.Y;
                int currentRoomCenterX = _map.Rooms[r].Center.X;
                int currentRoomCenterY = _map.Rooms[r].Center.Y;


                CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, previousRoomCenterX);
                CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, currentRoomCenterY);
            
            }

            PlacePlayer();
            return _map;
        }
        private void CreateRoom(Rectangle room)
        {
            for (int x = room.Left + 1; x < room.Right; x++)
            {
                for (int y = room.Top + 1; y < room.Bottom; y++)
                {
                    _map.SetCellProperties(x, y, true, true, true);
                }
            }
        }
        private void PlacePlayer()
        {
            Player player = Game.Player;
            player ??= new Player();
            player.X = _map.Rooms[0].Center.X;
            player.Y = _map.Rooms[0].Center.Y;
            _map.AddPlayer(player);
        }
        //создание горизонтального пути
        private void CreateHorizontalTunnel(int xStart, int xEnd, int yPosition)
        {
            for (int x = Math.Min(xStart, xEnd); x <= Math.Max(xStart, xEnd); x++)
            {
                _map.SetCellProperties(x, yPosition, true, true);
            }
        }
        // вертикальный
        private void CreateVerticalTunnel(int yStart, int yEnd, int xPosition)
        {
            for (int y = Math.Min(yStart, yEnd); y <= Math.Max(yStart, yEnd); y++)
            {
                _map.SetCellProperties(xPosition, y, true, true);
            }
        }
    }
    

}
