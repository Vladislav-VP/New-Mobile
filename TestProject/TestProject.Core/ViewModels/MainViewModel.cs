using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using TestProject.Repositories.Interfaces;
using TestProject.Repositories;
using SQLite;
using TestProject.Entity;
using System.Threading.Tasks;

namespace TestProject.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        
        public IMvxAsyncCommand LoadTodoItemListCommand { get; private set; }

        public MainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            LoadTodoItemListCommand = new MvxAsyncCommand(async () =>
                await _navigationService.Navigate<LoginViewModel>());
        }
    }
}
