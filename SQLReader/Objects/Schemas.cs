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
    public class Schema : AbstractSQLObject
    {
        #region Private Fields

        private string _name; //sysname
        private int _principal_id; //int
        private int _schema_id; //int

        #endregion

        #region public properties

        [Description("Name of the schema. Is unique within the database")]
        public string name
        {
            get { return _name; }
            //set {  _name = value; }
        }

        [Description("ID of the schema. Is unique within the database")]
        public int schema_id
        {
            get { return _schema_id; }
            //set {  _schema_id = value; }
        }

        [Description("ID of the principal that owns this schema")]
        public int principal_id
        {
            get { return _principal_id; }
            //set {  _principal_id = value; }
        }


        #endregion

        public string Description
        {
            get
            {
                if (Owner.Owner.ExtendedProperties != null)
                {
                    ExtendedProperty desc = Owner.Owner.ExtendedProperties.GetExtendedProperty(ExtendedProperty_Type.SCHEMA, this.schema_id, 0);
                    if (desc != null)
                        return desc.value.ToString();
                    else
                        return "";
                }
                else
                    return "";
            }
        }

        public User InheritedBy
        {
            get
            {
                return Owner.Owner.Users.GetItem(_principal_id);
            }
        }

        public ExtendedProperty[] ExtProperties
        {
            get
            {
                if (Owner.Owner.ExtendedProperties != null)
                    return Owner.Owner.ExtendedProperties.GetExtendedProperties(ExtendedProperty_Type.SCHEMA, this.schema_id, 0);
                else
                    return null;
            }
        }

        private Schemas _Owner;
        [Browsable(false)]
        public Schemas Owner
        {
            get { return _Owner; }
        }

        private SQLiteConnectionStringBuilder SQLConnSet = new SQLiteConnectionStringBuilder();

        private void AddFromRecordSet(SQLiteDataReader rs)
        {
            try
            {
                if (!rs.IsDBNull(0)) { _name = rs.GetString(0); }
                if (!rs.IsDBNull(1)) { _principal_id = rs.GetInt32(1); }
                if (!rs.IsDBNull(2)) { _schema_id = rs.GetInt32(2); }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Schema(SQLiteConnectionStringBuilder SQLConnSetting, SQLiteDataReader rs, Schemas owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            AddFromRecordSet(rs);
        }
        public Schema(SQLiteConnectionStringBuilder SQLConnSetting, int Schema_ID, Schemas owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;

            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "SELECT name,principal_id,schema_id FROM sys.schemas WHERE schema_id=" + Schema_ID + " ORDER BY schema_id";
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
    public class Schemas : AbstractSQLObject
    {
        private List<Schema> _Items = new List<Schema>();
        [DisplayName("Schemas")]
        public Schema[] Items
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

        public Schemas(SQLiteConnectionStringBuilder SQLConnSetting, Database owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "SELECT name,principal_id,schema_id FROM sys.schemas ORDER BY schema_id";
                    SQLiteDataReader rs = Com.ExecuteReader();
                    while (rs.Read())
                    {
                        _Items.Add(new Schema(SQLConnSetting, rs, this));
                    }
                    rs.Close();
                    Conn.Close();
                    rs.Dispose();
                }

            }
        }

        public Schema GetItem(int? schema_id)
        {
            foreach (Schema S in _Items)
            {
                if (S.schema_id == schema_id)
                    return S;
            }
            return null;
        }

        public override string ToString()
        {
            if (_Items != null)
                return "[" + _Items.Count + "] Schemas";
            return base.ToString();
        }

        [Browsable(false)]
        public override string ObjectName
        {
            get { return "Schemas"; }
        }

        [Browsable(false)]
        public override string UniqueObjectName
        {
            get { return Owner.ObjectName + "Schemas" + SQLServer.ObjectNameSeperator; }
        }
    }
}
