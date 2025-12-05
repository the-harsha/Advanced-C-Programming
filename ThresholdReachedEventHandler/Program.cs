using System;
using System.Diagnostics.Metrics;
using static ConsoleApplication.ProgramTwo;

/*This example demonstrates how to declare a custom delegate for an event.
The delegate ThresholdReachedEventHandler defines the method signature that event handlers must follow.
In modern .NET code, you usually do not need to declare your own delegate type, 
because you can use the built-in EventHandler or EventHandler<TEventArgs> delegates*/
namespace ConsoleApplication
{
    class ProgramTwo
    {
        static void Main(string[] args)
        {
            // Create a Counter object (event publisher) with a random threshold
            Counter c = new Counter(new Random().Next(10));
            // This attaches our event handler method to the ThresholdReached event.
            c.ThresholdReached += c_ThresholdReached;

            Console.WriteLine("press 'a' key to increate total");
            while (Console.ReadKey(true).KeyChar == 'a')
            {
                Console.WriteLine("adding one");
                c.Add(1);
            }

            // Event handler method.
            // This method will automatically run when Counter raises the event.
            static void c_ThresholdReached(object sender, ThresholdReachedEventArgs e)
            {
                Console.WriteLine($"The threshold of {e.Threshold} was reached at {e.TimeReached}.");
                Environment.Exit(0);
            }
        }
        class Counter
        {
            private int threshold;
            private int total;

            //Store the threshold when the Counter is created.
            public Counter(int passedThreshold)
            {
                threshold = passedThreshold;
            }
            public void Add(int x)
            {
                total += x;
                // If the threshold is exceeded, raise the event.
                if (total >= threshold)
                {
                    ThresholdReachedEventArgs args = new ThresholdReachedEventArgs();
                    args.Threshold = threshold;
                    args.TimeReached = DateTime.Now;
                    OnThresholdReached(args);
                }
            }
            // Protected virtual method allows derived classes to override event invocation behavior.
            protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
            {
                ThresholdReachedEventHandler handler = ThresholdReached;
                if (handler != null)
                {
                    handler(this, e);
                }
            }

            public event ThresholdReachedEventHandler ThresholdReached;
        }

        public class ThresholdReachedEventArgs : EventArgs
        {
            public int Threshold { get; set; }
            public DateTime TimeReached { get; set; }
        }

        public delegate void ThresholdReachedEventHandler(Object sender, ThresholdReachedEventArgs e);
    }
}