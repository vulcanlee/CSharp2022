using System.Diagnostics;

namespace mauiMonitorPropertyChanged;

public partial class MainPage : ContentPage
{
    public MainPageViewModel MainPageViewModel { get; }
    int count = 0;

    public MainPage(MainPageViewModel mainPageViewModel)
    {
        InitializeComponent();
        MainPageViewModel = mainPageViewModel;
        this.BindingContext = MainPageViewModel;

        MainPageViewModel.Person.PropertyChanging += (s, e) =>
        {
            Debug.WriteLine($"   ViewModel.Person : {e.PropertyName} 正在進行變更完成");
        };
        MainPageViewModel.Person.PropertyChanged += (s, e) =>
        {
            Debug.WriteLine($"   ViewModel.Person :  {e.PropertyName} 已經變更完成");
        };
        entryName.PropertyChanged += (s, e) =>
        {
            Debug.WriteLine($"   姓名控制項 {e.PropertyName} 已經變更完成");
        };
        entryName.PropertyChanging += (s, e) =>
        {
            Debug.WriteLine($"   姓名控制項 {e.PropertyName} 正在進行變更完成");
        };
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}

