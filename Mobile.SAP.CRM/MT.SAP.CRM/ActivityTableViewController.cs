// This file has been autogenerated from parsing an Objective-C header file added in Xcode.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SAP.CRM.Core.BL;
using System.Collections.Generic;
using SAP.CRM.Core.BL.Managers;

namespace SAP.CRM.MT
{
	public partial class ActivityTableViewController : UITableViewController
	{

		private List<Activity> activities;

		LoadingOverlay loadingOverlay;

		public ActivityTableViewController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

			if(true) //  Update data if required 
			{
				// Add window loading overlay 
				loadingOverlay = new LoadingOverlay (UIScreen.MainScreen.Bounds);
				this.ParentViewController.Add(loadingOverlay);
				if(true) // Perform a full refresh
				{
					// Wire completion event and start update process
					UpdateList ();
				}
				else // Perform a delta refresh
				{
					UpdateList ();
				}
			}
			else
			{
				activities = ActivityManager.GetActivityList();
				TableView.Source = new ActivityTableViewSource(activities);
				this.TableView.ReloadData();
			}    
        }

		void UpdateList()
		{
			try 
			{
				// Wire completion event and start update process
				ActivityManager.UpdateFinished += HandleUpdateFinished;		
				ActivityManager.RefreshActivityList();
			} 
			catch (Exception ex) {
				new UIAlertView("Error", ex.Message, null, "OK", null).Show();
				HandleUpdateFinished (null, EventArgs.Empty);
			}
		}

        void HandleUpdateFinished (object sender, EventArgs e)
        {
			activities = ActivityManager.GetActivityList();

			this.InvokeOnMainThread (() => {
				this.TableView.Source = new ActivityTableViewSource(activities);
				this.TableView.ReloadData ();
				loadingOverlay.Hide ();
			});
        }

	}

    public class ActivityTableViewSource : UITableViewSource
    {
        #region -= class variables =-

        // declare vars
        protected List<Activity> activityItems;
        private string cellIdentifier = "activityCell";

        #endregion


        #region -= constructors =-

        protected ActivityTableViewSource() { }

        public ActivityTableViewSource(List<Activity> items)
        {
            activityItems = items;
        }

        #endregion


        #region -= data binding/display methods =-

        /// <summary>
        /// Called by the TableView to determine how many sections(groups) there are.
        /// </summary>
        public override int NumberOfSections(UITableView tableView)
        {
			return 1;
        }

        /// <summary>
        /// Called by the TableView to determine how many cells to create for that particular section.
        /// </summary>
        public override int RowsInSection(UITableView tableview, int section)
        {
            return activityItems.Count;
        }

        /// <summary>
        /// Called by the TableView to retrieve the header text for the particular section(group)
        /// </summary>
        //public override string TitleForHeader(UITableView tableView, int section)
        //{
        //    return tableItems[section].Name;
        //}

        /// <summary>
        /// Called by the TableView to retrieve the footer text for the particular section(group)
        /// </summary>
        //public override string TitleForFooter(UITableView tableView, int section)
        //{
        //    return tableItems[section].Footer;
        //}

		public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 75;
		}

        #endregion


        #region -= user interaction methods =-

        //public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        //{
        //    new UIAlertView("Row Selected", activityItems[indexPath.Section].Items[indexPath.Row].Heading, null, "OK", null).Show();
        //}

        //public override void RowDeselected(UITableView tableView, NSIndexPath indexPath)
        //{
        //    Console.WriteLine("Row " + indexPath.Row.ToString() + " deselected");
        //}

        //public override void AccessoryButtonTapped(UITableView tableView, NSIndexPath indexPath)
        //{
        //    Console.WriteLine("Accessory for Section, " + indexPath.Section.ToString() + " and Row, " + indexPath.Row.ToString() + " tapped");
        //}

        #endregion


        /// <summary>
        /// Called by the TableView to get the actual UITableViewCell to render for the particular section and row
        /// </summary>

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            // in a Storyboard, Dequeue will ALWAYS return a cell, 
            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);

            // Set the properties as normal
            ((CustomActivityCell)cell).Title.Text = activityItems[indexPath.Row].ActivityCommentField.ToString();
            ((CustomActivityCell)cell).SubTitle1.Text = "Customer Name ...";
            ((CustomActivityCell)cell).SubTitle2.Text = string.Format("{0} {1} Status: {2}",
                activityItems[indexPath.Row].ActivityTypeField.ToString(),
                activityItems[indexPath.Row].FromDateField.ToString(),
                activityItems[indexPath.Row].StateField.ToString());

            return cell;
        }



    }
}
