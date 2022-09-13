using System;

namespace WebApiTestingTool
{
    internal class ResultShow
    {
        public static void AllTestsCompleted(int errorCount, int passedTestsCount)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\ntest completed...OK");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n# of errors: {errorCount}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n# PASSED tests: {passedTestsCount}");
            Console.Beep(500, 200);
            Console.Beep(500, 200);
            Console.Beep(500, 200);
            Console.ReadKey();
        }
    }
}
