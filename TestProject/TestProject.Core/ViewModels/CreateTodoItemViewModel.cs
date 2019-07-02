using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Core.Models;

namespace TestProject.Core.ViewModels
{
    public class CreateTodoItemViewModel : MvxViewModel
    {
        private TodoItemModel _item;

        public TodoItemModel Item
        {
            get => _item;
            set
            {
                _item = value;
                RaisePropertyChanged(() => Item);
            }
        }


        private async Task CreateItem()
        {

        }

        private async Task DeleteItem()
        {

        }

        private async Task Edit()
        {

        }

    }
}
