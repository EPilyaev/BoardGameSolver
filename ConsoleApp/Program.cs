using System;
using Models;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initial State:");

            GameSolver gs = new GameSolver();
            
            Console.WriteLine(gs.InitialState.DrawState());

            var result = gs.SearchForSolution();
            
            Console.WriteLine($"Overall moves made: {gs.OverallMoves:N0}");
            Console.WriteLine(result);

            Console.WriteLine();
            Console.WriteLine("Detailed movements:");

            var state = gs.InitialState;

            foreach (var movement in result.MovementsMade)
            {
                Console.WriteLine(movement);
                state = state.Go(movement);
                //TODO: consider refactoring logic of removal matched ids from CheckBoardState
                state.CheckBoardState();
                Console.WriteLine(state.DrawState());
            }
            
            Console.ReadLine();
        }
    }
}