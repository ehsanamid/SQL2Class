using System;
using System.Collections;
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
    
    [TypeConverter(typeof(ExpandableObject))]
    public class Database 
    {

        public List<Table> Tables = new List<Table>(); 
        

       

        #region Private SQL variables

        
        private string _name; // sysname
        

        #endregion

        #region Public SQL Properties

        [Description("Name of database, unique within an instance of SQL Server")]
        public string name
        {
            get { return _name; }
            //set {  _name = value; }
        }

        

        #endregion

        
        

        private SQL_Types _Types;
        public SQL_Types Types
        {
            get { return _Types; }
        }

        

        
        private DefaultConstraints _DefaultConstraints;
        [Browsable(false)]
        public DefaultConstraints DefaultConstraints
        {
            get { return _DefaultConstraints; }
        }

        




        private SQLiteConnectionStringBuilder SQLConnSet = new SQLiteConnectionStringBuilder();

        
        public Database(SQLiteConnectionStringBuilder SQLConnSetting)
        {
            
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            
            LoadAssociatedObjects();
        }
        
        private void LoadAssociatedObjects()
        {
            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 20;
                    Com.CommandText = "Select * from sqlite_master  WHERE type = 'table'  AND name <> 'sqlite_master' AND name <> 'sqlite_temp_master' AND name <> 'sqlite_sequence' ORDER BY name";
                    
                    SQLiteDataReader rs = Com.ExecuteReader();
                    while (rs.Read())
                    {
                        //if ((rs.IsDBNull(rs.GetOrdinal("name")) == false))
                        //{
                        //    name = rs.GetString(rs.GetOrdinal("name"));
                        //    type = rs.GetString(rs.GetOrdinal("type"));
                        //    pk = rs.GetBoolean(rs.GetOrdinal("pk"));
                        //}
                        Table _table = new Table(SQLConnSet, rs.GetString(rs.GetOrdinal("name")));

                        Tables.Add(_table);
                    }
                    rs.Close();
                    Conn.Close();
                    rs.Dispose();
                }
            }
            foreach (Table _table in Tables)
            {

            }
            
        }

        

        [Browsable(false)]
        public override string ObjectName
        {
            get { return name.Replace(" ","_"); }
        }

        

    }

    
}
