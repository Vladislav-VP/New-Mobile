using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using TestProject.Core.Services.Interfaces;
using TestProject.Core.Services;
using SQLite;
using TestProject.Core.Models;

namespace TestProject.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private IMvxNavigationService _navigationService;
        private IDBService _dBService;

        public IMvxAsyncCommand LoadTodoItemListCommand { get; private set; }

        public MainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            LoadTodoItemListCommand = new MvxAsyncCommand(async () =>
                await _navigationService.Navigate<LoginViewModel>());
        }

    }
}
