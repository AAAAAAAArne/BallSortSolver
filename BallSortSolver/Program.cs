// See https://aka.ms/new-console-template for more information
using System;
using System.Runtime.CompilerServices;

namespace GetColorSolver // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserInteraction userInteraction = new UserInteraction();
            LevelState levelState;

            while (true)
            {
                userInteraction.Introduction();
                userInteraction.PrintLineAndWhiteSpace();
                levelState = new LevelState();
                levelState.AmountOfTubes = userInteraction.AskTubeAmountLoop();
                levelState.TubeCapacity =userInteraction.AskTubeCapacityLoop();
                AskAndFillTubesLoop(levelState, userInteraction);
                levelState.GenerateAnswer();
                levelState.PrintAnswer();
                userInteraction.PrintLineAndWhiteSpace();
            }
        }

        public static void AskAndFillTubesLoop(LevelState levelState, UserInteraction userInteraction)
        {
            string input;
            for (int i = 0; i < levelState.AmountOfTubes; i++)
            {
                levelState.addTube(new Tube(levelState.TubeCapacity));

                for (int j = 0; j < levelState.TubeCapacity; j++)
                {
                    input = userInteraction.AskTubeColorAtSpot(i + 1, j, levelState.TubeCapacity);
                    if (input.Equals(""))
                    {
                        levelState.printTubes();
                        break;
                    }
                    else
                    {
                        levelState.Tubes.ElementAt(i).AddOneColor(input);
                        levelState.printTubes();
                    }
                }
            }
        }
    }
}
