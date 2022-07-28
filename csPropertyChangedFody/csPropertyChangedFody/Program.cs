global using static System.Console;
using System.ComponentModel;

namespace csPropertyChangedFody
{
    public class Person : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        #region 針對屬性的成員，都都採用自動屬性方式來宣告即可
        #region 姓名
        public string Name { get; set; }
        #endregion

        #region 年紀
        public int Age { get; set; }
        #endregion
        #endregion
    }

    internal class Program
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