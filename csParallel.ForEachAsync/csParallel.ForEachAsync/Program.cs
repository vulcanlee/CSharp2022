#region 若在 Parallel.Foreach 內使用同步方式進行委派方法的設計，會等到所有委派方法都執行完畢後，才會繼續往下執行
Console.WriteLine($"使用 Parallel.Foreach 與同步委派方法 開始 {DateTime.Now}");
Parallel.ForEach(Enumerable.Range(0, 3), async (x, t) =>
{
    Console.WriteLine($"  Bpfsync {x}");
    Thread.Sleep(3000);
    Console.WriteLine($"  Cpfsync {x}");
});
// 當看到這行敘述，表示 Parallel.ForEach 已經結束執行，不過，將還沒看到所有的 Cpf 文字輸出
Console.WriteLine($"使用 Parallel.Foreach 與同步委派方法 結束 {DateTime.Now}");
#endregion

#region 若在 Parallel.Foreach 內使用 async 方法，將會立即結束平行敘述，相關程式碼會在背景執行中
Console.WriteLine($"使用 Parallel.Foreach 開始 {DateTime.Now}");
Parallel.ForEach(Enumerable.Range(0, 3), async (x, t) =>
{
    Console.WriteLine($"  Bpf {x}");
    await Task.Delay(3000);
    Console.WriteLine($"  Cpf {x}");
});
// 當看到這行敘述，表示 Parallel.ForEach 已經結束執行，不過，將還沒看到所有的 Cpf 文字輸出
Console.WriteLine($"使用 Parallel.Foreach 結束 {DateTime.Now}");
#endregion


#region 故意休息五秒，等待上述的平行作業全部都結束
Console.WriteLine();
Console.WriteLine($"休息 五秒鐘");
await Task.Delay(5000);
Console.WriteLine();
#endregion


#region 這裡使用 Parallel.ForEachAsync 來平行非同步方法，將不會有上述問題，全部的非同步作業都平行執行完畢，該行敘述才會繼續往下執行，這可以從時間戳記看出
Console.WriteLine($"使用 Parallel.ForEachAsync 開始 {DateTime.Now}");
await Parallel.ForEachAsync(Enumerable.Range(0, 3), async (x, t) =>
{
    Console.WriteLine($"  Bpfa {x}");
    await Task.Delay(3000);
    Console.WriteLine($"  Cpfa {x}");
});
Console.WriteLine($"使用 Parallel.ForEachAsync 結束 {DateTime.Now}");
#endregion


