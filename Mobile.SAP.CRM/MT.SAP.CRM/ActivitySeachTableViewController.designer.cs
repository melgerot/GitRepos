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
		MonoTouch.UIKit.UITableViewCell DaysBackwardCell { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel DaysBackwardLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableViewCell DaysForwardCell { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableViewCell MyActivities { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableViewCell MyCustomers { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DaysBackwardCell != null) {
				DaysBackwardCell.Dispose ();
				DaysBackwardCell = null;
			}

			if (DaysBackwardLabel != null) {
				DaysBackwardLabel.Dispose ();
				DaysBackwardLabel = null;
			}

			if (DaysForwardCell != null) {
				DaysForwardCell.Dispose ();
				DaysForwardCell = null;
			}

			if (MyActivities != null) {
				MyActivities.Dispose ();
				MyActivities = null;
			}

			if (MyCustomers != null) {
				MyCustomers.Dispose ();
				MyCustomers = null;
			}
		}
	}
}
