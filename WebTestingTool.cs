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
            WriteLineWithColor("\npress any key to start testing...\n", 14);
            Console.ReadKey();
            TestExecuting.TestExecution(count, _pathOfRequestsFile);
        }

        static void ListOfTestInitialyze()
        {
            WriteLineWithColor($"# of tests in list {count}\n", 15);
            WriteLineWithColor("Show the list ? Y\\N\n", 14);
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

        public static void WriteLineWithColor(string text, int ForegroundColor)

        {
            Console.ForegroundColor = (ConsoleColor)ForegroundColor;
            Console.WriteLine(text);
        }


    }
}
