using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LabelCounterSample.ViewModels
{
    public class TestPageViewModel : ViewModelBase
    {
        public TestPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
