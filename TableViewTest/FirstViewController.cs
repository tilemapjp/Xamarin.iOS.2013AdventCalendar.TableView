using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace TableViewTest
{
	public class FirstViewController : UITableViewController
	{
		int EventInserted = 0;

		public FirstViewController () : base ()
		{
			Title = NSBundle.MainBundle.LocalizedString ("First", "First");
			TabBarItem.Image = UIImage.FromBundle ("first");
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
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override int NumberOfSections (UITableView tableView)
		{
			return 100;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return 5;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("cell");

			if (cell == null) {
				cell = new UITableViewCell(UITableViewCellStyle.Value1, "cell");
			}
			cell.Tag = indexPath.Section * 10 + indexPath.Row;
			cell.TextLabel.Text = String.Format ("{0} - {1}", indexPath.Section, indexPath.Row);

			return cell;
		}

		public override string TitleForHeader (UITableView tableView, int section)
		{
			return String.Format ("{0}", section);
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var message = String.Format ("{0} - {1}", indexPath.Section, indexPath.Row);

			if (EventInserted == 4) {
				message += "\nSome Event Inserted!";

				this.TableView.DecelerationStarted += (sender, e) => {
					Console.WriteLine("DecelerationStarted");
				};

				this.TableView.DecelerationEnded += (sender, e) => {
					Console.WriteLine("DecelerationEnded");
				};

				this.TableView.DraggingStarted += (sender, e) => {
					Console.WriteLine("DraggingStarted");
				};

				this.TableView.WillEndDragging += (sender, e) => {
					Console.WriteLine("WillEndDragging");
				};

				this.TableView.DraggingEnded += (sender, e) => {
					Console.WriteLine("DraggingEnded");
				};

				this.TableView.Scrolled += (sender, e) => {
					Console.WriteLine("Scrolled");
				};

				this.TableView.ScrollAnimationEnded += (sender, e) => {
					Console.WriteLine("ScrollAnimationEnded");
				};

				this.TableView.ScrolledToTop += (sender, e) => {
					Console.WriteLine("ScrolledToTop");
				};
			}
			EventInserted++;

			var alert = new UIAlertView ("Selected!", message, null, "OK", null);
			alert.Show ();
		}
	}
}

