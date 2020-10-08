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
    [TypeConverter(typeof(ExpandableObject)), Description("Contains a row for each column that is an identity column.")]
    public class IdentityColumn : AbstractSQLObject
    {
        #region Private Fields

        private int _object_id;
        private int _column_id;
        private object _seed_value;
        private object _increment_value;
        private object _last_value;
        private bool _is_not_for_replication;

        #endregion

        #region public properties

        [Description("Object identification number. Is unique within a database.")]
        public int object_id
        {
            get { return _object_id; }
            //set {  _object_id = value; }
        }

        [Description("ID of the column. Is unique within the object.\nColumn IDs might not be sequential")]
        public int column_id
        {
            get { return _column_id; }
            //set {  _object_id = value; }
        }

        [Description("Seed value for this identity column. The data type of the seed value is the same as the data type of the column itself")]
        public object seed_value
        {
            get { return _seed_value; }
            //set {  _seed_value = value; }
        }

        [Description("Increment value for this identity column. The data type of the seed value is the same as the data type of the column itself")]
        public object increment_value
        {
            get { return _increment_value; }
            //set {  _increment_value = value; }
        }

        [Description("Last value generated for this identity column. The data type of the seed value is the same as the data type of the column itself")]
        public object last_value
        {
            get { return _last_value; }
            //set {  _last_value = value; }
        }

        [Description("Identity column is declared NOT FOR REPLICATION")]
        public bool is_not_for_replication
        {
            get { return _is_not_for_replication; }
            //set {  _is_not_for_replication = value; }
        }

        #endregion

        private IdentityColumns _Owner;
        [Browsable(false)]
        public IdentityColumns Owner
        {
            get { return _Owner; }
        }

        private SQLiteConnectionStringBuilder SQLConnSet = new SQLiteConnectionStringBuilder();

        private void AddFromRecordSet(SQLiteDataReader rs)
        {
            try
            {
                if (!rs.IsDBNull(0)) { _object_id = rs.GetInt32(0); }
                if (!rs.IsDBNull(1)) { _column_id = rs.GetInt32(1); }
                if (!rs.IsDBNull(2)) { _seed_value = rs.GetValue(2); }
                if (!rs.IsDBNull(3)) { _increment_value = rs.GetValue(3); }
                if (!rs.IsDBNull(4)) { _last_value = rs.GetValue(4); }
                if (!rs.IsDBNull(5)) { _is_not_for_replication = rs.GetBoolean(5); }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IdentityColumn(SQLiteConnectionStringBuilder SQLConnSetting, SQLiteDataReader rs, IdentityColumns owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            AddFromRecordSet(rs);
        }
        public IdentityColumn(SQLiteConnectionStringBuilder SQLConnSetting, int object_id, int column_id, IdentityColumns owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;

            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "SELECT object_id, column_id,seed_value, increment_value, last_value, is_not_for_replication FROM sys.identity_columns WHERE object_id=" + object_id + " AND column_id=" + column_id;
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
            get { return object_id.ToString(); }
        }

        [Browsable(false)]
        public override string UniqueObjectName
        {
            get { return Owner.ObjectName + object_id.ToString() + SQLServer.ObjectNameSeperator; }
        }
    }

    [TypeConverter(typeof(ExpandableObject))]
    public class IdentityColumns : AbstractSQLObject
    {
        private List<IdentityColumn> _Items = new List<IdentityColumn>();
        [DisplayName("Identity Columns")]
        public IdentityColumn[] Items
        {
            get { return _Items.ToArray(); }
        }

        private Table _Owner;
        [Browsable(false)]
        public Table Owner
        {
            get { return _Owner; }
        }

        private SQLiteConnectionStringBuilder SQLConnSet = new SQLiteConnectionStringBuilder();

        public IdentityColumns(SQLiteConnectionStringBuilder SQLConnSetting, Table owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "SELECT object_id, column_id, seed_value, increment_value, last_value, is_not_for_replication FROM sys.identity_columns WHERE object_id=" + owner.object_id + " ORDER BY column_id";
                    SQLiteDataReader rs = Com.ExecuteReader();
                    while (rs.Read())
                    {
                        _Items.Add(new IdentityColumn(SQLConnSetting, rs, this));
                    }
                    rs.Close();
                    Conn.Close();
                    rs.Dispose();
                }

            }
        }

        public IdentityColumn GetItem(int column_id)
        {
            foreach (IdentityColumn I in _Items)
            {
                if (I.column_id == column_id)
                    return I;
            }
            return null;
        }

        public override string ToString()
        {
            if (_Items != null)
                return "[" + _Items.Count + "] IdentityColumns";
            return base.ToString();
        }

        [Browsable(false)]
        public override string ObjectName
        {
            get { return "IdentityColumns"; }
        }

        [Browsable(false)]
        public override string UniqueObjectName
        {
            get { return Owner.ObjectName + "IdentityColumns" + SQLServer.ObjectNameSeperator; }
        }
    }
}
