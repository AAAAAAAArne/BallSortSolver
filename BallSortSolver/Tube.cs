using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetColorSolver
{
    public class Tube
    {
        public Stack<string> Stack { get; set; }
        public int Capacity { get; set; }

        public Tube(int capacity) 
        {
            this.Capacity = capacity;
            this.Stack = new Stack<string>();
        }

        public void AddOneColor(string color)
        {
            this.Stack.Push(color);       
        }

        public string RemoveOneColor()
        {
            if (this.Stack.Count > 0)
            {
                return this.Stack.Pop();
            }
            return "";
        }

        public string ViewTopColor()
        {
            if(this.Stack.Count > 0)
            {
                return this.Stack.Peek();
            }
            return "";
        }

        public int AmountOfColorsInTube() 
        {
            return this.Stack.Count; 
        }

        public string GetColorAtPosition(int index)
        {
            return this.Stack.ElementAtOrDefault(index);
        }

        public void PrintColorAtPosition(int index)
        {
            Console.WriteLine(GetColorAtPosition);
        }

        public void PrintTube()
        {
            foreach(string color in this.Stack)
            {
                Console.WriteLine(color);
            }
        }

        public string TubeStateInStringFormat()
        {
            string result = "";
            for (int i = 0; i < Capacity; i++)
            {
                if(GetColorAtPosition(i) == null)
                {
                    result += " ";
                }
                else
                {
                    result += GetColorAtPosition(i);
                }
            }
            return result;
        }
    }
}
