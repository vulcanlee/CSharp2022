namespace csMultiTaskTimeout;

// 模擬印表機需求
public class Printer
{
    // 提供列印的功能
    public void PrintContent(string content)
    {
        Console.WriteLine($"> {content}");
        Thread.Sleep(1000);
        Console.WriteLine($"@ {content}");
    }
}
public class Program
{

    static async Task Main(string[] args)
    {
        // 建立取消權杖來源物件
        CancellationTokenSource cancellationTokenSource =
            new CancellationTokenSource();
        // 取得取消權杖的物件
        CancellationToken cancellationToken = cancellationTokenSource.Token;
        Printer printer = new Printer();

        // 測試參數
        int 全部列印時間次數 = 13;
        int 印表機逾時時間 = 7000;
        int 會產生印表機故障Index = 3;

        Task task1 = Task.Run(() =>
        {
            int printLines = 全部列印時間次數;
            for (int i = 0; i < printLines; i++)
            {
                try
                {
                    // 透過取消權杖判斷，若有取消請求發出，則不再繼續列印
                    if (cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine($"超過列印時間，即將強制取消");
                        break;
                    }
                    printer.PrintContent($"正在列印 {i}");

                    #region 模擬印表機強制故障的情境
                    if (i== 會產生印表機故障Index)
                    {
                        throw new Exception("印表機發生故障");
                    }
                    #endregion
                }
                catch (Exception)
                {
                    // 這裡需要把例外異常紀錄下來
                    Console.WriteLine($"例外異常事件：印表機因為斷電，無法連線上");
                    // 再度拋出例外異常，讓此工作知道該非同步工作有例外異常
                    throw;
                }
            }
        });
        Task task2 = Task.Delay(印表機逾時時間);

        // 等候其中一個工作完成 (第一個為列印工作，第二個為時間逾時)
        // https://learn.microsoft.com/zh-tw/dotnet/api/system.threading.tasks.task.whenany
        var whenTask = await Task.WhenAny(task1, task2);
        if(whenTask == task1)
        {
            // 正常列印完成或者印表機發生故障
            if(whenTask.Status == TaskStatus.Faulted)
            {
                Console.WriteLine($"最後結果1：列印時候產生莫名例外異常");
            }
            // 列印請求將會被程式碼強制取消
            else if (whenTask.Status == TaskStatus.Canceled)
            {
                Console.WriteLine($"最後結果2：列印被強制取消");
            }
            // 正常列印完成
            else
            {
                //whenTask.Status == TaskStatus.RanToCompletion
                Console.WriteLine($"最後結果3：列印工作完成");
            }
        }
        else if(whenTask == task2)
        {
            Console.WriteLine($"最後結果4：列印逾時");
            cancellationTokenSource.Cancel();
        }
        else
        {
            // ??
            Console.WriteLine($"最後結果5：印表故障，拋出例外異常");
        }

        Console.ReadLine();
    }
}