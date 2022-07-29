using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mauiMonitorPropertyChanged
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;

    public partial class MainPageViewModel : ObservableObject
    {
        public MainPageViewModel()
        {
            //person = new Person();
        }
        [ObservableProperty]
        private Person person = new Person();

        [RelayCommand]
        void ChangeData()
        {
            Person.Name = "Vulcan Lee";
            Person.Age = 25;
        }
    }
    public partial class Person : ObservableObject
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private int age;

        private string customDesign;
        public string CustomDesign
        {
            get => name;
            set => SetProperty(ref customDesign, value);
        }
    }
}
