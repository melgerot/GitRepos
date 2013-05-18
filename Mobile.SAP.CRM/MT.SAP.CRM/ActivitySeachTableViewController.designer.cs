// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace SAP.CRM.MT
{
	[Register ("ActivitySeachTableViewController")]
	partial class ActivitySeachTableViewController
	{
		[Outlet]
		MonoTouch.UIKit.UITableViewCell MyActivityCell { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableViewCell MyCustomersCell { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (MyActivityCell != null) {
				MyActivityCell.Dispose ();
				MyActivityCell = null;
			}

			if (MyCustomersCell != null) {
				MyCustomersCell.Dispose ();
				MyCustomersCell = null;
			}
		}
	}
}
