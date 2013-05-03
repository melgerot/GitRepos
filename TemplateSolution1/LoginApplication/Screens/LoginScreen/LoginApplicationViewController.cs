using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace LoginApplication
{
	public partial class LoginApplicationViewController : UIViewController
	{
		public LoginApplicationViewController () : base ("LoginApplicationViewController", null)
		{
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib

			txtFldUsername.ShouldReturn = delegate (UITextField field){
				txtFldPassword.BecomeFirstResponder();	
				return true;
			};

			txtFldPassword.ShouldReturn = delegate (UITextField field){
				field.ResignFirstResponder ();
				return true;
			};

			this.btnLogin.TouchDown += (sender, e) => { 
				txtFldPassword.ResignFirstResponder();
			};

//			this.btnLogin.TouchUpInside += (sender, e) => { 
//			
//			};
		}

		partial void onBtnClick (MonoTouch.Foundation.NSObject sender)
		{
			string message = string.Format("User {0} successfully logged in.",this.txtFldUsername.Text);
			using (UIAlertView alert = new UIAlertView ("Success", message, null, "OK", null))
			{
				alert.Show ();
			}		
			this.txtFldPassword.Text = "";
		}

//		
//		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
//		{
//			// Return true for supported orientations
//			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
//		}
	}
}

