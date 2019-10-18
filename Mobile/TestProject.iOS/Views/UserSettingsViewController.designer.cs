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
    [Register ("UserSettingsViewController")]
    partial class UserSettingsViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btChangePassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btDeleteAccount { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btSaveChanges { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbUsername { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tfUsername { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView vUsername { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btChangePassword != null) {
                btChangePassword.Dispose ();
                btChangePassword = null;
            }

            if (btDeleteAccount != null) {
                btDeleteAccount.Dispose ();
                btDeleteAccount = null;
            }

            if (btSaveChanges != null) {
                btSaveChanges.Dispose ();
                btSaveChanges = null;
            }

            if (lbUsername != null) {
                lbUsername.Dispose ();
                lbUsername = null;
            }

            if (tfUsername != null) {
                tfUsername.Dispose ();
                tfUsername = null;
            }

            if (vUsername != null) {
                vUsername.Dispose ();
                vUsername = null;
            }
        }
    }
}