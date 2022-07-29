global using static System.Console;
using CommunityToolkit.Mvvm.ComponentModel;

namespace csCommunityToolkitMVVM
{
public partial class Person : ObservableObject
{
    #region 需要做資料綁定的屬性，使用欄位的方式來宣告即可，都都採用自動屬性方式來宣告即可
    #region 姓名
    [ObservableProperty]
    private string name;
    #endregion

    #region 年紀
    [ObservableProperty]
    private int age;
    #endregion

    #region 自行手動設計
    private string customDesign;

    public string CustomDesign
    {
        get => name;
        set => SetProperty(ref customDesign, value);
    }
    #endregion
    #endregion
}

    public class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person();
            person.PropertyChanged += (s, e) =>
            {
                WriteLine($"屬性 {e.PropertyName} 已經變更");
            };

            WriteLine("準備要修改 Name 屬性值");
            person.Name = "Vulcan Lee";

            WriteLine("Press any key for continuing...");
            ReadKey();

            WriteLine("準備要修改 Age 屬性值");
            person.Age = 25;

            WriteLine("Press any key for continuing...");
            ReadKey();
        }
    }
}