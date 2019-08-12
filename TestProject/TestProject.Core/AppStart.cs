using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.ViewModels;
using MvvmCross.Navigation;
using TestProject.Entities;
using TestProject.Services.Helpers;
using TestProject.Services.Helpers.Interfaces;
using MvvmCross;

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

            var storage = Mvx.IoCProvider.Resolve<IStorageHelper<User>>();
            var user = await storage.Retrieve();
            if (user == null)
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
