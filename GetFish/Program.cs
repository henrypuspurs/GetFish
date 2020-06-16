using GetFish.Core;
using GetFish.Core.Models;
using System;

namespace GetFish
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Please enter how many entries to search");
            var input = Console.ReadLine();
            int num = int.Parse(input);
            AccessPage.UrlConstructor(num);
        }


    }
}
