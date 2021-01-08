using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabelCounterSample.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private double _money;
        public double Money
        {
            get => _money;
            set => SetProperty(ref _money, value);
        }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
            this.Money = (double)1000.99;
        }
    }
}
;