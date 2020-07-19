using System;
using Models;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var board = new Board();

            var result = board.SearchForSolution(3);
            
            Console.WriteLine(result);
            
            Console.ReadLine();
        }
    }
}