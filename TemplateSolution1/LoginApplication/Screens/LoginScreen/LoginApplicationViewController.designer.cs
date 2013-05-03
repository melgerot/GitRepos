// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace LoginApplication
{
	[Register ("LoginApplicationViewController")]
	partial class LoginApplicationViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnLogin { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtFldUsername { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtFldPassword { get; set; }

		[Action ("onBtnClick:")]
		partial void onBtnClick (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (btnLogin != null) {
				btnLogin.Dispose ();
				btnLogin = null;
			}

			if (txtFldUsername != null) {
				txtFldUsername.Dispose ();
				txtFldUsername = null;
			}

			if (txtFldPassword != null) {
				txtFldPassword.Dispose ();
				txtFldPassword = null;
			}
		}
	}
}
