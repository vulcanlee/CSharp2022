namespace MultipleThreadAccessCollection
{
    using static System.Console;

    internal class Program
    {
        static List<string> list = new List<string>();
        static void Main(string[] args)
        {
            #region 執行緒 1
            new Thread(() =>
            {
                while (true)
                { list.Add("A"); Write("A"); }
            })
            { IsBackground = true }.Start();
            #endregion

            #region 執行緒 2
            //new Thread(() =>
            //{
            //    while (true)
            //        foreach (var item in list) { Write("R"); }
            //    list.Clear();
            //    Write("C");
            //})
            //{ IsBackground = true }.Start();
            #endregion

            #region 執行緒 3
            new Thread(() =>
            {
                while (true)
                    for (int i = 0; i < list.Count; i++)
                    { var b = list[i]; Write("R"); }
                list.Clear();
                Write("C");
            })
            { IsBackground = true }.Start();
            #endregion

            Console.ReadKey();
        }
    }
}