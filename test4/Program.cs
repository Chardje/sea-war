using System;
using System.Diagnostics;

namespace test4
{
    /// <summary>
    /// Состояние ячейки
    /// </summary>
    struct CellType
    {
        /// <summary>
        /// есть или нет корабль в этой ячейке
        /// </summary>
        internal bool ship;
        /// <summary>
        /// стреляли или нет в эту ячейку
        /// </summary>
        internal bool fire;
    }

    class Program
    {
        const int N = 10;

        static void Main(string[] args)
        {
            var rand = new Random();
            CellType[,] polev1 = RasstavitKorabli(rand);

            Console.WriteLine("Морской бой");
            Console.WriteLine("~ пустота O промах X подбит корабль");

            while (true)
            {
                NapechatatPole(polev1);
                {
                    string line = Console.ReadLine();
                    string[] coords = line.Trim().Split(' ');
                    if (coords.Length == 2 && int.Parse(coords[0]) <= N && int.Parse(coords[1]) <= N
                        && int.Parse(coords[1]) > 0 && int.Parse(coords[0]) > 0)
                    {
                        int row = int.Parse(coords[0]);
                        int col = int.Parse(coords[1]);
                        if (!polev1[row, col].fire)
                        {
                            polev1[row, col].fire = true;
                        }
                        else
                        {
                            Console.WriteLine("Вы пропустили ход");
                        }
                    }
                    else
                    {
                        Console.WriteLine("неправильное количество координат ");
                    }

                }
            }
        }
        //dsadasfdsargga
        private static CellType[,] RasstavitKorabli(Random rand)
        {
            CellType[,] pole = new CellType[N + 2, N + 2];
            #region Расставляем 4-клеточные корабли
            for (int i = 0; i < 1;)
            {
                int vert = rand.Next(2);
                int left, top, right, bottom;
                if (vert != 0)
                {
                    left = rand.Next(N) + 1;
                    top = rand.Next(N - 3) + 1;
                    right = left;
                    bottom = top + 3;
                }
                else
                {
                    left = rand.Next(N - 3) + 1;
                    top = rand.Next(N) + 1;
                    right = left + 3;
                    bottom = top;
                }

                if (UzheEst(pole, left - 1, top - 1, right + 1, bottom + 1))
                {
                    //Console.WriteLine($"Уже есть {left}:{top}");
                }
                else
                {
                    pole[left, top].ship = true;
                    pole[(2 * left + right) / 3, (2 * top + bottom) / 3].ship = true;
                    pole[(left + 2 * right) / 3, (top + 2 * bottom) / 3].ship = true;
                    pole[right, bottom].ship = true;
                    i += 1;
                }
            }
            #endregion
            #region Расставляем 3-клеточные корабли
            for (int i = 0; i < 2;)
            {
                int vert = rand.Next(2);
                int left, top, right, bottom;
                if (vert != 0)
                {
                    left = rand.Next(N) + 1;
                    top = rand.Next(N - 2) + 1;
                    right = left;
                    bottom = top + 2;
                }
                else
                {
                    left = rand.Next(N - 2) + 1;
                    top = rand.Next(N) + 1;
                    right = left + 2;
                    bottom = top;
                }

                if (UzheEst(pole, left - 1, top - 1, right + 1, bottom + 1))
                {
                    //Console.WriteLine($"Уже есть {left}:{top}");
                }
                else
                {
                    pole[left, top].ship = true;
                    pole[(left + right) / 2, (top + bottom) / 2].ship = true;
                    pole[right, bottom].ship = true;
                    i += 1;
                }
            }
            #endregion
            #region Расставляем 2-клеточные корабли
            for (int i = 0; i < 3;)
            {
                int vert = rand.Next(2);
                int left, top, right, bottom;
                if (vert != 0)
                {
                    left = rand.Next(N) + 1;
                    top = rand.Next(N - 1) + 1;
                    right = left;
                    bottom = top + 1;
                }
                else
                {
                    left = rand.Next(N - 1) + 1;
                    top = rand.Next(N) + 1;
                    right = left + 1;
                    bottom = top;
                }

                if (UzheEst(pole, left - 1, top - 1, right + 1, bottom + 1))
                {
                    //Console.WriteLine($"Уже есть {left}:{top}");
                }
                else
                {
                    pole[left, top].ship = true;
                    pole[right, bottom].ship = true;
                    i += 1;
                }
            }
            #endregion
            #region Расставляем 1-клеточные корабли
            for (int i = 0; i < 4;)
            {
                int row = rand.Next(N) + 1;
                int col = rand.Next(N) + 1;
                if (UzheEst(pole, row - 1, col - 1, row + 1, col + 1))
                {
                    //Console.WriteLine($"Уже есть {row}:{col}");
                }
                else
                {
                    pole[row, col].ship = true;
                    i += 1;
                }
            }
            #endregion
            return pole;
        }

        private static bool UzheEst(CellType[,] pole, int r0, int c0, int r1, int c1)
        {
            for (int r = r0; r <= r1; r += 1)
            {
                for (int c = c0; c <= c1; c += 1)
                {
                    if (pole[r, c].ship)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        //
        private static void NapechatatPole(CellType[,] polev1)
        {
            //первая строка
            Console.Write("  :");
            for (int col = 1; col <= N; col++)
            {
                Console.Write($" {col}");
            }
            Console.WriteLine();

            //линия
            Console.Write("  :");
            for (int col = 1; col <= N; col++)
            {
                Console.Write("--");
            }
            Console.WriteLine();

            //начало поля
            for (int row = 1; row <= N; row++)
            {
                Console.Write("{0,2}:", row);
                for (int col = 1; col <= N; col++)
                {
                    CellType kletka = polev1[row, col];
                    char simvol = !kletka.fire ? '~' : kletka.ship ? 'X' : 'O';
                    Console.Write($" {simvol}");
                }
                Console.WriteLine();
            }
        }
    }
}
