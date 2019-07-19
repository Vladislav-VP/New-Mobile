using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO.IsolatedStorage;
using SQLite;
using TestProject.Entities;
using System.Threading.Tasks;
using TestProject.Configurations;

namespace TestProject.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(IMvxNavigationService navigationService)
            : base(navigationService)
        {
            ShowListTodoItemsViewModelCommand = new MvxAsyncCommand(async () =>
                  await _navigationService.Navigate<TodoListItemViewModel>());
            ShowMenuViewModelCommand = new MvxAsyncCommand(async () =>
                  await _navigationService.Navigate<MenuViewModel>());
        }
        
        public IMvxAsyncCommand ShowListTodoItemsViewModelCommand { get; private set; }

        public IMvxAsyncCommand ShowMenuViewModelCommand { get; private set; }
    }
}
