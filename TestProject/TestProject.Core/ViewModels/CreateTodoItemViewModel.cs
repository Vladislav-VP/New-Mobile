using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Navigation;
using System.Threading.Tasks;

namespace TestProject.Core.ViewModels
{
    public class CreateTodoItemViewModel : BaseTodoItemViewModel
    {
        public CreateTodoItemViewModel(IMvxNavigationService navigationService)
            : base(navigationService)
        {
        }

        private async Task CreateItem()
        {

        }
    }
}
