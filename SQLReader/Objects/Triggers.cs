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
    [TypeConverter(typeof(ExpandableObject)), Description("Contains a row for each object that is a trigger, with a type of TR or TA. DML trigger names are schema-scoped and, therefore, are visible in sys.objects. DDL trigger names are scoped by the parent entity and are only visible in this view.")]
    public class Trigger : AbstractSQLObject
    {
        #region Private Fields

        private string _name;
        private int _object_id;
        private byte _parent_class;
        private string _parent_class_desc;
        private int _parent_id;
        private string _type;
        private string _type_desc;
        private DateTime _create_date;
        private DateTime _modify_date;
        private bool _is_ms_shipped;
        private bool _is_disabled;
        private bool _is_not_for_replication;
        private bool _is_instead_of_trigger;

        #endregion

        #region public properties

        [Description("Trigger name. DML trigger names are schema-scoped. DDL trigger names are scoped with respect to the parent entity")]
        public string name
        {
            get { return _name; }
            //set {  _name = value; }
        }

        [Description("Object identification number. Is unique within a database")]
        public int object_id
        {
            get { return _object_id; }
            //set {  _object_id = value; }
        }

        [Description("Class of the parent of the trigger.\n0 = Database, for the DDL triggers.\n1 = Object or column for the DML triggers")]
        public byte parent_class
        {
            get { return _parent_class; }
            //set {  _parent_class = value; }
        }

        [Description("Description of the parent class of the trigger.\nDATABASE\nOBJECT_OR_COLUM")]
        public string parent_class_desc
        {
            get { return _parent_class_desc; }
            //set {  _parent_class_desc = value; }
        }

        [Description("ID of the parent of the trigger, as follows:\n0 = Triggers that are database-parented triggers.\nFor DML triggers, this is the object_id of the table or view on which the DML trigger is defined")]
        public int parent_id
        {
            get { return _parent_id; }
            //set {  _parent_id = value; }
        }

        [Description("Object type:\nTA = Assembly (CLR) trigger\nTR = SQL trigge")]
        public string type
        {
            get { return _type; }
            //set {  _type = value; }
        }

        [Description("Description of object type.\nCLR_TRIGGER\nSQL_TRIGGE")]
        public string type_desc
        {
            get { return _type_desc; }
            //set {  _type_desc = value; }
        }

        [Description("Date the trigger was created")]
        public DateTime create_date
        {
            get { return _create_date; }
            //set {  _create_date = value; }
        }

        [Description("Date the object was last modified by using an ALTER statement")]
        public DateTime modify_date
        {
            get { return _modify_date; }
            //set {  _modify_date = value; }
        }

        [Description("Trigger created on behalf of the user by an internal SQL Server 2005 component")]
        public bool is_ms_shipped
        {
            get { return _is_ms_shipped; }
            //set {  _is_ms_shipped = value; }
        }

        [Description("Trigger is disabled")]
        public bool is_disabled
        {
            get { return _is_disabled; }
            //set {  _is_disabled = value; }
        }

        [Description("Trigger was created as NOT FOR REPLICATION")]
        public bool is_not_for_replication
        {
            get { return _is_not_for_replication; }
            //set {  _is_not_for_replication = value; }
        }

        [Description("1 = INSTEAD OF triggers\n0 = AFTER triggers")]
        public bool is_instead_of_trigger
        {
            get { return _is_instead_of_trigger; }
            //set {  _is_instead_of_trigger = value; }
        }

        #endregion

        [Description("Description for the trigger")]
        public string Description
        {
            get
            {
                if (Owner.Owner.ExtendedProperties != null)
                {
                    //ExtendedProperty desc = Owner.Owner.ExtendedProperties.GetExtendedProperty(Description_Class.SCHEMA, this.schema_id, 0);
                    //if (desc != null)
                    //    return desc.value.ToString();
                    //else
                    return "";
                }
                else
                    return "";
            }
        }

        public Comment Definition
        {
            get
            {
                if (Owner.Owner.Comments != null)
                    return Owner.Owner.Comments.GetItem(_object_id);
                else
                    return null;
            }
        }

        private Triggers _Owner;
        [Browsable(false)]
        public Triggers Owner
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
                if (!rs.IsDBNull(2)) { _parent_class = rs.GetByte(2); }
                if (!rs.IsDBNull(3)) { _parent_class_desc = rs.GetString(3); }
                if (!rs.IsDBNull(4)) { _parent_id = rs.GetInt32(4); }
                if (!rs.IsDBNull(5)) { _type = rs.GetString(5); }
                if (!rs.IsDBNull(6)) { _type_desc = rs.GetString(6); }
                if (!rs.IsDBNull(7)) { _create_date = rs.GetDateTime(7); }
                if (!rs.IsDBNull(8)) { _modify_date = rs.GetDateTime(8); }
                if (!rs.IsDBNull(9)) { _is_ms_shipped = rs.GetBoolean(9); }
                if (!rs.IsDBNull(10)) { _is_disabled = rs.GetBoolean(10); }
                if (!rs.IsDBNull(11)) { _is_not_for_replication = rs.GetBoolean(11); }
                if (!rs.IsDBNull(12)) { _is_instead_of_trigger = rs.GetBoolean(12); }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Trigger(SQLiteConnectionStringBuilder SQLConnSetting, SQLiteDataReader rs, Triggers owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            AddFromRecordSet(rs);
        }
        public Trigger(SQLiteConnectionStringBuilder SQLConnSetting, int Trigger_ID, Triggers owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;

            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "SELECT name, object_id, parent_class, parent_class_desc, parent_id, type, type_desc, create_date, modify_date, is_ms_shipped, is_disabled, is_not_for_replication, is_instead_of_trigger FROM sys.triggers WHERE Trigger_id=" + Trigger_ID + " ORDER BY Trigger_id";
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
    public class Triggers : AbstractSQLObject
    {
        private List<Trigger> _Items = new List<Trigger>();
        [DisplayName("Triggers")]
        public Trigger[] Items
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

        public Triggers(SQLiteConnectionStringBuilder SQLConnSetting, Database owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "SELECT name, object_id, parent_class, parent_class_desc, parent_id, type, type_desc, create_date, modify_date, is_ms_shipped, is_disabled, is_not_for_replication, is_instead_of_trigger FROM sys.triggers --ORDER BY Trigger_id";
                    SQLiteDataReader rs = Com.ExecuteReader();
                    while (rs.Read())
                    {
                        _Items.Add(new Trigger(SQLConnSetting, rs, this));
                    }
                    rs.Close();
                    Conn.Close();
                    rs.Dispose();
                }

            }
        }

        public Trigger GetItem(int object_id)
        {
            foreach (Trigger T in _Items)
            {
                if (T.object_id == object_id)
                    return T;
            }
            return null;
        }

        public Trigger[] GetItems(int parent_id)
        {
            List<Trigger> TR = new List<Trigger>();
            foreach (Trigger T in _Items)
            {
                if (T.parent_id == parent_id)
                    TR.Add(T);
            }
            return TR.ToArray();
        }

        public override string ToString()
        {
            if (_Items != null)
                return "[" + _Items.Count + "] Triggers";
            return base.ToString();
        }

        [Browsable(false)]
        public override string ObjectName
        {
            get { return "Triggers"; }
        }

        [Browsable(false)]
        public override string UniqueObjectName
        {
            get { return Owner.ObjectName + "Triggers" + SQLServer.ObjectNameSeperator; }
        }
    }
}
