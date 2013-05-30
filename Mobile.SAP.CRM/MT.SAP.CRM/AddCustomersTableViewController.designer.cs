// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace SAP.CRM.MT
{
	[Register ("AddCustomersTableViewController")]
	partial class AddCustomersTableViewController
	{
		[Outlet]
		MonoTouch.UIKit.UISearchBar searchBar { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (searchBar != null) {
				searchBar.Dispose ();
				searchBar = null;
			}
		}
	}
}
