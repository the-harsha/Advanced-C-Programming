using System;

namespace ConsoleApplication
{
    class ProgramOne
    {
        static void Main(string[] args)
        {
            // Creating a new Counter object with a random threshold between 0 and 9.
            // This object acts as the *event publishe
            Counter c = new Counter(new Random().Next(10));
            c.ThresholdReached += c_ThresholdReached;

            Console.WriteLine("press 'h' key to increase total");
            while (Console.ReadKey(true).KeyChar == 'h')
            {
                Console.WriteLine("adding one");
                c.Add(1);
            }
        }

        // This is the event handler method.
        // It will be called automatically when the event is raised in the Counter class.
        static void c_ThresholdReached(object sender, EventArgs e)
        {
            Console.WriteLine("The threshold was reached.");
            Environment.Exit(0);
        }
    }

    class Counter
    {
        private int threshold;
        private int total;

        public Counter(int passedThreshold)
        {
            threshold = passedThreshold;
        }

        public void Add(int x)
        {
            total += x;
            // If total meets or exceeds threshold, raise the event.
            // Events can ONLY be invoked from within the class where they are declared.
            if (total >= threshold)
            {
                ThresholdReached?.Invoke(this, EventArgs.Empty);
            }
        }

        // This event uses the built-in EventHandler delegate.
        // It represents a multicast delegate following the .NET event model.
        public event EventHandler ThresholdReached;
    }
}