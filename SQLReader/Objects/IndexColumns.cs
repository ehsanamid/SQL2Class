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
    [TypeConverter(typeof(ExpandableObject)), Description("Contains one row per column that is part of a <b>sys.indexes</b> index or unordered table (heap).")]
    public class IndexColumn : AbstractSQLObject
    {
        #region Private Fields

        private int _object_id;
        private int _index_id;
        private int _index_column_id;
        private int _column_id;
        private byte _key_ordinal;
        private byte _partition_ordinal;
        private bool _is_descending_key;
        private bool _is_included_column;

        #endregion

        #region public properties

        [Description("ID of the object the index is defined on.")]
        public int object_id
        {
            get { return _object_id; }
            //set {  _object_id = value; }
        }

        [Description("ID of the index in which the column is defined.")]
        public int index_id
        {
            get { return _index_id; }
            //set {  _index_id = value; }
        }

        [Description("ID of the index column. index_column_id is unique only within index_id.")]
        public int index_column_id
        {
            get { return _index_column_id; }
            //set {  _index_column_id = value; }
        }

        [Description("ID of the column in object_id.  0 = Row Identifier (RID) in a nonclustered index.  column_id is unique only within object_id.")]
        public int column_id
        {
            get { return _column_id; }
            //set {  _column_id = value; }
        }

        [Description("Ordinal (1-based) within set of key-columns. 0 = Not a key column, or is an XML index. Columns of type xml are not comparable, so an XML index does not induce an ordering on the underlying column values. Since an XML index is, therefore not a key, the key_ordinal value will always be 0.")]
        public byte key_ordinal
        {
            get { return _key_ordinal; }
            //set {  _key_ordinal = value; }
        }

        [Description("Ordinal (1-based) within set of partitioning columns.  0 = Not a partitioning column.")]
        public byte partition_ordinal
        {
            get { return _partition_ordinal; }
            //set {  _partition_ordinal = value; }
        }

        [Description("1 = Index key column has a descending sort direction.  0 = Index key column has an ascending sort direction.")]
        public bool is_descending_key
        {
            get { return _is_descending_key; }
            //set {  _is_descending_key = value; }
        }

        [Description("1 = Column is a nonkey column added to the index by using the CREATE INDEX INCLUDE clause. 0 = Column is not an included column.")]
        public bool is_included_column
        {
            get { return _is_included_column; }
            //set {  _is_included_column = value; }
        }

        #endregion

        public Column Column
        {
            get
            {
                if (this.Owner.Owner.Owner.Owner.Columns != null)
                    return this.Owner.Owner.Owner.Owner.Columns.GetColumn(_column_id);
                else
                    return null;
            }
        }

        private IndexColumns _Owner;
        [Browsable(false)]
        public IndexColumns Owner
        {
            get { return _Owner; }
        }

        private SQLiteConnectionStringBuilder SQLConnSet = new SQLiteConnectionStringBuilder();

        private void AddFromRecordSet(SQLiteDataReader rs)
        {
            try
            {
                if (!rs.IsDBNull(0)) { _object_id = rs.GetInt32(0); }
                if (!rs.IsDBNull(1)) { _index_id = rs.GetInt32(1); }
                if (!rs.IsDBNull(2)) { _index_column_id = rs.GetInt32(2); }
                if (!rs.IsDBNull(3)) { _column_id = rs.GetInt32(3); }
                if (!rs.IsDBNull(4)) { _key_ordinal = rs.GetByte(4); }
                if (!rs.IsDBNull(5)) { _partition_ordinal = rs.GetByte(5); }
                if (!rs.IsDBNull(6)) { _is_descending_key = rs.GetBoolean(6); }
                if (!rs.IsDBNull(7)) { _is_included_column = rs.GetBoolean(7); }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IndexColumn(SQLiteConnectionStringBuilder SQLConnSetting, SQLiteDataReader rs, IndexColumns owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            AddFromRecordSet(rs);
        }
        public IndexColumn(SQLiteConnectionStringBuilder SQLConnSetting, int Object_id, int Index_id, int Index_column_id, IndexColumns owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;

            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "SELECT object_id, index_id, index_column_id, column_id, key_ordinal, partition_ordinal, is_descending_key, is_included_column FROM sys.index_columns WHERE object_id=" + Object_id + " AND index_id=" + Index_id + " AND index_column_id=" + Index_column_id + " ORDER BY index_column_id";
                    SQLiteDataReader rs = Com.ExecuteReader();
                    while (rs.Read())
                        AddFromRecordSet(rs);
                    rs.Close();
                    Conn.Close();
                    rs.Dispose();
                }

            }
        }

        public override string ToString()
        {
            try
            {
                if (this.Owner.Owner.Owner.Owner.Columns != null)
                    return this.Owner.Owner.Owner.Owner.Columns.GetColumn(_column_id).name;
            }
            catch (Exception)
            {
                return _column_id.ToString();
            }
            return base.ToString();
        }

        [Browsable(false)]
        public override string ObjectName
        {
            get { return Column.name; }
        }

        [Browsable(false)]
        public override string UniqueObjectName
        {
            get { return Owner.ObjectName + Column.name + SQLServer.ObjectNameSeperator; }
        }
    }

    [TypeConverter(typeof(ExpandableObject))]
    public class IndexColumns : AbstractSQLObject
    {
        private List<IndexColumn> _Items = new List<IndexColumn>();
        [DisplayName("Index Columns")]
        public IndexColumn[] Items
        {
            get { return _Items.ToArray(); }
        }

        private Index _Owner;
        [Browsable(false)]
        public Index Owner
        {
            get { return _Owner; }
        }

        public IndexColumn GetItem(int column_id)
        {
            foreach (IndexColumn ic in Items)
            {
                if (ic.column_id == column_id)
                    return ic;
            }
            return null;
        }

        private SQLiteConnectionStringBuilder SQLConnSet = new SQLiteConnectionStringBuilder();

        public IndexColumns(SQLiteConnectionStringBuilder SQLConnSetting, Index owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "SELECT object_id, index_id, index_column_id, column_id, key_ordinal, partition_ordinal, is_descending_key, is_included_column FROM sys.index_columns WHERE object_id=" + owner.object_id + " AND index_id=" + owner.index_id + " ORDER BY index_column_id";
                    SQLiteDataReader rs = Com.ExecuteReader();
                    while (rs.Read())
                    {
                        _Items.Add(new IndexColumn(SQLConnSetting, rs, this));
                    }
                    rs.Close();
                    Conn.Close();
                    rs.Dispose();
                }

            }
        }

        public override string ToString()
        {
            if (_Items != null)
                return "[" + _Items.Count + "] IndexColumns";
            return base.ToString();
        }

        [Browsable(false)]
        public override string ObjectName
        {
            get { return "IndexColumns"; }
        }

        [Browsable(false)]
        public override string UniqueObjectName
        {
            get { return Owner.ObjectName + "IndexColumns" + SQLServer.ObjectNameSeperator; }
        }
    }
}
