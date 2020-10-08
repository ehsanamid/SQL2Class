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
    [TypeConverter(typeof(ExpandableObject)), Description("Contains a row per object that is a FOREIGN KEY constraint, with sys.object.type = F.")]
    public class foreign_key : AbstractSQLObject
    {
        #region Private Fields

        private string _name;
        private int _object_id;
        private int _principal_id;
        private int _schema_id;
        private int _parent_object_id;
        private Object_Type _type;
        private string _type_desc;
        private DateTime _create_date;
        private DateTime _modify_date;
        private bool _is_ms_shipped;
        private bool _is_published;
        private bool _is_schema_published;
        private int _referenced_object_id;
        private int _key_index_id;
        private bool _is_disabled;
        private bool _is_not_for_replication;
        private bool _is_not_trusted;
        private byte _delete_referential_action;
        private string _delete_referential_action_desc;
        private byte _update_referential_action;
        private string _update_referential_action_desc;
        private bool _is_system_named;

        #endregion

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

        [Description("ID of the referenced object")]
        public int referenced_object_id
        {
            get { return _referenced_object_id; }
            //set {  _referenced_object_id = value; }
        }

        [Description("ID of the key index within the referenced object")]
        public int key_index_id
        {
            get { return _key_index_id; }
            //set {  _key_index_id = value; }
        }

        [Description("FOREIGN KEY constraint is disabled")]
        public bool is_disabled
        {
            get { return _is_disabled; }
            //set {  _is_disabled = value; }
        }

        [Description("FOREIGN KEY constraint was created by using the NOT FOR REPLICATION option")]
        public bool is_not_for_replication
        {
            get { return _is_not_for_replication; }
            //set {  _is_not_for_replication = value; }
        }

        [Description("FOREIGN KEY constraint has not been verified by the system")]
        public bool is_not_trusted
        {
            get { return _is_not_trusted; }
            //set {  _is_not_trusted = value; }
        }

        [Description("The referential action that was declared for this FOREIGN KEY when a delete happens.\n0 = No action\n1 = Cascade\n2 = Set null\n3 = Set defaul")]
        public byte delete_referential_action
        {
            get { return _delete_referential_action; }
            //set {  _delete_referential_action = value; }
        }

        [Description("Description of the referential action that was declared for this FOREIGN KEY when a delete occurs:\nNO_ACTION\nCASCADE\nSET_NULL\nSET_DEFAUL")]
        public string delete_referential_action_desc
        {
            get { return _delete_referential_action_desc; }
            //set {  _delete_referential_action_desc = value; }
        }

        [Description("The referential action that was declared for this FOREIGN KEY when an update happens.\n0 = No action\n1 = Cascade\n2 = Set null\n3 = Set defaul")]
        public byte update_referential_action
        {
            get { return _update_referential_action; }
            //set {  _update_referential_action = value; }
        }

        [Description("Description of the referential action that was declared for this FOREIGN KEY when an update happens:\nNO_ACTION\nCASCADE\nSET_NULL\nSET_DEFAUL")]
        public string update_referential_action_desc
        {
            get { return _update_referential_action_desc; }
            //set {  _update_referential_action_desc = value; }
        }

        [Description("1 = Name was generated by the system.\n0 = Name was supplied by the user")]
        public bool is_system_named
        {
            get { return _is_system_named; }
            //set {  _is_system_named = value; }
        }

        #endregion

        private foreign_key_columns _ForeignKeyColumns;

        public foreign_key_columns ForeignKeyColumns
        {
            get { return _ForeignKeyColumns; }
        }

        public Table Referenced_Table
        {
            get
            {
                if (Owner.Owner.Owner.Owner.Tables != null)
                    return Owner.Owner.Owner.Owner.Tables.GetItem(_referenced_object_id);
                else
                    return null;
            }
        }

        private foreign_keys _Owner;
        [Browsable(false)]
        public foreign_keys Owner
        {
            get { return _Owner; }
        }

        private SQLiteConnectionStringBuilder SQLConnSet = new SQLiteConnectionStringBuilder();

        private void AddFromRecordSet(SQLiteDataReader rs)
        {
            try
            {
                if (!rs.IsDBNull(0)) { _name = rs.GetString(0); }
                if (!rs.IsDBNull(1)) { _object_id = rs.GetInt32(1); }
                if (!rs.IsDBNull(2)) { _principal_id = rs.GetInt32(2); }
                if (!rs.IsDBNull(3)) { _schema_id = rs.GetInt32(3); }
                if (!rs.IsDBNull(4)) { _parent_object_id = rs.GetInt32(4); }
                if (!rs.IsDBNull(5)) { _type = (Object_Type)Enum.Parse(typeof(Object_Type), rs.GetString(5)); }
                if (!rs.IsDBNull(6)) { _type_desc = rs.GetString(6); }
                if (!rs.IsDBNull(7)) { _create_date = rs.GetDateTime(7); }
                if (!rs.IsDBNull(8)) { _modify_date = rs.GetDateTime(8); }
                if (!rs.IsDBNull(9)) { _is_ms_shipped = rs.GetBoolean(9); }
                if (!rs.IsDBNull(10)) { _is_published = rs.GetBoolean(10); }
                if (!rs.IsDBNull(11)) { _is_schema_published = rs.GetBoolean(11); }
                if (!rs.IsDBNull(12)) { _referenced_object_id = rs.GetInt32(12); }
                if (!rs.IsDBNull(13)) { _key_index_id = rs.GetInt32(13); }
                if (!rs.IsDBNull(14)) { _is_disabled = rs.GetBoolean(14); }
                if (!rs.IsDBNull(15)) { _is_not_for_replication = rs.GetBoolean(15); }
                if (!rs.IsDBNull(16)) { _is_not_trusted = rs.GetBoolean(16); }
                if (!rs.IsDBNull(17)) { _delete_referential_action = rs.GetByte(17); }
                if (!rs.IsDBNull(18)) { _delete_referential_action_desc = rs.GetString(18); }
                if (!rs.IsDBNull(19)) { _update_referential_action = rs.GetByte(19); }
                if (!rs.IsDBNull(20)) { _update_referential_action_desc = rs.GetString(20); }
                if (!rs.IsDBNull(21)) { _is_system_named = rs.GetBoolean(21); }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public foreign_key(SQLiteConnectionStringBuilder SQLConnSetting, SQLiteDataReader rs, foreign_keys owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            AddFromRecordSet(rs);
            _ForeignKeyColumns = new foreign_key_columns(SQLConnSet, this);
        }
        public foreign_key(SQLiteConnectionStringBuilder SQLConnSetting, int object_id, foreign_keys owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;

            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "SELECT name, object_id, principal_id, schema_id, parent_object_id, type, type_desc, create_date, modify_date, is_ms_shipped, is_published, is_schema_published, referenced_object_id, key_index_id, is_disabled, is_not_for_replication, is_not_trusted, delete_referential_action, delete_referential_action_desc, update_referential_action, update_referential_action_desc, is_system_named FROM sys.foreign_keys  WHERE object_id=" + object_id + " AND parent_object_id=" + owner.Owner.object_id + " ORDER BY name";
                    SQLiteDataReader rs = Com.ExecuteReader();
                    while (rs.Read())
                        AddFromRecordSet(rs);
                    rs.Close();
                    Conn.Close();
                    rs.Dispose();
                }

            }
            _ForeignKeyColumns = new foreign_key_columns(SQLConnSet, this);
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
    public class foreign_keys : AbstractSQLObject
    {
        private List<foreign_key> _Items = new List<foreign_key>();
        [DisplayName("Foreign Keys")]
        public foreign_key[] Items
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

        public foreign_keys(SQLiteConnectionStringBuilder SQLConnSetting, Table owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "SELECT name, object_id, principal_id, schema_id, parent_object_id, type, type_desc, create_date, modify_date, is_ms_shipped, is_published, is_schema_published, referenced_object_id, key_index_id, is_disabled, is_not_for_replication, is_not_trusted, delete_referential_action, delete_referential_action_desc, update_referential_action, update_referential_action_desc, is_system_named FROM sys.foreign_keys WHERE parent_object_id=" + owner.object_id + " ORDER BY name";
                    SQLiteDataReader rs = Com.ExecuteReader();
                    while (rs.Read())
                    {
                        _Items.Add(new foreign_key(SQLConnSetting, rs, this));
                    }
                    rs.Close();
                    Conn.Close();
                    rs.Dispose();
                }

            }
        }

        public foreign_key GetForeignKey(int object_id)
        {
            foreach (foreign_key I in _Items)
            {
                if (I.object_id == object_id)
                    return I;
            }
            return null;
        }

        public foreign_key GetForeignKeyByColumn(int column_id)
        {
            foreach (foreign_key I in _Items)
            {
                if (I.ForeignKeyColumns.GetForeignKeyColumnByParentId(column_id) != null)
                    return I;
            }
            return null;
        }
        public foreign_key_column GetForeignKeyColumnByColumn(int column_id)
        {
            foreach (foreign_key I in _Items)
            {
                if (I.ForeignKeyColumns.GetForeignKeyColumnByParentId(column_id) != null)
                    return I.ForeignKeyColumns.GetForeignKeyColumnByParentId(column_id);
            }
            return null;
        }

        public override string ToString()
        {
            if (_Items != null)
                return "[" + _Items.Count + "] foreign_keys";
            return base.ToString();
        }

        [Browsable(false)]
        public override string ObjectName
        {
            get { return "ForeignKeys"; }
        }

        [Browsable(false)]
        public override string UniqueObjectName
        {
            get { return Owner.ObjectName + "foreign_keys" + SQLServer.ObjectNameSeperator; }
        }
    }
}