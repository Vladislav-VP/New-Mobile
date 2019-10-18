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
    [Register ("MenuViewController")]
    partial class MenuViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imviewProfile { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbUsername { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIStackView stviewMenuItems { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView vHeader { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imviewProfile != null) {
                imviewProfile.Dispose ();
                imviewProfile = null;
            }

            if (lbUsername != null) {
                lbUsername.Dispose ();
                lbUsername = null;
            }

            if (stviewMenuItems != null) {
                stviewMenuItems.Dispose ();
                stviewMenuItems = null;
            }

            if (vHeader != null) {
                vHeader.Dispose ();
                vHeader = null;
            }
        }
    }
}