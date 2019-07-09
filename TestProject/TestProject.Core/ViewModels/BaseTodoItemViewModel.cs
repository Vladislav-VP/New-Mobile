using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.ViewModels;
using TestProject.Entity;

namespace TestProject.Core.ViewModels
{
    public class BaseTodoItemViewModel : MvxViewModel
    {
        protected readonly IMvxNavigationService _navigationService;

        protected TodoItem _item;

        public TodoItem Item
        {
            get => _item;
            set
            {
                _item = value;
                RaisePropertyChanged(() => Item);
            }
        }

        public BaseTodoItemViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
