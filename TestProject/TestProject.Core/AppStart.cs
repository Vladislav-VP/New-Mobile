using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.ViewModels;
using MvvmCross.Navigation;
using TestProject.Services.Storages;
using TestProject.Entities;

namespace TestProject.Core
{
    public class AppStart : MvxAppStart
    {
        public AppStart(IMvxApplication application, IMvxNavigationService navigationService) 
            : base(application, navigationService)
        {
        }

        protected async override Task NavigateToFirstViewModel(object hint = null)
        {
            if (!LocalStorage<User>.StorageExists())
            {
                await NavigationService.Navigate<LoginViewModel>();
            }
            else
            {
                await NavigationService.Navigate<TodoListItemViewModel>();
            }
        }
    }
}
