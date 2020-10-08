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
    public class DatabaseFile : AbstractSQLObject
    {
        #region Private Fields

        private decimal? _backup_lsn; // numeric(25,0)
        private decimal? _create_lsn; // numeric(25,0)
        private int? _data_space_id; // int
        private int? _database_id; // int
        private Guid _differential_base_guid; // uniqueidentifier
        private decimal? _differential_base_lsn; // numeric(25,0)
        private DateTime? _differential_base_time; // datetime
        private decimal? _drop_lsn; // numeric(25,0)
        private Guid _file_guid; // uniqueidentifier
        private int? _file_id; // int
        private int? _growth; // int
        private bool? _is_media_read_only; // bit
        private bool? _is_name_reserved; // bit
        private bool? _is_percent_growth; // bit
        private bool? _is_read_only; // bit
        private bool? _is_sparse; // bit
        private int? _max_size; // int
        private string _name; // sysname
        private string _physical_name; // nvarchar(260)
        private decimal? _read_only_lsn; // numeric(25,0)
        private decimal? _read_write_lsn; // numeric(25,0)
        private Guid _redo_start_fork_guid; // uniqueidentifier
        private decimal? _redo_start_lsn; // numeric(25,0)
        private Guid _redo_target_fork_guid; // uniqueidentifier
        private decimal? _redo_target_lsn; // numeric(25,0)
        private int? _size; // int
        private FileState _state; // tinyint
        private string _state_desc; // nvarchar(60)
        private File_Type _type; // tinyint
        private string _type_desc; // nvarchar(60)

        #endregion

        #region public properties

        public decimal? backup_lsn
        {
            get { return _backup_lsn; }
            //set {  _backup_lsn = value; }
        }
        public decimal? create_lsn
        {
            get { return _create_lsn; }
            //set {  _create_lsn = value; }
        }
        public int? data_space_id
        {
            get { return _data_space_id; }
            //set {  _data_space_id = value; }
        }
        public int? database_id
        {
            get { return _database_id; }
            //set {  _database_id = value; }
        }
        public Guid differential_base_guid
        {
            get { return _differential_base_guid; }
            //set {  _differential_base_guid = value; }
        }
        public decimal? differential_base_lsn
        {
            get { return _differential_base_lsn; }
            //set {  _differential_base_lsn = value; }
        }
        public DateTime? differential_base_time
        {
            get { return _differential_base_time; }
            //set {  _differential_base_time = value; }
        }
        public decimal? drop_lsn
        {
            get { return _drop_lsn; }
            //set {  _drop_lsn = value; }
        }
        public Guid file_guid
        {
            get { return _file_guid; }
            //set {  _file_guid = value; }
        }
        public int? file_id
        {
            get { return _file_id; }
            //set {  _file_id = value; }
        }
        public int? growth
        {
            get { return _growth; }
            //set {  _growth = value; }
        }
        public bool? is_media_read_only
        {
            get { return _is_media_read_only; }
            //set {  _is_media_read_only = value; }
        }
        public bool? is_name_reserved
        {
            get { return _is_name_reserved; }
            //set {  _is_name_reserved = value; }
        }
        public bool? is_percent_growth
        {
            get { return _is_percent_growth; }
            //set {  _is_percent_growth = value; }
        }
        public bool? is_read_only
        {
            get { return _is_read_only; }
            //set {  _is_read_only = value; }
        }
        public bool? is_sparse
        {
            get { return _is_sparse; }
            //set {  _is_sparse = value; }
        }
        public int? max_size
        {
            get { return _max_size; }
            //set {  _max_size = value; }
        }
        public string name
        {
            get { return _name; }
            //set {  _name = value; }
        }
        public string physical_name
        {
            get { return _physical_name; }
            //set {  _physical_name = value; }
        }
        public decimal? read_only_lsn
        {
            get { return _read_only_lsn; }
            //set {  _read_only_lsn = value; }
        }
        public decimal? read_write_lsn
        {
            get { return _read_write_lsn; }
            //set {  _read_write_lsn = value; }
        }
        public Guid redo_start_fork_guid
        {
            get { return _redo_start_fork_guid; }
            //set {  _redo_start_fork_guid = value; }
        }
        public decimal? redo_start_lsn
        {
            get { return _redo_start_lsn; }
            //set {  _redo_start_lsn = value; }
        }
        public Guid redo_target_fork_guid
        {
            get { return _redo_target_fork_guid; }
            //set {  _redo_target_fork_guid = value; }
        }
        public decimal? redo_target_lsn
        {
            get { return _redo_target_lsn; }
            //set {  _redo_target_lsn = value; }
        }
        public int? size
        {
            get { return _size; }
            //set {  _size = value; }
        }
        public FileState state
        {
            get { return _state; }
            //set {  _state = value; }
        }
        public string state_desc
        {
            get { return _state_desc; }
            //set {  _state_desc = value; }
        }
        public File_Type type
        {
            get { return _type; }
            //set {  _type = value; }
        }
        public string type_desc
        {
            get { return _type_desc; }
            //set {  _type_desc = value; }
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
                if (!rs.IsDBNull(0)) { _backup_lsn = rs.GetDecimal(0); }
                if (!rs.IsDBNull(1)) { _create_lsn = rs.GetDecimal(1); }
                if (!rs.IsDBNull(2)) { _data_space_id = rs.GetInt32(2); }
                if (!rs.IsDBNull(3)) { _database_id = rs.GetInt32(3); }
                if (!rs.IsDBNull(4)) { _differential_base_guid = rs.GetGuid(4); }
                if (!rs.IsDBNull(5)) { _differential_base_lsn = rs.GetDecimal(5); }
                if (!rs.IsDBNull(6)) { _differential_base_time = rs.GetDateTime(6); }
                if (!rs.IsDBNull(7)) { _drop_lsn = rs.GetDecimal(7); }
                if (!rs.IsDBNull(8)) { _file_guid = rs.GetGuid(8); }
                if (!rs.IsDBNull(9)) { _file_id = rs.GetInt32(9); }
                if (!rs.IsDBNull(10)) { _growth = rs.GetInt32(10); }
                if (!rs.IsDBNull(11)) { _is_media_read_only = rs.GetBoolean(11); }
                if (!rs.IsDBNull(12)) { _is_name_reserved = rs.GetBoolean(12); }
                if (!rs.IsDBNull(13)) { _is_percent_growth = rs.GetBoolean(13); }
                if (!rs.IsDBNull(14)) { _is_read_only = rs.GetBoolean(14); }
                if (!rs.IsDBNull(15)) { _is_sparse = rs.GetBoolean(15); }
                if (!rs.IsDBNull(16)) { _max_size = rs.GetInt32(16); }
                if (!rs.IsDBNull(17)) { _name = rs.GetString(17); }
                if (!rs.IsDBNull(18)) { _physical_name = rs.GetString(18); }
                if (!rs.IsDBNull(19)) { _read_only_lsn = rs.GetDecimal(19); }
                if (!rs.IsDBNull(20)) { _read_write_lsn = rs.GetDecimal(20); }
                if (!rs.IsDBNull(21)) { _redo_start_fork_guid = rs.GetGuid(21); }
                if (!rs.IsDBNull(22)) { _redo_start_lsn = rs.GetDecimal(22); }
                if (!rs.IsDBNull(23)) { _redo_target_fork_guid = rs.GetGuid(23); }
                if (!rs.IsDBNull(24)) { _redo_target_lsn = rs.GetDecimal(24); }
                if (!rs.IsDBNull(25)) { _size = rs.GetInt32(25); }
                if (!rs.IsDBNull(26)) { _state = (FileState)rs.GetByte(26); }
                if (!rs.IsDBNull(27)) { _state_desc = rs.GetString(27); }
                if (!rs.IsDBNull(28)) { _type = (File_Type)rs.GetByte(28); }
                if (!rs.IsDBNull(29)) { _type_desc = rs.GetString(29); }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DatabaseFile(SQLiteConnectionStringBuilder SQLConnSetting, SQLiteDataReader rs, Database owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            AddFromRecordSet(rs);
        }
        public DatabaseFile(SQLiteConnectionStringBuilder SQLConnSetting, int database_id, int file_id, Database owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;

            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "Select backup_lsn, create_lsn, data_space_id, database_id, differential_base_guid, differential_base_lsn, differential_base_time, drop_lsn, file_guid, file_id, growth, is_media_read_only, is_name_reserved, is_percent_growth, is_read_only, is_sparse, max_size, name, physical_name, read_only_lsn, read_write_lsn, redo_start_fork_guid, redo_start_lsn, redo_target_fork_guid, redo_target_lsn, size, state, state_desc, type, type_desc from sys.master_files where file_id=" + file_id + " AND database_id=" + database_id;
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
    public class DatabaseFiles : AbstractSQLObject
    {
        private List<DatabaseFile> _Items = new List<DatabaseFile>();
        [DisplayName("Database Files")]
        public DatabaseFile[] Items
        {
            get { return _Items.ToArray(); }
        }

        public string Description
        {
            get
            {
                if (Owner.ExtendedProperties != null)
                {
                    ExtendedProperty desc = Owner.ExtendedProperties.GetExtendedProperty(ExtendedProperty_Type.DATABASE_FILE, 0, 1);
                    if (desc != null)
                        return desc.value.ToString();
                    else
                        return "";
                }
                else
                    return "";
            }
        }

        private Database _Owner;
        [Browsable(false)]
        public Database Owner
        {
            get { return _Owner; }
        }

        private SQLiteConnectionStringBuilder SQLConnSet = new SQLiteConnectionStringBuilder();

        public DatabaseFiles(SQLiteConnectionStringBuilder SQLConnSetting, Database owner)
        {
            _Owner = owner;
            SQLConnSet = SQLConnSetting;
            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "Select backup_lsn, create_lsn, data_space_id, database_id, differential_base_guid, differential_base_lsn, differential_base_time, drop_lsn, file_guid, file_id, growth, is_media_read_only, is_name_reserved, is_percent_growth, is_read_only, is_sparse, max_size, name, physical_name, read_only_lsn, read_write_lsn, redo_start_fork_guid, redo_start_lsn, redo_target_fork_guid, redo_target_lsn, size, state, state_desc, type, type_desc from sys.master_files where database_id=" + Owner.database_id;
                    SQLiteDataReader rs = Com.ExecuteReader();
                    while (rs.Read())
                    {
                        _Items.Add(new DatabaseFile(SQLConnSet, rs, owner));
                    }
                    rs.Close();
                    Conn.Close();
                    rs.Dispose();
                }

            }
        }

        public DatabaseFile GetDatabaseFile(int? file_id)
        {
            foreach (DatabaseFile DF in _Items)
            {
                if (DF.file_id == file_id)
                    return DF;
            }
            return null;
        }

        public override string ToString()
        {
            if (_Items != null)
                return "[" + _Items.Count + "] Files";
            return base.ToString();
        }

        [Browsable(false)]
        public override string ObjectName
        {
            get { return "Databasefiles"; }
        }

        [Browsable(false)]
        public override string UniqueObjectName
        {
            get { return Owner.ObjectName + "Databasefiles" + SQLServer.ObjectNameSeperator; }
        }
    }
}
