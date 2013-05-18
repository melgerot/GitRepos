using System;
using System.Collections.Generic;
using System.IO;
using SAP.CRM.Core.BL;
using SAP.CRM.Core.DL;
using System.Linq;


namespace SAP.CRM.Core.DAL
{
    public class ApplicationRepository
    {
        DL.ActivityDatabase db = null;
		protected static string dbLocation;		
		protected static ApplicationRepository me;		
		
		static ApplicationRepository ()
		{
			me = new ApplicationRepository();
		}

        protected ApplicationRepository()
		{
			// set the db location
			dbLocation = DatabaseFilePath;
			
			// instantiate the database	
			db = new ActivityDatabase(dbLocation);
		}
		
		public static string DatabaseFilePath {
			get { 
				var sqliteFilename = "ActivityDB.db3";
#if WIN32NT
            string unicodeString = sqliteFilename;
            System.Text.Encoding unicode = System.Text.Encoding.Unicode;
            System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
            byte[] unicodeBytes = unicode.GetBytes(unicodeString);
            byte[] utf8Bytes = System.Text.Encoding.Convert(unicode, utf8, unicodeBytes);
            char[] utf8Chars = new char[utf8.GetCharCount(utf8Bytes, 0, utf8Bytes.Length)];
            utf8.GetChars(utf8Bytes, 0, utf8Bytes.Length, utf8Chars, 0);
            string utf8String = new string(utf8Chars);
            sqliteFilename = utf8String;
#endif

#if NETFX_CORE
                var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, sqliteFilename);
#else

#if SILVERLIGHT
				// Windows Phone expects a local path, not absolute
	            var path = sqliteFilename;
#else

#if WIN32NT
                var path = sqliteFilename;
#else

#if __ANDROID__
				// Just use whatever directory SpecialFolder.Personal returns
	            string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
#else
                // we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
                // (they don't want non-user-generated data in Documents)
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
                string libraryPath = Path.Combine(documentsPath, "../Library/"); // Library folder
#endif
                var path = Path.Combine(libraryPath, sqliteFilename);
#endif

#endif

#endif
                return path;
            }
        }

        #region TargetService

        public static TargetService GetActiveTargetService()
        {
            return me.db.GetItems<TargetService>().FirstOrDefault<TargetService>(t => t.Active == true);  
        }

        public static TargetService GetTargetService(int id)
        {
            return me.db.GetItem<TargetService>(id);
        }

        public static IEnumerable<TargetService> GetTargetServices()
        {
            return me.db.GetItems<TargetService>();
        }

        public static int SaveTargetService(TargetService item)
        {
            return me.db.SaveItem<TargetService>(item);
        }

        public static int DeleteTargetService(int id)
        {
            return me.db.DeleteItem<TargetService>(id);
        }

        #endregion

        #region Activity

        public static Activity GetActivity(int id)
		{
            return me.db.GetItem<Activity>(id);
		}

        public static IEnumerable<Activity> GetActivities()
		{
            return me.db.GetItems<Activity>();
		}

        public static int SaveActivity(Activity item)
		{
            return me.db.SaveItem<Activity>(item);
		}

        public static int DeleteActivity(int id)
		{
            return me.db.DeleteItem<Activity>(id);
        }

		public static void DeleteActivities ()
		{
			// TODO Implement efficient delete query
		}

        #endregion

        #region ActivityPartner

        public static int SaveActivityPartner(ActivityPartner item)
        {
            return me.db.SaveItem<ActivityPartner>(item);
        }

        #endregion

        #region ActivitySearchSettings

		public static ActivitySearchSettings GetFirstActivitySearchSettings()
		{
			return me.db.GetItems<ActivitySearchSettings>().FirstOrDefault();
		}

        public static ActivitySearchSettings GetActivitySearchSettings(int id)
        {
            return me.db.GetItem<ActivitySearchSettings>(id);
        }

        public static IEnumerable<ActivitySearchSettings> GetActivitiesSearchSettings()
        {
            return me.db.GetItems<ActivitySearchSettings>();
        }

        public static int SaveActivitySearchSettings(ActivitySearchSettings item)
        {
            return me.db.SaveItem<ActivitySearchSettings>(item);
        }

        public static int DeleteActivitySearchSettings(int id)
        {
            return me.db.DeleteItem<ActivitySearchSettings>(id);
        }

        #endregion


    }
}
