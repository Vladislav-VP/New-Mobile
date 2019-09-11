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

namespace TipCalc.iOS
{
    [Register ("LoginViewController")]
    partial class LoginViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbUsername { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView vUserInfo { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (lbUsername != null) {
                lbUsername.Dispose ();
                lbUsername = null;
            }

            if (vUserInfo != null) {
                vUserInfo.Dispose ();
                vUserInfo = null;
            }
        }
    }
}