using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetColorSolver
{
    public class UserInteraction
    {
        public UserInteraction() { }

        public void Introduction()
        {
            Console.WriteLine("Hi this is the GetColorSolverprogram!");
        }

        public void PrintLineAndWhiteSpace()
        {
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine();
        }

        private (int, bool) VerifyIfInputIsParsableToInt(string input)
        {
            int number;
            bool isNumber = int.TryParse(input, out number);
            return (number, isNumber);
        }

        public int AskTubeAmountLoop()
        {
            string input = AskTubeAmount();
            (int number, bool isNumber) = VerifyIfInputIsParsableToInt(input);

            while (!isNumber)
            {
                Console.WriteLine("The input is not a valid integer!");
                (number, isNumber) = VerifyIfInputIsParsableToInt(AskTubeAmount());
            }

            Console.WriteLine("The input is a valid integer: " + number);
            Console.WriteLine();
            return number;
        }

        private string AskTubeAmount()
        {
            Console.WriteLine("Please input how many tubes you have in your level:");
            string input = Console.ReadLine();
            return input;
        }

        public int AskTubeCapacityLoop()
        {
            string input = AskTubeCapacity();
            (int number, bool isNumber) = VerifyIfInputIsParsableToInt(input);

            while (!isNumber)
            {
                Console.WriteLine("The input is not a valid integer!");
                (number, isNumber) = VerifyIfInputIsParsableToInt(AskTubeCapacity());
            }

            Console.WriteLine("The input is a valid integer: " + number);
            Console.WriteLine();
            return number;
        }

        private string AskTubeCapacity()
        {
            Console.WriteLine("Please input the tube capacity (#colors per tube):");
            string input = Console.ReadLine();
            return input;
        }

        public string AskTubeColorAtSpot(int tubeNr, int colorSpot, int tubeMax)
        {
            Console.WriteLine("Color at spot " + colorSpot + " (bottom = 0, top = " + tubeMax + ") for tube " + tubeNr + ":");
            Console.WriteLine("(Just press ENTER when there is nothing at this spot)");
            string input = Console.ReadLine();
            Console.WriteLine();
            return input;
        }
    }
}
