using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using System.ComponentModel;
using System.CodeDom;
using System.Reflection;

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
    public class Table 
    {
        #region Private fields

        private DateTime _create_date;
        private int _filestream_data_space_id;
        private bool _has_replication_filter;
        private bool _has_unchecked_assembly_data;
        private bool _is_merge_published;
        private bool _is_ms_shipped;
        private bool _is_published;
        private bool _is_replicated;
        private bool _is_schema_published;
        private bool _is_sync_tran_subscribed;
        private bool _large_value_types_out_of_row;
        private int _lob_data_space_id;
        private bool _lock_on_bulk_load;
        private int _max_column_id_used;
        private DateTime _modify_date;
        private string _name;
        private int _object_id;
        private int _parent_object_id;
        private int _principal_id;
        private int _schema_id;
        private int _text_in_row_limit;
        private Object_Type _type;
        private string _type_desc;
        private bool _uses_ansi_nulls;

        #endregion

        private Columns _Columns;
        private Indexes _Indexes;
        private foreign_keys _foreign_keys;
        private KeyConstraints _Keys;
        private IdentityColumns _IdentityColumns;

        #region public properties

        [Description("Object name.")]
        public string name
        {
            get { return _name; }
            //set {  _name = value; }
        }

        [Description("Object identification number. Is unique within a database.")]
        public int object_id
        {
            get { return _object_id; }
            //set {  _object_id = value; }
        }

        [Description("ID of the individual owner, if different from the schema owner. By default, schema-contained objects are owned by the schema owner. However, an alternate owner can be specified by using the ALTER AUTHORIZATION statement to change ownership.\nIs NULL if there is no alternate individual owner.\nIs NULL if the object type is one of the following:\nC = CHECK constraint\nD = DEFAULT (constraint or stand-alone)\nF = FOREIGN KEY constraint\nPK = PRIMARY KEY constraint\nR = Rule (old-style, stand-alone)\nTA = Assembly (CLR-integration) trigger\nTR = SQL trigger\nUQ = UNIQUE constrain")]
        public int principal_id
        {
            get { return _principal_id; }
            //set {  _principal_id = value; }
        }

        [Description("ID of the schema that the object is contained in.\nFor all schema-scoped system objects that ship with SQL Server 2005, this value will always be\nIN (schema_id('sys'), schema_id('INFORMATION_SCHEMA'))")]
        public int schema_id
        {
            get { return _schema_id; }
            //set {  _schema_id = value; }
        }

        [Description("ID of the object to which this object belongs.\n0 = Not a child object")]
        public int parent_object_id
        {
            get { return _parent_object_id; }
            //set {  _parent_object_id = value; }
        }

        [Description("Object type:\nAF = Aggregate function (CLR)\nC = CHECK constraint\nD = DEFAULT (constraint or stand-alone)\nF = FOREIGN KEY constraint\nPK = PRIMARY KEY constraint\nP = SQL stored procedure\nPC = Assembly (CLR) stored procedure\nFN = SQL scalar function\nFS = Assembly (CLR) scalar function\nFT = Assembly (CLR) table-valued function\nR = Rule (old-style, stand-alone)\nRF = Replication-filter-procedure\nS = System base table\nSN = Synonym\nSQ = Service queue\nTA = Assembly (CLR) DML trigger\nTR = SQL DML trigger\nIF = SQL inline table-valued function\nTF = SQL table-valued-function\nU = Table (user-defined)\nUQ = UNIQUE constraint\nV = View\nX = Extended stored procedure\nIT = Internal tabl")]
        public Object_Type type
        {
            get { return _type; }
            //set {  _type = value; }
        }

        [Description("Description of the object type.\nAGGREGATE_FUNCTION\nCHECK_CONSTRAINT\nDEFAULT_CONSTRAINT\nFOREIGN_KEY_CONSTRAINT\nPRIMARY_KEY_CONSTRAINT\nSQL_STORED_PROCEDURE\nCLR_STORED_PROCEDURE\nSQL_SCALAR_FUNCTION\nCLR_SCALAR_FUNCTION\nCLR_TABLE_VALUED_FUNCTION\nRULE\nREPLICATION_FILTER_PROCEDURE\nSYSTEM_TABLE\nSYNONYM\nSERVICE_QUEUE\nCLR_TRIGGER\nSQL_TRIGGER\nSQL_INLINE_TABLE_VALUED_FUNCTION\nSQL_TABLE_VALUED_FUNCTION\nUSER_TABLE\nUNIQUE_CONSTRAINT\nVIEW\nEXTENDED_STORED_PROCEDURE\nINTERNAL_TABL")]
        public string type_desc
        {
            get { return _type_desc; }
            //set {  _type_desc = value; }
        }

        [Description("Date the object was created")]
        public DateTime create_date
        {
            get { return _create_date; }
            //set {  _create_date = value; }
        }

        [Description("Date the object was last modified by using an ALTER statement. If the object is a table or a view, modify_date also changes when a clustered index on the table or view is created or altered")]
        public DateTime modify_date
        {
            get { return _modify_date; }
            //set {  _modify_date = value; }
        }

        [Description("Object is created by an internal SQL Server component")]
        public bool is_ms_shipped
        {
            get { return _is_ms_shipped; }
            //set {  _is_ms_shipped = value; }
        }

        [Description("Object is published")]
        public bool is_published
        {
            get { return _is_published; }
            //set {  _is_published = value; }
        }

        [Description("Only the schema of the object is published")]
        public bool is_schema_published
        {
            get { return _is_schema_published; }
            //set {  _is_schema_published = value; }
        }

        [Description("A nonzero value is the ID of the data space (filegroup or partition scheme) that holds the text, ntext, and image data for this table.\n0 = The table does not contain text, ntext, or image data")]
        public int lob_data_space_id
        {
            get { return _lob_data_space_id; }
            //set {  _lob_data_space_id = value; }
        }

        [Description("For internal system use only")]
        public int filestream_data_space_id
        {
            get { return _filestream_data_space_id; }
            //set {  _filestream_data_space_id = value; }
        }

        [Description("Maximum column ID ever used by this table")]
        public int max_column_id_used
        {
            get { return _max_column_id_used; }
            //set {  _max_column_id_used = value; }
        }

        [Description("Table is locked on bulk load. For more information, see sp_tableoption (Transact-SQL) [ http://msdn2.microsoft.com/en-us/library/ms173530(printer).aspx ]")]
        public bool lock_on_bulk_load
        {
            get { return _lock_on_bulk_load; }
            //set {  _lock_on_bulk_load = value; }
        }

        [Description("Table was created with the SET ANSI_NULLS database option ON")]
        public bool uses_ansi_nulls
        {
            get { return _uses_ansi_nulls; }
            //set {  _uses_ansi_nulls = value; }
        }

        [Description("1 = Table is published using snapshot replication or transactional replication")]
        public bool is_replicated
        {
            get { return _is_replicated; }
            //set {  _is_replicated = value; }
        }

        [Description("1 = Table has a replication filter")]
        public bool has_replication_filter
        {
            get { return _has_replication_filter; }
            //set {  _has_replication_filter = value; }
        }

        [Description("1 = Table is published using merge replication")]
        public bool is_merge_published
        {
            get { return _is_merge_published; }
            //set {  _is_merge_published = value; }
        }

        [Description("1 = Table is subscribed using an immediate updating subscription")]
        public bool is_sync_tran_subscribed
        {
            get { return _is_sync_tran_subscribed; }
            //set {  _is_sync_tran_subscribed = value; }
        }

        [Description("1 = Table contains persisted data that depends on an assembly whose definition changed during the last ALTER ASSEMBLY. Will be reset to 0 after the next successful DBCC CHECKDB or DBCC CHECKTABLE")]
        public bool has_unchecked_assembly_data
        {
            get { return _has_unchecked_assembly_data; }
            //set {  _has_unchecked_assembly_data = value; }
        }

        [Description("The maximum bytes allowed for text in row.\n0 = Text in row option is not set. For more information, see sp_tableoption (Transact-SQL) [ http://msdn2.microsoft.com/en-us/library/ms173530(printer).aspx ]")]
        public int text_in_row_limit
        {
            get { return _text_in_row_limit; }
            //set {  _text_in_row_limit = value; }
        }

        [Description("1 = Large value types are stored out-of-row. For more information, see sp_tableoption (Transact-SQL) [ http://msdn2.microsoft.com/en-us/library/ms173530(printer).aspx ]")]
        public bool large_value_types_out_of_row
        {
            get { return _large_value_types_out_of_row; }
            //set {  _large_value_types_out_of_row = value; }
        }

        #endregion

        [Browsable(true)]
        public Indexes Indexes
        {
            get { return _Indexes; }
        }
        [Browsable(true)]
        public Columns Columns
        {
            get { return _Columns; }
        }
        [Browsable(true)]
        public KeyConstraints Keys
        {
            get { return _Keys; }
        }
        [Browsable(true)] //false
        public foreign_keys ForeignKeys
        {
            get { return _foreign_keys; }
        }
        [Browsable(true)]
        public IdentityColumns IdentityColumns
        {
            get { return _IdentityColumns; }
        }

        
        
        [Description("If not null, PrimaryKeyIndex contains the primary key index")]
        public Index PrimaryKeyIndex
        {
            get
            {
                foreach (Index i in _Indexes.Items)
                {
                    if (i.is_primary_key)
                        return i;
                }
                return null;
            }
        }
        [Description("If not null, PrimaryKeyConstraint contains the primary key constraint")]
        public KeyConstraint PrimaryKeyConstraint
        {
            get
            {
                return Keys.GetPrimaryKeyItem();
            }
        }
        [Description("Returns true if the table contains a primary key")]
        public bool HasPrimaryKey
        {
            get
            {
                foreach (Index i in _Indexes.Items)
                {
                    if (i.is_primary_key)
                        return true;
                }
                return false;
            }
        }
        [Description("Returns true if the table contains foreign key(s)")]
        public bool HasForeignKey
        {
            get
            {
                if (ForeignKeys.Items.Length > 0)
                    return true;
                else
                    return false;
            }
        }
        [Description("Returns true if the table contains Index(es)")]
        public bool HasIndexes
        {
            get
            {
                if (Indexes.Items.Length > 0)
                    return true;
                else
                    return false;
            }
        }
        [Description("Returns true if the table contains a Unique Index")]
        public bool HasUniqueIndex
        {
            get { return _Indexes.HasUniqueIndex; }
        }
        [Description("Returns true if the table contains Identity Columns")]
        public bool HasIdentityColumns
        {
            get
            {
                if (IdentityColumns.Items.Length > 0)
                    return true;
                else
                    return false;
            }
        }

        //public Table[] DependenceOnTables
        //{
        //    get
        //    {
        //        if (HasForeignKey)
        //        {
        //            List<Table> rtntables = new List<Table>();
        //            foreach (foreign_key fk in ForeignKeys.Items)
        //            {
        //                if (!rtntables.Contains(fk.Referenced_Table))
        //                    rtntables.Add(fk.Referenced_Table);
        //            }
        //            return rtntables.ToArray();
        //        }
        //        else
        //            return null;
        //    }
        //}
        //public Table[] TableDependency
        //{
        //    get
        //    {
        //        List<Table> rtntables = new List<Table>();
        //        foreach (Table T in Owner.Items)
        //        {
        //            if (!T.Equals(this))
        //            {
        //                foreach (foreign_key fk in T.ForeignKeys.Items)
        //                {
        //                    if (fk.Referenced_Table.Equals(this) && !rtntables.Contains(fk.Owner.Owner))
        //                        rtntables.Add(fk.Owner.Owner);
        //                }
        //            }

        //        }
        //        if (rtntables.Count > 0)
        //            return rtntables.ToArray();
        //        else
        //            return null;
        //    }
        //}

        //public foreign_key[] ForeignKeyDependency
        //{
        //    get
        //    {
        //        List<foreign_key> rtntables = new List<foreign_key>();
        //        foreach (Table T in Owner.Items)
        //        {
        //            if (!T.Equals(this))
        //            {
        //                foreach (foreign_key fk in T.ForeignKeys.Items)
        //                {
        //                    if (fk.Referenced_Table.Equals(this) && !rtntables.Contains(fk))
        //                        rtntables.Add(fk);
        //                }
        //            }

        //        }
        //        if (rtntables.Count > 0)
        //            return rtntables.ToArray();
        //        else
        //            return null;
        //    }
        //}

        //public Column GetColumn(int column_id)
        //{
        //    foreach (Column C in Columns.Items)
        //    {
        //        if (C.column_id == column_id)
        //            return C;
        //    }
        //    return null;
        //}

        //public long ApproximateRowCount
        //{
        //    get { return Owner.GetRowCount(_object_id); }
        //}
        //public long DataUsageSize
        //{
        //    get { return Owner.GetDataSize(_object_id); }
        //}
        //public long IndexUsageSize
        //{
        //    get { return Owner.GetIndexSize(_object_id); }
        //}
        //public long ReservedSize
        //{
        //    get { return Owner.GetReservedSize(_object_id); }
        //}
        //public long UnusedSize
        //{
        //    get { return Owner.GetUnusedSize(_object_id); }
        //}

        

        private SQLiteConnectionStringBuilder SQLConnSet = new SQLiteConnectionStringBuilder();

        //private void AddFromRecordSet(SQLiteDataReader rs)
        //{
        //    try
        //    {
        //        if (!rs.IsDBNull(0)) { _create_date = rs.GetDateTime(0); }
        //        if (!rs.IsDBNull(1)) { _filestream_data_space_id = rs.GetInt32(1); }
        //        if (!rs.IsDBNull(2)) { _has_replication_filter = rs.GetBoolean(2); }
        //        if (!rs.IsDBNull(3)) { _has_unchecked_assembly_data = rs.GetBoolean(3); }
        //        if (!rs.IsDBNull(4)) { _is_merge_published = rs.GetBoolean(4); }
        //        if (!rs.IsDBNull(5)) { _is_ms_shipped = rs.GetBoolean(5); }
        //        if (!rs.IsDBNull(6)) { _is_published = rs.GetBoolean(6); }
        //        if (!rs.IsDBNull(7)) { _is_replicated = rs.GetBoolean(7); }
        //        if (!rs.IsDBNull(8)) { _is_schema_published = rs.GetBoolean(8); }
        //        if (!rs.IsDBNull(9)) { _is_sync_tran_subscribed = rs.GetBoolean(9); }
        //        if (!rs.IsDBNull(10)) { _large_value_types_out_of_row = rs.GetBoolean(10); }
        //        if (!rs.IsDBNull(11)) { _lob_data_space_id = rs.GetInt32(11); }
        //        if (!rs.IsDBNull(12)) { _lock_on_bulk_load = rs.GetBoolean(12); }
        //        if (!rs.IsDBNull(13)) { _max_column_id_used = rs.GetInt32(13); }
        //        if (!rs.IsDBNull(14)) { _modify_date = rs.GetDateTime(14); }
        //        if (!rs.IsDBNull(15)) { _name = rs.GetString(15); }
        //        if (!rs.IsDBNull(16)) { _object_id = rs.GetInt32(16); }
        //        if (!rs.IsDBNull(17)) { _parent_object_id = rs.GetInt32(17); }
        //        if (!rs.IsDBNull(18)) { _principal_id = rs.GetInt32(18); }
        //        if (!rs.IsDBNull(19)) { _schema_id = rs.GetInt32(19); }
        //        if (!rs.IsDBNull(20)) { _text_in_row_limit = rs.GetInt32(20); }
        //        if (!rs.IsDBNull(21)) { _type = (Object_Type)Enum.Parse(typeof(Object_Type), rs.GetString(21)); }
        //        if (!rs.IsDBNull(22)) { _type_desc = rs.GetString(22); }
        //        if (!rs.IsDBNull(23)) { _uses_ansi_nulls = rs.GetBoolean(23); }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public Table(SQLiteConnectionStringBuilder SQLConnSetting,string _tablename)
        {
            
            SQLConnSet = SQLConnSetting;
            
            _Columns = new Columns(SQLConnSetting, this);
            _Indexes = new Indexes(SQLConnSet, this);
            _foreign_keys = new foreign_keys(SQLConnSet, this);
            _Keys = new KeyConstraints(SQLConnSet, this);
            _IdentityColumns = new IdentityColumns(SQLConnSet, this);
        }
        

        [Browsable(false)]
        public override string ObjectName
        {
            get { return name.Replace(" ","_"); }
        }

        
    }

   
}
