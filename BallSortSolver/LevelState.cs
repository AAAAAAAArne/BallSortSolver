using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace GetColorSolver
{
    public class LevelState
    {
        public List<Tube> Tubes { get; set; }
        public int TubeCapacity { get; set; }
        public int AmountOfTubes { get; set; }
        public HashSet<string> VisitedStates { get; set; }
        public List<(int, int)> Answer { get; set; }
         
        public LevelState()
        {
            Tubes = new List<Tube>();
            VisitedStates = new HashSet<string>();
            Answer = new List<(int, int)>();
        }

        public void addTube(Tube tube)
        {
            Tubes.Add(tube);
        }

        public void removeTube(Tube tube)
        {
            Tubes.Remove(tube);
        }

        public void printTubes()
        {
            for (int tubeNr = 0; tubeNr < Tubes.Count(); tubeNr++)
            {
                PrintTubeAtNr(tubeNr);
                Console.WriteLine();
            }
        }

        public void PrintTubeAtNr(int tubeNr)
        {
            int printedNr = tubeNr + 1;
            Console.WriteLine("Tube " + printedNr);
            Tubes.ElementAt(tubeNr).PrintTube();

        }

        public void printTubes(List<Tube> list)
        {
            for (int tubeNr = 0; tubeNr < Tubes.Count(); tubeNr++)
            {
                PrintTubeAtNr(tubeNr, list);
                Console.WriteLine();
            }
        }

        public void PrintTubeAtNr(int tubeNr, List<Tube> list)
        {

            Console.WriteLine("Tube " + tubeNr);
            list.ElementAt(tubeNr).PrintTube();

        }

        public void PrintAnswer()
        {
            foreach((int, int) answer in Answer)
            {
                Console.WriteLine("Move color from tube " + (answer.Item1+1) + " to tube "+ (answer.Item2+1));
            }
            Console.WriteLine();
        }

        public void GenerateAnswer()
        {
            (int, int) tryMove;
            VisitedStates.Add(StateInStringFormat());
            while (!IsSorted())
            {
                tryMove = TryGenerateValidMove(); // try a move
                if (tryMove.Item1 != -1) // if it is a move further in the tree, add it to my answer and to visitedMoves
                {
                    Answer.Add(tryMove);
                    VisitedStates.Add(StateInStringFormat());
                }
                else
                {
                    VisitedStates.Add(StateInStringFormat());
                    DoMove(Tubes, Answer.Last().Item2, Answer.Last().Item1); // if there is no move to try from this point, go back  one move and 
                    Answer.RemoveAt(Answer.Count - 1); //remove this move from the answer
                }
            }

            DoFinishingMoves();

            Console.WriteLine("Answer generated:");
            Console.WriteLine();
        }

        private bool IsSorted()
        {
            bool isSorted = true;
            foreach (Tube tube in Tubes)
            {
                if (tube.ViewTopColor().Equals(""))
                {
                    continue;
                }
                foreach (string color in tube.Stack)
                {
                    if (color != tube.ViewTopColor())
                    {
                        isSorted = false;
                        break;
                    }
                }
            }
            return isSorted;
        }

        private void DoFinishingMoves()
        {
            int tubeNr = 0;
            foreach (Tube tube in Tubes)
            {
                if (tube.ViewTopColor().Equals("") || tube.AmountOfColorsInTube() == TubeCapacity)
                {
                    continue;
                }
                for (int i = 0; i < Tubes.Count; i++) // loop over all tubes but the current tube from the above loop is excluded in the if below
                {
                    if (tubeNr == i)
                    {
                        continue;
                    }
                    if(Tubes[tubeNr].ViewTopColor() == Tubes[i].ViewTopColor())
                    {
                        while (Tubes[i].AmountOfColorsInTube() != TubeCapacity)
                        {
                            DoMove(Tubes, tubeNr, i);
                            VisitedStates.Add(StateInStringFormat());
                            Answer.Add((tubeNr,i));
                        }
                    }
                }
                tubeNr++;
            }
        }

        private (int, int) TryGenerateValidMove()
        {
            int tubeNr = 0;

            foreach (Tube tube in Tubes) // loop over all tubes
            {
                if(tube.Stack.Count > 0)
                {
                    for (int i = 0; i < Tubes.Count; i++) // loop over all tubes but the current tube from the above loop is excluded in the if below
                    {
                        if (tubeNr == i)
                        {
                            continue;
                        }
                        if(ValidateMove(Tubes.ElementAt(tubeNr), Tubes.ElementAt(i)))
                        {
                            DoMove(Tubes, tubeNr, i);
                            if (VisitedStates.Contains(StateInStringFormat())) // If the state is visited,
                            {
                                DoMove(Tubes, i, tubeNr); // Reverse the move 
                            }
                            else
                            {
                                return (tubeNr, i);
                            }
                        }
                    }
                    tubeNr++;
                }
            }
            return (-1, -1);
        }

        private void DoMove(List<Tube> tubes, int tube1, int tube2) // a color from tube1 will be moved to tube2
        {
            tubes.ElementAt(tube2).AddOneColor(tubes.ElementAt(tube1).RemoveOneColor());
        }

        private bool ValidateMove(Tube tube1, Tube tube2 ) // tube1 is trying to move a color from its tube to tube2
        {
            bool colorsinTube1AreAllSame = true;
            foreach (string color in tube1.Stack)
            {
                if (color.Equals(tube1.ViewTopColor()))
                {
                    continue;
                }
                colorsinTube1AreAllSame = false;
            }
            if(tube2.AmountOfColorsInTube() < TubeCapacity && (tube1.ViewTopColor().Equals(tube2.ViewTopColor()) == true || (tube2.Stack.Count == 0 && !colorsinTube1AreAllSame)))
            {
                return true;
            }

            return false;
        }

        public string StateInStringFormat()
        {
            string result = "";
            foreach(Tube tube in Tubes)
            {
                result += tube.TubeStateInStringFormat();
            }
            return result;
        }
    }
}
