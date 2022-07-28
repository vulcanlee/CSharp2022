global using static System.Console;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace csBindableBase
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        #region 實作出 INotifyPropertyChanged 的程式碼
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }
        #endregion

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            // 先比對新舊的物件值是否相同，若不同，才會觸發 屬性變更 的通知事件
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

            // 將此次設定的物件值，指派給該欄位
            storage = value;
            // 觸發 屬性變更 的通知事件
            RaisePropertyChanged(propertyName);

            return true;
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Person : BindableBase
    {
        #region 針對每個具有 PropertyChanged 的屬性，都需要有底下的程式碼設計方式

        #region 姓名
        private string name;

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        #endregion

        #region 年紀
        private int age;

        public int Age
        {
            get { return age; }
            set { SetProperty(ref age, value); }
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