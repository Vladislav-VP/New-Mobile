using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using TestProject.Entities;

namespace TestProject.Services
{
    public static class StaticObjects
    {
        static StaticObjects()
        {
            User = new User();
            TodoItems = new MvxObservableCollection<TodoItem>();
        }

        public static User User { get; set; }

        public static MvxObservableCollection<TodoItem> TodoItems { get; set; }
    }
}

