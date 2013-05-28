// This file has been autogenerated from parsing an Objective-C header file added in Xcode.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using SAP.CRM.Core.BL;
using SAP.CRM.Core.BL.Managers;

namespace SAP.CRM.MT
{
	public partial class StatusSelectionTableViewController : UITableViewController
	{
		private ActivitySearchSettings settings;

		public StatusSelectionTableViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			ActivitySearchSettings activitySearchSettings;
			
			// Get data for view
			activitySearchSettings = ActivityManager.GetActivitySearchSettings();
			if(activitySearchSettings == null) 
			{
				// Create new if not exists
				activitySearchSettings = new ActivitySearchSettings();
				ActivityManager.SaveActivitySearchSettings(activitySearchSettings);
			}

			// Prepare controls for output
			if(!activitySearchSettings.StatusOpen) StatusOpenCell.Accessory = UITableViewCellAccessory.None;
			else StatusOpenCell.Accessory = UITableViewCellAccessory.Checkmark;

			if(!activitySearchSettings.StatusInProgress) StatusInProgressCell.Accessory = UITableViewCellAccessory.None;
			else StatusInProgressCell.Accessory = UITableViewCellAccessory.Checkmark;

			if(!activitySearchSettings.StatusCompleted) StatusCompletedCell.Accessory = UITableViewCellAccessory.None;
			else StatusCompletedCell.Accessory = UITableViewCellAccessory.Checkmark;

			this.TableView.Delegate = new StatusSelectionTableViewDelegate(activitySearchSettings);
		}
	}

	public class StatusSelectionTableViewDelegate : UITableViewDelegate
	{
		private ActivitySearchSettings activitySearchSettings;
		
		public StatusSelectionTableViewDelegate(){}
		
		public StatusSelectionTableViewDelegate(ActivitySearchSettings settings)
		{
			activitySearchSettings = settings;
		}
		
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			if(indexPath.Row == 0 && indexPath.Section == 0)
			{
				if(!activitySearchSettings.StatusOpen)
				{
					activitySearchSettings.StatusOpen = true;
					tableView.CellAt (indexPath).Accessory = UITableViewCellAccessory.Checkmark;
				}
				else
				{
					activitySearchSettings.StatusOpen = false;
					tableView.CellAt (indexPath).Accessory = UITableViewCellAccessory.None;
				}
				ActivityManager.SaveActivitySearchSettings(activitySearchSettings);
			}
			
			if(indexPath.Row == 1 && indexPath.Section == 0)
			{
				if(!activitySearchSettings.StatusInProgress)
				{
					activitySearchSettings.StatusInProgress = true;
					tableView.CellAt (indexPath).Accessory = UITableViewCellAccessory.Checkmark;
				}
				else
				{
					activitySearchSettings.StatusInProgress = false;
					tableView.CellAt (indexPath).Accessory = UITableViewCellAccessory.None;
				}
				ActivityManager.SaveActivitySearchSettings(activitySearchSettings);
			}

			if(indexPath.Row == 2 && indexPath.Section == 0)
			{
				if(!activitySearchSettings.StatusCompleted)
				{
					activitySearchSettings.StatusCompleted = true;
					tableView.CellAt (indexPath).Accessory = UITableViewCellAccessory.Checkmark;
				}
				else
				{
					activitySearchSettings.StatusCompleted = false;
					tableView.CellAt (indexPath).Accessory = UITableViewCellAccessory.None;
				}
				ActivityManager.SaveActivitySearchSettings(activitySearchSettings);
			}
			tableView.DeselectRow (indexPath, true);		
		}
	}
}


