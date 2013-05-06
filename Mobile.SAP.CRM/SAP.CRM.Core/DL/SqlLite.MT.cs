//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Mono.Data.Sqlite;
//using System.IO;


//namespace SAP.CRM.Core
//{
//    internal class SqlLite
//    {
//        #region Data memebers

//        private const string DbFileName = "SAP.CRM.db3";
//        private const string TbTemplateName = "TEMPLATES";

//        #endregion

//        #region CRUD methods for SAP.CRM.Core.Activity

//        public static IEnumerable<Activity> GetActivities()
//        {
//            return null;
//        }

//        public static void AddActivity(Activity activity)
//        {

//        }

//        public static void RemoveAllActivities()
//        {

//        }

//        #endregion

//        #region CRUD methods for SAP.CRM.Core.ActivityPartner

//        #endregion

//        #region CRUD methods for SAP.CRM.Core.ActivityText

//        #endregion

//        #region CRUD methods for Contacts

//        #region

//        #region Private methods

//        /// <summary>
//        /// Get sonnection to DB
//        /// </summary>
//        private static SqliteConnection GetConnection()
//        {
//            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DbFileName);
//            var exists = File.Exists(dbPath);

//            if (!exists)
//                SqliteConnection.CreateFile(dbPath);

//            var conn = new SqliteConnection("Data Source=" + dbPath);

//            if (!exists)
//                CreateDatabase(conn);

//            return conn;
//        }

//        /// <summary>
//        /// Create tables
//        /// </summary>
//        private static void CreateDatabase(SqliteConnection connection)
//        {
//            string[] tablesDDLscript = 
//            {
//                String.Format ("CREATE TABLE {0} (Id INTEGER PRIMARY KEY AUTOINCREMENT, NAME TEXT, DESCRIPTION TEXT, TEMPLATE_ID TEXT, TEMPLATETYPE_ID TEXT);", TbTemplateName),
//            };

//            try
//            {
//                connection.Open();

//                foreach (string query in tablesDDLscript)
//                {
//                    using (var cmd = connection.CreateCommand())
//                    {
//                        cmd.CommandText = query;
//                        cmd.ExecuteNonQuery();
//                    }
//                }
//                connection.Close();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.ToString());
//            }
//        }

//        #endregion
//    }
//}
