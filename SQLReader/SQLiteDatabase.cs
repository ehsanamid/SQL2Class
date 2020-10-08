using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace DCS
{

    


    public class SQLiteDatabase
    {
        string connectstring;
        private List<SQLiteTable> _listsqlitetable;
        public List<SQLiteTable> listSQLiteTable
        {
            get
            {
                if (_listsqlitetable == null)
                {
                    _listsqlitetable = new List<SQLiteTable>();
                    
                }
                return _listsqlitetable;
            }
            set
            {
                _listsqlitetable = value;
            }
        }
        

        public SQLiteDatabase(string str)
        {
            //SQLConnSet = new SQLiteConnectionStringBuilder();
            //SQLConnSet.DataSource = str;
            connectstring = str;
            //listSQLiteTable = new List<SQLiteTable>();
        }
        private string _databasename;
        public string databaseName
        {
            get
            {
                return _databasename;
            }
            set
            {
                _databasename = value;
            }
        }

        public void Save2CSV()
        {

        }
        public void LoadAssociatedObjects()
        {
            using (SQLiteConnection Conn = new SQLiteConnection(connectstring))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 20;
                    Com.CommandText = "Select * from sqlite_master  WHERE type = 'table'  AND name <> 'sqlite_master' AND name <> 'sqlite_temp_master' AND name <> 'sqlite_sequence' ORDER BY name";

                    SQLiteDataReader rs = Com.ExecuteReader();
                    while (rs.Read())
                    {

                        SQLiteTable _sqlitetable = new SQLiteTable(connectstring);
                        _sqlitetable.m_parent = this;
                        _sqlitetable.tableName = rs.GetString(rs.GetOrdinal("name"));
                        listSQLiteTable.Add(_sqlitetable);
                    }
                    rs.Close();
                    Conn.Close();
                    rs.Dispose();
                }
            }
            foreach (SQLiteTable _table in listSQLiteTable)
            {
                _table.LoadTableInfo();

            }

            foreach (SQLiteTable _table in listSQLiteTable)
            {
                _table.LoadTableForeign_keyInfo();

            }

        }
        public void addReference(string parenttable,string parentcolumn, string childtable, string childcolumn)
        {
            foreach (SQLiteTable sqlitetable in listSQLiteTable)
            {
                if (sqlitetable.tableName.ToLower() == parenttable.ToLower())
                {
                    sqlitetable.IsReferenced = true;
                    sqlitetable.addReference(parentcolumn,  childtable,  childcolumn);
                    break;
                }
            }
            //ReferncedTablelist.Add(_refenceinfo);
        }
    }
}
