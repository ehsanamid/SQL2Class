using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace DCS
{
    
    public class SQLiteTable
    {


        public SQLiteDatabase m_parent;
        private List<SQLiteTableColumn> _listsqlitetablecolumn;
        public List<SQLiteTableColumn> listSQLiteTableColumn
        {
            get
            {
                if (_listsqlitetablecolumn == null)
                {
                    _listsqlitetablecolumn = new List<SQLiteTableColumn>();
                    
                }
                return _listsqlitetablecolumn;
            }
            set
            {
                _listsqlitetablecolumn = value;
            }
        }

        private string _tablename;
        public string tableName
        {
            get
            {
                return _tablename;
            }
            set
            {
                _tablename = value;
            }
        }
        private string _description = "";
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        
        private bool hasprimarykey = false;
        public bool HasPrimaryKey
        {
            get
            {
                
                return hasprimarykey;
            }
            set
            {
                hasprimarykey = value;
            }
        }

        private bool hasforeignkey = false;
        public bool HasForeignKey
        {
            get
            {

                return hasforeignkey;
            }
            set
            {
                hasforeignkey = value;
            }
        }

        private bool isreferenced = false;
        public bool IsReferenced
        {
            get
            {

                return isreferenced;
            }
            set
            {
                isreferenced = value;
            }
        }
        string connectstring;
        public SQLiteTable(string str)
        {
            connectstring = str;
        }

        

        public void LoadTableInfo()
        {

            SQLiteConnection Conn = new SQLiteConnection(connectstring);
            SQLiteCommand Com = Conn.CreateCommand();
            Com.CommandText = "PRAGMA table_info(" + tableName + ")";

            Conn.Open();
            SQLiteDataReader rs = Com.ExecuteReader();
            while (rs.Read())
            {
                SQLiteTableColumn tablecolumn = new SQLiteTableColumn();
                tablecolumn.m_parent = this;
                tablecolumn.ColumnName = rs.GetString(rs.GetOrdinal("name"));
                tablecolumn.ColumnType = rs.GetString(rs.GetOrdinal("type"));
                //tablecolumn.DefaultValue = rs.GetString(rs.GetOrdinal("dflt_value"));
                tablecolumn.pk = rs.GetBoolean(rs.GetOrdinal("pk"));
                if (tablecolumn.pk == true)
                {
                    HasPrimaryKey = true;
                }
                listSQLiteTableColumn.Add(tablecolumn);

            }
            rs.Close();
            Conn.Close();
            rs.Dispose();
            Com.Dispose();
            Conn.Dispose();
        }
        public void addReference(string parentcolumn, string childtable, string childcolumn)
        {
            foreach (SQLiteTableColumn sqlitetablecolumn in listSQLiteTableColumn)
            {
                if (sqlitetablecolumn.ColumnName == parentcolumn)
                {
                    sqlitetablecolumn.IsReference = true;
                    sqlitetablecolumn.addReference( new ReferenceInfo(childtable,childcolumn));
                    break;
                }
            }
            //ReferncedTablelist.Add(_refenceinfo);
        }
        public void LoadTableForeign_keyInfo()
        {
            string from;
            string to;
            string table;
 
            SQLiteConnection Conn = new SQLiteConnection(connectstring);
            SQLiteCommand Com = Conn.CreateCommand();
            Com.CommandText = "PRAGMA foreign_key_list(" + tableName + ")";
            
            Conn.Open();
            SQLiteDataReader rs = Com.ExecuteReader();
            while (rs.Read())
            {
                

                if ((rs.IsDBNull(rs.GetOrdinal("from")) == false))
                {
                    from = rs.GetString(rs.GetOrdinal("from"));
                    foreach (SQLiteTableColumn sqlitetablecolumn in listSQLiteTableColumn)
                    {
                        if (sqlitetablecolumn.ColumnName.ToLower() == from.ToLower())
                        {
                            if ((rs.IsDBNull(rs.GetOrdinal("table")) == false))
                            {
                                table = rs.GetString(rs.GetOrdinal("table"));
                                if ((rs.IsDBNull(rs.GetOrdinal("to")) == false))
                                {
                                    to = rs.GetString(rs.GetOrdinal("to")); ;
                                    sqlitetablecolumn.IsForeignKey = true;
                                    sqlitetablecolumn.addReference(new ReferenceInfo(table, to));
                                    HasForeignKey = true;
                                    m_parent.addReference(table, to, this.tableName, from);
        
                                    
                                    break;
                                }
                            }
                            
                        }
                    }
                }
            }
            rs.Close();
            Conn.Close();
            rs.Dispose();
            Com.Dispose();
            Conn.Dispose();
        }
        
        
    }
}
