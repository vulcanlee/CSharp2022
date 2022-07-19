using System.Collections.Concurrent;
using System.Diagnostics;

namespace Change_ThreadPool_Thread_Property_Question
{
    internal class Program
    {
        //static Dictionary<int, Thread> threads = new Dictionary<int, Thread>();
        static ConcurrentBag<Thread> queue = new ConcurrentBag<Thread>();
        static void Main(string[] args)
        {
            Console.WriteLine($"邏輯處理器數量 : {Environment.ProcessorCount}");

            Console.WriteLine("請求邏輯處理器數量的執行緒");
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                int idx = i;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    // 設定執行緒的相關參數
                    Console.Write($"{idx}");
                    Thread thread = Thread.CurrentThread;
                    thread.Name = $"重新設定{idx}";
                    thread.IsBackground = (idx % 3 == 0) ? false : true;
                    queue.Add(thread);
                    Console.Write($"*");
                    Thread.Sleep(10000);
                });
            }

            Thread.Sleep(8000);
            Console.WriteLine($"查看所取得的執行緒狀態");

            Thread getThread = null;
            while (queue.Count > 0)
            {
                queue.TryTake(out getThread);
                Console.WriteLine($"ID : {getThread.ManagedThreadId}");
                Console.WriteLine($"Name : {getThread.Name}");
                Console.WriteLine($"IsBackground : {getThread.IsBackground}");
                Console.WriteLine();
            }

            Console.WriteLine($"休息10秒鐘，重新配置相同數量執行緒");
            Thread.Sleep(10000);
            queue.Clear();
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                int idx = i;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    Console.Write($"{idx}");
                    Thread thread = Thread.CurrentThread;
                    queue.Add(thread);
                    Thread.Sleep(10000);
                });
            }

            Thread.Sleep(8000);
            Console.WriteLine($"第二次查看所取得的執行緒狀態");
            while (queue.Count > 0)
            {
                queue.TryTake(out getThread);
                Console.WriteLine($"ID : {getThread.ManagedThreadId}");
                Console.WriteLine($"Name : {getThread.Name}");
                Console.WriteLine($"IsBackground : {getThread.IsBackground}");
                Console.WriteLine();
            }

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}