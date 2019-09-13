using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace TestProject.iOS.Sources
{
    public class TableSource : MvxStandardTableViewSource
    {
        private static readonly NSString _identifier = new NSString(Constants.CellIdentifier);

        public TableSource(UITableView tableView, string bindingText)
            : base(tableView, UITableViewCellStyle.Default, _identifier, bindingText)
        {
        }
    }
}