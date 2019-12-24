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
    [Register ("ResetPasswordViewController")]
    partial class ResetPasswordViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btSend { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbRecoveryEmail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tfRecoveryEmail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView vUserInfo { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btSend != null) {
                btSend.Dispose ();
                btSend = null;
            }

            if (lbRecoveryEmail != null) {
                lbRecoveryEmail.Dispose ();
                lbRecoveryEmail = null;
            }

            if (tfRecoveryEmail != null) {
                tfRecoveryEmail.Dispose ();
                tfRecoveryEmail = null;
            }

            if (vUserInfo != null) {
                vUserInfo.Dispose ();
                vUserInfo = null;
            }
        }
    }
}