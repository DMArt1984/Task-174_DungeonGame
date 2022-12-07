using System;

namespace _174DungeonGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task 174");
            Console.WriteLine(CalculateMinimusHP(new int[][] {
                new int[] { -2, -3, 3 },
                new int[] { -5, -10, 1 },
                new int[] { 10, 30, -5 }
            }));
            Console.WriteLine(CalculateMinimusHP(new int[][] { new int[] { -200 } }));
            Console.WriteLine(CalculateMinimusHP(new int[][] { new int[] { 100 } }));
            Console.WriteLine(CalculateMinimusHP(new int[][] { new int[] { 0, 0 } }));
            Console.WriteLine(CalculateMinimusHP(new int[][] { 
                new int[] { 3, -20, 30 },
                new int[] {-3, 4, 0 }
            }));
            Console.WriteLine(CalculateMinimusHP(new int[][] {
                 new int[] { 0,  -74, -47, -20, -23, -39, -48 },
                 new int[] { 37, -30,  37, -65, -82,  28, -27 },
                 new int[] {-76, -33,   7,  42,   3,  49, -93 },
                 new int[] { 37, -41,  35, -16, -96, -56,  38 },
                 new int[] {-52,  19, -37,  14, -65, -42,   9 },
                 new int[] {  5, -26, -30, -65,  11,   5,  16 },
                 new int[] {-60,   9,  36, -36,  41, -47, -86 },
                 new int[] {-22,  19,  -5, -41,  -8, -96, -95 }
            }));
            Console.ReadKey();
        }

        // https://leetcode.com/problems/dungeon-game/
        // 174. Dungeon Game
        // The demons had captured the princess and imprisoned her in the bottom-right corner of a dungeon. The dungeon consists of m x n rooms laid out in a 2D grid. Our valiant knight was initially positioned in the top-left room and must fight his way through dungeon to rescue the princess.
        // The knight has an initial health point represented by a positive integer.If at any point his health point drops to 0 or below, he dies immediately.
        // Some of the rooms are guarded by demons (represented by negative integers), so the knight loses health upon entering these rooms; other rooms are either empty(represented as 0) or contain magic orbs that increase the knight's health (represented by positive integers).
        // To reach the princess as quickly as possible, the knight decides to move only rightward or downward in each step.
        // Return the knight's minimum initial health so that he can rescue the princess.
        // Note that any room can contain threats or power-ups, even the first room the knight enters and the bottom-right room where the princess is imprisoned.
        // Input: dungeon = [[-2,-3,3],[-5,-10,1],[10,30,-5]]
        // Output: 7
        // Explanation: The initial health of the knight must be at least 7 if he follows the optimal path: RIGHT-> RIGHT -> DOWN -> DOWN.

        static int MinHP;

        static public string[][] CopyArray(string[][] dungeon)
        {
            string[][] used = new string[dungeon.Length][];
            for (var i = 0; i < dungeon.Length; i++)
            {
                used[i] = new string[dungeon[i].Length];
                for (var j = 0; j < dungeon[i].Length; j++)
                {
                    used[i][j] = dungeon[i][j].ToString();
                }
            }
            return used;
        }

        static public int CalculateMinimusHP(int[][] dungeon)
        {
            string[][] used = new string[dungeon.Length][];
            for(var i = 0; i < dungeon.Length; i++)
            {
                used[i] = new string[dungeon[i].Length];
                for (var j = 0; j < dungeon[i].Length; j++)
                {
                    used[i][j] = dungeon[i][j].ToString();
                }
            }

            MinHP = -1;
            int HP = Went(used, 0, 0 , 1, 1, ">");

            return MinHP;
        }

        static private int Went(string[][] dungeon, int row, int col, int value, int HP, string Path)
        {

            if (row >= 0 && row < dungeon.Length && col >= 0 && col < dungeon[0].Length)
            {
                string cell = dungeon[row][col];
                if (cell != "x")
                {
                    int result = 0;
                    var x = int.TryParse(cell, out result);
                    value += result;

                    if (value <= 0)
                    {
                        Path += $" err{value} ";
                        HP += (-value)+1;
                        value = 1;
                    }

                    Path += $" {value}/{HP} ";

                    if (HP > MinHP && MinHP > 0)
                        return HP;

                    // ---

                    if (row == dungeon.Length - 1 && col == dungeon[0].Length - 1)
                    {
                        if (MinHP == -1 || HP < MinHP)
                        {
                            MinHP = HP;
                        }
                        Console.WriteLine($"{Path} = end! value = {value}, HP = {HP}");
                        return HP;
                    }

                    // ---

                    dungeon[row][col] = "x";
                    var dungeon1 = CopyArray(dungeon);
                    var dungeon2 = CopyArray(dungeon);
                    var dungeon3 = CopyArray(dungeon);
                    var dungeon4 = CopyArray(dungeon);

                    var HP1 = Went(dungeon1, row , col - 1, value, HP, String.Copy(Path + " l"));
                    var HP2 = Went(dungeon2, row , col + 1, value, HP, String.Copy(Path + " r"));
                    var HP3 = Went(dungeon3, row - 1, col , value, HP, String.Copy(Path + " u"));
                    var HP4 = Went(dungeon4, row + 1, col , value, HP, String.Copy(Path + " d"));

                    HP = Math.Min(Math.Min(HP1, HP2), Math.Min(HP3, HP4));
                    return HP;
                }
            }
            return HP;
        }

    }
}
