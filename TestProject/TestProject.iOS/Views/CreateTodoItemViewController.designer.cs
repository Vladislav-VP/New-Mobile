// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace TestProject.iOS.Views
{
    [Register ("CreateTodoItemViewController")]
    partial class CreateTodoItemViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btSave { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbDescription { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbDone { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbTaskName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch swDone { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tfTaskName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView tvDescription { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView vTodoItem { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btSave != null) {
                btSave.Dispose ();
                btSave = null;
            }

            if (lbDescription != null) {
                lbDescription.Dispose ();
                lbDescription = null;
            }

            if (lbDone != null) {
                lbDone.Dispose ();
                lbDone = null;
            }

            if (lbTaskName != null) {
                lbTaskName.Dispose ();
                lbTaskName = null;
            }

            if (swDone != null) {
                swDone.Dispose ();
                swDone = null;
            }

            if (tfTaskName != null) {
                tfTaskName.Dispose ();
                tfTaskName = null;
            }

            if (tvDescription != null) {
                tvDescription.Dispose ();
                tvDescription = null;
            }

            if (vTodoItem != null) {
                vTodoItem.Dispose ();
                vTodoItem = null;
            }
        }
    }
}