using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using System.ComponentModel;
using System.CodeDom;

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
    [TypeConverter(typeof(ExpandableObject)), Description("Contains a row per index or heap of a tabular object, such as a table, view, or table-valued function.")]
    public class Index : AbstractSQLObject
    {
        #region Private Fields

        private int _object_id;
        private string _name;
        private int _index_id;
        private Index_type _type;
        private string _type_desc;
        private bool _is_unique;
        private int _data_space_id;
        private bool _ignore_dup_key;
        private bool _is_primary_key;
        private bool _is_unique_constraint;
        private byte _fill_factor;
        private bool _is_padded;
        private bool _is_disabled;
        private bool _is_hypothetical;
        private bool _allow_row_locks;
        private bool _allow_page_locks;

        #endregion

        #region public properties

        [Description("ID of the object to which this index belongs.")]
        public int object_id
        {
            get { return _object_id; }
            //set {  _object_id = value; }
        }

        [Description("Name of the index. name is unique only within the object.  NULL = Heap")]
        public string name
        {
            get { return _name; }
            //set {  _name = value; }
        }

        [Description("ID of the index. index_id is unique only within the object.  0 = Heap 1 = Clustered index &gt; 1 = Nonclustered index")]
        public int index_id
        {
            get { return _index_id; }
            //set {  _index_id = value; }
        }

        [Description("Type of index: 0 = Heap 1 = Clustered 2 = Nonclustered 3 = XML")]
        public Index_type type
        {
            get { return _type; }
            //set {  _type = value; }
        }

        [Description("Description of index type: HEAP CLUSTERED NONCLUSTERED XML")]
        public string type_desc
        {
            get { return _type_desc; }
            //set {  _type_desc = value; }
        }

        [Description("1 = Index is unique. 0 = Index is not unique.")]
        public bool is_unique
        {
            get { return _is_unique; }
            //set {  _is_unique = value; }
        }

        [Description("ID of the data space for this index. Data space is either a filegroup or partition scheme.  0 = object_id is a table-valued function.")]
        public int data_space_id
        {
            get { return _data_space_id; }
            //set {  _data_space_id = value; }
        }

        [Description("1 = IGNORE_DUP_KEY is ON. 0 = IGNORE_DUP_KEY is OFF.")]
        public bool ignore_dup_key
        {
            get { return _ignore_dup_key; }
            //set {  _ignore_dup_key = value; }
        }

        [Description("1 = Index is part of a PRIMARY KEY constraint.")]
        public bool is_primary_key
        {
            get { return _is_primary_key; }
            //set {  _is_primary_key = value; }
        }

        [Description("1 = Index is part of a UNIQUE constraint.")]
        public bool is_unique_constraint
        {
            get { return _is_unique_constraint; }
            //set {  _is_unique_constraint = value; }
        }

        [Description("0 = FILLFACTOR percentage used when the index was created or rebuilt. 0 = Default value")]
        public byte fill_factor
        {
            get { return _fill_factor; }
            //set {  _fill_factor = value; }
        }

        [Description("1 = PADINDEX is ON. 0 = PADINDEX is OFF.")]
        public bool is_padded
        {
            get { return _is_padded; }
            //set {  _is_padded = value; }
        }

        [Description("1 = Index is disabled. 0 = Index is not disabled.")]
        public bool is_disabled
        {
            get { return _is_disabled; }
            //set {  _is_disabled = value; }
        }

        [Description("1 = Index is hypothetical and cannot be used directly as a data access path. Hypothetical Index hold column-level statistics. 0 = Index is not hypothetical.")]
        public bool is_hypothetical
        {
            get { return _is_hypothetical; }
            //set {  _is_hypothetical = value; }
        }

        [Description("1 = Index allows row locks. 0 = Index does not allow row locks.")]
        public bool allow_row_locks
        {
            get { return _allow_row_locks; }
            //set {  _allow_row_locks = value; }
        }

        [Description("1 = Index allows page locks. 0 = Index does not allow page locks.")]
        public bool allow_page_locks
        {
            get { return _allow_page_locks; }
            //set {  _allow_page_locks = value; }
        }

        #endregion

        private IndexColumns _IndexColumns;

        public IndexColumns IndexColumns
        {
            get { return _IndexColumns; }
        }

        public string Description
        {
            get
            {
                if (Owner.Owner.Owner.Owner.ExtendedProperties != null)
                {
                    ExtendedProperty desc = Owner.Owner.Owner.Owner.ExtendedProperties.GetExtendedProperty(ExtendedProperty_Type.INDEX, Owner.Owner.object_id, _index_id);
                    if (desc != null)
                        return desc.value.ToString();
                    else
                        return "";
                }
                else
                    return "";
            }
        }

        public ExtendedProperty[] ExtProperties
        {
            get
            {
                if (Owner.Owner.Owner.Owner.ExtendedProperties != null)
                    return Owner.Owner.Owner.Owner.ExtendedProperties.GetExtendedProperties(ExtendedProperty_Type.INDEX, Owner.Owner.object_id, _index_id);
                else
                    return null;
            }
        }

        private Indexes _Owner;
        [Browsable(false)]
        public Indexes Owner
        {
            get { return _Owner; }
        }

        private SQLiteConnectionStringBuilder SQLConnSet = new SQLiteConnectionStringBuilder();

        private void AddFromRecordSet(SQLiteDataReader rs)
        {
            try
            {
                if (!rs.IsDBNull(0)) { _object_id = rs.GetInt32(0); }
                if (!rs.IsDBNull(1)) { _name = rs.GetString(1); }
                if (!rs.IsDBNull(2)) { _index_id = rs.GetInt32(2); }
                if (!rs.IsDBNull(3))
                {
                    byte tmptype = rs.GetByte(3);
                    _type = (Index_type)tmptype;
                    //switch (tmptype)
                    //{
                    //    case 0:
                    //        _type = Index_type.Heap;
                    //        break;
                    //    case 1:
                    //        _type = Index_type.Clustered;
                    //        break;
                    //    case 2:
                    //        _type = Index_type.Nonclustered;
                    //        break;
                    //    case 3:
                    //        _type = Index_type.XML;
                    //        break;
                    //    default:
                    //        break;
                    //}
                }
                if (!rs.IsDBNull(4)) { _type_desc = rs.GetString(4); }
                if (!rs.IsDBNull(5)) { _is_unique = rs.GetBoolean(5); }
                if (!rs.IsDBNull(6)) { _data_space_id = rs.GetInt32(6); }
                if (!rs.IsDBNull(7)) { _ignore_dup_key = rs.GetBoolean(7); }
                if (!rs.IsDBNull(8)) { _is_primary_key = rs.GetBoolean(8); }
                if (!rs.IsDBNull(9)) { _is_unique_constraint = rs.GetBoolean(9); }
                if (!rs.IsDBNull(10)) { _fill_factor = rs.GetByte(10); }
                if (!rs.IsDBNull(11)) { _is_padded = rs.GetBoolean(11); }
                if (!rs.IsDBNull(12)) { _is_disabled = rs.GetBoolean(12); }
                if (!rs.IsDBNull(13)) { _is_hypothetical = rs.GetBoolean(13); }
                if (!rs.IsDBNull(14)) { _allow_row_locks = rs.GetBoolean(14); }
                if (!rs.IsDBNull(15)) { _allow_page_locks = rs.GetBoolean(15); }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Index(SQLiteConnectionStringBuilder SQLConnSetting, SQLiteDataReader rs, Indexes owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            AddFromRecordSet(rs);
            _IndexColumns = new IndexColumns(SQLConnSet, this);
        }
        public Index(SQLiteConnectionStringBuilder SQLConnSetting, int Object_id, int Index_id, Indexes owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;

            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "SELECT object_id, name, index_id, type, type_desc, is_unique, data_space_id, ignore_dup_key, is_primary_key, is_unique_constraint, fill_factor, is_padded, is_disabled, is_hypothetical, allow_row_locks, allow_page_locks FROM sys.indexes WHERE object_id=" + Object_id + " AND index_id=" + Index_id + " ORDER BY name";
                    SQLiteDataReader rs = Com.ExecuteReader();
                    while (rs.Read())
                        AddFromRecordSet(rs);
                    rs.Close();
                    Conn.Close();
                    rs.Dispose();
                }

            }
            _IndexColumns = new IndexColumns(SQLConnSet, this);
        }

        [Browsable(false)]
        public override string ObjectName
        {
            get { return name.Replace(" ","_"); }
        }

        [Browsable(false)]
        public override string UniqueObjectName
        {
            get { return Owner.ObjectName + name + SQLServer.ObjectNameSeperator; }
        }
    }

    [TypeConverter(typeof(ExpandableObject))]
    public class Indexes : AbstractSQLObject
    {
        private List<Index> _Items = new List<Index>();
        [DisplayName("Indexes")]
        public Index[] Items
        {
            get { return _Items.ToArray(); }
        }

        public IndexColumn GetIndexColumn(int column_id)
        {
            foreach (Index i in Items)
            {
                if (i.object_id == _Owner.object_id && i.is_primary_key == true)
                    return i.IndexColumns.GetItem(column_id);
            }
            return null;
        }

        private Table _Owner;
        [Browsable(false)]
        public Table Owner
        {
            get { return _Owner; }
        }

        public bool HasNonClustered
        {
            get
            {
                foreach (Index I in _Items)
                {
                    if ((I.type == Index_type.Nonclustered) && (I.is_primary_key == false))
                        return true;
                }
                return false;
            }
        }

        public bool HasClustered
        {
            get
            {
                foreach (Index I in _Items)
                {
                    if (I.type == Index_type.Clustered)
                        return true;
                }
                return false;
            }
        }

        public bool HasUniqueIndex
        {
            get
            {
                foreach (Index I in _Items)
                {
                    if (I.is_unique)
                        return true;
                }
                return false;
            }
        }

        public Index[] GetItems(Index_type Exclude)
        {
            List<Index> _GetItems = new List<Index>();
            foreach (Index I in _Items)
            {
                if ((I.type & Exclude) != I.type)
                    _GetItems.Add(I);
            }
            return _GetItems.ToArray();
        }

        public Index GetItem(int index_id)
        {
            foreach (Index I in _Items)
            {
                if (I.index_id.Equals(index_id))
                    return I;
            }
            return null;
        }

        public int CountIndexes(Index_type Exclude)
        {
            return CountIndexes(Exclude, true);
        }
        public int CountIndexes(Index_type Exclude, bool IncludePrimaryKeyIndex)
        {
            int cnt = 0;
            foreach (Index I in _Items)
            {
                if (((I.type & Exclude) != I.type))
                {
                    if (I.is_primary_key)
                    {
                        if (IncludePrimaryKeyIndex)
                            cnt++;
                    }
                    else
                        cnt++;

                }
            }
            return cnt;
        }

        private SQLiteConnectionStringBuilder SQLConnSet = new SQLiteConnectionStringBuilder();

        public Indexes(SQLiteConnectionStringBuilder SQLConnSetting, Table owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "SELECT object_id, name, index_id, type, type_desc, is_unique, data_space_id, ignore_dup_key, is_primary_key, is_unique_constraint, fill_factor, is_padded, is_disabled, is_hypothetical, allow_row_locks, allow_page_locks FROM sys.indexes WHERE object_id=" + owner.object_id + " ORDER BY name";
                    SQLiteDataReader rs = Com.ExecuteReader();
                    while (rs.Read())
                    {
                        _Items.Add(new Index(SQLConnSetting, rs, this));
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
                return "[" + _Items.Count + "] Indexes";
            return base.ToString();
        }

        [Browsable(false)]
        public override string ObjectName
        {
            get { return "Indexes"; }
        }

        [Browsable(false)]
        public override string UniqueObjectName
        {
            get { return Owner.ObjectName + "Indexes" + SQLServer.ObjectNameSeperator; }
        }
    }
}
