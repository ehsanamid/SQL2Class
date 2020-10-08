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
    [TypeConverter(typeof(ExpandableObject)), Description("Contains a row for each object that is a primary key or unique constraint. Includes sys.objects.type PK and UQ.")]
    public class KeyConstraint : AbstractSQLObject
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
        private int _unique_index_id;
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

        [Description("ID of the corresponding unique index in the parent object that was created to enforce this constraint")]
        public int unique_index_id
        {
            get { return _unique_index_id; }
            //set {  _unique_index_id = value; }
        }

        [Description("1 = Name was generated by system.\n0 = Name was supplied by the user")]
        public bool is_system_named
        {
            get { return _is_system_named; }
            //set {  _is_system_named = value; }
        }

        #endregion

        private KeyConstraints _Owner;
        [Browsable(false)]
        public KeyConstraints Owner
        {
            get { return _Owner; }
        }

        public Index KeyIndex
        {
            get
            {
                if (this.Owner.Owner != null)
                    return this.Owner.Owner.Indexes.GetItem(_unique_index_id);
                else
                    return null;
            }
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
                if (!rs.IsDBNull(12)) { _unique_index_id = rs.GetInt32(12); }
                if (!rs.IsDBNull(13)) { _is_system_named = rs.GetBoolean(13); }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public KeyConstraint(SQLiteConnectionStringBuilder SQLConnSetting, SQLiteDataReader rs, KeyConstraints owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            AddFromRecordSet(rs);
        }
        public KeyConstraint(SQLiteConnectionStringBuilder SQLConnSetting, int key_constraint_ID, KeyConstraints owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;

            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "SELECT name, object_id, principal_id, schema_id, parent_object_id, type, type_desc, create_date, modify_date, is_ms_shipped, is_published, is_schema_published, unique_index_id, is_system_named FROM sys.key_constraints WHERE object_id=" + key_constraint_ID + "";
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
            get { return name.Replace(" ","_"); }
        }

        [Browsable(false)]
        public override string UniqueObjectName
        {
            get { return Owner.ObjectName + name + SQLServer.ObjectNameSeperator; }
        }
    }

    [TypeConverter(typeof(ExpandableObject))]
    public class KeyConstraints : AbstractSQLObject
    {
        private List<KeyConstraint> _Items = new List<KeyConstraint>();
        [DisplayName("Key Constraints")]
        public KeyConstraint[] Items
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

        public KeyConstraints(SQLiteConnectionStringBuilder SQLConnSetting, Table owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "SELECT name, object_id, principal_id, schema_id, parent_object_id, type, type_desc, create_date, modify_date, is_ms_shipped, is_published, is_schema_published, unique_index_id, is_system_named FROM sys.key_constraints WHERE parent_object_id=" + owner.object_id + " ORDER BY name";
                    SQLiteDataReader rs = Com.ExecuteReader();
                    while (rs.Read())
                    {
                        _Items.Add(new KeyConstraint(SQLConnSetting, rs, this));
                    }
                    rs.Close();
                    Conn.Close();
                    rs.Dispose();
                }

            }
        }

        public KeyConstraint GetPrimaryKeyItem()
        {
            foreach (KeyConstraint K in _Items)
            {
                if (K.KeyIndex.is_primary_key)
                    return K;
            }
            return null;
        }
        public KeyConstraint GetItem(int object_id)
        {
            foreach (KeyConstraint K in _Items)
            {
                if (K.object_id == object_id)
                    return K;
            }
            return null;
        }
        public KeyConstraint[] GetItems(int parent_object_id)
        {
            List<KeyConstraint> ks = new List<KeyConstraint>();
            foreach (KeyConstraint K in _Items)
            {
                if (K.parent_object_id == parent_object_id)
                    ks.Add(K);
            }
            return ks.ToArray();
        }

        public override string ToString()
        {
            if (_Items != null)
                return "[" + _Items.Count + "] key_constraints";
            return base.ToString();
        }

        [Browsable(false)]
        public override string ObjectName
        {
            get { return "KeyConstraints"; }
        }

        [Browsable(false)]
        public override string UniqueObjectName
        {
            get { return Owner.ObjectName + "KeyConstraints" + SQLServer.ObjectNameSeperator; }
        }
    }
}
