namespace mauiLiveChart.ViewModels;

using System.Collections.ObjectModel;
using System.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
public class MainPageViewModel : INotifyPropertyChanged, INavigationAware
{
    public event PropertyChangedEventHandler PropertyChanged;

    private readonly INavigationService navigationService;
    public ObservableCollection<ISeries> Series { get; set; } =
        new ObservableCollection<ISeries>();
    public MainPageViewModel(INavigationService navigationService)
    {
        this.navigationService = navigationService;

    }

    public void OnNavigatedFrom(INavigationParameters parameters)
    {
    }

    public void OnNavigatedTo(INavigationParameters parameters)
    {
        Series.Add(new PieSeries<double> { Values = new double[] { 2 } });
        Series.Add(new PieSeries<double> { Values = new double[] { 4 } });
        Series.Add(new PieSeries<double> { Values = new double[] { 1 } });
        Series.Add(new PieSeries<double> { Values = new double[] { 4 } });
        Series.Add(new PieSeries<double> { Values = new double[] { 3 } });
    }

}
