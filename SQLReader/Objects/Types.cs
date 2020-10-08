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
    public class SQL_Type : AbstractSQLObject
    {
        #region Private Fields

        private string _name;
        private byte _system_type_id;
        private int _user_type_id;
        private int _schema_id;
        private int _principal_id;
        private Int16 _max_length;
        private byte _precision;
        private byte _scale;
        private string _collation_name;
        private bool _is_nullable;
        private bool _is_user_defined;
        private bool _is_assembly_type;
        private int _default_object_id;
        private int _rule_object_id;

        #endregion

        #region public properties


        [Description("Name of the type. Is unique within the schema")]
        public string name
        {
            get { return _name; }
            //set {  _name = value; }
        }

        [Description("ID of the internal system-type of the type")]
        public byte system_type_id
        {
            get { return _system_type_id; }
            //set {  _system_type_id = value; }
        }

        [Description("ID of the type. Is unique within the database. For system data types, user_type_id = system_type_id")]
        public int user_type_id
        {
            get { return _user_type_id; }
            //set {  _user_type_id = value; }
        }

        [Description("ID of the schema to which the type belongs")]
        public int schema_id
        {
            get { return _schema_id; }
            //set {  _schema_id = value; }
        }

        [Description("ID of the individual owner if different from schema owner. By default, schema-contained objects are owned by the schema owner. However, an alternate owner can be specified by using the ALTER AUTHORIZATION statement to change ownership.\nNULL if there is no alternate individual owner")]
        public int principal_id
        {
            get { return _principal_id; }
            //set {  _principal_id = value; }
        }

        [Description("Maximum Length (in bytes) of the type.\n-1 = Column data type is varchar(max), nvarchar(max), varbinary(max), or xml.\nFor text columns, the max_length value will be 16")]
        public Int16 max_length
        {
            get { return _max_length; }
            //set {  _max_length = value; }
        }

        [Description("Max precision of the type if it is numeric-based; otherwise, 0")]
        public byte precision
        {
            get { return _precision; }
            //set {  _precision = value; }
        }

        [Description("Max scale of the type if it is numeric-based; otherwise, 0")]
        public byte scale
        {
            get { return _scale; }
            //set {  _scale = value; }
        }

        [Description("Name of the collation of the type if it is character-based; other wise, NULL")]
        public string collation_name
        {
            get { return _collation_name; }
            //set {  _collation_name = value; }
        }

        [Description("Type is nullable")]
        public bool is_nullable
        {
            get { return _is_nullable; }
            //set {  _is_nullable = value; }
        }

        [Description("1 = User-defined type.\n0 = SQL Server 2005 system data type")]
        public bool is_user_defined
        {
            get { return _is_user_defined; }
            //set {  _is_user_defined = value; }
        }

        [Description("1 = Implementation of the type is defined in a CLR assembly.\n0 = Type is based on a SQL Server system data type")]
        public bool is_assembly_type
        {
            get { return _is_assembly_type; }
            //set {  _is_assembly_type = value; }
        }

        [Description("ID of the stand-alone default that is bound to the type by using sp_bindefault [ http://msdn2.microsoft.com/en-us/library/ms177503(printer).aspx ] .\n0 = No default exists")]
        public int default_object_id
        {
            get { return _default_object_id; }
            //set {  _default_object_id = value; }
        }

        [Description("ID of the stand-alone rule that is bound to the type by using sp_bindrule [ http://msdn2.microsoft.com/en-us/library/ms176063(printer).aspx ] .\n0 = No rule exists")]
        public int rule_object_id
        {
            get { return _rule_object_id; }
            //set {  _rule_object_id = value; }
        }

        #endregion

        
        public SQL_Type system_type
        {
            get
            {
                if (_system_type_id == _user_type_id)
                {
                    return null;
                }
                else
                {
                    return _Owner.GetSQLSystemType(_user_type_id);
                }
            }
        }

        public Schema Schema
        {
            get
            {
                if (Owner.Owner.Schemas != null && _schema_id > -1)
                    return Owner.Owner.Schemas.GetItem(_schema_id);
                else
                    return null;
            }
        }

        public Type NetType
        {
            get
            {
                if (_system_type_id == _user_type_id)
                {
                    return GetTypeFromName(_name);
                }
                else
                {
                    SQL_Type SQLT = _Owner.GetSQLSystemType(_system_type_id);
                    if (SQLT != null)
                        return GetTypeFromName(SQLT.name);
                    else
                        return typeof(object);
                }
            }
        }

        private SQL_Types _Owner;
        [Browsable(false)]
        public SQL_Types Owner
        {
            get { return _Owner; }
        }

        private SQLiteConnectionStringBuilder SQLConnSet = new SQLiteConnectionStringBuilder();

        private void AddFromRecordSet(SQLiteDataReader rs)
        {
            try
            {
                if (!rs.IsDBNull(0)) { _name = rs.GetString(0); }
                if (!rs.IsDBNull(1)) { _system_type_id = rs.GetByte(1); }
                if (!rs.IsDBNull(2)) { _user_type_id = rs.GetInt32(2); }
                if (!rs.IsDBNull(3)) { _schema_id = rs.GetInt32(3); }
                if (!rs.IsDBNull(4)) { _principal_id = rs.GetInt32(4); }
                if (!rs.IsDBNull(5)) { _max_length = rs.GetInt16(5); }
                if (!rs.IsDBNull(6)) { _precision = rs.GetByte(6); }
                if (!rs.IsDBNull(7)) { _scale = rs.GetByte(7); }
                if (!rs.IsDBNull(8)) { _collation_name = rs.GetString(8); }
                if (!rs.IsDBNull(9)) { _is_nullable = rs.GetBoolean(9); }
                if (!rs.IsDBNull(10)) { _is_user_defined = rs.GetBoolean(10); }
                if (!rs.IsDBNull(11)) { _is_assembly_type = rs.GetBoolean(11); }
                if (!rs.IsDBNull(12)) { _default_object_id = rs.GetInt32(12); }
                if (!rs.IsDBNull(13)) { _rule_object_id = rs.GetInt32(13); }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SQL_Type(SQLiteConnectionStringBuilder SQLConnSetting, SQLiteDataReader rs, SQL_Types owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            AddFromRecordSet(rs);

        }
        public SQL_Type(SQLiteConnectionStringBuilder SQLConnSetting, byte system_type_id, SQL_Types owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "Select name, system_type_id, user_type_id, schema_id, principal_id, max_length, precision, scale, collation_name, is_nullable, is_user_defined, is_assembly_type, default_object_id, rule_object_id from sys.types where system_type_id=" + system_type_id + "";
                    SQLiteDataReader rs = Com.ExecuteReader();
                    while (rs.Read())
                        AddFromRecordSet(rs);
                    rs.Close();
                    Conn.Close();
                    rs.Dispose();
                }


            }
        }

        private Type GetTypeFromName(string Tname)
        {
            switch (Tname.ToLower())
            {
                case "bigint": return typeof(System.Int64);
                case "binary": return typeof(System.Byte).MakeArrayType();
                case "bit": return typeof(System.Boolean);
                case "char": return typeof(System.String);
                case "date": return typeof(System.DateTime);
                case "datetime": return typeof(System.DateTime);
                case "datetime2": return typeof(System.DateTime);
                case "datetimeoffset": return typeof(System.DateTimeOffset);
                case "decimal": return typeof(System.Decimal);
                case "float": return typeof(System.Double);
                case "geography": return typeof(System.Object);
                case "geometry": return typeof(System.Object);
                case "hierarchyid": return typeof(System.Object);
                case "image": return typeof(System.Object);
                case "int": return typeof(System.Int32);
                case "money": return typeof(System.Decimal);
                case "nchar": return typeof(System.String);
                case "ntext": return typeof(System.String);
                case "numeric": return typeof(System.Decimal);
                case "nvarchar": return typeof(System.String);
                case "real": return typeof(System.Single);
                case "rowversion": return typeof(System.Byte).MakeArrayType();
                case "smalldatetime": return typeof(System.DateTime);
                case "smallint": return typeof(System.Int16);
                case "smallmoney": return typeof(System.Decimal);
                case "sql_variant": return typeof(System.Object);
                case "text": return typeof(System.String);
                case "time": return typeof(System.TimeSpan);
                case "timestamp": return typeof(System.Object);
                case "tinyint": return typeof(System.Byte);
                case "uniqueidentifier": return typeof(System.Guid);
                case "varbinary": return typeof(System.Byte).MakeArrayType();
                case "varchar": return typeof(System.String);
                case "xml": return typeof(System.String);
                default: return null;
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
    public class SQL_Types : AbstractSQLObject
    {
        private Database _Owner;
        [Browsable(false)]
        public Database Owner
        {
            get { return _Owner; }
        }

        public List<SQL_Type> _Items = new List<SQL_Type>();

        [DisplayName("Types")]
        public SQL_Type[] Items
        {
            get
            {
                return _Items.ToArray();
            }
        }

        public SQL_Type GetSQLType(int user_type_id)
        {
            foreach (SQL_Type st in _Items)
            {
                if (st.user_type_id == user_type_id)
                    return st;
            }
            return null;
        }
        public SQL_Type GetSQLSystemType(int user_type_id)
        {
            foreach (SQL_Type st in _Items)
            {
                if (st.system_type_id == user_type_id && st.user_type_id == user_type_id)
                    return st;
            }
            return null;
        }

        public SQL_Types(SQLiteConnectionStringBuilder SQLConnSetting, Database owner)
        {
            _Owner = owner;
            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSetting.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "Select name, system_type_id, user_type_id, schema_id, principal_id, max_length, precision, scale, collation_name, is_nullable, is_user_defined, is_assembly_type, default_object_id, rule_object_id from sys.types ORDER BY name";
                    SQLiteDataReader rs = Com.ExecuteReader();
                    while (rs.Read())
                    {
                        _Items.Add(new SQL_Type(SQLConnSetting, rs, this));
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
                return "[" + _Items.Count + "] Types";
            return base.ToString();
        }

        [Browsable(false)]
        public override string ObjectName
        {
            get { return "Types"; }
        }

        [Browsable(false)]
        public override string UniqueObjectName
        {
            get { return Owner.ObjectName+ "Types" + SQLServer.ObjectNameSeperator ; }
        }
    }

}
