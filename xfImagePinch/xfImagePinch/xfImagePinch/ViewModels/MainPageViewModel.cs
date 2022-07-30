using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xfImagePinch.ViewModels
{
    using System.ComponentModel;
    using Prism.Events;
    using Prism.Navigation;
    using Prism.Services;
    public class MainPageViewModel : INotifyPropertyChanged, INavigationAware
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly INavigationService navigationService;
        public double OriginalWidth { get; set; } = 300;
        public double OriginalHeigh { get; set; } = 300;
        public double CurrentWidth { get; set; } = 300;
        public double CurrentHeigh { get; set; } = 300;
        public double Ratio { get; set; } = 1;
        public DelegateCommand ResizeImageCommand { get; set; }
        public MainPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            RefreshImage();

            ResizeImageCommand = new DelegateCommand(() =>
            {
                RefreshImage();
            });
        }

        void RefreshImage()
        {
            CurrentWidth = OriginalWidth * Ratio;
            CurrentHeigh = OriginalHeigh * Ratio;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }

    }
}
