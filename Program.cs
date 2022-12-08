using System;
using System.Collections.Generic;
using System.Linq;

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

        struct Selector
        {
            public char direct;
            public int num;
        }

        static private int Went(string[][] dungeon, int row, int col, int value, int HP, string Path)
        {
            if (MinHP == 1)
                return HP;
            
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

                    List<Selector> sels = new List<Selector>();
                    //var down = (row < dungeon.Length - 1) ? dungeon[row + 1][col] : "x";
                    //if (down != "x")
                    //    sels.Add(new Selector { direct = 'd', num = int.Parse(down) });

                    //var right = (col < dungeon[0].Length - 1) ? dungeon[row][col + 1] : "x";
                    //if (right != "x")
                    //    sels.Add(new Selector { direct = 'r', num = int.Parse(right) });

                    //var up = (row > 0) ? dungeon[row - 1][col] : "x";
                    //if (up != "x")
                    //    sels.Add(new Selector { direct = 'u', num = int.Parse(up) });

                    //var left = (col > 0) ? dungeon[row][col-1] : "x";
                    //if (left != "x")
                    //    sels.Add(new Selector { direct = 'd', num = int.Parse(left) });

                    var down = (row < dungeon.Length - 1) ? dungeon[row + 1][col] : "x";
                    sels.Add(new Selector { direct = 'd', num = (down != "x") ? int.Parse(down) : -9999 });

                    var right = (col < dungeon[0].Length - 1) ? dungeon[row][col + 1] : "x";
                    sels.Add(new Selector { direct = 'r', num = (right != "x") ? int.Parse(right) : -9999 });

                    var up = (row > 0) ? dungeon[row - 1][col] : "x";
                    sels.Add(new Selector { direct = 'u', num = (up != "x") ? int.Parse(up) : -9999 });

                    var left = (col > 0) ? dungeon[row][col - 1] : "x";
                    sels.Add(new Selector { direct = 'l', num = (left != "x") ? int.Parse(left) : -9999 });

                    dungeon[row][col] = "x";
                    var dungeon1 = CopyArray(dungeon);
                    var dungeon2 = CopyArray(dungeon);
                    var dungeon3 = CopyArray(dungeon);
                    var dungeon4 = CopyArray(dungeon);
                    dungeon = new string[][] { };

                    var HP4 = HP;
                    var HP2 = HP;
                    var HP3 = HP;
                    var HP1 = HP;

                    string myorder = "";
                    //string myorder2 = "";

                    // d - 1
                    // r - 2
                    // u - 3
                    // l - 4
                    foreach (var item in sels.OrderByDescending(x => x.num))
                    {
                        myorder += item.direct;
                        //switch (item.direct)
                        //{
                        //    case 'd':
                        //        myorder2 += "1";
                        //        break;
                        //    case 'r':
                        //        myorder2 += "2";
                        //        break;
                        //    case 'u':
                        //        myorder2 += "3";
                        //        break;
                        //    case 'l':
                        //        myorder2 += "4";
                        //        break;
                        //}
                    }
                    //int valorder = int.Parse(myorder2);

                    //while (valorder > 0)
                    //{
                    //    int item = 0;
                    //    if (valorder >= 1000)
                    //    {
                    //        item = valorder / 1000;
                    //        valorder -= item * 1000;
                    //    } else if (valorder >= 100)
                    //    {
                    //        item = valorder / 100;
                    //        valorder -= item * 100;
                    //    }
                    //    else if (valorder >= 10)
                    //    {
                    //        item = valorder / 10;
                    //        valorder -= item * 10;
                    //    } else
                    //    {
                    //        item = valorder;
                    //        valorder = 0;
                    //    }

                    //    switch (item)
                    //    {
                    //        case 1:
                    //            HP4 = Went(dungeon4, row + 1, col, value, HP, String.Copy(Path + " d"));
                    //            break;
                    //        case 2:
                    //            HP2 = Went(dungeon2, row, col + 1, value, HP, String.Copy(Path + " r"));
                    //            break;
                    //        case 3:
                    //            HP3 = Went(dungeon3, row - 1, col, value, HP, String.Copy(Path + " u"));
                    //            break;
                    //        case 4:
                    //            HP1 = Went(dungeon1, row, col - 1, value, HP, String.Copy(Path + " l"));
                    //            break;
                    //    }
                    //}

                    foreach (var item in myorder)
                    {
                        switch (item)
                        {
                            case 'd':
                                HP4 = Went(dungeon4, row + 1, col, value, HP, String.Copy(Path + " d"));
                                break;
                            case 'r':
                                HP2 = Went(dungeon2, row, col + 1, value, HP, String.Copy(Path + " r"));
                                break;
                            case 'u':
                                HP3 = Went(dungeon3, row - 1, col, value, HP, String.Copy(Path + " u"));
                                break;
                            case 'l':
                                HP1 = Went(dungeon1, row, col - 1, value, HP, String.Copy(Path + " l"));
                                break;
                        }
                    }

                    //switch (myorder)
                    //{
                    //    case "drul":
                    //        HP4 = Went(dungeon4, row + 1, col, value, HP, String.Copy(Path + " d"));
                    //        HP2 = Went(dungeon2, row, col + 1, value, HP, String.Copy(Path + " r"));
                    //        HP3 = Went(dungeon3, row - 1, col, value, HP, String.Copy(Path + " u"));
                    //        HP1 = Went(dungeon1, row, col - 1, value, HP, String.Copy(Path + " l"));
                    //        break;
                    //    case "rdul":
                    //        HP2 = Went(dungeon2, row, col + 1, value, HP, String.Copy(Path + " r"));
                    //        HP4 = Went(dungeon4, row + 1, col, value, HP, String.Copy(Path + " d"));
                    //        HP3 = Went(dungeon3, row - 1, col, value, HP, String.Copy(Path + " u"));
                    //        HP1 = Went(dungeon1, row, col - 1, value, HP, String.Copy(Path + " l"));
                    //        break;
                    //    case "drlu":
                    //        HP4 = Went(dungeon4, row + 1, col, value, HP, String.Copy(Path + " d"));
                    //        HP2 = Went(dungeon2, row, col + 1, value, HP, String.Copy(Path + " r"));
                    //        HP1 = Went(dungeon1, row, col - 1, value, HP, String.Copy(Path + " l"));
                    //        HP3 = Went(dungeon3, row - 1, col, value, HP, String.Copy(Path + " u"));
                    //        break;
                    //    case "rdlu":
                    //        HP2 = Went(dungeon2, row, col + 1, value, HP, String.Copy(Path + " r"));
                    //        HP4 = Went(dungeon4, row + 1, col, value, HP, String.Copy(Path + " d"));
                    //        HP1 = Went(dungeon1, row, col - 1, value, HP, String.Copy(Path + " l"));
                    //        HP3 = Went(dungeon3, row - 1, col, value, HP, String.Copy(Path + " u"));
                    //        break;

                    //    default:
                    //        HP4 = Went(dungeon4, row + 1, col, value, HP, String.Copy(Path + " d"));
                    //        HP2 = Went(dungeon2, row, col + 1, value, HP, String.Copy(Path + " r"));
                    //        HP3 = Went(dungeon3, row - 1, col, value, HP, String.Copy(Path + " u"));
                    //        HP1 = Went(dungeon1, row, col - 1, value, HP, String.Copy(Path + " l"));
                    //        break;
                    //}

                    HP = Math.Min(Math.Min(HP1, HP2), Math.Min(HP3, HP4));
                    return HP;
                }
            }

            dungeon = new string[][] { };
            return HP;
        }

    }
}
