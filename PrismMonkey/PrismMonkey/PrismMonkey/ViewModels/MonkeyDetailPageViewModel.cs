using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismMonkey.ViewModels
{
    using System.ComponentModel;
    using System.Diagnostics;
    using Prism.Events;
    using Prism.Navigation;
    using Prism.Services;
    using PrismMonkey.Helpers;
    using PrismMonkey.Models;

    public class MonkeyDetailPageViewModel : INotifyPropertyChanged, INavigationAware
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly INavigationService navigationService;
        private readonly IPageDialogService dialogService;
        private readonly IMap map;
        public Monkey Monkey { get; set; } = new();
        public DelegateCommand OpenMapCommand { get; set; }

        public MonkeyDetailPageViewModel(INavigationService navigationService,
            IPageDialogService dialogService, IMap map)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.map = map;

            OpenMapCommand = new DelegateCommand(async () =>
            {
                try
                {
                    await map.OpenAsync(Monkey.Latitude, Monkey.Longitude, new MapLaunchOptions
                    {
                        Name = Monkey.Name,
                        NavigationMode = Microsoft.Maui.ApplicationModel.NavigationMode.None
                    });
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Unable to launch maps: {ex.Message}");
                    await dialogService.DisplayAlertAsync("Error, no Maps app!", ex.Message, "OK");
                }
            });
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            #region 若有猴子物件傳送近來，則透過 parameter 將其取出來
            if (parameters.ContainsKey(ConstantHelper.NavigationKeyMonkey))
            {
                Monkey = parameters.GetValue<Monkey>(ConstantHelper.NavigationKeyMonkey);
            }
            else
            {
                Monkey = new();
            }
            #endregion
        }

    }
}
