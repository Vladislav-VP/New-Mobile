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
    [Register ("CancelDialogViewController")]
    partial class CancelDialogViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btCancel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btNo { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btYes { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lbSaveChanges { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView vSaveChanges { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btCancel != null) {
                btCancel.Dispose ();
                btCancel = null;
            }

            if (btNo != null) {
                btNo.Dispose ();
                btNo = null;
            }

            if (btYes != null) {
                btYes.Dispose ();
                btYes = null;
            }

            if (lbSaveChanges != null) {
                lbSaveChanges.Dispose ();
                lbSaveChanges = null;
            }

            if (vSaveChanges != null) {
                vSaveChanges.Dispose ();
                vSaveChanges = null;
            }
        }
    }
}