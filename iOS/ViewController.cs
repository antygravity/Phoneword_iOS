using System;
using UIKit;
using Phoneword_iOS;
using Foundation;

namespace Phoneword_iOS.iOS
{
	public partial class ViewController : UIViewController
	{
		int count = 1;

		public ViewController (IntPtr handle) : base (handle)
		{		
		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Code to start the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start ();
			#endif


			string translateNumber = "";
			TranslateButton.TouchUpInside += (object sender, EventArgs e) => {
				translateNumber = PhoneTranslator.ToNumber(PhoneNumberText.Text);

				// Dismiss the keyboard if text field was tapped
				PhoneNumberText.ResignFirstResponder();

				if (translateNumber == "") {
					CallButton.SetTitle("Call", UIControlState.Normal);
					CallButton.Enabled = false;
				} else {
					CallButton.SetTitle("Call " + translateNumber, UIControlState.Normal);
					CallButton.Enabled = true;
				}		
			};
				

			CallButton.TouchUpInside += (object sender, EventArgs e) => {
				var url = new NSUrl("tel:" + translateNumber);

				if (!UIApplication.SharedApplication.OpenUrl(url)) {
					var alert = UIAlertController.Create("Not suported!", "Scheme 'tel:' is not supperted on this device.", UIAlertControllerStyle.Alert);
					alert.AddAction(UIAlertAction.Create("ok", UIAlertActionStyle.Default, null));
					PresentViewController(alert, true, null);
				}
			};

		}
		public override void DidReceiveMemoryWarning ()
		{		
			base.DidReceiveMemoryWarning ();		
			// Release any cached data, images, etc that aren't in use.		
		}
	}
}
