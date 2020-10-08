using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using System.ComponentModel;
using System.Data.SQLite;

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
    [TypeConverter(typeof(ExpandableObject)), Description("Contains entries for each view, rule, default, trigger, CHECK constraint, DEFAULT constraint, and stored procedure within the database. The text column contains the original SQL definition statements. These statements are limited to a maximum size of 4 megabytes (MB). ")]
    public class Comment : AbstractSQLObject
    {
        #region Private Fields

        private int _id;
        private Int16 _number;
        private Int16 _colid;
        private Int16 _status;
        //private byte _ctext;
        private Int16 _texttype;
        private Int16 _language;
        private bool _encrypted;
        private string _text;

        #endregion

        #region public properties

        [Description("Object ID to which this text applies")]
        public int id
        {
            get { return _id; }
            //set {  _id = value; }
        }

        [Description("Number within procedure grouping, if grouped.\n0 = Entries are not procedures")]
        public Int16 number
        {
            get { return _number; }
            //set {  _number = value; }
        }

        [Description("Row sequence number for object definitions that are longer than 4,000 characters")]
        public Int16 colid
        {
            get { return _colid; }
            //set {  _colid = value; }
        }

        [Description("Reserved for SQL Server internal use only. Future compatibility is not guaranteed.")]
        public Int16 status
        {
            get { return _status; }
            //set {  _status = value; }
        }

        [Description("0 = User-supplied comment\n1 = System-supplied comment\n4 = Encrypted commen")]
        public Int16 texttype
        {
            get { return _texttype; }
            //set {  _texttype = value; }
        }

        [Description("Reserved for SQL Server internal use only. Future compatibility is not guaranteed.")]
        public Int16 language
        {
            get { return _language; }
            //set {  _language = value; }
        }

        [Description("Indicates whether the procedure definition is obfuscated.\n0 = Not obfuscated\n1 = Obfuscated\nImportant: \nTo obfuscate stored procedure definitions, use CREATE PROCEDURE with the ENCRYPTION keyword.")]
        public bool encrypted
        {
            get { return _encrypted; }
            //set {  _encrypted = value; }
        }

        [Description("Actual text of the SQL definition statement.\nSQL Server 2005 differs from SQL Server 2000 in the way it decodes and stores SQL expressions in the catalog metadata.\nThe semantics of the decoded expression are equivalent to the original text; however, there are no syntactic guarantees.\nFor example, white spaces are removed from the decoded expression.")]
        public string text
        {
            get { return _text; }
            //set {  _text = value; }
        }

        #endregion

        private Comments _Owner;
        [Browsable(false)]
        public Comments Owner
        {
            get { return _Owner; }
        }

        private SQLiteConnectionStringBuilder SQLConnSet = new SQLiteConnectionStringBuilder();

        private void AddFromRecordSet(SQLiteDataReader rs)
        {
            try
            {
                if (!rs.IsDBNull(0)) { _id = rs.GetInt32(0); }
                if (!rs.IsDBNull(1)) { _number = rs.GetInt16(1); }
                if (!rs.IsDBNull(2)) { _colid = rs.GetInt16(2); }
                if (!rs.IsDBNull(3)) { _status = rs.GetInt16(3); }
                if (!rs.IsDBNull(5)) { _texttype = rs.GetInt16(5); }
                if (!rs.IsDBNull(6)) { _language = rs.GetInt16(6); }
                if (!rs.IsDBNull(7)) { _encrypted = rs.GetBoolean(7); }
                if (!rs.IsDBNull(8)) { _text = rs.GetString(8); }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Comment(SQLiteConnectionStringBuilder SQLConnSetting, SQLiteDataReader rs, Comments owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            AddFromRecordSet(rs);
        }
        public Comment(SQLiteConnectionStringBuilder SQLConnSetting, int Comment_ID, Comments owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;

            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "SELECT id, number, colid, status, ctext, texttype, language, encrypted, text FROM sys.syscomments WHERE id=" + Comment_ID + " ORDER BY Comment_id";
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
            get { return "[" + id + "," + number + "]"; }
        }

        [Browsable(false)]
        public override string UniqueObjectName
        {
            get { return Owner.ObjectName + "[" + id + "," + number + "]" + SQLServer.ObjectNameSeperator; }
        }
    }

    [TypeConverter(typeof(ExpandableObject))]
    public class Comments : AbstractSQLObject
    {
        private List<Comment> _Items = new List<Comment>();
        [DisplayName("Comments")]
        public Comment[] Items
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

        public Comments(SQLiteConnectionStringBuilder SQLConnSetting, Database owner)
        {
            _Owner = owner;
            SQLConnSet.ConnectionString = SQLConnSetting.ConnectionString;
            using (SQLiteConnection Conn = new SQLiteConnection(SQLConnSet.ConnectionString))
            {
                Conn.Open();
                using (SQLiteCommand Com = Conn.CreateCommand())
                {
                    Com.CommandTimeout = 10;
                    Com.CommandText = "SELECT id, number, colid, status, ctext, texttype, language, encrypted, text FROM sys.syscomments --ORDER BY Comment_id";
                    SQLiteDataReader rs = Com.ExecuteReader();
                    while (rs.Read())
                    {
                        _Items.Add(new Comment(SQLConnSetting, rs, this));
                    }
                    rs.Close();
                    Conn.Close();
                    rs.Dispose();
                }

            }
        }

        public Comment GetItem(int id)
        {
            return GetItem(id, 0);
        }
        public Comment GetItem(int id, short number)
        {
            foreach (Comment C in _Items)
            {
                if (C.id == id && C.number == number)
                    return C;
            }
            return null;
        }

        public Comment[] GetItems(int id)
        {
            List<Comment> CO = new List<Comment>();
            foreach (Comment C in _Items)
            {
                if (C.id == id)
                    CO.Add(C);
            }
            return CO.ToArray();
        }

        public override string ToString()
        {
            if (_Items != null)
                return "[" + _Items.Count + "] Comments";
            return base.ToString();
        }

        [Browsable(false)]
        public override string ObjectName
        {
            get { return "Comments"; }
        }

        [Browsable(false)]
        public override string UniqueObjectName
        {
            get { return Owner.ObjectName + "Comments" + SQLServer.ObjectNameSeperator; }
        }
    }
}
