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
    [TypeConverter(typeof(ExpandableObject)), Description("Contains a row for each view object, with sys.objects.type = V.")]
    public class View : AbstractSQLObject
    {
        #region Private Fields

        private string _name;
        private int _object_id;
        private int _principal_id;
        private int _schema_id;
        private int _parent_object_id;
        private string _type;
        private string _type_desc;
        private DateTime _create_date;
        private DateTime _modify_date;
        private bool _is_ms_shipped;
        private bool _is_published;
        private bool _is_schema_published;
        private bool _is_replicated;
        private bool _has_replication_filter;
        private bool _has_opaque_metadata;
        private bool _has_unchecked_assembly_data;
        private bool _with_check_option;
        private bool _is_date_correlation_view;

        #endregion

        private Columns _Columns;

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
        public string type
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

        [Description("1 = View is replicated")]
        public bool is_replicated
        {
            get { return _is_replicated; }
            //set {  _is_replicated = value; }
        }

        [Description("1 = View has a replication filter")]
        public bool has_replication_filter
        {
            get { return _has_replication_filter; }
            //set {  _has_replication_filter = value; }
        }

        [Description("1 = VIEW_METADATA option specified for view. For more information, see CREATE VIEW (Transact-SQL) [ http://msdn.microsoft.com/en-us/library/ms187956(printer).aspx ]")]
        public bool has_opaque_metadata
        {
            get { return _has_opaque_metadata; }
            //set {  _has_opaque_metadata = value; }
        }

        [Description("1 = View contains persisted data that depends on an assembly whose definition changed during the last ALTER ASSEMBLY. Resets to 0 after the next successful DBCC CHECKDB or DBCC CHECKTABLE")]
        public bool has_unchecked_assembly_data
        {
            get { return _has_unchecked_assembly_data; }
            //set {  _has_unchecked_assembly_data = value; }
        }

        [Description("1 = WITH CHECK OPTION was specified in the view definition")]
        public bool with_check_option
        {
            get { return _with_check_option; }
            //set {  _with_check_option = value; }
        }

        [Description("1 = View was created automatically by the system to store correlation information between datetime columns. Creation of this view was enabled by setting DATE_CORRELATION_OPTIMIZATION to ON")]
        public bool is_date_correlation_view
        {
            get { return _is_date_correlation_view; }
            //set {  _is_date_correlation_view = value; }
        }

        #endregion

        [Browsable(true)]
        public Columns Columns
        {
            get { return _Columns; }
        }

        [Description("Schema of the view")]
        public Schema Schema
        {
            get
            {
                if (Owner.Owner.Schemas != null)
                    return Owner.Owner.Schemas.GetItem(_schema_id);
                else
                    return null;
            }
        }
        [Description("Description for the view")]
        public string Description
        {
            get
            {
                if (Owner != null)
                {
                    ExtendedProperty desc = Owner.Owner.ExtendedProperties.GetExtendedProperty(ExtendedProperty_Type.OBJECT_OR_COLUMN, this.object_id, 0);
                    if (desc != null)
                        return desc.value.ToString();
                    else
                        return "";
                }
                else
                    return "";
            }
        }
        [Description("Extended properties for the view")]
        public ExtendedProperty[] ExtProperties
        {
            get
            {
                if (Owner.Owner.ExtendedProperties != null)
                    return Owner.Owner.ExtendedProperties.GetExtendedProperties(ExtendedProperty_Type.OBJECT_OR_COLUMN, this.object_id, 0);
                else
                    return null;
            }
        }

        private Views _Owner;
        [Browsable(false)]
        public Views Owner
        {
            get { return _Owner; }
        }

        private SQLiteConnectionStringBuilder SQLConnSet = new SQLiteConnectionStringBuilder();

        private void AddFromRecordSet(SQLiteDataReader rs)
        {
            if (!rs.IsDBNull(0)) { _name = rs.GetString(0); }
            if (!rs.IsDBNull(1)) { _object_id = rs.GetInt32(1); }
            if (!rs.IsDBNull(2)) { _principal_id = rs.GetInt32(2); }
            if (!rs.IsDBNull(3)) { _schema_id = rs.GetInt32(3); }
            if (!rs.IsDBNull(4)) { _parent_object_id = rs.GetInt32(4); }
            if (!rs.IsDBNull(5)) { _type = rs.GetString(5); }
            if (!rs.IsDBNull(6)) { _type_desc = rs.GetString(6); }
            if (!rs.IsDBNull(7)) { _create_date = rs.GetDateTime(7); }
            if (!rs.IsDBNull(8)) { _modify_date = rs.GetDateTime(8); }
            if (!rs.IsDBNull(9)) { _is_ms_shipped = rs.GetBoolean(9); }
            if (!rs.IsDBNull(10)) { _is_published = rs.GetBoolean(10); }
            if (!rs.IsDBNull(11)) { _is_schema_published = rs.GetBoolean(11); }
            if (!rs.IsDBNull(12)) { _is_replicated = rs.GetBoolean(12); }
            if (!rs.IsDBNull(13)) { _has_replication_filter = rs.GetBoolean(13); }
            if (!rs.IsDBNull(14)) { _has_opaque_metadata = rs.GetBoolean(14); }
            if (!rs.IsDBNull(15)) { _has_unchecked_assembly_data = rs.GetBoolean(15); }
            if (!rs.IsDBNull(16)) { _with_check_option = rs.GetBoolean(16); }
            if (!rs.IsDBNull(17)) { _is_date_correlation_view = rs.GetBoolean(17); }
        }

        public View(SQLiteConnectionStringBuilder SQLConnSetting, SQLiteDataReader rs, Views owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            AddFromRecordSet(rs);
            _Columns = new Columns(SQLConnSetting, this);
        }
        public View(SQLiteConnectionStringBuilder SQLConnSetting, int object_id, Views owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;

            SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString);
            Conn.Open();
            SQLiteCommand Com = Conn.CreateCommand();
            Com.CommandTimeout = 10;
            Com.CommandText = "SELECT name, object_id, principal_id, schema_id, parent_object_id, type, type_desc, create_date, modify_date, is_ms_shipped, is_published, is_schema_published, is_replicated, has_replication_filter, has_opaque_metadata, has_unchecked_assembly_data, with_check_option, is_date_correlation_view FROM sys.views WHERE object_id=" + object_id + " --ORDER BY name";
            SQLiteDataReader rs = Com.ExecuteReader();//Opretter objektet rs som SQLiteDataReader. (Dette er selve Recordsættet fra databasen).
            while (rs.Read())//Så længe programmet ikke er nået til slutningen af tabellen, så...
            {
                AddFromRecordSet(rs);
            }
            rs.Close();//Lukker SQLiteDataReader igen.
            Conn.Close();
            Com.Dispose();
            rs.Dispose();
            Conn.Dispose();
            _Columns = new Columns(SQLConnSetting, this);
        }

        [Browsable(false)]
        public override string ObjectName
        {
            get { return name.Replace(" ", "_"); }
        }

        [Browsable(false)]
        public override string UniqueObjectName
        {
            get { return Schema.name + "." + name + SQLServer.ObjectNameSeperator; }
        }
    }

    [TypeConverter(typeof(ExpandableObject))]
    public class Views : AbstractSQLObject
    {
        private List<View> _Items = new List<View>();
        public View[] Items
        {
            get { return _Items.ToArray(); }
        }

        private Database _Owner;
        [Browsable(false)]
        public Database Owner
        {
            get { return _Owner; }
        }

        private SQLiteConnectionStringBuilder SQLConnSet = new SQLiteConnectionStringBuilder();

        public Views(SQLiteConnectionStringBuilder SQLConnSetting, Database owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString);
            Conn.Open();
            SQLiteCommand Com = Conn.CreateCommand();
            Com.CommandTimeout = 10;
            Com.CommandText = "SELECT name, object_id, principal_id, schema_id, parent_object_id, type, type_desc, create_date, modify_date, is_ms_shipped, is_published, is_schema_published, is_replicated, has_replication_filter, has_opaque_metadata, has_unchecked_assembly_data, with_check_option, is_date_correlation_view FROM sys.views --ORDER BY name";
            SQLiteDataReader rs = Com.ExecuteReader();//Opretter objektet rs som SQLiteDataReader. (Dette er selve Recordsættet fra databasen).
            while (rs.Read())//Så længe programmet ikke er nået til slutningen af tabellen, så...
            {
                _Items.Add(new View(SQLConnSetting, rs, this));
            }
            rs.Close();//Lukker SQLiteDataReader igen.
            Conn.Close();
            Com.Dispose();
            rs.Dispose();
            Conn.Dispose();
        }

        public View GetItem(int object_id)
        {
            foreach (View V in _Items)
            {
                if (V.object_id == object_id)
                    return V;
            }
            return null;
        }

        public View[] GetItems(int parent_object_id)
        {
            List<View> VI = new List<View>();
            foreach (View V in _Items)
            {
                if (V.parent_object_id == parent_object_id)
                    VI.Add(V);
            }
            return VI.ToArray();
        }

        public override string ToString()
        {
            if (_Items != null)
                return "[" + _Items.Count + "] Views";
            return base.ToString();
        }

        [Browsable(false)]
        public override string ObjectName
        {
            get { return Owner.ObjectName; }
        }

        [Browsable(false)]
        public override string UniqueObjectName
        {
            get { return Owner.UniqueObjectName; }
        }
    }
}
