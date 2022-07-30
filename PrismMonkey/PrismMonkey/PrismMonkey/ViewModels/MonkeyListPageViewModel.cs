﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismMonkey.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using Prism.Events;
    using Prism.Navigation;
    using Prism.Services;
    using PrismMonkey.Helpers;
    using PrismMonkey.Models;
    using PrismMonkey.Services;

    public class MonkeyListPageViewModel : INotifyPropertyChanged, INavigationAware
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region 透過建構式注入的服務
        private readonly INavigationService navigationService;
        private readonly IPageDialogService dialogService;
        private readonly MonkeyService monkeyService;
        private readonly IConnectivity connectivity;
        private readonly IGeolocation geolocation;
        #endregion

        #region 用於進行 Data Binding 使用的命令或者資料物件
        public ObservableCollection<Monkey> Monkeys { get; set; } = new();
        public DelegateCommand<Monkey> GoToDetailsCommand { get; set; }
        public DelegateCommand GetMonkeysCommand { get; set; }
        public DelegateCommand GetClosestMonkeyCommand { get; set; }
        public bool IsRefreshing { get; set; }
        public bool IsBusy { get; set; }
        public bool IsNotBusy => !IsBusy;
        #endregion

        public MonkeyListPageViewModel(INavigationService navigationService,
            IPageDialogService dialogService,
            MonkeyService monkeyService, IConnectivity connectivity, IGeolocation geolocation)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.monkeyService = monkeyService;
            this.connectivity = connectivity;
            this.geolocation = geolocation;

            GoToDetailsCommand = new DelegateCommand<Monkey>(async monkey =>
            {
                // 若沒有取得猴子資訊，則不會有任何動作
                if (monkey == null)
                    return;

                NavigationParameters parameters = new();
                parameters.Add(ConstantHelper.NavigationKeyMonkey, monkey);
                await navigationService.NavigateAsync(ConstantHelper.MonkeyDetailPage, parameters);
            });

            GetMonkeysCommand = new DelegateCommand(async () =>
            {
                if (IsBusy)
                    return;

                try
                {
                    if (connectivity.NetworkAccess != NetworkAccess.Internet)
                    {
                        await dialogService.DisplayAlertAsync("No connectivity!",
                             $"Please check internet and try again.", "OK");
                        return;
                    }

                    IsBusy = true;
                    var monkeys = await monkeyService.GetMonkeysAsync();

                    if (Monkeys.Count != 0)
                        Monkeys.Clear();

                    foreach (var monkey in monkeys)
                        Monkeys.Add(monkey);

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Unable to get monkeys: {ex.Message}");
                    await dialogService.DisplayAlertAsync("Error!", ex.Message, "OK");
                }
                finally
                {
                    IsBusy = false;
                    IsRefreshing = false;
                }
            });

            GetClosestMonkeyCommand = new DelegateCommand(async () =>
            {
                if (IsBusy || Monkeys.Count == 0)
                    return;

                try
                {
                    // Get cached location, else get real location.
                    var location = await geolocation.GetLastKnownLocationAsync();
                    if (location == null)
                    {
                        location = await geolocation.GetLocationAsync(new GeolocationRequest
                        {
                            DesiredAccuracy = GeolocationAccuracy.Medium,
                            Timeout = TimeSpan.FromSeconds(30)
                        });
                    }

                    // Find closest monkey to us
                    var first = Monkeys.OrderBy(m => location.CalculateDistance(
                        new Location(m.Latitude, m.Longitude), DistanceUnits.Miles))
                        .FirstOrDefault();

                    await dialogService.DisplayAlertAsync("", first.Name + " " +
                        first.Location, "OK");

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Unable to query location: {ex.Message}");
                    await dialogService.DisplayAlertAsync("Error!", ex.Message, "OK");
                }
            });
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }

    }
}
