using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.ViewModels;
using TestProject.Core.Models;

namespace TestProject.Core.ViewModels
{

    public class ListItemViewModel : MvxViewModel
    {
        private MvxObservableCollection<Item> _items;

        public MvxObservableCollection<Item> Items
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
