using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using System.ComponentModel;


#region Information about the author and reference to source data

/*
	 * AUTHOR:
	 * ----------------------------------------------------------------------------
	 * SQL Object wrapper  developed by Paw Jershauge 
	 *
	 * Jan - Mar. 2008
	 * 
	 * 
	 * REFERENCE:
	 * ----------------------------------------------------------------------------
	 * Reference to this code can be found on MSDN website.
	 * More information about Indexes can be found here: http://msdn2.microsoft.com/en-us/library/ms173760.aspx
	 * More information about index_columns can be found here: http://msdn2.microsoft.com/en-us/library/ms175105.aspx
	 * More information about objects can be found here: http://msdn2.microsoft.com/en-us/library/ms190324.aspx
	 * More information about foreign_key can be found here: http://msdn2.microsoft.com/en-us/library/ms189807.aspx
	 * More information about Column can be found here: http://msdn2.microsoft.com/en-us/library/ms176106.aspx
	 * More information about Database can be found here: http://msdn2.microsoft.com/en-us/library/ms178534.aspx
	 * More information about extended_propertie can be found here: http://msdn2.microsoft.com/en-us/library/ms177541.aspx
	 * More information about schema can be found here: http://msdn2.microsoft.com/en-us/library/ms176011.aspx
	 * More information about table can be found here: http://msdn2.microsoft.com/en-us/library/ms187406.aspx
	 * More information about type can be found here: http://msdn2.microsoft.com/en-us/library/ms188021.aspx
	 * More information about default_constraint can be found here: http://msdn.microsoft.com/en-us/library/ms173758.aspx
	 * More information about trigger can be found here: http://msdn.microsoft.com/en-us/library/ms188746.aspx
	 * More information about comment can be found here: http://msdn.microsoft.com/en-us/library/ms186293.aspx
	 * More information about identity_column can be found here: http://msdn.microsoft.com/en-us/library/ms187334.aspx
	 * More information about foreign_key_column can be found here: http://msdn2.microsoft.com/en-us/library/ms186306.aspx
	 * 
	 * 
	 * LICENSE:
	 * ----------------------------------------------------------------------------
	 * This source code is coded by Paw Jershauge, no copyrights
     * But please refer the code to me, the original delevoper, thank you...
     * 
     * 
	 * CODEPROJECT.COM RELEASE:
	 * ----------------------------------------------------------------------------
	 * Published July 2008
	 * By Paw Jershauge
	 */

#endregion

namespace SQLRead
{
    public class SQLServer : AbstractSQLObject
    {
        public static string ObjectNameSeperator = ".";
        public static string DatabaseObjectNameSeperator = "\\";

        public event LoadingAssociatedObjects LoadingDatabases;
        public event LoadingAssociatedObjects LoadingDatabase;
        public event FinishedLoading Finished;

        private SQLiteConnectionStringBuilder SqlConnectionSetting = new SQLiteConnectionStringBuilder();
        [Browsable(false),DisplayName("SQL Connection Settings")]
        public SQLiteConnectionStringBuilder ConnectionSetting
        {
            get { return SqlConnectionSetting; }
            set { SqlConnectionSetting = value; }
        }

        public SQLServer()
        {
            //SqlConnectionSetting.ConnectTimeout = 30;
        }
        public SQLServer(string server)
        {
            //SqlConnectionSetting.ConnectTimeout = 30;
            SqlConnectionSetting.DataSource = server;
            //SqlConnectionSetting.InitialCatalog = "master";
            //SqlConnectionSetting.IntegratedSecurity = true;

            _Databases = new Databases(SqlConnectionSetting,this);
           // Database database = new Database(SqlConnectionSetting,,_Databases);
        }
        public SQLServer(string server, string Catalog)
        {
            //SqlConnectionSetting.ConnectTimeout = 30;
            //SqlConnectionSetting.DataSource = server;
            //SqlConnectionSetting.InitialCatalog = Catalog;
            //SqlConnectionSetting.IntegratedSecurity = true;
        }
        public SQLServer(string server, string username, string password)
        {
            //SqlConnectionSetting.ConnectTimeout = 30;
            //SqlConnectionSetting.DataSource = server;
            //SqlConnectionSetting.InitialCatalog = "master";
            //SqlConnectionSetting.UserID = username;
            //SqlConnectionSetting.Password = password;
            //SqlConnectionSetting.IntegratedSecurity = false;
        }
        public SQLServer(string server, string username, string password, string Catalog)
        {
            //SqlConnectionSetting.ConnectTimeout = 30;
            //SqlConnectionSetting.DataSource = server;
            //SqlConnectionSetting.InitialCatalog = Catalog;
            //SqlConnectionSetting.UserID = username;
            //SqlConnectionSetting.Password = password;
            //SqlConnectionSetting.IntegratedSecurity = false;
        }
        public SQLServer(string server, string username, string password, bool integratedsecurity)
        {
            //SqlConnectionSetting.ConnectTimeout = 30;
            //SqlConnectionSetting.DataSource = server;
            //SqlConnectionSetting.InitialCatalog = "master";
            //SqlConnectionSetting.UserID = username;
            //SqlConnectionSetting.Password = password;
            //SqlConnectionSetting.IntegratedSecurity = integratedsecurity;
        }
        public SQLServer(string server, string username, string password, bool integratedsecurity, string Catalog)
        {
            //SqlConnectionSetting.ConnectTimeout = 30;
            //SqlConnectionSetting.DataSource = server;
            //SqlConnectionSetting.InitialCatalog = Catalog;
            //SqlConnectionSetting.UserID = username;
            //SqlConnectionSetting.Password = password;
            //SqlConnectionSetting.IntegratedSecurity = integratedsecurity;
        }

        private Databases _Databases;
        public Databases Databases
        {
            get { return _Databases; }
        }

        public string[] GetDatabaseNames()
        {
            try
            {
                List<string> DbNames = new List<string>();
                using (SQLiteConnection Conn = new SQLiteConnection(SqlConnectionSetting.ConnectionString))
                {
                    Conn.Open();
                    using (SQLiteCommand Com = Conn.CreateCommand())
                    {
                        Com.CommandTimeout = 20;
                        Com.CommandText = "Select name from sys.databases WHERE has_dbaccess([name])=1 AND name <> 'master' ORDER BY name";
                        SQLiteDataReader rs = Com.ExecuteReader();
                        while (rs.Read())
                        {
                            if (!rs.IsDBNull(0))
                                DbNames.Add(rs.GetString(0));
                        }
                        rs.Close();
                        Conn.Close();
                        rs.Dispose();
                    }
                }
                return DbNames.ToArray();
            }
            catch
            {
                return new string[0];
            }
        }

        public void LoadDatabases()
        {
            LoadDatabases("");
        }
        public void LoadDatabases(string catalog)
        {
            _Databases = new Databases(SqlConnectionSetting, this, catalog, LoadingDatabase, LoadingDatabases, Finished);
        }

        [Browsable(false)]
        public override string ObjectName
        {
            get { return ConnectionSetting.DataSource.Replace(" ", "_"); }
        }

        [Browsable(false)]
        public override string UniqueObjectName
        {
            get 
            {
                return ConnectionSetting.DataSource.Replace(" ", "_") + DatabaseObjectNameSeperator;
            }
        }
    }
}
