// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace TestProject.iOS.Views
{
    [Register ("TodoItemListViewController")]
    partial class TodoItemListViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView tvTodoList { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (tvTodoList != null) {
                tvTodoList.Dispose ();
                tvTodoList = null;
            }
        }
    }
}