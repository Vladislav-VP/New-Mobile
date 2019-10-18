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
    [Register ("EditPasswordViewController")]
    partial class EditPasswordViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btCancel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btSaveChanges { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbNewPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbOldPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbPasswordConfirmation { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tfNewPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tfOldPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tfPasswordConfirmation { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView vEditPassword { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btCancel != null) {
                btCancel.Dispose ();
                btCancel = null;
            }

            if (btSaveChanges != null) {
                btSaveChanges.Dispose ();
                btSaveChanges = null;
            }

            if (lbNewPassword != null) {
                lbNewPassword.Dispose ();
                lbNewPassword = null;
            }

            if (lbOldPassword != null) {
                lbOldPassword.Dispose ();
                lbOldPassword = null;
            }

            if (lbPasswordConfirmation != null) {
                lbPasswordConfirmation.Dispose ();
                lbPasswordConfirmation = null;
            }

            if (tfNewPassword != null) {
                tfNewPassword.Dispose ();
                tfNewPassword = null;
            }

            if (tfOldPassword != null) {
                tfOldPassword.Dispose ();
                tfOldPassword = null;
            }

            if (tfPasswordConfirmation != null) {
                tfPasswordConfirmation.Dispose ();
                tfPasswordConfirmation = null;
            }

            if (vEditPassword != null) {
                vEditPassword.Dispose ();
                vEditPassword = null;
            }
        }
    }
}