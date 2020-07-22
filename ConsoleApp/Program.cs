using System;
using Models;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            GameSolver gs = new GameSolver();

            var result = gs.SearchForSolution();
            
            Console.WriteLine(result);
            
            Console.ReadLine();
        }
    }
}