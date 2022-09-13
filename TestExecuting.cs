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
                //Thread[] threadsArray = new Thread[count];
                Task[] threadsArray = new Task[count];
                Console.WriteLine("\nRunning in parralel threads..\n");
                for (int i = 0; i < count; i++)
                {
                    int localNum = i;
                    //threadsArray[i] = new Thread(() => httpWebRequest(localNum));
                    //threadsArray[i].Start();
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
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"\nrequest #{i + 1} testing..");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nRequest: " + System.IO.File.ReadAllLines(_pathOfRequestsFile)[i]);
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("error - exception catched");
                errorCount++;
                passedTestsCount = passedTestsCount - 1;
            }
            finally
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nResponse: {html}\n");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"request #{i + 1} tested of total {count}");
                passedTestsCount++;
            }
        }
        public static void SaveLog()
        {

        }
    }
}
