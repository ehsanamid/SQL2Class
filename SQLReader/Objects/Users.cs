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
    public class User : AbstractSQLObject
    {
        #region Private Fields

        private Int16 _uid;
        private Int16 _status;
        private string _name;
        private byte[] _sid;
        private byte[] _roles;
        private DateTime _createdate;
        private DateTime _updatedate;
        private Int16 _altuid;
        private byte[] _password;
        private Int16 _gid;
        private string _environ;
        private int _hasdbaccess;
        private int _islogin;
        private int _isntname;
        private int _isntgroup;
        private int _isntuser;
        private int _issqluser;
        private int _isaliased;
        private int _issqlrole;
        private int _isapprole;

        #endregion

        #region public properties

        [Description("User ID, unique in this database.\n1 = Database owner\nOverflows or returns NULL if the number of users and roles exceeds 32,767. For more information, see Querying the SQL Server System Catalog.")]
        public Int16 uid
        {
            get { return _uid; /*smallint */ }
        }

        [Description("Reserved for SQL Server internal use only. Future compatibility is not guaranteed.")]
        public Int16 status
        {
            get { return _status; /*smallint */ }
        }

        [Description("User name or group name, unique in this database.")]
        public string name
        {
            get { return _name; /*sysname */ }
        }

        [Description("Security identifier for this entry.")]
        public byte[] sid
        {
            get { return _sid; /*varbinary(85) */ }
        }

        [Description("Reserved for SQL Server internal use only. Future compatibility is not guaranteed.")]
        public byte[] roles
        {
            get { return _roles; /*varbinary(2048) */ }
        }

        [Description("Date the account was added.")]
        public DateTime createdate
        {
            get { return _createdate; /*datetime */ }
        }

        [Description("Date the account was last changed.")]
        public DateTime updatedate
        {
            get { return _updatedate; /*datetime */ }
        }

        [Description("Reserved for SQL Server internal use only. Future compatibility is not guaranteed. Overflows or returns NULL if the number of users and roles exceeds 32,767. For more information, see Querying the SQL Server System Catalog.")]
        public Int16 altuid
        {
            get { return _altuid; /*smallint */ }
        }

        [Description("Reserved for SQL Server internal use only. Future compatibility is not guaranteed.")]
        public byte[] password
        {
            get { return _password; /*varbinary(256) */ }
        }

        [Description("Group ID to which this user belongs. If uid is the same as gid, this entry defines a group. Overflows or returns NULL if the combined number of groups and users exceeds 32,767. For more information, see Querying the SQL Server System Catalog.")]
        public Int16 gid
        {
            get { return _gid; /*smallint */ }
        }

        [Description("Reserved.")]
        public string environ
        {
            get { return _environ; /*varchar(255) */ }
        }

        [Description("1 = Account has database access.")]
        public int hasdbaccess
        {
            get { return _hasdbaccess; /*int */ }
        }

        [Description("1 = Account is a Windows group, Windows user, or SQL Server user with a login account.")]
        public int islogin
        {
            get { return _islogin; /*int */ }
        }

        [Description("1 = Account is a Windows group or Windows user.")]
        public int isntname
        {
            get { return _isntname; /*int */ }
        }

        [Description("1 = Account is a Windows group.")]
        public int isntgroup
        {
            get { return _isntgroup; /*int */ }
        }

        [Description("1 = Account is a Windows user.")]
        public int isntuser
        {
            get { return _isntuser; /*int */ }
        }

        [Description("1 = Account is a SQL Server user.")]
        public int issqluser
        {
            get { return _issqluser; /*int */ }
        }

        [Description("1 = Account is aliased to another user.")]
        public int isaliased
        {
            get { return _isaliased; /*int */ }
        }

        [Description("1 = Account is a SQL Server role.")]
        public int issqlrole
        {
            get { return _issqlrole; /*int */ }
        }

        [Description("1 = Account is an application role.")]
        public int isapprole
        {
            get { return _isapprole; /*int */ }
        }



        #endregion

        public UserTypes UserType
        {
            get
            {
                if (_isntgroup == 1)
                    return UserTypes.NTGroup;
                else if (_isntuser == 1)
                    return UserTypes.NTUser;
                else if (_issqlrole == 1)
                    return UserTypes.SQLRole;
                else
                    return UserTypes.SQLUser;
            }
        }

        private Users _Owner;
        [Browsable(false)]
        public Users Owner
        {
            get { return _Owner; }
        }

        private SQLiteConnectionStringBuilder SQLConnSet = new SQLiteConnectionStringBuilder();

        private void AddFromRecordSet(SQLiteDataReader rs)
        {
            try
            {
                if (!rs.IsDBNull(0)) { _uid = rs.GetInt16(0); }
                if (!rs.IsDBNull(1)) { _status = rs.GetInt16(1); }
                if (!rs.IsDBNull(2)) { _name = rs.GetString(2); }
                //if (!rs.IsDBNull(3)) { _sid = rs.GetBytes(3); }
                //if (!rs.IsDBNull(4)) { _roles = rs.GetBytes(4); }
                if (!rs.IsDBNull(5)) { _createdate = rs.GetDateTime(5); }
                if (!rs.IsDBNull(6)) { _updatedate = rs.GetDateTime(6); }
                if (!rs.IsDBNull(7)) { _altuid = rs.GetInt16(7); }
                //if (!rs.IsDBNull(8)) { _password = rs.GetBytes(8); }
                if (!rs.IsDBNull(9)) { _gid = rs.GetInt16(9); }
                if (!rs.IsDBNull(10)) { _environ = rs.GetString(10); }
                if (!rs.IsDBNull(11)) { _hasdbaccess = rs.GetInt32(11); }
                if (!rs.IsDBNull(12)) { _islogin = rs.GetInt32(12); }
                if (!rs.IsDBNull(13)) { _isntname = rs.GetInt32(13); }
                if (!rs.IsDBNull(14)) { _isntgroup = rs.GetInt32(14); }
                if (!rs.IsDBNull(15)) { _isntuser = rs.GetInt32(15); }
                if (!rs.IsDBNull(16)) { _issqluser = rs.GetInt32(16); }
                if (!rs.IsDBNull(17)) { _isaliased = rs.GetInt32(17); }
                if (!rs.IsDBNull(18)) { _issqlrole = rs.GetInt32(18); }
                if (!rs.IsDBNull(19)) { _isapprole = rs.GetInt32(19); }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User(SQLiteConnectionStringBuilder SQLConnSetting, SQLiteDataReader rs, Users owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            AddFromRecordSet(rs);
        }
        public User(SQLiteConnectionStringBuilder SQLConnSetting, Int16 User_id, Users owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;

            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "SELECT uid, status, name, sid, roles, createdate, updatedate, altuid, password, gid, environ, hasdbaccess, islogin, isntname, isntgroup, isntuser, issqluser, isaliased, issqlrole, isapprole FROM sys.sysusers WHERE uid=" + User_id;
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
    public class Users : AbstractSQLObject
    {
        private List<User> _Items = new List<User>();
        [DisplayName("Users")]
        public User[] Items
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

        public Users(SQLiteConnectionStringBuilder SQLConnSetting, Database owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "SELECT uid, status, name, sid, roles, createdate, updatedate, altuid, password, gid, environ, hasdbaccess, islogin, isntname, isntgroup, isntuser, issqluser, isaliased, issqlrole, isapprole FROM sys.sysusers ORDER BY name";
                    SQLiteDataReader rs = Com.ExecuteReader();
                    while (rs.Read())
                    {
                        _Items.Add(new User(SQLConnSetting, rs, this));
                    }
                    rs.Close();
                    Conn.Close();
                    rs.Dispose();
                }

            }
        }

        public User GetItem(int uid)
        {
            foreach (User U in _Items)
            {
                if (U.uid == uid)
                    return U;
            }
            return null;
        }

        public bool HasUserRoles
        {
            get
            {
                bool rtnboolval = false;
                foreach (User u in this.Items)
                {
                    if (u.issqlrole == 1)
                    {
                        rtnboolval = true;
                        break;
                    }
                }
                return rtnboolval;
            }
        }

        public bool HasNTGroups
        {
            get
            {
                bool rtnboolval = false;
                foreach (User u in this.Items)
                {
                    if (u.isntgroup == 1)
                    {
                        rtnboolval = true;
                        break;
                    }
                }
                return rtnboolval;
            }
        }

        public bool HasNTUsers
        {
            get
            {
                bool rtnboolval = false;
                foreach (User u in this.Items)
                {
                    if (u.isntuser == 1)
                    {
                        rtnboolval = true;
                        break;
                    }
                }
                return rtnboolval;
            }
        }

        public bool HasUser
        {
            get
            {
                bool rtnboolval = false;
                foreach (User u in this.Items)
                {
                    if (u.issqluser == 1)
                    {
                        rtnboolval = true;
                        break;
                    }
                }
                return rtnboolval;
            }
        }

        public override string ToString()
        {
            if (_Items != null)
                return "[" + _Items.Count + "] Users";
            return base.ToString();
        }

        [Browsable(false)]
        public override string ObjectName
        {
            get { return "Users"; }
        }

        [Browsable(false)]
        public override string UniqueObjectName
        {
            get { return Owner.ObjectName + "Users" + SQLServer.ObjectNameSeperator; }
        }
    }
}


