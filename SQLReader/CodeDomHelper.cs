

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Collections;


namespace DCS
{
    public static class CodeDomGenerator
    {
        public static string CSPreFieldname = "_";
        public static string CSPostFieldname = "";

        public static string VBPreFieldname = "fld_";
        public static string VBPostFieldname = "";

        public static bool AddComments = true;
        public static bool MapDescription = true;
        public static bool TryCorrectPropertyName = true;
        public static bool TryCorrectDisplayName = true;
        public static bool UseDatabaseNamespace = true;
        public static bool UseDatabaseSchemaNamespace = true;
        public static bool CreateRefObjects = true;
        public static string UseNamespace = "";
        public static string[] NamespaceImports;
        public static string[] ClassBaseTypes;

        public static string FieldModifier = "Private";
        public static string PropertyModifier = "Public";
        
        [Description("Returns the modifer for the selected property")]
        internal static MemberAttributes GetModifier(string Modifier)
        {
            if (Modifier == "Private")
                return MemberAttributes.Private;
            else if (Modifier == "Internal")
                return MemberAttributes.Assembly;
            else if (Modifier == "Protected")
                return MemberAttributes.Family;
            else if (Modifier == "Protected Internal")
                return MemberAttributes.FamilyAndAssembly;
            else if (Modifier == "Public")
                return MemberAttributes.Public;
            else
                return MemberAttributes.Private;
        }

        internal static string SqlDataReaderGetMethod(Type type)
        {
            if (type == typeof(System.Boolean))
                return "GetBoolean";
            else if (type == typeof(System.Byte))
            {
                return "GetByte";
            }
            else if (type == typeof(System.Byte).MakeArrayType())
            {
                return "GetBytes";
            }
            else if (type == typeof(System.Char))
                return "GetChar";
            else if (type == typeof(System.DateTime))
                return "GetDateTime";
            else if (type == typeof(System.Decimal))
                return "GetDecimal";
            else if (type == typeof(System.Double))
                return "GetDouble";
            else if (type == typeof(System.Guid))
                return "GetGuid";
            else if (type == typeof(System.Int16))
                return "GetInt16";
            else if (type == typeof(System.Int32))
                return "GetInt32";
            else if (type == typeof(System.Int64))
                return "GetInt64";
            else if (type == typeof(System.Object))
                return "GetValue";
            else if (type == typeof(System.Single))
                return "GetFloat";
            else if (type == typeof(System.String))
                return "GetString";
            else
                return "GetValue";
        }

        internal static string SqlDbTypeConst(string type)
        {
            switch (type)
            {
                case "bigint": return "SqlDbType.BigInt";
                case "binary": return "SqlDbType.Binary";
                case "bit": return "SqlDbType.Bit";
                case "char": return "SqlDbType.Char";
                case "date": return "SqlDbType.Date";
                case "datetime": return "SqlDbType.DateTime";
                case "datetime2": return "SqlDbType.DateTime2";
                case "datetimeoffset": return "SqlDbType.DateTimeOffset";
                case "decimal": return "SqlDbType.Decimal";
                case "float": return "SqlDbType.Float";
                case "image": return "SqlDbType.Image";
                case "int": return "SqlDbType.Int";
                case "money": return "SqlDbType.Money";
                case "nchar": return "SqlDbType.NChar";
                case "ntext": return "SqlDbType.NText";
                case "nvarchar": return "SqlDbType.NVarChar";
                case "real": return "SqlDbType.Real";
                case "smalldatetime": return "SqlDbType.SmallDateTime";
                case "smallint": return "SqlDbType.SmallInt";
                case "smallmoney": return "SqlDbType.SmallMoney";
                case "structured": return "SqlDbType.Structured";
                case "text": return "SqlDbType.Text";
                case "time": return "SqlDbType.Time";
                case "timestamp": return "SqlDbType.Timestamp";
                case "tinyint": return "SqlDbType.TinyInt";
                case "udt": return "SqlDbType.Udt";
                case "uniqueidentifier": return "SqlDbType.UniqueIdentifier";
                case "varbinary": return "SqlDbType.VarBinary";
                case "varchar": return "SqlDbType.VarChar";
                case "variant": return "SqlDbType.Variant";
                case "xml": return "SqlDbType.Xml";
                default: return "";
            }
        }

        internal static string TryCorrectName(string value)
        {
            if (!TryCorrectPropertyName)
                return value;
            else
            {
                Regex Rx = new Regex("([a-z0-9])([A-Z0-9])");
                return Rx.Replace(value,"$1 $2");
            }
        }
        internal static string TryCorrectNameNoWhitespace(string value)
        {
            if (!TryCorrectPropertyName)
                return value.Replace(" ","");
            else
            {
                Regex Rx = new Regex("([a-z0-9])([A-Z0-9])");
                return Rx.Replace(value, "$1 $2").Replace(" ","");
            }
        }
        internal static string GetNameSpace(string value,string schemavalue)
        {
            if (!UseDatabaseNamespace)
                return UseNamespace;
            else
            {
                if (UseDatabaseSchemaNamespace)
                    return value + "." + schemavalue;
                else
                    return value;
            }
        }
        internal static string GetFieldName(string value, CodeDomProvider provider)
        {
            if (provider is VBCodeProvider)
                return VBPreFieldname + value.Replace(" ", "_") + VBPostFieldname;
            else
                return CSPreFieldname + value.Replace(" ", "_") + CSPostFieldname;
        }
        //internal static string GetFieldName(Column column, CodeDomProvider provider)
        //{
        //    return GetFieldName(column.name,provider);
        //}

        private static string CodeDomCorrectWhileLoop(string value, CodeDomProvider cdp)
        {
            if (!(cdp is CSharpCodeProvider))
                return value;
            else
            {
                if (!value.Contains("for ("))
                    return value;
                else
                {
                    Regex Rx = new Regex(@"\bfor\b \((.*?);\s(.*?)\;(.*?)\)", RegexOptions.Singleline);
                    return Rx.Replace(value, "while($2)");
                }
            }
        }

        internal static string CodeDomCorrector(string data, CodeDomProvider cdp)
        {
            string som = data;
            som = CodeDomCorrectWhileLoop(som, cdp);
            if (cdp is CSharpCodeProvider)
            {
                som = som.Replace(Environment.NewLine + "\t\t\t\t&&", @"&&");
                som = som.Replace(Environment.NewLine + "\t\t\t\t\t&&", @"&&");
                som = som.Replace(Environment.NewLine + "\t\t\t\t\t\t&&", @"&&");
                som = som.Replace(Environment.NewLine + "\t\t\t\t\t\t\t&&", @"&&");
                som = som.Replace(Environment.NewLine + "\t\t\t\t\t\t\t\t&&", @"&&");
                som = som.Replace(Environment.NewLine + "\t\t\t\t\t\t\t\t\t&&", @"&&");
                som = som.Replace("\" +" + Environment.NewLine + "\t\t\"", "");
                som = som.Replace("\" +" + Environment.NewLine + "\t\t\t\"", "");
                som = som.Replace("\" +" + Environment.NewLine + "\t\t\t\t\"", "");
                som = som.Replace("\" +" + Environment.NewLine + "\t\t\t\t\t\t\"", "");
                som = som.Replace("\" +" + Environment.NewLine + "\t\t\t\t\t\t\t\"", "");
                som = som.Replace("\" +" + Environment.NewLine + "\t\t\t\t\t\t\t\t\"", "");
                som = som.Replace(Environment.NewLine + "\t\t+", "+");
                som = som.Replace(Environment.NewLine + "\t\t\t+", "+");
                som = som.Replace(Environment.NewLine + "\t\t\t\t+", "+");
                som = som.Replace(Environment.NewLine + "\t\t\t\t\t\t+", "+");
                som = som.Replace(Environment.NewLine + "\t\t\t\t\t\t\t+", "+");
                som = som.Replace(Environment.NewLine + "\t\t\t\t\t\t\t\t+", "+");
            }
            else if (cdp is VBCodeProvider)
            {
                som = som.Replace("\"& _ " + Environment.NewLine + "\t\t\"", "");
                som = som.Replace("\"& _ " + Environment.NewLine + "\t\t\t\"", "");
                som = som.Replace("\"& _ " + Environment.NewLine + "\t\t\t\t\"", "");
                som = som.Replace("\"& _ " + Environment.NewLine + "\t\t\t\t\t\t\"", "");
                som = som.Replace("\"& _ " + Environment.NewLine + "\t\t\t\t\t\t\t\"", "");
                som = som.Replace("\"& _ " + Environment.NewLine + "\t\t\t\t\t\t\t\t\"", "");
                som = som.Replace(" _" + Environment.NewLine + "\t\t+", "+");
                som = som.Replace(" _" + Environment.NewLine + "\t\t\t+", "+");
                som = som.Replace(" _" + Environment.NewLine + "\t\t\t\t+", "+");
                som = som.Replace(" _" + Environment.NewLine + "\t\t\t\t\t\t+", "+");
                som = som.Replace(" _" + Environment.NewLine + "\t\t\t\t\t\t\t+", "+");
                som = som.Replace(" _" + Environment.NewLine + "\t\t\t\t\t\t\t\t+", "+");
                som = som.Replace(" _" + Environment.NewLine + "\t\tAndAlso ", " AndAlso ");
                som = som.Replace(" _" + Environment.NewLine + "\t\t\tAndAlso ", " AndAlso ");
                som = som.Replace(" _" + Environment.NewLine + "\t\t\t\tAndAlso ", " AndAlso ");
                som = som.Replace(" _" + Environment.NewLine + "\t\t\t\t\t\tAndAlso ", " AndAlso ");
                som = som.Replace(" _" + Environment.NewLine + "\t\t\t\t\t\t\tAndAlso ", " AndAlso ");
                som = som.Replace(" _" + Environment.NewLine + "\t\t\t\t\t\t\t\tAndAlso ", " AndAlso ");
                som = som.Replace(" _" + Environment.NewLine + "\t\tOrElse ", " OrElse ");
                som = som.Replace(" _" + Environment.NewLine + "\t\t\tOrElse ", " OrElse ");
                som = som.Replace(" _" + Environment.NewLine + "\t\t\t\tOrElse ", " OrElse ");
                som = som.Replace(" _" + Environment.NewLine + "\t\t\t\t\t\tOrElse ", " OrElse ");
                som = som.Replace(" _" + Environment.NewLine + "\t\t\t\t\t\t\tOrElse ", " OrElse ");
                som = som.Replace(" _" + Environment.NewLine + "\t\t\t\t\t\t\t\tOrElse ", " OrElse ");
                som = som.Replace(" _" + Environment.NewLine + "\t\tOr ", " Or ");
                som = som.Replace(" _" + Environment.NewLine + "\t\t\tOr ", " Or ");
                som = som.Replace(" _" + Environment.NewLine + "\t\t\t\tOr ", " Or ");
                som = som.Replace(" _" + Environment.NewLine + "\t\t\t\t\t\tOr ", " Or ");
                som = som.Replace(" _" + Environment.NewLine + "\t\t\t\t\t\t\tOr ", " Or ");
                som = som.Replace(" _" + Environment.NewLine + "\t\t\t\t\t\t\t\tOr ", " Or ");
                som = som.Replace(" _" + Environment.NewLine + "\t\tAnd ", " And ");
                som = som.Replace(" _" + Environment.NewLine + "\t\t\tAnd ", " And ");
                som = som.Replace(" _" + Environment.NewLine + "\t\t\t\tAnd ", " And ");
                som = som.Replace(" _" + Environment.NewLine + "\t\t\t\t\t\tAnd ", " And ");
                som = som.Replace(" _" + Environment.NewLine + "\t\t\t\t\t\t\tAnd ", " And ");
                som = som.Replace(" _" + Environment.NewLine + "\t\t\t\t\t\t\t\tAnd ", " And ");
            }
            if (cdp is CSharpCodeProvider)
                return som.Replace("sealed abstract", "static").Replace("abstract sealed", "static");
            else if (cdp is VBCodeProvider)
                return som.Replace("NotInheritable MustInherit", "Shared").Replace("MustInherit NotInheritable", "Shared");
            else
                return som;
        }
    }

    //Class that Contains the namespace of the file. 
    public class NameSpaceDatabaseCreate : CodeNamespace
    {
        public NameSpaceDatabaseCreate(SQLiteTable _sqlitetable, CodeDomProvider provider)
        {
            //this.Name = CodeDomGenerator.GetNameSpace(SQLObject.Owner.Owner.ObjectName,SQLObject.Schema.ObjectName);
            this.Name = _sqlitetable.tableName;
            if (CodeDomGenerator.NamespaceImports != null)
            {
                foreach (string NameSpace in CodeDomGenerator.NamespaceImports)
                    this.Imports.Add(new CodeNamespaceImport(NameSpace));
            }
        }
        
    }


    //Class that Contains the Class reflection of the table.
    public class ClassTableCreate : CodeTypeDeclaration
    {
        public ClassTableCreate(SQLiteTable _sqlitetable, CodeDomProvider provider)
	    {
            this.IsClass = true;
            this.Name = _sqlitetable.tableName;
            if (CodeDomGenerator.MapDescription && !string.IsNullOrEmpty(_sqlitetable.Description))
                this.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression(_sqlitetable.Description))));
            if (CodeDomGenerator.ClassBaseTypes != null)
            {
                foreach (string BaseType in CodeDomGenerator.ClassBaseTypes)
                    this.BaseTypes.Add(BaseType);
            }
	    }
    }

    

    ////Class that represents the SELECT Field in the class.
    //public class FieldSQLSelectString : CodeMemberField
    //{
    //    public FieldSQLSelectString(Table SQLObject, CodeDomProvider provider)
    //    {
    //        string selstr = "SELECT ";
    //        foreach (Column c in SQLObject.Columns.Items)
    //            selstr += "[" + c.name + "], ";
    //        selstr = selstr.Substring(0, selstr.Length - 2) + " FROM [" + SQLObject.Schema.name + "].[" + SQLObject.name + "] ";
    //        if (SQLObject.HasPrimaryKey)
    //        {
    //            selstr += "WHERE ";
    //            foreach (Column c in SQLObject.Columns.Items)
    //            {
    //                if (c.IsPrimaryKey)
    //                    selstr += "[" + c.name + "]=@" + c.name + " AND ";
    //            }
    //            selstr = selstr.Substring(0, selstr.Length - 4);
    //        }
    //        else if (SQLObject.HasUniqueIndex)
    //        {
    //            Index I = null;
    //            foreach (Index i in SQLObject.Indexes.Items)
    //            {
    //                if (i.is_unique)
    //                    I = i;
    //            }
    //            selstr += "WHERE ";
    //            foreach (IndexColumn ic in I.IndexColumns.Items)
    //                selstr += "[" + ic.Column.name + "]=@" + ic.Column.name + " AND ";
    //            selstr = selstr.Substring(0, selstr.Length - 4);   
    //        }
    //        this.Attributes = MemberAttributes.Assembly | MemberAttributes.Static;
    //        this.Type = new CodeTypeReference(typeof(string));
    //        this.Name = CodeDomGenerator.GetFieldName("SQL_Select", provider);
    //        if(SQLObject.HasPrimaryKey | SQLObject.HasUniqueIndex)
    //            this.Comments.Add(new CodeCommentStatement("<remarks>This _databasename represents the full SELECT string for the table " + SQLObject.name + ", with the WHERE clause.</remarks>", true));
    //        else
    //            this.Comments.Add(new CodeCommentStatement("<remarks>This _databasename represents the full SELECT string for the table " + SQLObject.name + ", without any WHERE clause.</remarks>", true));
    //        this.InitExpression = new CodePrimitiveExpression(selstr);
    //    }
    //}

    //Class that represents the SELECT Field in the class.
    public class FieldSQLSelectString : CodeMemberField
    {
        public FieldSQLSelectString(SQLiteTable _sqlitetable, CodeDomProvider provider)
        {
            string selstr = "SELECT ";
            foreach (SQLiteTableColumn c in _sqlitetable.listSQLiteTableColumn)
            {
                if (c.pk == false)
                {
                    selstr += "[" + c.ColumnName + "], ";
                }
            }
                selstr = selstr.Substring(0, selstr.Length - 2) + " FROM [" + _sqlitetable.tableName + "] ";
            if (_sqlitetable.HasPrimaryKey)
            {
                selstr += "WHERE ";
                foreach (SQLiteTableColumn c in _sqlitetable.listSQLiteTableColumn)
                {
                    if (c.pk)
                        selstr += "[" + c.ColumnName + "]=@" + c.ColumnName + " AND ";
                }
                selstr = selstr.Substring(0, selstr.Length - 4);
            }
            
            this.Attributes = MemberAttributes.Assembly | MemberAttributes.Static;
            this.Type = new CodeTypeReference(typeof(string));
            this.Name = CodeDomGenerator.GetFieldName("SQL_Select", provider);
            if (_sqlitetable.HasPrimaryKey)
                this.Comments.Add(new CodeCommentStatement("<remarks>This _databasename represents the full SELECT string for the table " + _sqlitetable.tableName + ", with the WHERE clause.</remarks>", true));
            else
                this.Comments.Add(new CodeCommentStatement("<remarks>This _databasename represents the full SELECT string for the table " + _sqlitetable.tableName + ", without any WHERE clause.</remarks>", true));
            this.InitExpression = new CodePrimitiveExpression(selstr);
        }

        
    }

    //Class that represents the INSERT Field in the class.
    public class FieldSQLInsertString : CodeMemberField
    {
        public FieldSQLInsertString(SQLiteTable _sqlitetable, CodeDomProvider provider)
        {
            string insstr = "INSERT INTO [" + _sqlitetable.tableName + "] (";
            foreach (SQLiteTableColumn c in _sqlitetable.listSQLiteTableColumn)
            {
                if (c.pk == false)
                {
                    insstr += "[" + c.ColumnName + "], ";
                }
                //insstr += "[" + c.ColumnName + "], ";
            }
            insstr = insstr.Substring(0, insstr.Length - 2);
            insstr += ") VALUES(";
            foreach (SQLiteTableColumn c in _sqlitetable.listSQLiteTableColumn)
            {
                if (c.pk == false)
                {
                    insstr += "@" + c.ColumnName + ", ";
                }
                
            }
                
            insstr = insstr.Substring(0, insstr.Length - 2);
            insstr += ") ";
            this.Attributes = MemberAttributes.Assembly | MemberAttributes.Static;
            this.Type = new CodeTypeReference(typeof(string));
            this.Name = CodeDomGenerator.GetFieldName("SQL_Insert", provider);
            this.Comments.Add(new CodeCommentStatement("<remarks>This _databasename represents the full INSERT INTO string for the table " + _sqlitetable.tableName + ".</remarks>", true));
            this.InitExpression = new CodePrimitiveExpression(insstr);
        }
    }

    //Class that represents the UPDATE Field in the class.
    public class FieldSQLUpdateString : CodeMemberField
    {
        public FieldSQLUpdateString(SQLiteTable _sqlitetable, CodeDomProvider provider)
        {
            string updstr = "UPDATE [" + _sqlitetable.tableName + "] SET ";
            foreach (SQLiteTableColumn c in _sqlitetable.listSQLiteTableColumn)
            {
                if (c.pk == false)
                {
                    updstr += "[" + c.ColumnName + "] = @" + c.ColumnName + ", ";
                }
            }
            updstr = updstr.Substring(0, updstr.Length - 2) + " ";
            if (_sqlitetable.HasPrimaryKey)
            {
                updstr += "WHERE ";
                foreach (SQLiteTableColumn c in _sqlitetable.listSQLiteTableColumn)
                {
                    if (c.pk)
                        updstr += "[" + c.ColumnName + "]=@" + c.ColumnName + " AND ";
                }
                updstr = updstr.Substring(0, updstr.Length - 4);
            }
            
            this.Attributes = MemberAttributes.Assembly | MemberAttributes.Static;
            this.Type = new CodeTypeReference(typeof(string));
            this.Name = CodeDomGenerator.GetFieldName("SQL_Update", provider);
            if (_sqlitetable.HasPrimaryKey)
                this.Comments.Add(new CodeCommentStatement("<remarks>This _databasename represents the full UPDATE string for the table " + _sqlitetable.tableName + ", with the WHERE clause.</remarks>", true));
            else
                this.Comments.Add(new CodeCommentStatement("<remarks>This _databasename represents the full UPDATE string for the table " + _sqlitetable.tableName + ", without any WHERE clause.</remarks>", true));
            this.InitExpression = new CodePrimitiveExpression(updstr);
        }
    }

    //Class that represents the DELETE Field in the class.
    public class FieldSQLDeleteString : CodeMemberField
    {
        public FieldSQLDeleteString(SQLiteTable _sqlitetable, CodeDomProvider provider)
        {
            string delstr = "DELETE FROM [" + _sqlitetable.tableName + "] ";
            if (_sqlitetable.HasPrimaryKey)
            {
                delstr += "WHERE ";
                foreach (SQLiteTableColumn c in _sqlitetable.listSQLiteTableColumn)
                {
                    if (c.pk)
                        delstr += "[" + c.ColumnName + "]=@" + c.ColumnName + " AND ";
                }
                delstr = delstr.Substring(0, delstr.Length - 4);
            }
            
            this.Attributes = MemberAttributes.Assembly | MemberAttributes.Static;
            this.Type = new CodeTypeReference(typeof(string));
            this.Name = CodeDomGenerator.GetFieldName("SQL_Delete", provider);
            if (_sqlitetable.HasPrimaryKey )
                this.Comments.Add(new CodeCommentStatement("<remarks>This _databasename represents the DELETE string for the table " + _sqlitetable.tableName + ", with the WHERE clause.</remarks>", true));
            else
                this.Comments.Add(new CodeCommentStatement("<remarks>This _databasename represents the DELETE string for the table " + _sqlitetable.tableName + ", with WHERE clause if there is any Primary keys on the table.</remarks>", true));
            this.InitExpression = new CodePrimitiveExpression(delstr);
        }
    }

    //Class that represents the private owned _databasename for the representing column.
    public class FieldColumnCreate : CodeMemberField
    {
        public FieldColumnCreate(SQLiteTableColumn _sqlitetablecolumn, CodeDomProvider provider)
        {
            this.Attributes = MemberAttributes.Final | CodeDomGenerator.GetModifier(CodeDomGenerator.FieldModifier);
            CodeTypeReference thistype = new CodeTypeReference(_sqlitetablecolumn.NetType);
            thistype.BaseType = "System.Nullable";
            this.Type = new CodeTypeReference(_sqlitetablecolumn.NetType);
            this.Name = _sqlitetablecolumn.ColumnName;
            if (CodeDomGenerator.AddComments)
                this.Comments.Add(new CodeCommentStatement("<remarks>SQL Type:" + _sqlitetablecolumn.ColumnName + "</remarks>", true));
        }
    }

    //Class that contains the Field member reference.
    public class FieldColumnRef : CodeFieldReferenceExpression
    {
        ////public FieldColumnRef(Column SQLObject, CodeDomProvider provider)
        ////{
        ////    this.TargetObject = null;
            
        ////        this.FieldName = CodeDomGenerator.CSPreFieldname + SQLObject.name.Replace(" ", "_") + CodeDomGenerator.CSPostFieldname;
        ////}
    }

    //Class that represents the databaseName of representing column.
    public class PropertyColumnCreate : CodeMemberProperty
    {
        ////public PropertyColumnCreate(Column SQLObject, CodeDomProvider provider)
        ////{
        ////    this.Attributes = MemberAttributes.Final | CodeDomGenerator.GetModifier(CodeDomGenerator.PropertyModifier);
        ////    if (CodeDomGenerator.MapDescription && !string.IsNullOrEmpty(SQLObject.Description))
        ////        this.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression(SQLObject.Description.Trim()))));
        ////    this.CustomAttributes.Add(new CodeAttributeDeclaration("DisplayName", new CodeAttributeArgument(new CodePrimitiveExpression(CodeDomGenerator.TryCorrectName(SQLObject.name)))));
        ////    if(SQLObject.IsPrimaryKey && !SQLObject.IsForeignKey)
        ////        this.CustomAttributes.Add(new CodeAttributeDeclaration("Category", new CodeAttributeArgument(new CodePrimitiveExpression("Primary Key"))));
        ////    else if (!SQLObject.IsPrimaryKey && SQLObject.IsForeignKey)
        ////        this.CustomAttributes.Add(new CodeAttributeDeclaration("Category", new CodeAttributeArgument(new CodePrimitiveExpression("Foreign Key"))));
        ////    else if (SQLObject.IsPrimaryKey && SQLObject.IsForeignKey)
        ////        this.CustomAttributes.Add(new CodeAttributeDeclaration("Category", new CodeAttributeArgument(new CodePrimitiveExpression("Primary and Foreign Key"))));
        ////    else
        ////        this.CustomAttributes.Add(new CodeAttributeDeclaration("Category", new CodeAttributeArgument(new CodePrimitiveExpression("Column"))));
        ////    this.Name = CodeDomGenerator.TryCorrectNameNoWhitespace(SQLObject.name);
        ////    this.Type = new CodeTypeReference(SQLObject.system_type.NetType);
        ////    CodeCatchClause CatchGet = new CodeCatchClause("err");
        ////    CodeTryCatchFinallyStatement TryGet = new CodeTryCatchFinallyStatement();
        ////    TryGet.TryStatements.Add(new CodeMethodReturnStatement(new FieldColumnRef(SQLObject, provider)));
        ////    CatchGet.Statements.Add(new CodeThrowExceptionStatement(new CodeObjectCreateExpression("Exception",new CodePrimitiveExpression("Error getting " + this.Name),new CodeVariableReferenceExpression("err"))));
        ////    TryGet.CatchClauses.Add(CatchGet);
        ////    this.GetStatements.Add(TryGet);

        ////    CodeCatchClause CatchSet = new CodeCatchClause("err");
        ////    CodeTryCatchFinallyStatement TrySet = new CodeTryCatchFinallyStatement();
        ////    CodeConditionStatement IfLength = new CodeConditionStatement();
        ////    CodeAssignStatement AssignValue = new CodeAssignStatement(new FieldColumnRef(SQLObject, provider), new CodeVariableReferenceExpression("value"));
        ////    if (SQLObject.system_type.NetType == typeof(string))
        ////    {
        ////        IfLength.Condition = new CodeBinaryOperatorExpression(new CodeFieldReferenceExpression(new CodeVariableReferenceExpression("value"), "Length"), CodeBinaryOperatorType.LessThanOrEqual, new CodePrimitiveExpression(SQLObject.max_length));
        ////        IfLength.TrueStatements.Add(AssignValue);
        ////        IfLength.FalseStatements.Add(new CodeThrowExceptionStatement(new CodeObjectCreateExpression("OverflowException", new CodePrimitiveExpression("Error setting " + this.Name + ", Length of value is to long. Maximum Length: " + SQLObject.max_length))));
        ////        TrySet.TryStatements.Add(IfLength);
        ////    }
        ////    else
        ////        TrySet.TryStatements.Add(AssignValue);
        ////    CatchSet.Statements.Add(new CodeThrowExceptionStatement(new CodeObjectCreateExpression("Exception",new CodePrimitiveExpression("Error setting " + this.Name),new CodeVariableReferenceExpression("err"))));
        ////    TrySet.CatchClauses.Add(CatchSet);
        ////    this.SetStatements.Add(TrySet);
        ////}
    }

    //Class that represents the Read only databaseName of representing column.
    public class PropertyColumnReadOnlyCreate : CodeMemberProperty
    {
        //public PropertyColumnReadOnlyCreate(Column SQLObject, CodeDomProvider provider)
        //{
        //    this.Attributes = MemberAttributes.Final | CodeDomGenerator.GetModifier(CodeDomGenerator.PropertyModifier);
        //    if (CodeDomGenerator.MapDescription && !string.IsNullOrEmpty(SQLObject.Description))
        //        this.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression(SQLObject.Description.Trim()))));
        //    this.CustomAttributes.Add(new CodeAttributeDeclaration("DisplayName", new CodeAttributeArgument(new CodePrimitiveExpression(CodeDomGenerator.TryCorrectName(SQLObject.name)))));
        //    this.CustomAttributes.Add(new CodeAttributeDeclaration("Category", new CodeAttributeArgument(new CodePrimitiveExpression("Column"))));
        //    this.Name = CodeDomGenerator.TryCorrectNameNoWhitespace(SQLObject.name);
        //    this.Type = new CodeTypeReference(SQLObject.system_type.NetType);
        //    CodeCatchClause CatchGet = new CodeCatchClause("err");
        //    CodeTryCatchFinallyStatement TryGet = new CodeTryCatchFinallyStatement();
        //    TryGet.TryStatements.Add(new CodeMethodReturnStatement(new FieldColumnRef(SQLObject, provider)));
        //    CatchGet.Statements.Add(new CodeThrowExceptionStatement(new CodeObjectCreateExpression("Exception", new CodePrimitiveExpression("Error getting " + this.Name), new CodeVariableReferenceExpression("err"))));
        //    TryGet.CatchClauses.Add(CatchGet);
        //    this.GetStatements.Add(TryGet);
        //}
    }

    public class PropertyReferenceToObject : CodePropertyReferenceExpression
    {
        ////public PropertyReferenceToObject(CodeExpression TargetObject, Column SQLObject, CodeDomProvider provider)
        ////{
        ////    this.PropertyName = CodeDomGenerator.TryCorrectNameNoWhitespace(SQLObject.name);
        ////    this.TargetObject = TargetObject;
        ////}
    }

    public class FieldRefObjectCreate : CodeMemberField
    {
        //public FieldRefObjectCreate(foreign_key SQLObject, CodeDomProvider provider)
        //{
        //    this.Attributes = MemberAttributes.Final | CodeDomGenerator.GetModifier(CodeDomGenerator.FieldModifier);
        //    this.Type = new CodeTypeReference(SQLObject.Referenced_Table.ObjectName);
        //    this.Name = CodeDomGenerator.GetFieldName(SQLObject.ForeignKeyColumns.Items[0].ParentColumn.ObjectName + "_" + SQLObject.Referenced_Table.ObjectName,provider);
        //    if (CodeDomGenerator.AddComments)
        //        this.Comments.Add(new CodeCommentStatement("<remarks>Represents the foreign key object</remarks>", true));
        //}
    }
    public class FieldRefObjectRef : CodeFieldReferenceExpression
    {
        //public FieldRefObjectRef(foreign_key SQLObject, CodeDomProvider provider)
        //{
        //    this.TargetObject = null;
        //    if (provider is VBCodeProvider)
        //        this.FieldName = CodeDomGenerator.VBPreFieldname + SQLObject.ForeignKeyColumns.Items[0].ParentColumn.ObjectName + "_" + SQLObject.Referenced_Table.ObjectName + CodeDomGenerator.VBPostFieldname;
        //    else
        //        this.FieldName = CodeDomGenerator.CSPreFieldname + SQLObject.ForeignKeyColumns.Items[0].ParentColumn.ObjectName + "_" + SQLObject.Referenced_Table.ObjectName + CodeDomGenerator.CSPostFieldname;
        //}
    }

    public class PropertyRefObjectCreate : CodeMemberProperty
    {
        //public PropertyRefObjectCreate(foreign_key SQLObject, CodeDomProvider provider)
        //{
        //    this.Attributes = MemberAttributes.Final | CodeDomGenerator.GetModifier(CodeDomGenerator.PropertyModifier);
        //    this.Type = new CodeTypeReference(SQLObject.Referenced_Table.ObjectName);
        //    if (CodeDomGenerator.MapDescription)
        //        this.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression("Represents the foreign key object of the type " + SQLObject.Referenced_Table.ObjectName))));
        //    this.Name = SQLObject.ForeignKeyColumns.Items[0].ParentColumn.ObjectName + "_" + SQLObject.Referenced_Table.ObjectName;

        //    CodeCatchClause CatchGet = new CodeCatchClause("err");
        //    CodeTryCatchFinallyStatement TryGet = new CodeTryCatchFinallyStatement();
        //    TryGet.TryStatements.Add(new CodeMethodReturnStatement(new FieldRefObjectRef(SQLObject, provider)));
        //    CatchGet.Statements.Add(new CodeThrowExceptionStatement(new CodeObjectCreateExpression("Exception", new CodePrimitiveExpression("Error getting " + this.Name), new CodeVariableReferenceExpression("err"))));
        //    TryGet.CatchClauses.Add(CatchGet);
        //    this.GetStatements.Add(TryGet);

        //    CodeCatchClause CatchSet = new CodeCatchClause("err");
        //    CodeTryCatchFinallyStatement TrySet = new CodeTryCatchFinallyStatement();
        //    TrySet.TryStatements.Add(new CodeAssignStatement(new FieldRefObjectRef(SQLObject, provider), new CodeVariableReferenceExpression("value")));
        //    foreach (foreign_key_column fkc in SQLObject.ForeignKeyColumns.Items)
        //        TrySet.TryStatements.Add(new CodeAssignStatement(new CodeVariableReferenceExpression(CodeDomGenerator.GetFieldName(fkc.ParentColumn, provider)), new PropertyReferenceToObject(new CodeVariableReferenceExpression(CodeDomGenerator.GetFieldName(SQLObject.ForeignKeyColumns.Items[0].ParentColumn.ObjectName + "_" + SQLObject.Referenced_Table.ObjectName,provider)), fkc.ReferencedColumn, provider)));
        //    CatchSet.Statements.Add(new CodeThrowExceptionStatement(new CodeObjectCreateExpression("Exception", new CodePrimitiveExpression("Error setting " + this.Name), new CodeVariableReferenceExpression("err"))));
        //    TrySet.CatchClauses.Add(CatchSet);
        //    this.SetStatements.Add(TrySet);
        //}
    }

    public class FieldRefObjectCollectionCreate : CodeMemberField
    {
        //public FieldRefObjectCollectionCreate(foreign_key SQLObject, CodeDomProvider provider)
        //{
        //    this.Attributes = MemberAttributes.Final | CodeDomGenerator.GetModifier(CodeDomGenerator.FieldModifier);
        //    this.Type = new CodeTypeReference(SQLObject.Owner.Owner.ObjectName + "Collection");
        //    if (provider is VBCodeProvider)
        //        this.Name = CodeDomGenerator.VBPreFieldname + SQLObject.Owner.Owner.ObjectName + "Collection" + CodeDomGenerator.VBPostFieldname;
        //    else
        //        this.Name = CodeDomGenerator.CSPreFieldname + SQLObject.Owner.Owner.ObjectName + "Collection" + CodeDomGenerator.CSPostFieldname;
        //    if (CodeDomGenerator.AddComments)
        //        this.Comments.Add(new CodeCommentStatement("<remarks>Represents the foreign key object</remarks>", true));
        //}
    }
    public class FieldRefObjectCollectionRef : CodeFieldReferenceExpression
    {
        //public FieldRefObjectCollectionRef(foreign_key SQLObject, CodeDomProvider provider)
        //{
        //    this.TargetObject = null;
        //    if (provider is VBCodeProvider)
        //        this.FieldName = CodeDomGenerator.VBPreFieldname + SQLObject.Owner.Owner.ObjectName + "Collection" + CodeDomGenerator.VBPostFieldname;
        //    else
        //        this.FieldName = CodeDomGenerator.CSPreFieldname + SQLObject.Owner.Owner.ObjectName + "Collection" + CodeDomGenerator.CSPostFieldname;
        //}
    }

    public class PropertyRefObjectCollectionCreate : CodeMemberProperty
    {
        ////public PropertyRefObjectCollectionCreate(foreign_key SQLObject, CodeDomProvider provider)
        ////{
        ////    this.Attributes = MemberAttributes.Final | CodeDomGenerator.GetModifier(CodeDomGenerator.PropertyModifier);
        ////    this.Type = new CodeTypeReference(SQLObject.Owner.Owner.ObjectName + "Collection");
        ////    if (CodeDomGenerator.MapDescription)
        ////        this.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression("Represents the foreign key relation. This is an Collection of " + SQLObject.Referenced_Table.ObjectName + "."))));
        ////    this.Name = SQLObject.Owner.Owner.ObjectName + "Collection";

        ////    CodeCatchClause CatchGet = new CodeCatchClause("err");
        ////    CodeTryCatchFinallyStatement TryGet = new CodeTryCatchFinallyStatement();
        ////    TryGet.TryStatements.Add(new CodeMethodReturnStatement(new FieldRefObjectCollectionRef(SQLObject, provider)));
        ////    CatchGet.Statements.Add(new CodeThrowExceptionStatement(new CodeObjectCreateExpression("Exception", new CodePrimitiveExpression("Error getting " + this.Name), new CodeVariableReferenceExpression("err"))));
        ////    TryGet.CatchClauses.Add(CatchGet);
        ////    this.GetStatements.Add(TryGet);

        ////    CodeCatchClause CatchSet = new CodeCatchClause("err");
        ////    CodeTryCatchFinallyStatement TrySet = new CodeTryCatchFinallyStatement();
        ////    TrySet.TryStatements.Add(new CodeAssignStatement(new FieldRefObjectCollectionRef(SQLObject, provider), new CodeVariableReferenceExpression("value")));
        ////    CatchSet.Statements.Add(new CodeThrowExceptionStatement(new CodeObjectCreateExpression("Exception", new CodePrimitiveExpression("Error setting " + this.Name), new CodeVariableReferenceExpression("err"))));
        ////    TrySet.CatchClauses.Add(CatchSet);
        ////    this.SetStatements.Add(TrySet);
        ////}
    }

    public class SelectMethod : CodeMemberMethod
    {
        ////public SelectMethod(Table SQLObject, CodeDomProvider provider)
        ////{
        ////    this.Attributes = MemberAttributes.Public | MemberAttributes.Final;
        ////    this.Name = "Select";
            
        ////    this.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)),"ConnectionString"));

        ////    CodeTryCatchFinallyStatement Try = new CodeTryCatchFinallyStatement();
        ////    CodeCatchClause Catch = new CodeCatchClause();
        ////    Catch.Statements.Add(new CodeThrowExceptionStatement());
        ////    Try.CatchClauses.Add(Catch);

        ////    Try.TryStatements.Add(new CodeVariableDeclarationStatement("OleDbConnection", "Conn", new CodeObjectCreateExpression("OleDbConnection", new CodeFieldReferenceExpression(null, "ConnectionString"))));
        ////    Try.TryStatements.Add(new CodeVariableDeclarationStatement("OleDbCommand", "Com", new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "CreateCommand")));
        ////    Try.TryStatements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeTypeReferenceExpression("Com"), "CommandText"), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(SQLObject.ObjectName), CodeDomGenerator.GetFieldName("SQL_Select", provider))));

        ////    CodeMethodInvokeExpression cmieAddRange = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com.Parameters"), "AddRange");
        ////    cmieAddRange.Parameters.Add(new CodeMethodInvokeExpression(null, "GetSqlParameters", new CodeExpression[] { }));
        ////    Try.TryStatements.Add(cmieAddRange);

        ////    Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Open"));
        ////    Try.TryStatements.Add(new CodeVariableDeclarationStatement("OleDbDataReader", "rs", new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com"), "ExecuteReader")));

        ////    CodeIterationStatement codeWhile = new CodeIterationStatement();
        ////    codeWhile.TestExpression = new CodeSnippetExpression("rs.Read()");
        ////    codeWhile.Statements.Add(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(null, "AddFromRecordSet"), new CodeExpression[] { new CodeVariableReferenceExpression("rs") }));
        ////    codeWhile.IncrementStatement = new CodeSnippetStatement(null);
        ////    codeWhile.InitStatement = new CodeSnippetStatement(null);
        ////    Try.TryStatements.Add(codeWhile);

        ////    Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("rs"), "Close"));
        ////    Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Close"));
        ////    Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("rs"), "Dispose"));
        ////    Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com"), "Dispose"));
        ////    Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Dispose"));

        ////    this.Statements.Add(Try);
        ////}
        
    }

    public class InsertMethod : CodeMemberMethod
    {
        ////public InsertMethod(Table SQLObject, CodeDomProvider provider)
        ////{
        ////    this.Attributes = MemberAttributes.Public | MemberAttributes.Final;
        ////    this.Name = "Insert";

        ////    this.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)), "ConnectionString"));

        ////    this.ReturnType = new CodeTypeReference(typeof(int));

        ////    CodeTryCatchFinallyStatement Try = new CodeTryCatchFinallyStatement();
        ////    CodeCatchClause Catch = new CodeCatchClause();
        ////    Catch.Statements.Add(new CodeThrowExceptionStatement());
        ////    Try.CatchClauses.Add(Catch);

        ////    Try.TryStatements.Add(new CodeVariableDeclarationStatement("OleDbConnection", "Conn", new CodeObjectCreateExpression("OleDbConnection", new CodeFieldReferenceExpression(null, "ConnectionString"))));
        ////    Try.TryStatements.Add(new CodeVariableDeclarationStatement("OleDbCommand", "Com", new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "CreateCommand")));
        ////    Try.TryStatements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeTypeReferenceExpression("Com"), "CommandText"), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(SQLObject.ObjectName), CodeDomGenerator.GetFieldName("SQL_Insert", provider))));

        ////    CodeMethodInvokeExpression cmieAddRange = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com.Parameters"), "AddRange");
        ////    cmieAddRange.Parameters.Add(new CodeMethodInvokeExpression(null, "GetSqlParameters", new CodeExpression[] { }));
        ////    Try.TryStatements.Add(cmieAddRange);
        ////    Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Open"));
        ////    Try.TryStatements.Add(new CodeVariableDeclarationStatement(typeof(int), "rowseffected", new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com"), "ExecuteNonQuery")));
        ////    Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Close"));
        ////    Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com"), "Dispose"));
        ////    Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Dispose"));
        ////    Try.TryStatements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("rowseffected")));

        ////    this.Statements.Add(Try);
        ////}
    }

    public class UpdateMethod : CodeMemberMethod
    {
        ////public UpdateMethod(Table SQLObject, CodeDomProvider provider)
        ////{
        ////    this.Attributes = MemberAttributes.Public | MemberAttributes.Final;
        ////    this.Name = "Update";

        ////    this.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)), "ConnectionString"));

        ////    this.ReturnType = new CodeTypeReference(typeof(int));

        ////    CodeTryCatchFinallyStatement Try = new CodeTryCatchFinallyStatement();
        ////    CodeCatchClause Catch = new CodeCatchClause();
        ////    Catch.Statements.Add(new CodeThrowExceptionStatement());
        ////    Try.CatchClauses.Add(Catch);

        ////    Try.TryStatements.Add(new CodeVariableDeclarationStatement("OleDbConnection", "Conn", new CodeObjectCreateExpression("OleDbConnection", new CodeFieldReferenceExpression(null, "ConnectionString"))));
        ////    Try.TryStatements.Add(new CodeVariableDeclarationStatement("OleDbCommand", "Com", new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "CreateCommand")));
        ////    Try.TryStatements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeTypeReferenceExpression("Com"), "CommandText"), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(SQLObject.ObjectName), CodeDomGenerator.GetFieldName("SQL_Update", provider))));

        ////    CodeMethodInvokeExpression cmieAddRange = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com.Parameters"), "AddRange");
        ////    cmieAddRange.Parameters.Add(new CodeMethodInvokeExpression(null, "GetSqlParameters", new CodeExpression[] { }));
        ////    Try.TryStatements.Add(cmieAddRange);
        ////    Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Open"));
        ////    Try.TryStatements.Add(new CodeVariableDeclarationStatement(typeof(int), "rowseffected", new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com"), "ExecuteNonQuery")));
        ////    Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Close"));
        ////    Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com"), "Dispose"));
        ////    Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Dispose"));
        ////    Try.TryStatements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("rowseffected")));

        ////    this.Statements.Add(Try);
        ////}
    }

    public class DeleteMethod : CodeMemberMethod
    {
        ////public DeleteMethod(Table SQLObject, CodeDomProvider provider)
        ////{
        ////    this.Attributes = MemberAttributes.Public | MemberAttributes.Final;
        ////    this.Name = "Delete";

        ////    this.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)), "ConnectionString"));

        ////    this.ReturnType = new CodeTypeReference(typeof(int));

        ////    CodeTryCatchFinallyStatement Try = new CodeTryCatchFinallyStatement();
        ////    CodeCatchClause Catch = new CodeCatchClause();
        ////    Catch.Statements.Add(new CodeThrowExceptionStatement());
        ////    Try.CatchClauses.Add(Catch);

        ////    Try.TryStatements.Add(new CodeVariableDeclarationStatement("OleDbConnection", "Conn", new CodeObjectCreateExpression("OleDbConnection", new CodeFieldReferenceExpression(null, "ConnectionString"))));
        ////    Try.TryStatements.Add(new CodeVariableDeclarationStatement("OleDbCommand", "Com", new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "CreateCommand")));
        ////    Try.TryStatements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeTypeReferenceExpression("Com"), "CommandText"), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(SQLObject.ObjectName), CodeDomGenerator.GetFieldName("SQL_Delete", provider))));

        ////    CodeMethodInvokeExpression cmieAddRange = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com.Parameters"), "AddRange");
        ////    cmieAddRange.Parameters.Add(new CodeMethodInvokeExpression(null, "GetSqlParameters", new CodeExpression[] { }));
        ////    Try.TryStatements.Add(cmieAddRange);
        ////    Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Open"));
        ////    Try.TryStatements.Add(new CodeVariableDeclarationStatement(typeof(int), "rowseffected", new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com"), "ExecuteNonQuery")));
        ////    Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Close"));
        ////    Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com"), "Dispose"));
        ////    Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Dispose"));
        ////    Try.TryStatements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("rowseffected")));

        ////    this.Statements.Add(Try);
        ////}
    }


    public class GetSqlCommandStringsMethod : CodeMemberMethod
    {
        ////public GetSqlCommandStringsMethod(Table SQLObject, CodeDomProvider provider)
        ////{
        ////    this.Attributes = MemberAttributes.Public | MemberAttributes.Final;
        ////    this.Name = "GetSqlCommandStrings";

        ////    this.ReturnType = new CodeTypeReference(typeof(string).MakeArrayType(1));

        ////    string Adder = "+";
        ////    if (provider is VBCodeProvider)
        ////        Adder = "&";

        ////    CodeTryCatchFinallyStatement Try = new CodeTryCatchFinallyStatement();
        ////    CodeCatchClause Catch = new CodeCatchClause();
        ////    Catch.Statements.Add(new CodeThrowExceptionStatement());
        ////    Try.CatchClauses.Add(Catch);
        ////    string[] CommStr = new string[4];
        ////    CommStr[0] += "\"SELECT ";
        ////    CommStr[1] += "\"INSERT INTO [" + SQLObject.Schema.name + "].[" + SQLObject.name + "] (";
        ////    CommStr[2] += "\"UPDATE [" + SQLObject.Schema.name + "].[" + SQLObject.name + "] SET ";
        ////    CommStr[3] += "\"DELETE FROM [" + SQLObject.Schema.name + "].[" + SQLObject.name + "] WHERE ";
        ////    Try.TryStatements.Add(new CodeVariableDeclarationStatement(typeof(string).MakeArrayType(1), "CommStr", new CodeArrayCreateExpression(typeof(string).MakeArrayType(1),4)));
        ////    foreach (Column c in SQLObject.Columns.Items)
        ////    { 
        ////        CommStr[0] += "[" + c.name + "], ";
        ////        CommStr[1] += "[" + c.name + "], ";
        ////        if (c.IsPrimaryKey)
        ////        {
        ////            if (c.system_type.NetType == typeof(string))
        ////                CommStr[3] += "[" + c.name + "] = '\" " + Adder + " " + CodeDomGenerator.GetFieldName(c, provider) + ".Replace(\"'\",\"''\") " + Adder + " \"' AND ";
        ////            else if (c.system_type.NetType == typeof(DateTime))
        ////                CommStr[3] += "[" + c.name + "] = '\" " + Adder + " " + CodeDomGenerator.GetFieldName(c, provider) + " " + Adder + " \"' AND ";
        ////            else
        ////                CommStr[3] += "[" + c.name + "] = \" " + Adder + " " + CodeDomGenerator.GetFieldName(c, provider) + " " + Adder + " \" AND ";
        ////        }
        ////        else
        ////        {
        ////            if(c.system_type.NetType == typeof(string))
        ////                CommStr[2] += "[" + c.name + "] = '\" " + Adder + " " + CodeDomGenerator.GetFieldName(c, provider) + ".Replace(\"'\",\"''\") " + Adder + " \"' ,";
        ////            else if (c.system_type.NetType == typeof(DateTime))
        ////                CommStr[2] += "[" + c.name + "] = '\" " + Adder + " " + CodeDomGenerator.GetFieldName(c, provider) + " " + Adder + " \"' ,";
        ////            else
        ////                CommStr[2] += "[" + c.name + "] = \" " + Adder + " " + CodeDomGenerator.GetFieldName(c, provider) + " " + Adder + " \" ,";
        ////        }
        ////    }
        ////    CommStr[0] = CommStr[0].Substring(0, CommStr[0].Length - 2) + " FROM [" + SQLObject.Schema.name + "].[" + SQLObject.name + "] WHERE ";
        ////    CommStr[1] = CommStr[1].Substring(0, CommStr[1].Length - 2) + ") VALUES(";
        ////    CommStr[2] = CommStr[2].Substring(0, CommStr[2].Length - 2) + " WHERE ";
        ////    CommStr[3] = CommStr[3].Substring(0, CommStr[3].Length - 5) + "\"";
        ////    foreach (Column c in SQLObject.Columns.Items)
        ////    {
        ////        if (c.system_type.NetType == typeof(string))
        ////            CommStr[1] += "'\" " + Adder + " " + CodeDomGenerator.GetFieldName(c, provider) + ".Replace(\"'\",\"''\") " + Adder + " \"', ";
        ////        else if (c.system_type.NetType == typeof(DateTime))
        ////            CommStr[1] += "'\" " + Adder + " " + CodeDomGenerator.GetFieldName(c, provider) + " " + Adder + " \"', ";
        ////        else
        ////            CommStr[1] += "\" " + Adder + " " + CodeDomGenerator.GetFieldName(c, provider) + " " + Adder + " \", ";
        ////        if (c.IsPrimaryKey)
        ////        {
        ////            if (c.system_type.NetType == typeof(string))
        ////                CommStr[0] += "[" + c.name + "] = '\" " + Adder + " " + CodeDomGenerator.GetFieldName(c, provider) + ".Replace(\"'\",\"''\") " + Adder + " \"' AND ";
        ////            else if (c.system_type.NetType == typeof(DateTime))
        ////                CommStr[0] += "[" + c.name + "] = '\" " + Adder + " " + CodeDomGenerator.GetFieldName(c, provider) + " " + Adder + " \"' AND ";
        ////            else
        ////                CommStr[0] += "[" + c.name + "] = \" " + Adder + " " + CodeDomGenerator.GetFieldName(c, provider) + " " + Adder + " \" AND ";

        ////            if (c.system_type.NetType == typeof(string))
        ////                CommStr[2] += "[" + c.name + "] = '\" " + Adder + " " + CodeDomGenerator.GetFieldName(c, provider) + ".Replace(\"'\",\"''\") " + Adder + " \"' AND ";
        ////            else if (c.system_type.NetType == typeof(DateTime))
        ////                CommStr[2] += "[" + c.name + "] = '\" " + Adder + " " + CodeDomGenerator.GetFieldName(c, provider) + " " + Adder + " \"' AND ";
        ////            else
        ////                CommStr[2] += "[" + c.name + "] = \" " + Adder + " " + CodeDomGenerator.GetFieldName(c, provider) + " " + Adder + " \" AND ";
        ////        }
        ////    }
        ////    CommStr[0] = CommStr[0].Substring(0, CommStr[0].Length - 5) + "\"";
        ////    CommStr[1] = CommStr[1].Substring(0, CommStr[1].Length - 2) + ")\"";
        ////    CommStr[2] = CommStr[2].Substring(0, CommStr[2].Length - 5) + "\"";
        ////    Try.TryStatements.Add(new CodeAssignStatement(new CodeArrayIndexerExpression(new CodeVariableReferenceExpression("CommStr"), new CodePrimitiveExpression(0)), new CodeSnippetExpression(CommStr[0])));
        ////    Try.TryStatements.Add(new CodeAssignStatement(new CodeArrayIndexerExpression(new CodeVariableReferenceExpression("CommStr"), new CodePrimitiveExpression(1)), new CodeSnippetExpression(CommStr[1])));
        ////    Try.TryStatements.Add(new CodeAssignStatement(new CodeArrayIndexerExpression(new CodeVariableReferenceExpression("CommStr"), new CodePrimitiveExpression(2)), new CodeSnippetExpression(CommStr[2])));
        ////    Try.TryStatements.Add(new CodeAssignStatement(new CodeArrayIndexerExpression(new CodeVariableReferenceExpression("CommStr"), new CodePrimitiveExpression(3)), new CodeSnippetExpression(CommStr[3])));
        ////    Try.TryStatements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("CommStr")));

        ////    this.Statements.Add(Try);
        ////}
    }

    public class GetSqlParameters : CodeMemberMethod
    {
        ////public GetSqlParameters(Table SQLObject, CodeDomProvider provider)
        ////{
        ////    this.ReturnType = new CodeTypeReference("SqlParameter", 1);
        ////    this.Name = "GetSqlParameters";

        ////    CodeTypeReference ListParms = new CodeTypeReference("List", new CodeTypeReference("SqlParameter"));
        ////    this.Statements.Add(new CodeVariableDeclarationStatement(ListParms, "SqlParmColl", new CodeObjectCreateExpression(ListParms, new CodeExpression[] { })));

        ////    CodeTryCatchFinallyStatement Try = new CodeTryCatchFinallyStatement();

        ////    #region ExceptionCatcher

        ////    CodeCatchClause Catch = new CodeCatchClause("Exc", new CodeTypeReference("Exception"));
        ////    Catch.Statements.Add(new CodeThrowExceptionStatement(new CodeVariableReferenceExpression("Exc")));
        ////    Try.CatchClauses.Add(Catch);

        ////    #endregion

        ////    foreach (Column c in SQLObject.Columns.Items)
        ////    {
        ////        CodeMethodInvokeExpression cmie = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(SQLObject.Owner.Owner.name), "AddSqlParm");
        ////        cmie.Parameters.Add(new CodePrimitiveExpression("@" + c.name));
        ////        cmie.Parameters.Add(new CodeVariableReferenceExpression(CodeDomGenerator.TryCorrectNameNoWhitespace(c.name)));
        ////        cmie.Parameters.Add(new CodeSnippetExpression(CodeDomGenerator.SqlDbTypeConst(c.system_type.name)));
        ////        CodeMethodInvokeExpression cmieAdd = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("SqlParmColl"), "Add");
        ////        cmieAdd.Parameters.Add(cmie);
        ////        Try.TryStatements.Add(cmieAdd);
        ////    }
        ////    Try.TryStatements.Add(new CodeMethodReturnStatement(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("SqlParmColl"), "ToArray", new CodeExpression[] { })));
        ////    this.Statements.Add(Try);
        ////}

        
    }

    public class AddFromRecordSetMethod : CodeMemberMethod
    {
        ////public AddFromRecordSetMethod(Table SQLObject, CodeDomProvider provider)
        ////{
        ////    this.Name = "AddFromRecordSet";
        ////    this.Parameters.Add(new CodeParameterDeclarationExpression("SQLiteDataReader", "rs"));
        ////    CodeMethodReferenceExpression IsDBNullMethod = new CodeMethodReferenceExpression(new CodeSnippetExpression("rs"), "IsDBNull");
        ////    CodeTryCatchFinallyStatement Try = new CodeTryCatchFinallyStatement();

        ////    #region ExceptionCatcher SQL

        ////    CodeCatchClause SQLCatch = new CodeCatchClause("sqlExc", new CodeTypeReference("SqlException"));
        ////    SQLCatch.Statements.Add(new CodeThrowExceptionStatement(new CodeVariableReferenceExpression("sqlExc")));

        ////    Try.CatchClauses.Add(SQLCatch);

        ////    CodeCatchClause Catch = new CodeCatchClause("Exc", new CodeTypeReference("Exception"));
        ////    Catch.Statements.Add(new CodeThrowExceptionStatement(new CodeVariableReferenceExpression("Exc")));
        ////    Try.CatchClauses.Add(Catch);

        ////    #endregion

        ////    int rscounter = 0;

        ////    foreach (Column column in SQLObject.Columns.Items)
        ////    {
        ////        CodeConditionStatement ccs = new CodeConditionStatement();
        ////        CodeMethodReferenceExpression GetOrdinalMethod = new CodeMethodReferenceExpression(new CodeSnippetExpression("rs"), "GetOrdinal");
        ////        CodeMethodInvokeExpression InvokeGetOrdinalMethod = new CodeMethodInvokeExpression(GetOrdinalMethod, new CodeExpression[] { new CodePrimitiveExpression(column.name) });
        ////        CodeMethodInvokeExpression InvokeIsDBNullMethod = new CodeMethodInvokeExpression(IsDBNullMethod, new CodeExpression[] { InvokeGetOrdinalMethod });
        ////        ccs.Condition = new CodeBinaryOperatorExpression(InvokeIsDBNullMethod, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(false));
        ////        CodeMethodReferenceExpression RSReaderMethod = new CodeMethodReferenceExpression(new CodeSnippetExpression("rs"), CodeDomGenerator.SqlDataReaderGetMethod(column.system_type.NetType));
        ////        CodeMethodInvokeExpression InvokeRSReaderMethod = new CodeMethodInvokeExpression(RSReaderMethod, new CodeExpression[] { InvokeGetOrdinalMethod });

        ////        if (column.system_type.name.Equals("varbinary") || column.system_type.name.Equals("binary") || column.system_type.name.Equals("image"))
        ////        {
        ////            CodeMethodInvokeExpression InvokeConvert = new CodeMethodInvokeExpression(
        ////                new CodeTypeReferenceExpression(typeof(Convert)), "ToInt32",
        ////                new CodeMethodInvokeExpression(RSReaderMethod,
        ////                new CodeExpression[] { InvokeGetOrdinalMethod, 
        ////                    new CodePrimitiveExpression(0), 
        ////                    new CodePrimitiveExpression(null), 
        ////                    new CodePrimitiveExpression(0), 
        ////                    new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typeof(Int32)),"MaxValue") }));
        ////            CodeArrayCreateExpression NewByte = new CodeArrayCreateExpression(typeof(Byte), InvokeConvert);
        ////            CodeAssignStatement cas = new CodeAssignStatement(new CodePropertyReferenceExpression(null, CodeDomGenerator.TryCorrectNameNoWhitespace(column.name)), NewByte);
        ////            ccs.TrueStatements.Add(cas);
        ////            CodeVariableDeclarationStatement cvds = new CodeVariableDeclarationStatement(typeof(long), column.name + "Received");
        ////            cvds.InitExpression = new CodeMethodInvokeExpression(RSReaderMethod, InvokeGetOrdinalMethod, new CodePrimitiveExpression(0),new CodePropertyReferenceExpression(null,CodeDomGenerator.TryCorrectNameNoWhitespace(column.name)), new CodePrimitiveExpression(0), new CodePropertyReferenceExpression(new CodePropertyReferenceExpression(null,CodeDomGenerator.TryCorrectNameNoWhitespace(column.name)), "Length"));
        ////            ccs.TrueStatements.Add(cvds);
        ////        }
        ////        else
        ////        {
        ////            CodeAssignStatement cas = new CodeAssignStatement(new CodePropertyReferenceExpression(null,CodeDomGenerator.TryCorrectNameNoWhitespace(column.name)), InvokeRSReaderMethod);
        ////            ccs.TrueStatements.Add(cas);
        ////        }
        ////        Try.TryStatements.Add(new CodeCommentStatement("if value from the recordset, to the " + column.name + " _databasename is NOT null then set the value."));
        ////        Try.TryStatements.Add(ccs);
        ////        rscounter++;
        ////    }
        ////    this.Statements.Add(Try);
        ////}

       

    }

    public class ClassFieldEnumeration : CodeTypeDeclaration
    {
        public ClassFieldEnumeration(SQLiteTable _sqlitetable, CodeDomProvider provider)
        {
            this.IsEnum = true;
            this.Name = _sqlitetable.tableName + "_Fields";
            foreach (SQLiteTableColumn c in _sqlitetable.listSQLiteTableColumn)
                this.Members.Add(new CodeMemberField(typeof(int), c.ColumnName));
            foreach (CodeMemberField cmf in this.Members)
                cmf.Attributes = MemberAttributes.Public;
        }
        
    }

    /*
     * New from the 19 Aug. 2008
     */

    //Class that reflects the Collection class of the table
    public class ClassCollection : CodeTypeDeclaration
    {
        public ClassCollection(SQLiteTable _sqlitetable, CodeDomProvider provider)
        {
            #region ClassCollection Setup

            this.Name = _sqlitetable.tableName + "Collection";
            this.IsClass = true;
            this.TypeAttributes = TypeAttributes.Public | TypeAttributes.Class;
            this.BaseTypes.Add(typeof(CollectionBase));
            if (!string.IsNullOrEmpty(_sqlitetable.Description))
                this.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression(_sqlitetable.Description))));

            #endregion

            #region Add Method

            CodeMemberMethod AddMethod = new CodeMemberMethod();
            AddMethod.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression("Adds a new " + _sqlitetable.tableName + " to the collection."))));
            AddMethod.Name = "Add";
            AddMethod.ReturnType = new CodeTypeReference(typeof(int));
            AddMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            AddMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(_sqlitetable.tableName), "item"));
            AddMethod.Statements.Add(new CodeVariableDeclarationStatement(typeof(int), "newindex", new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("List"), "Add", new CodeVariableReferenceExpression("item"))));
            AddMethod.Statements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("newindex")));

            this.Members.Add(AddMethod);

            #endregion

            #region Remove Method

            CodeMemberMethod RemoveMethod = new CodeMemberMethod();
            RemoveMethod.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression("Removes a " + _sqlitetable.tableName + " from the collection."))));
            RemoveMethod.Name = "Remove";
            RemoveMethod.ReturnType = new CodeTypeReference(typeof(void));
            RemoveMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            RemoveMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(_sqlitetable.tableName), "item"));
            RemoveMethod.Statements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("List"), "Remove", new CodeVariableReferenceExpression("item")));
            this.Members.Add(RemoveMethod);


            #endregion

            #region Insert Method

            CodeMemberMethod InsertMethod = new CodeMemberMethod();
            InsertMethod.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression("Inserts an " + _sqlitetable.tableName + " into the collection at the specified index."))));
            InsertMethod.Name = "Insert";
            InsertMethod.ReturnType = new CodeTypeReference(typeof(void));
            InsertMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            InsertMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(int)), "index"));
            InsertMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(_sqlitetable.tableName), "item"));
            InsertMethod.Statements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("List"), "Insert", new CodeVariableReferenceExpression("index"), new CodeVariableReferenceExpression("item")));
            this.Members.Add(InsertMethod);


            #endregion

            #region IndexOf Method

            CodeMemberMethod IndexOfMethod = new CodeMemberMethod();
            IndexOfMethod.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression("Returns the index value of the " + _sqlitetable.tableName + " class in the collection."))));
            IndexOfMethod.Name = "IndexOf";
            IndexOfMethod.ReturnType = new CodeTypeReference(typeof(int));
            IndexOfMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            IndexOfMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(_sqlitetable.tableName), "item"));
            IndexOfMethod.Statements.Add(new CodeMethodReturnStatement(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("List"), "IndexOf", new CodeVariableReferenceExpression("item"))));
            this.Members.Add(IndexOfMethod);

            #endregion

            #region Contains Method

            CodeMemberMethod ContainsMethod = new CodeMemberMethod();
            ContainsMethod.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression("Returns true if the " + _sqlitetable.tableName + " class is present in the collection."))));
            ContainsMethod.Name = "Contains";
            ContainsMethod.ReturnType = new CodeTypeReference(typeof(bool));
            ContainsMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            ContainsMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(_sqlitetable.tableName), "item"));
            ContainsMethod.Statements.Add(new CodeMethodReturnStatement(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("List"), "Contains", new CodeVariableReferenceExpression("item"))));
            this.Members.Add(ContainsMethod);

            #endregion
        }
    }

    //Class that reflects the Collection class of the view
    public class ClassViewCollection : CodeTypeDeclaration
    {
        
    }
}
