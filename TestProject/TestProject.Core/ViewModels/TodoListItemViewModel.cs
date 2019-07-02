using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;
using TestProject.Core.Models;

namespace TestProject.Core.ViewModels
{

    public class TodoListItemViewModel : MvxViewModel
    {
        private MvxObservableCollection<TodoItemModel> _items;

        public MvxObservableCollection<TodoItemModel> Items
        {
            get => _items;
            set
            {
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }
    }
}
