using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TestProject.Entities;

namespace TestProject.Services
{
    public static class StaticObjects
    {
        public static User CurrentUser { get; set; }

        public static TodoItem CurrentTodoItem { get; set; }

        public static IEnumerable<TodoItem> CurrentTodoItems { get; set; }
    }
}

