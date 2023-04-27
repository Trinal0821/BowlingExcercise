using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Xml.Schema;

[assembly: InternalsVisibleTo("Testing")]
namespace BowlingScore
{
   public class Program
    {

        public static void Main(String[] args)
        {
            Console.WriteLine("Please enter your name");
            string name = Console.ReadLine();


            while (name.Equals("") || name.Equals(" "))
            {
                Console.WriteLine("Not a valid name");
                name = Console.ReadLine();
            }

            //Create an object
            Bowling bowling = new Bowling(name);

            int num1 = -1;
            int num2 = -1;
            int returnedNum = 0;
            string round1 = null, round2 = null;


            //Iterates through 10 frames, check the inputs and place them in the corresponding place
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    switch (j)
                    {
                        case 0:
                            Console.WriteLine("How many pins did you knock down on your first roll? (frame " + i + ")");
                            round1 = Console.ReadLine();

                            while (round1.Trim().Equals("") || round1.Equals("/"))
                            {
                                Console.WriteLine("This is not a valid input. Please try again");
                                round1 = Console.ReadLine();
                            }

                            Int32.TryParse(round1, out num1);
                            while (num1 > 10)
                            {
                                Console.WriteLine("This is not a valid input. Please try again");
                                round1 = Console.ReadLine();
                                Int32.TryParse(round1, out num1);
                            }
                            returnedNum = bowling.checkFrames1(j, round1, i);

                            break;
                        case 1:
                            if (round1.Equals("X") || round1.Trim().Equals("10"))
                            {
                                continue;
                            }

                            Console.WriteLine("How many pins did you knock down on your second roll? (frame " + i + ")");
                            round2 = Console.ReadLine();


                            while (round2.Trim().Equals("") || round2 == "X")
                            {
                                Console.WriteLine("This is not a valid input. Please try again");
                                round2 = Console.ReadLine();
                            }

                            if (!round2.Equals("/"))
                            {
                                Int32.TryParse(round2, out num2);

                                while (num1 + num2 > 10 || round2 == "X")
                                {
                                    Console.WriteLine("Invalid input. Please enter a valid value.");
                                    round2 = Console.ReadLine();
                                    Int32.TryParse(round2, out num2);
                                }

                            }

                            returnedNum = bowling.checkFrames2(j, round1, round2, i);
                            break;
                    }

                }

                bowling.redrawTable(returnedNum, i, round1, round2);


                //Check if the 10th frame's first roll was a strike, if so give the user's 2 more tries
                if (i == 10 && round1 == "X")
                {
                    for (int j = 0; j < 2; j++)
                    {
                        switch (j)
                        {
                            case 0:
                                Console.WriteLine("How many pins did you knock down on your second roll? (frame " + i + ")");
                                round1 = Console.ReadLine();

                                while (round1.Trim().Equals("") || round1.Equals("/"))
                                {
                                    Console.WriteLine("This is not a valid input. Please try again");
                                    round1 = Console.ReadLine();
                                }

                                Int32.TryParse(round1, out num1);
                                while (num1 > 10)
                                {
                                    Console.WriteLine("This is not a valid input. Please try again");
                                    round1 = Console.ReadLine();
                                    Int32.TryParse(round1, out num1);
                                }
                                returnedNum = bowling.checkFrames1(j, round1, i);

                                break;
                            case 1:
                                if (round1.Equals("X") || round1.Trim().Equals("10"))
                                {
                                    continue;
                                }

                                Console.WriteLine("How many pins did you knock down on your third roll? (frame " + i + ")");
                                round2 = Console.ReadLine();


                                while (round2.Trim().Equals("") || round2 == "X")
                                {
                                    Console.WriteLine("This is not a valid input. Please try again");
                                    round2 = Console.ReadLine();
                                }

                                if (!round2.Equals("/"))
                                {
                                    Int32.TryParse(round2, out num2);

                                    while (num1 + num2 > 10 || round2 == "X")
                                    {
                                        Console.WriteLine("Invalid input. Please enter a valid value.");
                                        round2 = Console.ReadLine();
                                        Int32.TryParse(round2, out num2);
                                    }

                                }

                                returnedNum = bowling.checkFrames2(j, round1, round2, i);
                                break;
                        }

                    }

                    bowling.redrawTable(returnedNum, i, "X", round1, round2);
                }
            }

            //Calculate the score
            bowling.calculateScore();
        }
    }

    /// <summary>
    /// A class used to calculate the user's bowling score
    /// </summary>
    public class Bowling
    {

        Dictionary<string, string[]> dict = new Dictionary<string, string[]>();
        string[,] table = new string[3, 11];
        int[] scores = new int[10];
        string name;

        /// <summary>
        /// A constructor that stores the user's name and intialize the scores array
        /// </summary>
        /// <param name="name"></param>
        public Bowling(string name)
        {
            this.name = name;

            dict.Add(name, new string[11]);

            //Intialize the score array
            for (int i = 0; i < scores.Length; i++)
            {
                scores[i] = 0;
            }
        }

        /// <summary>
        /// Redraws the table, specifically for the 10th frame
        /// </summary>
        /// <param name="num"></param>
        /// <param name="frame"></param>
        /// <param name="round1"></param>
        /// <param name="round2"></param>
        /// <param name="round3"></param>
        public void redrawTable(int num, int frame, string round1, string round2, string round3)
        {
            switch (num)
            {
                case 1:
                    table[1, frame] = "X";
                    break;
                case 0:
                    table[1, frame] = round1 + " /";
                    break;
                case -1:
                    table[1, frame] = round1 + " " + round2 + " " + round3;
                    break;
            }
            drawTable();
        }

        /// <summary>
        /// Redraws the table
        /// </summary>
        /// <param name="num"></param>
        /// <param name="frame"></param>
        /// <param name="round1"></param>
        /// <param name="round2"></param>
        public void redrawTable(int num, int frame, string round1, string round2)
        {
            switch (num)
            {
                case 1:
                    table[1, frame] = "X";
                    break;
                case 0:
                    table[1, frame] = round1 + " /";
                    break;
                case -1:
                    table[1, frame] = round1 + " " + round2;
                    break;
            }
            drawTable();
        }

        /// <summary>
        /// Check the input of the frame's second roll
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="round1"></param>
        /// <param name="round2"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public int checkFrames2(int frame, string round1, string round2, int col)
        {
            int num = 0;
            int num1 = -1;
            int num2 = -1;

            if (round2 == "/")
            {
                table[1, col] = "/";
                scores[frame - 1] = 10;
                return 0;
            }

            Int32.TryParse(round2, out num2);
            Int32.TryParse(round1, out num1);

            if (num1 + num2 == 10)
            {
                table[1, col] = "/";
                scores[frame - 1] = 10;
                return 0;
            }

            return checkInput(round2, col);
        }


        /// <summary>
        /// Check the input of the frame's first roll
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="input"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public int checkFrames1(int frame, string input, int col)
        {

            //Check what the user put in
            return checkInput(input, col);
        }

        /// <summary>
        /// Calculates the score
        /// </summary>
        public void calculateScore()
        {
            int score = 0;
            for (int i = 1; i <= 10; i++)
            {
                if (!table[1, i].Equals(""))
                {

                    if (scores[i - 1] == 10)
                    {
                        if (table[1, i].Equals("X"))
                        {
                            score += scores[i - 1] + scores[i] + scores[i + 1];
                        }
                        else
                        {
                            score += scores[i - 1] + scores[i];
                        }
                    }
                    else
                    {
                        score += scores[i - 1];
                    }

                }
            }
            Console.WriteLine("Score " + score);
        }


        /// <summary>
        /// A method used to check whether the input is a strike, spare or just a number
        /// </summary>
        /// <param name="input"></param>
        public int checkInput(string input, int x)
        {
            switch (input)
            {
                case "X":
                    table[1, x] = "X";
                    scores[x - 1] = 10;
                    return 1;

                case "/":
                    table[1, x] = "/";
                    scores[x - 1] = 10;
                    return 0;
                default:

                    int num = -1;
                    Int32.TryParse(input, out num);
                    scores[x - 1] += num;
                    if (num == 10)
                    {
                        table[1, x] = "X";
                        return 1;
                    }
                    return -1;
            }


        }

        /// <summary>
        /// A method used to draw a table used for keeping score and intialize the data
        /// </summary>
        public void drawTable()
        {
            //Label the each column and row in the table
            for (int i = 0; i < table.GetLength(1); i++)
            {
                switch (i)
                {
                    case 0:
                        table[0, i] = "Name";
                        break;
                    case 1:
                        table[0, i] = "1";
                        break;
                    case 2:
                        table[0, i] = "2";
                        break;
                    case 3:
                        table[0, i] = "3";
                        break;
                    case 4:
                        table[0, i] = "4";
                        break;
                    case 5:
                        table[0, i] = "5";
                        break;
                    case 6:
                        table[0, i] = "6";
                        break;
                    case 7:
                        table[0, i] = "7";
                        break;
                    case 8:
                        table[0, i] = "8";
                        break;
                    case 9:
                        table[0, i] = "9";
                        break;
                    case 10:
                        table[0, i] = "10";
                        break;
                }
            }

            int counter = 1;
            foreach (string s in dict.Keys)
            {
                table[counter, 0] = s;
                counter++;
            }

            //Draw the table
            for (int i = 0; i < dict.Count + 1; i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    Console.Write("|------");
                }
                Console.WriteLine("|");
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    Console.Write($"|{table[i, j],-6}"); // add the placeholder variable inside the cell
                }
                Console.WriteLine("|");
            }
            for (int j = 0; j < table.GetLength(1); j++)
            {
                Console.Write("|------");
            }
            Console.WriteLine("|");

        }
    }
}
