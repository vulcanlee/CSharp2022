namespace Multiple_Thread_Using_List_Type_Is_Thread_Safety
{
    internal class Program
    {
        static int MaxCount = 20000;
        static int ThreadCount = Environment.ProcessorCount;
        static List<int> ints = new List<int>();
        static List<AutoResetEvent> events = new List<AutoResetEvent>();
        static void Main(string[] args)
        {
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                int idx = i;
                AutoResetEvent autoResetEvent = new AutoResetEvent(false);
                events.Add(autoResetEvent);
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    Console.Write($"{idx}");
                    for (int k = 1; k <= MaxCount; k++)
                    {
                        ints.Add(k);
                    }
                    Console.Write($"[{idx}]");
                    autoResetEvent.Set();
                });
            }
            WaitHandle.WaitAll(events.ToArray());
            Console.WriteLine();

            //Thread.Sleep(5000);

            var groupInts = ints.GroupBy(x => x)
                 .Select(x => new { Number = x.Key, Count = x.Count() })
                 .OrderBy(x => x.Count);

            //foreach (var item in groupInts)
            //{
            //    Console.WriteLine($"{item.Number} = {item.Count}");
            //}

            var groupByCount = groupInts
                .GroupBy(x => x.Count)
                .Select(x => new { Count = x.Key, Times = x.Count() })
                .OrderBy(x => x.Count);

            foreach (var item in groupByCount)
            {
                Console.WriteLine($"{item.Count} = {item.Times}");
            }
        }
    }
}