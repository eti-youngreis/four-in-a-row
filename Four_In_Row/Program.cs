using System.Net.Http.Headers;
using System;
namespace Four_In_Row
{
    internal class Program
    {
        //בדיקת תקינות מיקום
        public static bool IsPossible(int[,] game, int choice, ref int count, bool[] isColumnNotFree)
        {

            if (choice >= game.GetLength(1) || game[game.GetLength(0) - 1, choice] != 0)
            {
                if (choice < game.GetLength(1))
                {
                    if (!isColumnNotFree[choice])
                    {
                        count++;
                        isColumnNotFree[choice] = true;
                    }


                }
                return false;
            }
            return true;

        }

        //השחלה
        public static int Thread(int[,] game, int choice, int player)
        {
            for (int i = 0; ; i++)
            {
                if (game[i, choice] == 0)
                {
                    game[i, choice] = player;
                    return i;
                }
            }
        }
        public static bool CheckWin(int[,] game, int lastRow, int lastColumn)
        {
            bool  down, left, right, mainDiagonalUp, secondDiagonalUp, mainDiagonalDown, secondDiagonalDown;
             down = left = right = mainDiagonalUp = secondDiagonalUp = mainDiagonalDown = secondDiagonalDown = true;
            int column, row, mainDiagonal, secondDiagonal;
            column = row = mainDiagonal = secondDiagonal = 0;

            for (int i = 1; i < 4; i++)
            {
                
                if (down)
                    if (lastRow - i <0 || game[lastRow - i, lastColumn] != game[lastRow, lastColumn])
                        down = false;
                    else
                        column++;
                if (left)
                    if (lastColumn - i == -1 || game[lastRow, lastColumn - i] != game[lastRow, lastColumn])
                        left = false;
                    else
                        row++;
                if (right)
                    if (lastColumn + i == game.GetLength(1) || game[lastRow, lastColumn + i] != game[lastRow, lastColumn])
                        right = false;
                    else
                        row++;
                if (mainDiagonalUp)
                    if (lastColumn - i == -1 || lastRow - i == -1 || game[lastRow - i, lastColumn - i] != game[lastRow, lastColumn])
                        mainDiagonalUp = false;
                    else
                        mainDiagonal++;
                if (mainDiagonalDown)
                    if (lastColumn + i == game.GetLength(1) || lastRow + i == game.GetLength(0) || game[lastRow + i, lastColumn + i] != game[lastRow, lastColumn])
                        mainDiagonalDown = false;
                    else
                        mainDiagonal++;
                if (secondDiagonalUp)
                    if (lastRow - i == -1 || lastColumn + i == game.GetLength(1) || game[lastRow - i, lastColumn + i] != game[lastRow, lastColumn])
                        secondDiagonalUp = false;
                    else
                        secondDiagonal++;
                if (secondDiagonalDown)
                    if (lastRow + i == game.GetLength(0) || lastColumn - i == -1 || game[lastRow + i, lastColumn - i] != game[lastRow, lastColumn])
                        secondDiagonalDown = false;
                    else
                        secondDiagonal++;
                if (row == 3 || column == 3 || mainDiagonal == 3 || secondDiagonal == 3)
                    return true;

            }
            return false;

        }

            //הדפסה


            public static void PrintGame(int[,] game)
        {
            for (int i = game.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < game.GetLength(1); j++)
                {
                    if (game[i, j] == 0)
                        Console.Write("  -  ");
                    else
                    {
                        if (game[i, j] == 1)
                            Console.ForegroundColor = ConsoleColor.Red;
                        else
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("  O  ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    
                }
                Console.WriteLine();
            }
            
            Console.ForegroundColor= ConsoleColor.White;
        }



        public static void PrintGame(int[,]game,int row,int col)
        {
            for (int i = game.GetLength(0) - 1; i > row-1; i--)
            {
                System.Threading.Thread.Sleep(500);
                Console.Clear();
                Console.WriteLine("\n\n");
                for (int j = game.GetLength(0) - 1; j >= 0; j--)
                {
                    for (int k = 0; k < game.GetLength(1); k++)
                    {
                        if (k == col && j >= row)
                        {
                            if (j == i)
                            {
                                if (game[row,col] == 1)
                                    Console.ForegroundColor = ConsoleColor.Red;
                                else
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("  O  ");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                                Console.Write("  -  ");
                        }
                        else
                            if (game[j,k] == 0)
                               Console.Write("  -  ");
                            else
                            {
                                if (game[j,k] == 1)
                                    Console.ForegroundColor = ConsoleColor.Red;
                                else
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("  O  ");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                    
                    }
                    Console.WriteLine();
                }
                
            }
            System.Threading.Thread.Sleep(1000);
        }
    
        
        static void Main(string[] args)
        {
            int[,] game = new int[4, 6];
            bool[] isColumnNotFree = new bool[game.GetLength(1)];
            string player1, player2;
            bool win = false;
            int i = 0, choice=-1, player = 0, count = 0,row=-1;
            Console.WriteLine("Enter your names");
            player1 = Console.ReadLine();
            player2 = Console.ReadLine();
            while (!win)
            {

                if (i % 2 == 0)
                {
                    player = 1;
                    Console.WriteLine($"{player1} this is your turn, please enter your choice");
                }
                else
                {
                    player = 2;
                    Console.WriteLine($"{player2} this is your turn, please enter your choice");
                }
                choice = int.Parse(Console.ReadLine()) - 1;
                while (!IsPossible(game, choice, ref count,isColumnNotFree))
                {
                    if (count == game.GetLength(1))
                    {
                        break;
                    }
                    Console.WriteLine("your choice isn't possible,please enter another column");
                    choice = int.Parse(Console.ReadLine()) - 1;

                }
                if (count == game.GetLength(1))
                {
                    break;
                }
                row = Thread(game, choice, player);
                PrintGame(game, row,choice);
                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                if (CheckWin(game,row ,choice ))
                    win = true;
                i++;
               
            }
            if (!win)
            {
                Console.WriteLine("Game Over");
                return;
            }
            if (player == 1)
                Console.WriteLine($"{player1} you win!!!!");
            else
                Console.WriteLine($"{player2} you win!!!!");
            PrintGame(game);


        }

    }
}