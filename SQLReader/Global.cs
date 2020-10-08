
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace DCS
{
    public sealed class Global
    {
        /// <summary>
        /// This is an expensive resource.
        /// We need to only store it in one place.
        /// </summary>
        

        private SQLiteDatabase _sqlitedatabase;
        public SQLiteDatabase SqliteDatabase
        {
            get
            {
                return _sqlitedatabase;
            }
            set
            {
                
                _sqlitedatabase = value;
                
            }
        }
        private string _databasefullpath;
        public string databaseFullpath
        {
            get
            {
                return _databasefullpath;
            }
            set
            {
                _databasefullpath = value;
                

            }
        }
        private string _connectstring;
        public string ConnectString
        {
            get
            {
                return _connectstring;
            }
            set
            {
                _connectstring = value;

            }
        }
        /// <summary>
        /// Allocate ourselves.
        /// We have a private constructor, so no one else can.
        /// </summary>
        static readonly Global _instance = new Global();

        /// <summary>
        /// Access SiteStructure.Instance to get the singleton object.
        /// Then call methods on that instance.
        /// </summary>
        public static Global Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// This is a private constructor, meaning no outsiders have access.
        /// </summary>
        private Global()
        {
            

            //connectionbuilder.Provider = "Microsoft.Jet.OLEDB.4.0";
            


            
        }

        public void Init( )
        {
            SQLiteConnectionStringBuilder connectionbuilder = new SQLiteConnectionStringBuilder();
            connectionbuilder.DataSource = databaseFullpath;
            //connectionbuilder.Password = "12345678";
            ConnectString = connectionbuilder.ConnectionString;
            SqliteDatabase = new SQLiteDatabase(ConnectString);
        }
    }

    public struct ReferenceInfo
    {
        public string TableName;
        public string ColumnName;
        public ReferenceInfo(string _tablename, string _columnname)
        {
            TableName = _tablename;
            ColumnName = _columnname;
        }
    }

}
