using System;
using System.Diagnostics;

namespace ServiceTestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            var tester = new TodosPostTester();
            tester.TestCrud();
            Console.ReadLine();
        }
    }
}
