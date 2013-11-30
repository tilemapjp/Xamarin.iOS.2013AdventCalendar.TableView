using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace TableViewTest
{
	public class SecondViewController : UIViewController
	{
		int EventInserted = 0;
		SecondViewDataSource _dataSource;
		SecondViewDelegate   _delegate;

		public UITableView TableView
		{
			get { 
				return (UITableView)this.View;
			}
		}

		public SecondViewController () : base ()
		{
			Title = NSBundle.MainBundle.LocalizedString ("Second", "Second");
			TabBarItem.Image = UIImage.FromBundle ("second");
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void LoadView ()
		{
			base.LoadView ();

			var tableView = new UITableView ();
			this.View = tableView;

			_dataSource = new SecondViewDataSource (this);
			tableView.WeakDataSource = _dataSource;
			_delegate = new SecondViewDelegate (this);
			tableView.WeakDelegate = _delegate;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public int NumberOfSections (UITableView tableView)
		{
			return 100;
		}

		public int RowsInSection (UITableView tableview, int section)
		{
			return 5;
		}

		public UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("cell");

			if (cell == null) {
				cell = new UITableViewCell(UITableViewCellStyle.Value1, "cell");
			}
			cell.Tag = indexPath.Section * 10 + indexPath.Row;
			cell.TextLabel.Text = String.Format ("{0} - {1}", indexPath.Section, indexPath.Row);

			return cell;
		}

		public string TitleForHeader (UITableView tableView, int section)
		{
			return String.Format ("{0}", section);
		}

		public void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var message = String.Format ("{0} - {1}", indexPath.Section, indexPath.Row);

			if (EventInserted == 14) {
				message += "\nSome Event Inserted!";

				this.TableView.DecelerationStarted += (sender, e) => {
					Console.WriteLine("DecelerationStarted " + this.TableView.WeakDelegate.ToString());
				};

				this.TableView.DecelerationEnded += (sender, e) => {
					Console.WriteLine("DecelerationEnded " + this.TableView.WeakDelegate.ToString());
				};

				this.TableView.DraggingStarted += (sender, e) => {
					Console.WriteLine("DraggingStarted " + this.TableView.WeakDelegate.ToString());
				};

				this.TableView.WillEndDragging += (sender, e) => {
					Console.WriteLine("WillEndDragging " + this.TableView.WeakDelegate.ToString());
				};

				this.TableView.DraggingEnded += (sender, e) => {
					Console.WriteLine("DraggingEnded " + this.TableView.WeakDelegate.ToString());
				};

				this.TableView.Scrolled += (sender, e) => {
					Console.WriteLine("Scrolled " + this.TableView.WeakDelegate.ToString());
				};

				this.TableView.ScrollAnimationEnded += (sender, e) => {
					Console.WriteLine("ScrollAnimationEnded " + this.TableView.WeakDelegate.ToString());
				};

				this.TableView.ScrolledToTop += (sender, e) => {
					Console.WriteLine("ScrolledToTop " + this.TableView.WeakDelegate.ToString());
				};
			}
			EventInserted++;

			var alert = new UIAlertView ("Selected!", message, null, "OK", null);
			alert.Show ();
		}
	}

	public class SecondViewDataSource : UITableViewDataSource
	{
		WeakReference viewController;
		SecondViewController ViewController {
			get { 
				return viewController == null ? null : (SecondViewController) viewController.Target;
			}

			set {
				viewController = value == null ? null : new WeakReference (value);
			}
		}

		public SecondViewDataSource (SecondViewController viewController) : base()
		{
			this.ViewController = viewController;
		}

		public override int NumberOfSections (UITableView tableView)
		{
			return ViewController.NumberOfSections (tableView);
		}

		public override int RowsInSection (UITableView tableView, int section)
		{
			return ViewController.RowsInSection (tableView, section);
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			return ViewController.GetCell (tableView, indexPath);
		}

		public override string TitleForHeader (UITableView tableView, int section)
		{
			return ViewController.TitleForHeader (tableView, section);
		}
	}

	public class SecondViewDelegate : UITableViewDelegate
	{
		WeakReference viewController;
		SecondViewController ViewController {
			get { 
				return viewController == null ? null : (SecondViewController) viewController.Target;
			}

			set {
				viewController = value == null ? null : new WeakReference (value);
			}
		}

		public SecondViewDelegate (SecondViewController viewController) : base()
		{
			this.ViewController = viewController;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			ViewController.RowSelected (tableView, indexPath);
		}

		public override void DecelerationStarted (UIScrollView scrollView)
		{
			Console.WriteLine("DecelerationStarted " + ViewController.TableView.WeakDelegate.ToString());
		}

		public override void DecelerationEnded (UIScrollView scrollView)
		{
			Console.WriteLine("DecelerationEnded " + ViewController.TableView.WeakDelegate.ToString());
		}

		public override void DraggingStarted (UIScrollView scrollView)
		{
			Console.WriteLine("DraggingStarted " + ViewController.TableView.WeakDelegate.ToString());
		}

		public override void WillEndDragging (UIScrollView scrollView, PointF velocity, ref PointF targetContentOffset)
		{
			Console.WriteLine("WillEndDragging " + ViewController.TableView.WeakDelegate.ToString());
		}

		public override void DraggingEnded (UIScrollView scrollView, bool willDecelerate)
		{
			Console.WriteLine("DraggingEnded " + ViewController.TableView.WeakDelegate.ToString());
		}

		public override void Scrolled (UIScrollView scrollView)
		{
			Console.WriteLine("Scrolled " + ViewController.TableView.WeakDelegate.ToString());
		}

		public override void ScrollAnimationEnded (UIScrollView scrollView)
		{
			Console.WriteLine("ScrollAnimationEnded " + ViewController.TableView.WeakDelegate.ToString());
		}

		public override void ScrolledToTop (UIScrollView scrollView)
		{
			Console.WriteLine("ScrolledToTop " + ViewController.TableView.WeakDelegate.ToString());
		}
	}
}

