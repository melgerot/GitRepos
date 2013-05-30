using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAP.CRM.Core.SQLite;


namespace SAP.CRM.Core.DL
{
    /// <summary>
    /// TaskDatabase builds on SQLite.Net and represents a specific database, in our case, the Task DB.
    /// It contains methods for retrieval and persistance as well as db creation, all based on the 
    /// underlying ORM.
    /// </summary>
    public class ActivityDatabase : SQLiteConnection
    {
        static object locker = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
        /// if the database doesn't exist, it will create the database and all the tables.
        /// </summary>
        /// <param name='path'>
        /// Path.
        /// </param>
        public ActivityDatabase(string path)
            : base(path)
        {
            // create the tables
            CreateTable<SAP.CRM.Core.BL.Activity>();
			//CreateTable<SAP.CRM.Core.BL.ActivityDetail> ();
            CreateTable<SAP.CRM.Core.BL.ActivityPartner>();
            CreateTable<SAP.CRM.Core.BL.ActivityText>();
            CreateTable<SAP.CRM.Core.BL.ActivitySearchSettings>();
            CreateTable<SAP.CRM.Core.BL.Customer>();
            CreateTable<SAP.CRM.Core.BL.CustomerContact>();
			CreateTable<SAP.CRM.Core.BL.TargetService> ();
        }

        public IEnumerable<T> GetItems<T>() where T : SAP.CRM.Core.BL.Contracts.IBusinessEntity, new()
        {
            lock (locker)
            {
                return (from i in Table<T>() select i).ToList();
            }
        }

        public T GetItem<T>(int id) where T : SAP.CRM.Core.BL.Contracts.IBusinessEntity, new()
        {
            lock (locker)
            {
                return Table<T>().FirstOrDefault(x => x.ID == id);
                // Following throws NotSupportedException - thanks aliegeni
                //return (from i in Table<T> ()
                //        where i.ID == id
                //        select i).FirstOrDefault ();
            }
        }

        public int SaveItem<T>(T item) where T : SAP.CRM.Core.BL.Contracts.IBusinessEntity
        {
            lock (locker)
            {
                if (item.ID != 0)
                {
                    Update(item);
                    return item.ID;
                }
                else
                {
                    return Insert(item);
                }
            }
        }

        public int DeleteItem<T>(int id) where T : SAP.CRM.Core.BL.Contracts.IBusinessEntity, new()
        {
            lock (locker)
            {
                return Delete<T>(new T() { ID = id });
            }
        }
    }
}
