using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Core.Models;
using System.Threading.Tasks;
using MvvmCross.ViewModels;

namespace TestProject.Core.ViewModels
{
    public class BaseTodoItemViewModel : MvxViewModel
    {
        protected IMvxNavigationService _navigationService;

        protected TodoItemModel _item;

        public TodoItemModel Item
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
