using System;
using System.IO;

// delegate is a variable that can call a method
namespace DelegateBasic
{
  
    public class Program
    {
        // Declaring  delegate that can point to any method 
        // which has 'void return type' and takes a single 'string' parameter
        delegate  void LogDel(string text);
        static void Main(string[] args)
        {
            GetLogger log = new GetLogger();

            LogDel LogTextToScreenDel, LogTextToFileDel;

            LogTextToScreenDel = new LogDel(log.LogTextToScreen);

            LogTextToFileDel = new LogDel(log.LogTextToFile);

            // Combine delegates ->  Multicast delegate.
            // Now calling multiLogDel() will execute both methods.
            LogDel multiLogDel = LogTextToScreenDel + LogTextToFileDel;

            Console.WriteLine("Please enter your name");

            var name = Console.ReadLine();

            // Pass the multicast delegate to a method.
            LogText(multiLogDel, name);

            Console.ReadKey();

        }

        // Method that accepts a delegate and executes it.
        static void LogText(LogDel logDel, string text)
        {
            logDel(text);
        }

    }

    // Separate class responsible for logging operations
    public class GetLogger
    {
        public void LogTextToScreen(string text)
        {
            Console.WriteLine($"{DateTime.Now}: {text}");

        }

        public void LogTextToFile(string text)
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log.txt"), true))
            {
                sw.WriteLine($"{DateTime.Now}: {text}");
            }
        }
    }

}