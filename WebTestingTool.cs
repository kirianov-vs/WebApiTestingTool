using System;
using System.IO;

namespace WebApiTestingTool
{
    internal class WebTestingTool
    {
        public static string _pathOfRequestsFile = Directory.GetCurrentDirectory() + "\\ListOfRequests.txt";
        public static int count = System.IO.File.ReadAllLines(_pathOfRequestsFile).Length;

        static void Main(string[] args)
        {
            //Task.Factory.StartNew(() => SaveLog()); 
            ListOfTestInitialyze();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\npress any key to start testing...\n");
            Console.ReadKey();
            TestExecuting.TestExecution(count, _pathOfRequestsFile);
        }

        static void ListOfTestInitialyze()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"# of tests in list {count}\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Show the list ? Y\\N\n");
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                Console.WriteLine("List of requests to test:\n");
                Console.ForegroundColor = ConsoleColor.Blue;
                StreamReader f = new StreamReader(Directory.GetCurrentDirectory() + "\\ListOfRequests.txt");
                while (!f.EndOfStream)
                {
                    string s = f.ReadLine();
                    Console.WriteLine(s);
                }
                f.Close();
            }
        }


    }
}
