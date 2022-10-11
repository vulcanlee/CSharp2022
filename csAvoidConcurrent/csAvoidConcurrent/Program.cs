using System.Threading;

namespace csAvoidConcurrent
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            #region 沒有執行緒同步設計，將會導致執行緒安全這樣需求不存在
            Console.WriteLine("沒有執行緒同步設計，將會導致執行緒安全這樣需求不存在");
            Task task1 = Task.Run(() =>
            {
                PrintJob(1, "abc");
            });
            Task task2 = Task.Run(() =>
            {
                PrintJob(2, "xyz");
            });

            await Task.WhenAll(task1, task2);
            #endregion

            #region 使用 lock 做到執行緒同步設計，如此將會具有執行緒安全
            Console.WriteLine();
            Console.WriteLine("使用 lock 做到執行緒同步設計，如此將會具有執行緒安全");
            Task task3 = Task.Run(() =>
            {
                PrintJobUsingLock(1, "abc");
            });
            Task task4 = Task.Run(() =>
            {
                PrintJobUsingLock(2, "xyz");
            });

            await Task.WhenAll(task3, task4);
            #endregion

            #region 使用 AutoResetEvent 事件的執行緒同步設計，衝突的列印將會放棄
            Console.WriteLine();
            Console.WriteLine("使用 AutoResetEvent 事件的執行緒同步設計，衝突的列印將會放棄");
            Task task5 = Task.Run(() =>
            {
                PrintJobUsingAutoResetEvent(1, "abc");
            });
            Task task6 = Task.Run(() =>
            {
                PrintJobUsingAutoResetEvent(2, "xyz");
            });

            Task task7 = Task.Run(() =>
            {
                Thread.Sleep(1300);
                PrintJobUsingAutoResetEvent(4, "123");
            });

            await Task.WhenAll(task5, task6, task7);
            #endregion
        }

        static void PrintJob(int jobId, string printContent)
        {
            Console.WriteLine($"Printing({jobId}) Job Prepare");
            Thread.Sleep(100);
            Console.WriteLine($"Printing({jobId}) open printer connection");
            Thread.Sleep(300);
            Console.WriteLine($"Printing({jobId}) write content to printer ({printContent})");
            Thread.Sleep(1000);
            Console.WriteLine($"Printing({jobId}) close printer connection");
            Thread.Sleep(300);
        }

        static object locker = new object();
        static void PrintJobUsingLock(int jobId, string printContent)
        {
            lock (locker)
            {
                Console.WriteLine($"Printing({jobId}) Job Prepare");
                Thread.Sleep(100);
                Console.WriteLine($"Printing({jobId}) open printer connection");
                Thread.Sleep(300);
                Console.WriteLine($"Printing({jobId}) write content to printer ({printContent})");
                Thread.Sleep(1000);
                Console.WriteLine($"Printing({jobId}) close printer connection");
                Thread.Sleep(300);
            }
        }

        // true 表示初始狀態設定為已收到信號，false 表示初始狀態設定為未收到信號。
        static AutoResetEvent resetEvent = new AutoResetEvent(true);
        static void PrintJobUsingAutoResetEvent(int jobId, string printContent)
        {
            bool waitResult = resetEvent.WaitOne(500);
            if (waitResult==true)
            {
                Console.WriteLine($"Printing({jobId}) Job Prepare");
                Thread.Sleep(100);
                Console.WriteLine($"Printing({jobId}) open printer connection");
                Thread.Sleep(300);
                Console.WriteLine($"Printing({jobId}) write content to printer ({printContent})");
                Thread.Sleep(1000);
                Console.WriteLine($"Printing({jobId}) close printer connection");
                Thread.Sleep(300);
                // 將事件的狀態設定為未收到信號，讓一個或多個等候執行緒繼續執行。 (繼承來源 EventWaitHandle)
                resetEvent.Set();
            }
        }
    }
}