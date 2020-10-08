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
    [TypeConverter(typeof(ExpandableObject))]
    public class Column : AbstractSQLObject
    {
        #region Private Fields

        private int _object_id;
        private string _name;
        private int _column_id;
        private byte _system_type_id;
        private int _user_type_id;
        private Int16 _max_length;
        private byte _precision;
        private byte _scale;
        private string _collation_name;
        private bool _is_nullable;
        private bool _is_ansi_padded;
        private bool _is_rowguidcol;
        private bool _is_identity;
        private bool _is_computed;
        private bool _is_filestream;
        private bool _is_replicated;
        private bool _is_non_sql_subscribed;
        private bool _is_merge_published;
        private bool _is_dts_replicated;
        private bool _is_xml_document;
        private int _xml_collection_id;
        private int _default_object_id;
        private int _rule_object_id;

        #endregion

        #region Public Properties

        [Description("ID of the object to which this column belongs")]
        public int object_id
        {
            get { return _object_id; }
            //set {  _object_id = value; }
        }

        [Description("Name of the column. Is unique within the object")]
        public string name
        {
            get { return _name; }
            //set {  _name = value; }
        }

        [Description("ID of the column. Is unique within the object.\nColumn IDs might not be sequential")]
        public int column_id
        {
            get { return _column_id; }
            //set {  _column_id = value; }
        }

        [Description("ID of the system type of the column")]
        public byte system_type_id
        {
            get { return _system_type_id; }
            //set {  _system_type_id = value; }
        }

        [Description("ID of the type of the column as defined by the user.\nTo return the name of the type, join to the sys.types [ http://msdn2.microsoft.com/en-us/library/ms188021(printer).aspx ] catalog view on this column")]
        public int user_type_id
        {
            get { return _user_type_id; }
            //set {  _user_type_id = value; }
        }

        [Description("Maximum Length (in bytes) of the column.\n-1 = Column data type is varchar(max), nvarchar(max), varbinary(max), or xml.\nFor text columns, the max_length value will be 16 or the value set by sp_tableoption 'text in row'")]
        public Int16 max_length
        {
            get { return _max_length; }
            //set {  _max_length = value; }
        }

        [Description("Precision of the column if numeric-based; otherwise, 0")]
        public byte precision
        {
            get { return _precision; }
            //set {  _precision = value; }
        }

        [Description("Scale of column if numeric-based; otherwise, 0")]
        public byte scale
        {
            get { return _scale; }
            //set {  _scale = value; }
        }

        [Description("Name of the collation of the column if character-based; otherwise, NULL")]
        public string collation_name
        {
            get { return _collation_name; }
            //set {  _collation_name = value; }
        }

        [Description("1 = Column is nullable")]
        public bool is_nullable
        {
            get { return _is_nullable; }
            //set {  _is_nullable = value; }
        }

        [Description("1 = Column uses ANSI_PADDING ON behavior if character, binary, or variant.\n0 = Column is not character, binary, or variant")]
        public bool is_ansi_padded
        {
            get { return _is_ansi_padded; }
            //set {  _is_ansi_padded = value; }
        }

        [Description("1 = Column is a declared ROWGUIDCOL")]
        public bool is_rowguidcol
        {
            get { return _is_rowguidcol; }
            //set {  _is_rowguidcol = value; }
        }

        [Description("1 = Column has identity value")]
        public bool is_identity
        {
            get { return _is_identity; }
            //set {  _is_identity = value; }
        }

        [Description("1 = Column is a computed column")]
        public bool is_computed
        {
            get { return _is_computed; }
            //set {  _is_computed = value; }
        }

        [Description("Reserved for future use")]
        public bool is_filestream
        {
            get { return _is_filestream; }
            //set {  _is_filestream = value; }
        }

        [Description("1 = Column is replicated")]
        public bool is_replicated
        {
            get { return _is_replicated; }
            //set {  _is_replicated = value; }
        }

        [Description("1 = Column has a non-SQL Server subscriber")]
        public bool is_non_sql_subscribed
        {
            get { return _is_non_sql_subscribed; }
            //set {  _is_non_sql_subscribed = value; }
        }

        [Description("1 = Column is merge-published")]
        public bool is_merge_published
        {
            get { return _is_merge_published; }
            //set {  _is_merge_published = value; }
        }

        [Description("1 = Column is replicated by using SQL Server 2005 Integration Services (SSIS)")]
        public bool is_dts_replicated
        {
            get { return _is_dts_replicated; }
            //set {  _is_dts_replicated = value; }
        }

        [Description("1 = Content is a complete XML document.\n0 = Content is a document fragment or the column data type is not xml")]
        public bool is_xml_document
        {
            get { return _is_xml_document; }
            //set {  _is_xml_document = value; }
        }

        [Description("Nonzero if the data type of the column is xml and the XML is typed. The value will be the ID of the collection containing the validating XML schema namespace of the column.\n0 = No XML schema collection")]
        public int xml_collection_id
        {
            get { return _xml_collection_id; }
            //set {  _xml_collection_id = value; }
        }

        [Description("ID of the default object, regardless of whether it is a stand-alone object sys.sp_bindefault [ http://msdn2.microsoft.com/en-us/library/ms177503(printer).aspx ] , or an inline, column-level DEFAULT constraint. The parent_object_id column of an inline column-level default object is a reference back to the table itself.\n0 = No default")]
        public int default_object_id
        {
            get { return _default_object_id; }
            //set {  _default_object_id = value; }
        }

        [Description("ID of the stand-alone rule bound to the column by using sys.sp_bindrule.\n0 = No stand-alone rule. For column-level CHECK constraints, see sys.check_constraints (Transact-SQL) [ http://msdn2.microsoft.com/en-us/library/ms187388(printer).aspx ]")]
        public int rule_object_id
        {
            get { return _rule_object_id; }
            //set {  _rule_object_id = value; }
        }

        public SQL_Type system_type
        {
            get
            {
                try
                {
                    if(Owner.Owner is Table)
                        return ((Table)Owner.Owner).Owner.Owner.Types.GetSQLType(_user_type_id);
                    else if (Owner.Owner is View)
                        return ((View)Owner.Owner).Owner.Owner.Types.GetSQLType(_user_type_id);
                    else
                        return null;
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion

        public string Description
        {
            get
            {
                Database tmpDb = null;
                if (Owner.Owner is Table)
                    tmpDb = ((Table)Owner.Owner).Owner.Owner;
                else if (Owner.Owner is View)
                    tmpDb = ((View)Owner.Owner).Owner.Owner;
                if (tmpDb.ExtendedProperties != null)
                {
                    ExtendedProperty desc = tmpDb.ExtendedProperties.GetExtendedProperty(ExtendedProperty_Type.OBJECT_OR_COLUMN, this.object_id, this.column_id);
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
                Database tmpDb = null;
                if (Owner.Owner is Table)
                    tmpDb = ((Table)Owner.Owner).Owner.Owner;
                else if (Owner.Owner is View)
                    tmpDb = ((View)Owner.Owner).Owner.Owner;
                if (tmpDb.ExtendedProperties != null)
                    return tmpDb.ExtendedProperties.GetExtendedProperties(ExtendedProperty_Type.OBJECT_OR_COLUMN, this.object_id, this.column_id);
                else
                    return null;
            }
        }

        public IdentityColumn Identity
        {
            get
            {
                if (_is_identity)
                {
                    Table tmpT = null;
                    if (Owner.Owner is Table)
                        tmpT = ((Table)Owner.Owner);
                    if (tmpT == null)
                        return null;
                    if (tmpT.IdentityColumns != null)
                        return tmpT.IdentityColumns.GetItem(_column_id);
                    else
                        return null;
                }
                else
                    return null;
            }
        }

        private Columns _Owner;
        [Browsable(false)]
        public Columns Owner
        {
            get { return _Owner; }
        }

        public bool IsPrimaryKey
        {
            get
            {
                Table tmpT = null;
                if (Owner.Owner is Table)
                    tmpT = ((Table)Owner.Owner);
                if (tmpT == null)
                    return false;
                if (tmpT.Indexes.GetIndexColumn(_column_id) != null)
                    return true;
                else
                    return false;
            }
        }

        public bool IsForeignKey
        {
            get
            {
                Table tmpT = null;
                if (Owner.Owner is Table)
                    tmpT = ((Table)Owner.Owner);
                if (tmpT == null)
                    return false;
                if (tmpT.ForeignKeys.GetForeignKeyByColumn(_column_id) != null)
                    return true;
                else
                    return false;
            }
        }

        public DefaultConstraint DefaultConstraint
        {
            get
            {
                Database tmpDB = null;
                if (Owner.Owner is Table)
                    tmpDB = ((Table)Owner.Owner).Owner.Owner;
                if (tmpDB == null)
                    return null;
                if (tmpDB.DefaultConstraints != null)
                    return tmpDB.DefaultConstraints.GetItem(((Table)Owner.Owner).object_id, _column_id);
                else
                    return null;
            }
        }

        public foreign_key_column ForeignKeyColumn
        {
            get
            {
                Table tmpT = null;
                if (Owner.Owner is Table)
                    tmpT = ((Table)Owner.Owner);
                if (tmpT == null)
                    return null;
                if (tmpT.ForeignKeys != null)
                    return tmpT.ForeignKeys.GetForeignKeyColumnByColumn(_column_id);
                else
                    return null;
            }
        }

        public foreign_key ForeignKey
        {
            get
            {
                Table tmpT = null;
                if (Owner.Owner is Table)
                    tmpT = ((Table)Owner.Owner);
                if (tmpT == null)
                    return null;
                if (tmpT.ForeignKeys != null)
                    return tmpT.ForeignKeys.GetForeignKeyByColumn(_column_id);
                else
                    return null;
            }
        }

        private SQLiteConnectionStringBuilder SQLConnSet = new SQLiteConnectionStringBuilder();

        private void AddFromRecordSet(SQLiteDataReader rs)
        {
            try
            {
                if (!rs.IsDBNull(0)) { _collation_name = rs.GetString(0); }
                if (!rs.IsDBNull(1)) { _column_id = rs.GetInt32(1); }
                if (!rs.IsDBNull(2)) { _default_object_id = rs.GetInt32(2); }
                if (!rs.IsDBNull(3)) { _is_ansi_padded = rs.GetBoolean(3); }
                if (!rs.IsDBNull(4)) { _is_computed = rs.GetBoolean(4); }
                if (!rs.IsDBNull(5)) { _is_dts_replicated = rs.GetBoolean(5); }
                if (!rs.IsDBNull(6)) { _is_filestream = rs.GetBoolean(6); }
                if (!rs.IsDBNull(7)) { _is_identity = rs.GetBoolean(7); }
                if (!rs.IsDBNull(8)) { _is_merge_published = rs.GetBoolean(8); }
                if (!rs.IsDBNull(9)) { _is_non_sql_subscribed = rs.GetBoolean(9); }
                if (!rs.IsDBNull(10)) { _is_nullable = rs.GetBoolean(10); }
                if (!rs.IsDBNull(11)) { _is_replicated = rs.GetBoolean(11); }
                if (!rs.IsDBNull(12)) { _is_rowguidcol = rs.GetBoolean(12); }
                if (!rs.IsDBNull(13)) { _is_xml_document = rs.GetBoolean(13); }
                if (!rs.IsDBNull(14)) { _max_length = rs.GetInt16(14); }
                if (!rs.IsDBNull(15)) { _name = rs.GetString(15); }
                if (!rs.IsDBNull(16)) { _object_id = rs.GetInt32(16); }
                if (!rs.IsDBNull(17)) { _precision = rs.GetByte(17); }
                if (!rs.IsDBNull(18)) { _rule_object_id = rs.GetInt32(18); }
                if (!rs.IsDBNull(19)) { _scale = rs.GetByte(19); }
                if (!rs.IsDBNull(20)) { _system_type_id = rs.GetByte(20); }
                if (!rs.IsDBNull(21)) { _user_type_id = rs.GetInt32(21); }
                if (!rs.IsDBNull(22)) { _xml_collection_id = rs.GetInt32(22); }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Column(SQLiteConnectionStringBuilder SQLConnSetting, SQLiteDataReader rs, Columns owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            AddFromRecordSet(rs);
        }
        public Column(SQLiteConnectionStringBuilder SQLConnSetting, int column_id, Columns owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "Select collation_name, column_id, default_object_id, is_ansi_padded, is_computed, is_dts_replicated, is_filestream, is_identity, is_merge_published, is_non_sql_subscribed, is_nullable, is_replicated, is_rowguidcol, is_xml_document, max_length, name, object_id, precision, rule_object_id, scale, system_type_id, user_type_id, xml_collection_id from sys.columns where column_id=" + column_id + "";
                    Conn.Open();
                    using (SQLiteDataReader rs = Com.ExecuteReader())
                    {
                        while (rs.Read())
                            AddFromRecordSet(rs);
                    }
                }
            }
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
    public class Columns : AbstractSQLObject
    {
        private List<Column> _Items = new List<Column>();
        [DisplayName("Columns")]
        public Column[] Items
        {
            get { return _Items.ToArray(); }
        }

        private AbstractSQLObject _Owner;
        [Browsable(false)]
        public AbstractSQLObject Owner
        {
            get { return _Owner; }
        }

        private SQLiteConnectionStringBuilder SQLConnSet = new SQLiteConnectionStringBuilder();

        public Columns(SQLiteConnectionStringBuilder SQLConnSetting, AbstractSQLObject owner)
        {
            SQLConnSet = SQLConnSetting;
            _Owner = owner;
            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    int objectid = -1;
                    if (_Owner is Table)
                        objectid = ((Table)_Owner).object_id;
                    else if (_Owner is View)
                        objectid = ((View)_Owner).object_id;

                    if (objectid > 0)
                    {
                        Com.CommandTimeout = 10;
                        Com.CommandText = "Select collation_name, column_id, default_object_id, is_ansi_padded, is_computed, is_dts_replicated, is_filestream, is_identity, is_merge_published, is_non_sql_subscribed, is_nullable, is_replicated, is_rowguidcol, is_xml_document, max_length, name, object_id, precision, rule_object_id, scale, system_type_id, user_type_id, xml_collection_id from sys.columns where object_id=" + objectid + " ORDER BY column_id";

                        SQLiteDataReader rs = Com.ExecuteReader();
                        while (rs.Read())
                            _Items.Add(new Column(SQLConnSet, rs, this));
                        rs.Close();
                        Conn.Close();
                        rs.Dispose();
                    }
                }

            }
        }

        public Column GetColumn(int Column_id)
        {
            foreach (Column C in this.Items)
            {
                if (C.column_id == Column_id)
                    return C;
            }
            return null;
        }

        public override string ToString()
        {
            if (_Items != null)
                return "[" + _Items.Count + "] Columns";
            return base.ToString();
        }

        [Browsable(false)]
        public override string ObjectName
        {
            get { return "Columns"; }
        }

        [Browsable(false)]
        public override string UniqueObjectName
        {
            get { return Owner.ObjectName + "Columns" + SQLServer.ObjectNameSeperator; }
        }
    }
}
