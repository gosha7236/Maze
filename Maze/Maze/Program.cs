using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Labirint
{
    internal class Program
    {
        static char[,] map;
        static int playerX, playerY;
        static void Main(string[] args)
        {
            Console.CursorVisible = false; // убирает моргающий курсор

            // Загружаем карту
            LoadMap("map.txt");

            // Рисуем карту
            DrawMap();

            // Игровой цикл
            while (true)
            {
                Console.SetCursorPosition(playerX, playerY);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("P"); // игрок
                Console.ResetColor();

                var key = Console.ReadKey(true).Key; // ждет нажатия клавиши

                int newX = playerX, newY = playerY;

                switch (key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow: newY--; break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow: newY++; break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow: newX--; break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow: newX++; break;
                }

                if (map[newY, newX] != '#') // не стена
                {
                    // Стираем игрока со старой позиции
                    Console.SetCursorPosition(playerX, playerY);
                    Console.Write(map[playerY, playerX]);

                    playerX = newX;
                    playerY = newY;
                }

                // Проверка на выход
                if (map[playerY, playerX] == 'E')
                {
                    Console.Clear();
                    Console.WriteLine("🎉 Победа! Вы нашли выход!");
                    break;
                }
            }
        }

        static void LoadMap(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            int height = lines.Length;
            int width = lines[0].Length;

            map = new char[height, width];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    map[y, x] = lines[y][x];
                    if (map[y, x] == 'P') // старт игрока
                    {
                        playerX = x;
                        playerY = y;
                        map[y, x] = ' '; //  заменяем на пустоту
                    }
                }
            }
        }

        static void DrawMap()
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (map[y, x] == '#')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write('#');
                        Console.ResetColor();
                    }
                    else if (map[y, x] == 'E')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write('E');
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(map[y, x]);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
