using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private IMvxNavigationService _navigationService;

        public IMvxAsyncCommand LoadTodoItemListCommand { get; private set; }

        public MainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            LoadTodoItemListCommand = new MvxAsyncCommand(async () =>
                await _navigationService.Navigate</*TodoListItemViewModel*/LoginViewModel>());
        }
    }
}
