// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace SAP.CRM.MT
{
	[Register ("StatusSelectionTableViewController")]
	partial class StatusSelectionTableViewController
	{
		[Outlet]
		MonoTouch.UIKit.UITableViewCell StatusOpenCell { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableViewCell StatusInProgressCell { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableViewCell StatusCompletedCell { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (StatusOpenCell != null) {
				StatusOpenCell.Dispose ();
				StatusOpenCell = null;
			}

			if (StatusInProgressCell != null) {
				StatusInProgressCell.Dispose ();
				StatusInProgressCell = null;
			}

			if (StatusCompletedCell != null) {
				StatusCompletedCell.Dispose ();
				StatusCompletedCell = null;
			}
		}
	}
}
