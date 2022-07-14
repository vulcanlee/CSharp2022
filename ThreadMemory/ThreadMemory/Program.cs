public class Program
{
    static void Main(string[] args)
    {
        ThreadWithMemory threadWithMemory = new ThreadWithMemory();
        threadWithMemory.Start();

        #region 在此 靜態 Main 方法下，啟動執行緒
        ThreadPool.QueueUserWorkItem(M2, "第一個M2");
        #endregion

        Console.WriteLine("Press any key for continuing...");
        Console.ReadKey();
    }
    static void M2(object obj)
    {
        // 宣告 函式本身的區域變數，該物件儲存在每個執行緒的呼叫堆疊 call stack 內
        // 因此，每個執行緒所存取的 "函式本地變數" 都是不同的
        string 函式本地變數 = "M2";

        Console.WriteLine($"M2 方法 開始執行");

        #region 將傳入該工作單元委派方法內的參數，分別指定給 TLS & 靜態變數
        MyThreadLocalcClass.MyMessage.Value = obj as string;
        MyThreadLocalcClass.靜態變數 = obj as string;
        #endregion

        Task.Delay(2000).Wait();
        Console.WriteLine($"M2 方法 結束執行，執行緒本機儲存體狀態 : {MyThreadLocalcClass.MyMessage}");
        Console.WriteLine($"M2 方法 結束執行，靜態變數狀態 : {MyThreadLocalcClass.靜態變數}");
    }

}
public class ThreadWithMemory
{
    // 宣告要啟動執行緒當時所存在的物件所擁有的 欄位 Field
    // 這也就是說，只要在這個物件所啟動的執行緒
    // 都可以存取到 "當時執行緒的物件欄位的共用變數" 這個欄位值
    string 當時執行緒的物件欄位的共用變數 = "Main";
    public void Start()
    {
        #region 在此物件A下，啟動兩個執行緒
        ThreadPool.QueueUserWorkItem(M1, "第一個M1");
        ThreadPool.QueueUserWorkItem(M1, "第二個M1");
        #endregion
    }
    public void M1(object obj)
    {
        // 宣告 函式本身的區域變數，該物件儲存在每個執行緒的呼叫堆疊 call stack 內
        // 因此，每個執行緒所存取的 "函式本地變數" 都是不同的
        string 函式本地變數 = "M1";

        Console.WriteLine($"M1 方法 開始執行");

        #region 將傳入該工作單元委派方法內的參數，分別指定給 TLS & 靜態變數
        MyThreadLocalcClass.MyMessage.Value = obj as string;
        MyThreadLocalcClass.靜態變數 = obj as string;
        #endregion

        Task.Delay(new Random().Next(1000,3000)).Wait();
        Console.WriteLine($"M1 方法 結束執行，執行緒本機儲存體狀態 : {MyThreadLocalcClass.MyMessage}");
        Console.WriteLine($"M1 方法 結束執行，靜態變數狀態 : {MyThreadLocalcClass.靜態變數}");
    }
}
public class MyThreadLocalcClass
{
    // 宣告 執行緒本機儲存體 物件 : 對於每個執行緒而言，都是唯一一個全域靜態變數
    // 也就是，雖然是全域物件，每個執行緒看到的是不同物件
    public static ThreadLocal<string> MyMessage =
        new ThreadLocal<string>(() => "執行緒本機儲存體 (TLS)");
    // 宣告 .NET 中的靜態物件，對於每個執行緒而言，看到的都是同一個物件
    public static string 靜態變數 = "在任何地方皆可存取的物件";
}
