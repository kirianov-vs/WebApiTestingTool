using System;

namespace WebApiTestingTool
{
    internal class ResultShow
    {
        public static void AllTestsCompleted(int errorCount, int passedTestsCount)
        {
            WebTestingTool.WriteLineWithColor("\ntest completed...OK", 11);
            WebTestingTool.WriteLineWithColor($"\n# of errors: {errorCount}", 12);
            WebTestingTool.WriteLineWithColor($"\n# PASSED tests: {passedTestsCount}", 10);
            SoundSignalEnd();
            Console.ReadKey();
        }

        static void SoundSignalEnd()
        {
            Console.Beep(500, 200);
            Console.Beep(500, 200);
            Console.Beep(500, 200);
        }
    }
}
