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
    [Register ("LoginViewController")]
    partial class LoginViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btLogin { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btRegistration { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbForgotPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbUsername { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbWithoutAccount { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tfPassword { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField tfUsername { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView vUserInfo { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btLogin != null) {
                btLogin.Dispose ();
                btLogin = null;
            }

            if (btRegistration != null) {
                btRegistration.Dispose ();
                btRegistration = null;
            }

            if (lbForgotPassword != null) {
                lbForgotPassword.Dispose ();
                lbForgotPassword = null;
            }

            if (lbPassword != null) {
                lbPassword.Dispose ();
                lbPassword = null;
            }

            if (lbUsername != null) {
                lbUsername.Dispose ();
                lbUsername = null;
            }

            if (lbWithoutAccount != null) {
                lbWithoutAccount.Dispose ();
                lbWithoutAccount = null;
            }

            if (tfPassword != null) {
                tfPassword.Dispose ();
                tfPassword = null;
            }

            if (tfUsername != null) {
                tfUsername.Dispose ();
                tfUsername = null;
            }

            if (vUserInfo != null) {
                vUserInfo.Dispose ();
                vUserInfo = null;
            }
        }
    }
}