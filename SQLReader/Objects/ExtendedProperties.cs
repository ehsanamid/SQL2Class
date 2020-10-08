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
    public class ExtendedProperty : AbstractSQLObject
    {
        #region Private Fields

        private ExtendedProperty_Type _class_id;
        private string _class_desc;
        private int _major_id;
        private int _minor_id;
        private string _name;
        private object _value;

        #endregion

        #region public properties

        [Description("Identifies the class of item on which the property exists. Can be one of the following:\n0 = Database\n1 = Object or column\n2 = Parameter\n3 = Schema\n4 = Database principal\n5 = Assembly\n6 = Type\n7 = Index\n10 = XML schema collection\n15 = Message type\n16 = Service contract\n17 = Service\n18 = Remote service binding\n19 = Route\n20 = Dataspace (filegroup or partition scheme)\n21 = Partition function\n22 = Database fil")]
        public ExtendedProperty_Type class_id
        {
            get { return _class_id; }
        }

        [Description("Description of the class on which the extended property exists. Can be one of the following:\nDATABASE\nOBJECT_OR_COLUMN\nPARAMETER\nSCHEMA\nDATABASE_PRINCIPAL\nASSEMBLY\nTYPE\nINDEX\nXML_SCHEMA_COLLECTION\nMESSAGE_TYPE\nSERVICE_CONTRACT\nSERVICE\nREMOTE_SERVICE_BINDING\nROUTE\nDATASPACE\nPARTITION_FUNCTION\nDATABASE_FIL")]
        public string class_desc
        {
            get { return _class_desc; }
            //set {  _class_desc = value; }
        }

        [Description("ID of the item on which the extended property exists, interpreted according to its class. For most items, this is the ID that applies to what the class represents. Interpretation for nonstandard major IDs is as follows:\nIf class is 0, major_id is always 0.\nIf class is 1, 2, or 7 = major_id is object_id")]
        public int major_id
        {
            get { return _major_id; }
            //set {  _major_id = value; }
        }

        [Description("Secondary ID of the item on which the extended property exists, interpreted according to its class. For most items this is 0; otherwise, the ID is as follows:\nIf class = 1, minor_id is the column_id if column, else 0 if object.\nIf class = 2, minor_id is the parameter_id.\nIf class 7 = minor _id is the index_id")]
        public int minor_id
        {
            get { return _minor_id; }
            //set {  _minor_id = value; }
        }

        [Description("Property name, unique with class, major_id, and minor_id")]
        public string name
        {
            get { return _name; }
            //set {  _name = value; }
        }

        [Description("Value of the extended property")]
        public Object value
        {
            get { return _value; }
            //set {  _value = value; }
        }

        #endregion

        private Database _Owner;
        [Browsable(false)]
        public Database Owner
        {
            get { return _Owner; }
        }

        private SQLiteConnectionStringBuilder SQLConnSet = new SQLiteConnectionStringBuilder();
        
        private void AddFromRecordSet(SQLiteDataReader rs)
        {
            try
            {
                if (!rs.IsDBNull(0)) { _class_id = (ExtendedProperty_Type)rs.GetByte(0); }
                if (!rs.IsDBNull(1)) { _class_desc = rs.GetString(1); }
                if (!rs.IsDBNull(2)) { _major_id = rs.GetInt32(2); }
                if (!rs.IsDBNull(3)) { _minor_id = rs.GetInt32(3); }
                if (!rs.IsDBNull(4)) { _name = rs.GetString(4); }
                if (!rs.IsDBNull(5)) { _value = rs.GetValue(5); }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ExtendedProperty(SQLiteConnectionStringBuilder SQLConnSetting, SQLiteDataReader rs, Database owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            AddFromRecordSet(rs);
        }
        public ExtendedProperty(SQLiteConnectionStringBuilder SQLConnSetting, byte class_id, int major_id, int minor_id, Database owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;

            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "SELECT [class], [class_desc], [major_id], [minor_id], [name], [value] FROM sys.extended_properties WHERE [class]=" + class_id + " AND [major_id]=" + major_id + " AND [minor_id]=" + minor_id + " ORDER BY [major_id], [minor_id]";
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
    public class ExtendedProperties : AbstractSQLObject
    {
        private List<ExtendedProperty> _Items = new List<ExtendedProperty>();
        [DisplayName("Extended Properties")]
        public ExtendedProperty[] Items
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

        public ExtendedProperties(SQLiteConnectionStringBuilder SQLConnSetting, Database owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "SELECT [class], [class_desc], [major_id], [minor_id], [name], [value] FROM sys.extended_properties ORDER BY [major_id], [minor_id]";
                    SQLiteDataReader rs = Com.ExecuteReader();
                    while (rs.Read())
                    {
                        _Items.Add(new ExtendedProperty(SQLConnSetting, rs, owner));
                    }
                    rs.Close();
                    Conn.Close();
                    rs.Dispose();
                }

            }
        }

        public ExtendedProperty GetExtendedProperty(ExtendedProperty_Type class_id, int major_id, int minor_id, string name)
        {
            foreach (ExtendedProperty EP in _Items)
            {
                if (EP.class_id == class_id && EP.major_id == major_id && EP.minor_id == minor_id && EP.name == name)
                    return EP;
            }
            return null;
        }
        public ExtendedProperty GetExtendedProperty(ExtendedProperty_Type class_id, int major_id, int minor_id)
        {
            return GetExtendedProperty(class_id, major_id, minor_id, "MS_Description");
        }
        public ExtendedProperty[] GetExtendedProperties(ExtendedProperty_Type class_id, int major_id, int minor_id)
        {
            List<ExtendedProperty> newList = new List<ExtendedProperty>();
            foreach (ExtendedProperty EP in _Items)
            {
                if (EP.class_id == class_id && EP.major_id == major_id && EP.minor_id == minor_id)
                    newList.Add(EP);
            }
            return newList.ToArray();
        }

        public override string ToString()
        {
            if (_Items != null)
                return "[" + _Items.Count + "] Extended Properties";
            return base.ToString();
        }

        [Browsable(false)]
        public override string ObjectName
        {
            get { return "ExtendedProperties"; }
        }

        [Browsable(false)]
        public override string UniqueObjectName
        {
            get { return Owner.ObjectName + "ExtendedProperties" + SQLServer.ObjectNameSeperator; }
        }
    }
}
