using System;
using Foundation;
using UIKit;
using System.Linq;
using DeliveriesApp.Model;

namespace DeliveriesApp.iOS
{
	public partial class ViewController : UIViewController
	{
        bool hasLoggedIn = false;
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

            signinButton.TouchUpInside += SigninButton_TouchUpInside;
		}

        private async void SigninButton_TouchUpInside(object sender, EventArgs e)
        {
            var email = emailTextField.Text;
            var password = passwordTextField.Text;
            UIAlertController alert = null;

            hasLoggedIn = false;

            var result = await User.Login(email, password);

            if(result)
            {
                hasLoggedIn = true;
                alert = UIAlertController.Create("Success", "Welcome", UIAlertControllerStyle.Alert);
            }
            else
            {
                alert = UIAlertController.Create("Failure", "Couldn't log you in, please try again later", UIAlertControllerStyle.Alert);
            }

            PerformSegue("loginSegue", this);

            alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
            PresentViewController(alert, true, null);
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            if(segue.Identifier == "registerSegue")
            {
                var destinationViewController = segue.DestinationViewController as RegisterViewController;
                destinationViewController.emailAddress = emailTextField.Text;
            }
        }

        public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
        {
            if(segueIdentifier == "loginSegue")
            {
                return hasLoggedIn;
            }
            return true;
        }

        public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

