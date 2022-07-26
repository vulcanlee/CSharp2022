global using static System.Console;
using System.ComponentModel;

namespace csINotifyPropertyChanged
{
    public class Person : INotifyPropertyChanged
    {
        #region 實作出 INotifyPropertyChanged 的程式碼
        // 在這個 INotifyPropertyChanged 介面內，只有宣告一個 event PropertyChangedEventHandler? PropertyChanged; 成員
        public event PropertyChangedEventHandler? PropertyChanged;
        // 設計底下的方法，是要方便可以觸發 PropertyChanged 這個事件
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

        #region 針對每個具有 PropertyChanged 的屬性，都需要有底下的程式碼設計方式

        #region 姓名
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        #endregion

        #region 年紀
        private int age;

        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                OnPropertyChanged("Age");
            }
        }

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