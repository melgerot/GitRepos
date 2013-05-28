// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace SAP.CRM.MT
{
	[Register ("CustomActivityCell")]
	partial class CustomActivityCell
	{
		[Outlet]
		MonoTouch.UIKit.UILabel TitleLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel SubTitle1Label { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel SubTitle2Label { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}

			if (SubTitle1Label != null) {
				SubTitle1Label.Dispose ();
				SubTitle1Label = null;
			}

			if (SubTitle2Label != null) {
				SubTitle2Label.Dispose ();
				SubTitle2Label = null;
			}
		}
	}
}
