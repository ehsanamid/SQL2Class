using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;

using System.Drawing;

namespace DCS
{
    public class SQLiteTableColumn
    {
        public SQLiteTable m_parent;
        private List<ReferenceInfo> _referncedtablelist;
        public List<ReferenceInfo> ReferncedTablelist
        {
            get
            {
                if (_referncedtablelist == null)
                {
                    _referncedtablelist = new List<ReferenceInfo>();

                }
                return _referncedtablelist;
            }
            set
            {
                _referncedtablelist = value;
            }
        }
        private string _columnname;
        public string ColumnName
        {
            get
            {
                return _columnname;
            }
            set
            {
                _columnname = value;
            }
        }
        private string _columntype;
        public string ColumnType
        {
            get
            {
                return _columntype;
            }
            set
            {
                
                _columntype = value;

                //_nettype = GetTypeFromName(_columntype);
            }
        }
        private string _defaultvalue;
        public string DefaultValue
        {
            get
            {
                return _defaultvalue;
            }
            set
            {
                _defaultvalue = value;
            }
        }
        private bool _pk;
        public bool pk
        {
            get
            {
                return _pk;
            }
            set
            {
                _pk = value;
            }
        }

        public bool IsPrimaryKey
        {
            get
            {
                return _pk;
            }
        }

        private bool _isforeignkey;
        public bool IsForeignKey
        {
            get
            {
                return _isforeignkey;
            }
            set
            {
                _isforeignkey = value;
            }
        }

        private bool isreference = false;
        public bool IsReference
        {
            get
            {

                return isreference;
            }
            set
            {
                isreference = value;
            }
        }
        //private Type _nettype;
        //public Type NetType
        //{
        //    get
        //    {
        //        return _nettype;
        //    }
        //}
        //private string _referencetable = "";
        //public string Referencetable
        //{
        //    get
        //    {
        //        return _referencetable;
        //    }
        //    set
        //    {
        //        _referencetable = value;
        //    }
        //}

        //private string _referencecolumnname = "";
        //public string ReferenceColumnName
        //{
        //    get
        //    {
        //        return _referencecolumnname;
        //    }
        //    set
        //    {
        //        _referencecolumnname = value;
        //    }
        //}
        public SQLiteTableColumn()
        {
           

        }

        public void addReference(ReferenceInfo _refenceinfo)
        {
            //_isforeignkey = true;
            ReferncedTablelist.Add(_refenceinfo);
        }
        public Type NetType
        {

            get
            {
                string delimStr = "()";
                char[] delimiter = delimStr.ToCharArray();
                string words = _columntype;
                string[] split = null;


                split = words.Split(delimiter);
                split[0] = split[0].Trim();
                switch (split[0].ToLower())
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
                    case "float": return typeof(System.Single);
                    case "geography": return typeof(System.Object);
                    case "geometry": return typeof(System.Object);
                    case "hierarchyid": return typeof(System.Object);
                    case "image": return typeof(System.Object);
                    case "integer": return typeof(System.Int64);
                    case "int": return typeof(System.Int32);
                    case "memo": return typeof(System.String);
                    case "money": return typeof(System.Decimal);
                    case "nchar": return typeof(System.String);
                    case "ntext": return typeof(System.String);
                    case "numeric": return typeof(System.Decimal);
                    case "nvarchar": return typeof(System.String);
                    case "real": return typeof(System.Double);
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
                    case "color": return typeof(Color);
                    case "poutype": return typeof(POUTYPE);
                    case "pouexecutiontype": return typeof(POUEXECUTIONTYPE);
                    case "poulanguagetype": return typeof(PouLanguageType);
                    case "blob": return typeof(byte[]);
                    default: return null;
                }
            }
        }

        public Type NetTypeColor
        {

            get
            {
                string delimStr = "()";
                char[] delimiter = delimStr.ToCharArray();
                string words = _columntype;
                string[] split = null;


                split = words.Split(delimiter);
                split[0] = split[0].Trim();
                switch (split[0].ToLower())
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
                    case "float": return typeof(System.Single);
                    case "geography": return typeof(System.Object);
                    case "geometry": return typeof(System.Object);
                    case "hierarchyid": return typeof(System.Object);
                    case "image": return typeof(System.Object);
                    case "integer": return typeof(System.Int64);
                    case "int": return typeof(System.Int32);
                    case "memo": return typeof(System.String);
                    case "money": return typeof(System.Decimal);
                    case "nchar": return typeof(System.String);
                    case "ntext": return typeof(System.String);
                    case "numeric": return typeof(System.Decimal);
                    case "nvarchar": return typeof(System.String);
                    case "real": return typeof(System.Double);
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
                    case "color": return typeof(System.Int32);
                    case "poutype": return typeof(System.Int32);
                    case "pouexecutiontype": return typeof(System.Int32);
                    case "poulanguagetype": return typeof(System.Int32);
                    case "blob": return typeof(byte[]);
                    default: return null;
                }
            }
        }


        public string SQLDBType
        {

            get
            {
                string delimStr = "()";
                char[] delimiter = delimStr.ToCharArray();
                string words = _columntype;
                string[] split = null;


                //DbType Sqltype;
                //Sqltype = DbType.

                split = words.Split(delimiter);
                split[0] = split[0].Trim();
                switch (split[0].ToLower())
                {
                    case "bigint": return "DbType.Int64";
                    case "binary": return "DbType.Binary";
                    case "bit": return "DbType.Boolean";
                    case "char": return "DbType.String";
                    case "date": return "DbType.DateTime";
                    case "datetime": return "DbType.DateTime";
                    case "datetime2": return "DbType.DateTime";
                    case "datetimeoffset": return "DbType.DateTimeOffset";
                    case "decimal": return "DbType.Decimal";
                    case "float": return "DbType.Single";
                    case "geography": return "DbType.Object";
                    case "geometry": return "DbType.Object";
                    case "hierarchyid": return "DbType.Object";
                    case "image": return "DbType.Object";
                    case "integer": return "DbType.Int64";
                    case "int": return "DbType.Int32";
                    case "memo": return "DbType.String";
                    case "money": return "DbType.Decimal";
                    case "nchar": return "DbType.String";
                    case "ntext": return "DbType.String";
                    case "numeric": return "DbType.Decimal";
                    case "nvarchar": return "DbType.String";
                    case "real": return "DbType.Double";
                    case "smalldatetime": return "DbType.DateTime";
                    case "smallint": return "DbType.Int16";
                    case "smallmoney": return "DbType.Decimal";
                    case "sql_variant": return "DbType.Object";
                    case "text": return "DbType.String";
                    case "time": return "DbType.TimeSpan";
                    case "timestamp": return "DbType.Object";
                    case "tinyint": return "DbType.Byte";
                    case "varbinary": return "DbType.String";
                    case "varchar": return "DbType.String";
                    case "xml": return "DbType.String";
                    case "color": return "DbType.Int32";
                    case "poutype": return "DbType.Int32";
                    case "pouexecutiontype": return "DbType.Int32";
                    case "poulanguagetype": return "DbType.Int32";
                    case "blob": return "DbType.Binary";
                    default: return "";
                }
            }
        }
    }
    
        
}
