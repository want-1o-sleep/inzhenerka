using System;
using System.Reflection.Metadata;

namespace Game
{
    internal class Program
    {
        private const int ScreenWidth = 150;
        private const int ScreenHeight = 50;

        private const int MapWidth = 32;
        private const int MapHeight = 32;

        private const double Fov = Math.PI / 3; //обзор playera
        private const double Depth = 16; //точка обзора\горизонта

        private static double _playerX; //перемещение игрока
        private static double _playerY;
        private static double _playerA = 0;

        private static string _map = "";


        private static readonly char[] Screen = new char[ScreenWidth * ScreenHeight];
        static void Main(string[] args)
        {
            Console.SetWindowSize(ScreenWidth, ScreenHeight);
            Console.SetBufferSize(ScreenWidth, ScreenHeight);  // размер окна
            Console.OutputEncoding = System.Text.Encoding.UTF8;  //перевод кодировки
            Console.CursorVisible = false;  //курсор невидемый

            _map += "################################"; //решетка = стена
            _map += "#..............................#";
            _map += "#..............................#"; // точка - пустота
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "#..............................#";
            _map += "################################";
        }


        private static void RenderFrame()
        {
            for (int x = 0; x < ScreenWidth; x++) //fps
            {
                double rayAngle = _playerA + Fov / 2 - x * Fov / ScreenWidth;

                double rayX = Math.Sin(rayAngle);
                double rayY = Math.Cos(rayAngle);

                double distanceToWall = 0; //дистанция до стены
                bool hitWall = false;

                while(!hitWall && distanceToWall < Depth)
                {
                    distanceToWall += 0.1; // увеличение дистанции пока летит луч

                    int testX = (int)(_playerX + rayX * distanceToWall);
                    int testY = (int)(_playerY + rayY * distanceToWall);

                    if (testX < 0 || testX >= MapWidth || testY <0 || testY >= MapHeight)
                    {
                        hitWall = true;
                        distanceToWall = Depth;
                    }
                    else
                    {
                        char testCell = _map[testY * MapWidth + testX];

                        if (testCell == '#')
                        {
                            hitWall = true;
                        }
                    }
                }

                int ceiling = (int)(ScreenHeight / 2d - ScreenHeight + Fov / distanceToWall);
                int floor = ScreenHeight - ceiling;

                char wallshade;

                if (distanceToWall < Depth / 4d)
                {
                    wallshade = '\u2588';
                }
                else if (distanceToWall < Depth / 3d)
                {
                    wallshade = '\u2593';
                }
                else if (distanceToWall < Depth / 2d)
                {
                    wallshade = '\u2592';
                }
                else if (distanceToWall < Depth)
                {
                    wallshade = '\u2591';
                }
                else
                {
                    wallshade = ' ';
                }

                for (int y = 0; y < ScreenHeight; y++)
                {
                    if (y <= ceiling)
                    {
                        Screen[y * ScreenWidth + x] = ' ';
                    }
                    else if (y > ceiling && y <= floor) 
                    {
                        Screen[y * ScreenWidth + x] = wallshade;
                    }
                    else
                    {
                        Screen[y * ScreenWidth + x] = '.';
                    }
                }
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(Screen);
        }

    }

}
