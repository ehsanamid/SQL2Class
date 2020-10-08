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
    [TypeConverter(typeof(ExpandableObject)), Description("Contains a row for each column, or set of columns, that comprise a foreign key.")]
    public class foreign_key_column : AbstractSQLObject
    {
        #region Private Fields

        private int _constraint_object_id;
        private int _constraint_column_id;
        private int _parent_object_id;
        private int _parent_column_id;
        private int _referenced_object_id;
        private int _referenced_column_id;

        #endregion

        public Table ReferencedTable
        {
            get
            {
                if (Owner.Owner.Referenced_Table != null)
                    return Owner.Owner.Referenced_Table;
                else
                    return null;
            }
        }
        public Column ReferencedColumn
        {
            get
            {
                if (Owner.Owner.Referenced_Table != null)
                    return Owner.Owner.Referenced_Table.GetColumn(_referenced_column_id);
                else
                    return null;
            }
        }
        public Table ParentTable
        {
            get
            {
                if (Owner.Owner.Owner.Owner != null)
                    return Owner.Owner.Owner.Owner;
                else
                    return null;
            }
        }
        public Column ParentColumn
        {
            get
            {
                if (Owner.Owner.Owner.Owner != null)
                    return Owner.Owner.Owner.Owner.GetColumn(_parent_column_id);
                else
                    return null;
            }
        }

        #region public properties

        [Description("ID of the FOREIGN KEY constrain")]
        public int constraint_object_id
        {
            get { return _constraint_object_id; }
            //set {  _constraint_object_id = value; }
        }

        [Description("ID of the column, or set of columns, that comprise the FOREIGN KEY (1..n where n=number of columns")]
        public int constraint_column_id
        {
            get { return _constraint_column_id; }
            //set {  _constraint_column_id = value; }
        }

        [Description("ID of the parent of the constraint, which is the referencing object")]
        public int parent_object_id
        {
            get { return _parent_object_id; }
            //set {  _parent_object_id = value; }
        }

        [Description("ID of the parent column, which is the referencing column")]
        public int parent_column_id
        {
            get { return _parent_column_id; }
            //set {  _parent_column_id = value; }
        }

        [Description("ID of the referenced object, which has the candidate key")]
        public int referenced_object_id
        {
            get { return _referenced_object_id; }
            //set {  _referenced_object_id = value; }
        }

        [Description("ID of the referenced column (candidate key column)")]
        public int referenced_column_id
        {
            get { return _referenced_column_id; }
            //set {  _referenced_column_id = value; }
        }

        #endregion

        private foreign_key_columns _Owner;
        [Browsable(false)]
        public foreign_key_columns Owner
        {
            get { return _Owner; }
        }

        private SQLiteConnectionStringBuilder SQLConnSet = new SQLiteConnectionStringBuilder();

        private void AddFromRecordSet(SQLiteDataReader rs)
        {
            try
            {
                if (!rs.IsDBNull(0)) { _constraint_object_id = rs.GetInt32(0); }
                if (!rs.IsDBNull(1)) { _constraint_column_id = rs.GetInt32(1); }
                if (!rs.IsDBNull(2)) { _parent_object_id = rs.GetInt32(2); }
                if (!rs.IsDBNull(3)) { _parent_column_id = rs.GetInt32(3); }
                if (!rs.IsDBNull(4)) { _referenced_object_id = rs.GetInt32(4); }
                if (!rs.IsDBNull(5)) { _referenced_column_id = rs.GetInt32(5); }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public foreign_key_column(SQLiteConnectionStringBuilder SQLConnSetting, SQLiteDataReader rs, foreign_key_columns owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            AddFromRecordSet(rs);
        }
        public foreign_key_column(SQLiteConnectionStringBuilder SQLConnSetting, int constraint_object_id, foreign_key_columns owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;

            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "SELECT constraint_object_id, constraint_column_id, parent_object_id, parent_column_id, referenced_object_id, referenced_column_id FROM sys.foreign_key_columns WHERE parent_object_id=" + owner.Owner.Owner.Owner.object_id + " AND constraint_object_id=" + constraint_object_id;
                    SQLiteDataReader rs = Com.ExecuteReader();
                    while (rs.Read())
                        AddFromRecordSet(rs);
                    rs.Close();
                    Conn.Close();
                    rs.Dispose();
                }

            }
        }

        [Browsable(false)]
        public override string ObjectName
        {
            get { return ReferencedTable.name + "." + ReferencedColumn.name; }
        }

        [Browsable(false)]
        public override string UniqueObjectName
        {
            get { return Owner.ObjectName + ReferencedTable.name + "." + ReferencedColumn.name + SQLServer.ObjectNameSeperator; }
        }
    }

    [TypeConverter(typeof(ExpandableObject))]
    public class foreign_key_columns : AbstractSQLObject
    {
        private List<foreign_key_column> _Items = new List<foreign_key_column>();
        [DisplayName("Foreign Keys Columns")]
        public foreign_key_column[] Items
        {
            get { return _Items.ToArray(); }
        }

        private foreign_key _Owner;
        [Browsable(false)]
        public foreign_key Owner
        {
            get { return _Owner; }
        }

        private SQLiteConnectionStringBuilder SQLConnSet = new SQLiteConnectionStringBuilder();

        public foreign_key_columns(SQLiteConnectionStringBuilder SQLConnSetting, foreign_key owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "SELECT constraint_object_id, constraint_column_id, parent_object_id, parent_column_id, referenced_object_id, referenced_column_id FROM sys.foreign_key_columns WHERE parent_object_id=" + owner.Owner.Owner.object_id + " AND constraint_object_id=" + owner.object_id;
                    SQLiteDataReader rs = Com.ExecuteReader();
                    while (rs.Read())
                    {
                        _Items.Add(new foreign_key_column(SQLConnSetting, rs, this));
                    }
                    rs.Close();
                    Conn.Close();
                    rs.Dispose();
                }

            }
        }

        public foreign_key_column Getforeign_key_column(int constraint_object_id)
        {
            foreach (foreign_key_column F in _Items)
            {
                if (F.constraint_object_id == constraint_object_id)
                    return F;
            }
            return null;
        }

        public foreign_key_column GetForeignKeyColumnByParentId(int parent_column_id)
        {
            foreach (foreign_key_column F in _Items)
            {
                if (F.parent_column_id == parent_column_id)
                    return F;
            }
            return null;
        }

        public override string ToString()
        {
            if (_Items != null)
                return "[" + _Items.Count + "] foreign_key_columns";
            return base.ToString();
        }

        [Browsable(false)]
        public override string ObjectName
        {
            get { return "foreign_key_columns"; }
        }

        [Browsable(false)]
        public override string UniqueObjectName
        {
            get { return Owner.ObjectName + "foreign_key_columns" + SQLServer.ObjectNameSeperator; }
        }
    }
}
