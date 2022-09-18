using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace WebApiTestingTool
{
    internal class TestExecuting
    {
        public static int errorCount = 0;
        public static int passedTestsCount = 0;
        public static void TestExecution(int count, string _pathOfRequestsFile)
        {
            Console.WriteLine("\nRun tests sequentially ? Y\\N\n");

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                Console.WriteLine("\nRunning sequentially..\n");
                for (int i = 0; i < count; i++)
                {
                    httpWebRequest(i, count, _pathOfRequestsFile);
                }
            }
            else
            {
                Task[] threadsArray = new Task[count];
                Console.WriteLine("\nRunning in parralel threads..\n");
                for (int i = 0; i < count; i++)
                {
                    int localNum = i;
                    threadsArray[i] = Task.Factory.StartNew(() => httpWebRequest(localNum, count, _pathOfRequestsFile));
                }
                Task.WaitAll(threadsArray);
            }
            ResultShow.AllTestsCompleted(errorCount, passedTestsCount);
        }
        public static void httpWebRequest(int i, int count, string _pathOfRequestsFile)
        {
            string html = string.Empty;
            string url = System.IO.File.ReadAllLines(_pathOfRequestsFile)[i];
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebTestingTool.WriteLineWithColor($"\nrequest #{i + 1} testing..", 15);
                WebTestingTool.WriteLineWithColor("\nRequest: " + System.IO.File.ReadAllLines(_pathOfRequestsFile)[i], 14);
                request.AutomaticDecompression = DecompressionMethods.GZip;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    html = reader.ReadToEnd();
                    Console.Write("Status code.." + (int)response.StatusCode + "\n");
                }
            }
            catch
            {
                WebTestingTool.WriteLineWithColor("error - exception catched", 12);
                errorCount++;
                passedTestsCount = passedTestsCount - 1;
            }
            finally
            {
                WebTestingTool.WriteLineWithColor($"\nResponse: {html}\n", 9);
                WebTestingTool.WriteLineWithColor($"request #{i + 1} tested of total {count}", 2);
                passedTestsCount++;
            }
        }
        public static void SaveLog()
        {

        }
    }
}
