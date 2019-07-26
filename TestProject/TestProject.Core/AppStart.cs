using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.ViewModels;
using MvvmCross.Navigation;
using TestProject.Entities;
using TestProject.Services.Helpers;

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

            if (await new CredentialsStorageHelper().Load() == null)
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
