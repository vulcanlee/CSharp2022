namespace Background_Service_New_Thread_Await
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(async () =>
            {
                Thread.CurrentThread.Name = "新 new Thread 的處理服務用執行緒(非來自執行緒集區)";
                Console.WriteLine($"開始進行背景服務程式執行");
                // 模擬這個背景服務要處理的同步程式碼執行動作
                Thread.Sleep(2000);
                Console.WriteLine($"準備進行非同步 await 呼叫");
                Console.WriteLine();

                ShowThreadInformation("呼叫 await 前，new Thread 的相關資訊");

                // 模擬要使用 await 來進行非同步呼叫
                await Task.Delay(3000);

                ShowThreadInformation("呼叫 await 後，new Thread 的相關資訊");
            });
            thread.Start();

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }

        // 將當前的執行緒資訊顯示出來
        static void ShowThreadInformation(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine($"執行緒 Id : {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"執行緒名稱 : {Thread.CurrentThread.Name}");
            Console.WriteLine($"來自集區 : {Thread.CurrentThread.IsThreadPoolThread}");
            Console.WriteLine($"為背景執行緒 : {Thread.CurrentThread.IsBackground}");
            Console.WriteLine();
        }
    }
}