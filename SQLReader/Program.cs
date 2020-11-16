using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.CodeDom;
using System.Reflection;
using System.Data;
using System.Collections;


namespace DCS
{
    class Program
    {
        public static string NAMESPACESTRING = "VP2FTConvert";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //SQLConnSet = new SQLiteConnectionStringBuilder("D:\\DSC.dev\\Source-VS11\\Source-VS11\\Source-VS11\\KTC_DATA\\Phase2021.Sqlite");
            Global.Instance.databaseFullpath = @"Z:\Share\EhsanAmid\GitHub\VP2FT\GLNG3.db";
            Global.Instance.Init();
            Global.Instance.SqliteDatabase.LoadAssociatedObjects();
            Global.Instance.SqliteDatabase.Save2CSV();
            
            //generateCode(new CSharpCodeProvider());
            generateCode(new CSharpCodeProvider() , "tblProject");
            //generateCode(new CSharpCodeProvider(), "tblSolution");
            //generateCode(new CSharpCodeProvider(), "tblAlarm");
            //generateCode(new CSharpCodeProvider(), "tblAlarmGroup");
            //generateCode(new CSharpCodeProvider(), "tblPlantStructure");
            //generateCode(new CSharpCodeProvider(), "tblInstrumentUnits");
            //generateCode(new CSharpCodeProvider(), "tblInstrumentUnitsGrp");
            //generateCode(new CSharpCodeProvider(), "tblEquipment");

            //generateCode(new CSharpCodeProvider(), "tblDisplay");
            //generateCode(new CSharpCodeProvider(), "tblEditBox");
            //generateCode(new CSharpCodeProvider(), "tblFormalParameter");
            //generateCode(new CSharpCodeProvider(), "tblVariable");
            //generateCode(new CSharpCodeProvider(), "tblBOOL");
            //generateCode(new CSharpCodeProvider(), "tblREAL");
            //generateCode(new CSharpCodeProvider(), "tblPou");
            //generateCode(new CSharpCodeProvider(), "tblFBDBlock");
            //generateCode(new CSharpCodeProvider(), "tblFBDBlockPin");
            //generateCode(new CSharpCodeProvider(), "tblFBDPinConnection");
            //generateCode(new CSharpCodeProvider(), "tblFunction");
            //generateCode(new CSharpCodeProvider(), "tblBlockState");
            //generateCode(new CSharpCodeProvider(),"tblRect");
            //generateCode(new CSharpCodeProvider(), "test");
            //generateCode(new CSharpCodeProvider(), "tblADText");
            //generateCode(new CSharpCodeProvider(), "tblBitmap");
            //generateCode(new CSharpCodeProvider(), "tblSymbolBitmap");
            //generateCode(new CSharpCodeProvider(), "tblSymbolRect");
            //generateCode(new CSharpCodeProvider(), "tblSymbolPolyline");
            //generateCode(new CSharpCodeProvider(), "tblSymbolPointsPolyline");
            //generateCode(new CSharpCodeProvider(), "tblSymbolLine");
            //generateCode(new CSharpCodeProvider(), "tblSymbolStatus");
            //generateCode(new CSharpCodeProvider(), "tblSymbolADText");
            //generateCode(new CSharpCodeProvider(), "tblSymbols");
            //generateCode(new CSharpCodeProvider(), "tblPlantStructure");
        }

        static void generateCode(CodeDomProvider provider,string tablename)
        {
            Global.Instance.SqliteDatabase.databaseName = @"Z:\Share\EhsanAmid\GitHub\VP2FT\VP2FTConvert\SQLite";
            foreach (SQLiteTable _sqlitetable in Global.Instance.SqliteDatabase.listSQLiteTable)
            {
                if (_sqlitetable.tableName == tablename)
                {
                    generateTableCode(_sqlitetable, provider);
                }
            }

        }
        static void generateCode(CodeDomProvider provider)
        {
            Global.Instance.SqliteDatabase.databaseName =  @"Z:\Share\EhsanAmid\GitHub\VP2FT\VP2FTConvert\SQLite";
            foreach (SQLiteTable _sqlitetable in Global.Instance.SqliteDatabase.listSQLiteTable)
            {
                generateTableCode(_sqlitetable, provider);
            }

        }
        static void generateTableCode(SQLiteTable _sqlitetable, CodeDomProvider provider)
        {


            string outputpath = @"Z:\Share\EhsanAmid\GitHub\VP2FT\VP2FTConvert\SQLite";
            string filename = Path.Combine(outputpath, _sqlitetable.tableName + "." + provider.FileExtension.Replace(".", ""));
            
            #region Making the output path

            

            //outputpath = Path.Combine(outputpath, db.name);
            if (!Directory.Exists(outputpath))
                Directory.CreateDirectory(outputpath);

            
            #endregion

            // Create a compile unit
            CodeCompileUnit compileUnit = new CodeCompileUnit();

            // Define a Namespace
            //CodeNamespace automobileNamespace = new CodeNamespace("Automobile");
            CodeNamespace sqlclassnamespace = new CodeNamespace();

            // Import Namespaces
            sqlclassnamespace.Imports.Add(new CodeNamespaceImport("System"));
            sqlclassnamespace.Imports.Add(new CodeNamespaceImport("System.Collections"));
            sqlclassnamespace.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            sqlclassnamespace.Imports.Add(new CodeNamespaceImport("System.ComponentModel"));
            sqlclassnamespace.Imports.Add(new CodeNamespaceImport("System.Data"));
            sqlclassnamespace.Imports.Add(new CodeNamespaceImport("System.Data.SqlClient"));
            sqlclassnamespace.Imports.Add(new CodeNamespaceImport("System.Data.SQLite"));
            sqlclassnamespace.Imports.Add(new CodeNamespaceImport("System.Text"));
            sqlclassnamespace.Imports.Add(new CodeNamespaceImport("System.Drawing"));
            sqlclassnamespace.Imports.Add(new CodeNamespaceImport("System.Windows.Forms"));
            compileUnit.Namespaces.Add(sqlclassnamespace);
            CodeNamespace SourceCodeNamespace = new CodeNamespace(NAMESPACESTRING);
            compileUnit.Namespaces.Add(SourceCodeNamespace);


            CodeTypeDeclaration tableclass_CodeTypeDeclaration = new CodeTypeDeclaration(_sqlitetable.tableName);
            if (CodeDomGenerator.MapDescription && !string.IsNullOrEmpty(_sqlitetable.Description))
                tableclass_CodeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression(_sqlitetable.Description))));

            tableclass_CodeTypeDeclaration.IsClass = true;
            tableclass_CodeTypeDeclaration.IsPartial = true;
            // Inherit a type
            tableclass_CodeTypeDeclaration.BaseTypes.Add("SQLiteTable");
            SourceCodeNamespace.Types.Add(tableclass_CodeTypeDeclaration);

            // Set custom attribute
            //CodeAttributeDeclaration codeAttribute = new CodeAttributeDeclaration("System.Serializable");
            //TableClass.CustomAttributes.Add(codeAttribute);
            // Set class attribute
            tableclass_CodeTypeDeclaration.IsClass = true;
            tableclass_CodeTypeDeclaration.TypeAttributes = TypeAttributes.Public;

            AddFieldSQLInsertString(_sqlitetable, tableclass_CodeTypeDeclaration);
            AddFieldSQLUpdateString(_sqlitetable, tableclass_CodeTypeDeclaration);
            AddFieldSQLSelectString(_sqlitetable, tableclass_CodeTypeDeclaration);
            AddFieldSQLDeleteString(_sqlitetable, tableclass_CodeTypeDeclaration);
            
            int StartRegion = 0;
            tableclass_CodeTypeDeclaration.Members[StartRegion].StartDirectives.Add(new CodeRegionDirective(CodeRegionMode.Start, "Static SQL String Memebers"));
            tableclass_CodeTypeDeclaration.Members[tableclass_CodeTypeDeclaration.Members.Count - 1].EndDirectives.Add(new CodeRegionDirective(CodeRegionMode.End, string.Empty));

            #region Constructor

            CodeConstructor codeconstructor = new CodeConstructor();
            codeconstructor.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression("Constructor"))));
            foreach (SQLiteTableColumn sqlitetablecolumn in _sqlitetable.listSQLiteTableColumn)
            {
                if (sqlitetablecolumn.IsForeignKey)
                {
                    codeconstructor.Parameters.Add(new CodeParameterDeclarationExpression(sqlitetablecolumn.ReferncedTablelist[0].TableName, "_parent"));


                    CodeFieldReferenceExpression codefieldreferenceexpression = new CodeFieldReferenceExpression();
                    codefieldreferenceexpression.FieldName = "_Parent_" + sqlitetablecolumn.ReferncedTablelist[0].TableName;

                    CodeAssignStatement AssignValue = new CodeAssignStatement(codefieldreferenceexpression, new CodeVariableReferenceExpression("_parent"));
                    codeconstructor.Statements.Add(AssignValue);
                }
                //"_" + _sqlitetablecolumn.ReferncedTablelist[0].ColumnName + "_" + _sqlitetablecolumn.ReferncedTablelist[0].TableName;
            }


            codeconstructor.Attributes = MemberAttributes.Public;


            tableclass_CodeTypeDeclaration.Members.Add(codeconstructor);

            #endregion
            

            StartRegion = tableclass_CodeTypeDeclaration.Members.Count;
            foreach (SQLiteTableColumn sqlitetablecolumn in _sqlitetable.listSQLiteTableColumn)
            {
                FieldColumnCreate(sqlitetablecolumn, tableclass_CodeTypeDeclaration);
                PropertyColumnCreate(sqlitetablecolumn, tableclass_CodeTypeDeclaration);
            }
            
            tableclass_CodeTypeDeclaration.Members[StartRegion].StartDirectives.Add(new CodeRegionDirective(CodeRegionMode.Start, "Tables Memebers"));
            tableclass_CodeTypeDeclaration.Members[tableclass_CodeTypeDeclaration.Members.Count - 1].EndDirectives.Add(new CodeRegionDirective(CodeRegionMode.End, string.Empty));

            if (_sqlitetable.HasForeignKey)
            {
                StartRegion = tableclass_CodeTypeDeclaration.Members.Count;
                foreach (SQLiteTableColumn sqlitetablecolumn in _sqlitetable.listSQLiteTableColumn)
                {
                    if (sqlitetablecolumn.IsForeignKey)
                    {
                        FieldRefObjectCreate(sqlitetablecolumn, tableclass_CodeTypeDeclaration);
                        PropertyRefObjectCreate(sqlitetablecolumn, tableclass_CodeTypeDeclaration);
                    }
                }
                tableclass_CodeTypeDeclaration.Members[StartRegion].StartDirectives.Add(new CodeRegionDirective(CodeRegionMode.Start, "Related Objects"));
                tableclass_CodeTypeDeclaration.Members[tableclass_CodeTypeDeclaration.Members.Count - 1].EndDirectives.Add(new CodeRegionDirective(CodeRegionMode.End, string.Empty));
            }


            if (_sqlitetable.IsReferenced)
            {
                StartRegion = tableclass_CodeTypeDeclaration.Members.Count;
                foreach (SQLiteTableColumn sqlitetablecolumn in _sqlitetable.listSQLiteTableColumn)
                {
                    if (sqlitetablecolumn.IsReference)
                    {
                        for (int i = 0; i < sqlitetablecolumn.ReferncedTablelist.Count; i++)
                        {
                            FieldRefObjectCollectionLockCreate(sqlitetablecolumn, i, tableclass_CodeTypeDeclaration);
                            FieldRefObjectCollectionCreate(sqlitetablecolumn,i, tableclass_CodeTypeDeclaration);
                            PropertyRefObjectCollectionCreate(sqlitetablecolumn,i, tableclass_CodeTypeDeclaration);
                            
                        }
                    }
                }
                if (StartRegion < tableclass_CodeTypeDeclaration.Members.Count)
                {
                    tableclass_CodeTypeDeclaration.Members[StartRegion].StartDirectives.Add(new CodeRegionDirective(CodeRegionMode.Start, "Collection Objects"));
                    tableclass_CodeTypeDeclaration.Members[tableclass_CodeTypeDeclaration.Members.Count - 1].EndDirectives.Add(new CodeRegionDirective(CodeRegionMode.End, string.Empty));
                }
            }


            StartRegion = tableclass_CodeTypeDeclaration.Members.Count;
			DeleteMethod(_sqlitetable.tableName, tableclass_CodeTypeDeclaration);
            SelectMethod(_sqlitetable.tableName, tableclass_CodeTypeDeclaration);
            InsertMethod(_sqlitetable, tableclass_CodeTypeDeclaration);
            UpdateMethod(_sqlitetable.tableName, tableclass_CodeTypeDeclaration);

            if (_sqlitetable.IsReferenced)
            {
                //StartRegion = tableclass_CodeTypeDeclaration.Members.Count;
                foreach (SQLiteTableColumn sqlitetablecolumn in _sqlitetable.listSQLiteTableColumn)
                {
                    if (sqlitetablecolumn.IsReference)
                    {
                        for (int i = 0; i < sqlitetablecolumn.ReferncedTablelist.Count; i++)
                        {
                        	LoadMethod(_sqlitetable.tableName,sqlitetablecolumn, i, tableclass_CodeTypeDeclaration);
                            
                            
                        }
                    }
                }
                //if (StartRegion < tableclass_CodeTypeDeclaration.Members.Count)
                {
                    //tableclass_CodeTypeDeclaration.Members[StartRegion].StartDirectives.Add(new CodeRegionDirective(CodeRegionMode.Start, "Collection Objects"));
                //    tableclass_CodeTypeDeclaration.Members[tableclass_CodeTypeDeclaration.Members.Count - 1].EndDirectives.Add(new CodeRegionDirective(CodeRegionMode.End, string.Empty));
                }
            }
            
            //ConstructorMethod(_sqlitetable, tableclass_CodeTypeDeclaration);

          	tableclass_CodeTypeDeclaration.Members[StartRegion].StartDirectives.Add(new CodeRegionDirective(CodeRegionMode.Start, "Public Methods"));
            tableclass_CodeTypeDeclaration.Members[tableclass_CodeTypeDeclaration.Members.Count - 1].EndDirectives.Add(new CodeRegionDirective(CodeRegionMode.End, string.Empty));

            StartRegion = tableclass_CodeTypeDeclaration.Members.Count;

            //DbType Sqltype;
            //Sqltype = DbType.

            GetSqlParameters(_sqlitetable, tableclass_CodeTypeDeclaration);
            AddFromRecordSetMethod(_sqlitetable, tableclass_CodeTypeDeclaration);

            AddFromString(_sqlitetable, tableclass_CodeTypeDeclaration);

            tableclass_CodeTypeDeclaration.Members[StartRegion].StartDirectives.Add(new CodeRegionDirective(CodeRegionMode.Start, "Private Methods"));
            tableclass_CodeTypeDeclaration.Members[tableclass_CodeTypeDeclaration.Members.Count - 1].EndDirectives.Add(new CodeRegionDirective(CodeRegionMode.End, string.Empty));


            Classdelegate( _sqlitetable,  SourceCodeNamespace);


           // ClassCollection(_sqlitetable, SourceCodeNamespace);

            CodeGeneratorOptions options = new CodeGeneratorOptions();
            //options.BracingStyle = "C";
            options.BlankLinesBetweenMembers = true;
            options.BracingStyle = "C";
            options.IndentString = "\t";
            options.VerbatimOrder = true;
            using (StreamWriter sourceWriter = new StreamWriter(filename))
            {
                provider.GenerateCodeFromCompileUnit(compileUnit, sourceWriter, options);
            }
        }


        

        public static void AddFieldSQLSelectString(SQLiteTable _sqlitetable, CodeTypeDeclaration tableclass_CodeTypeDeclaration)
        {
            CodeMemberField codememberfield = new CodeMemberField();
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

            codememberfield.Attributes = MemberAttributes.Assembly | MemberAttributes.Static;
            codememberfield.Type = new CodeTypeReference(typeof(string));
            codememberfield.Name = "SQL_Select";
            if (_sqlitetable.HasPrimaryKey)
                codememberfield.Comments.Add(new CodeCommentStatement("<remarks>This _databasename represents the full SELECT string for the table " + _sqlitetable.tableName + ", with the WHERE clause.</remarks>", true));
            else
                codememberfield.Comments.Add(new CodeCommentStatement("<remarks>This _databasename represents the full SELECT string for the table " + _sqlitetable.tableName + ", without any WHERE clause.</remarks>", true));
            codememberfield.InitExpression = new CodePrimitiveExpression(selstr);
            //codememberfield.InitExpression = new CodePrimitiveExpression("SELECT [DisplayID], Value,[SymbolID], [oIndex], [DlgType], [DlgIndex], [Left], [Top],   ");
            tableclass_CodeTypeDeclaration.Members.Add(codememberfield);

        }

        public static void AddFieldSQLInsertString(SQLiteTable _sqlitetable, CodeTypeDeclaration tableclass_CodeTypeDeclaration)
        {
            CodeMemberField codememberfield = new CodeMemberField();
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
            insstr += ") ; select last_insert_rowid(); ";
            codememberfield.Attributes = MemberAttributes.Assembly | MemberAttributes.Static;
            codememberfield.Type = new CodeTypeReference(typeof(string));
            codememberfield.Name = "SQL_Insert";
            codememberfield.Comments.Add(new CodeCommentStatement("<remarks>This _databasename represents the full INSERT INTO string for the table " + _sqlitetable.tableName + ".</remarks>", true));
            codememberfield.InitExpression = new CodePrimitiveExpression(insstr);

            tableclass_CodeTypeDeclaration.Members.Add(codememberfield);
        }
        public static void AddFieldSQLUpdateString(SQLiteTable _sqlitetable, CodeTypeDeclaration tableclass_CodeTypeDeclaration)
        {
            CodeMemberField codememberfield = new CodeMemberField();
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

            codememberfield.Attributes = MemberAttributes.Assembly | MemberAttributes.Static;
            codememberfield.Type = new CodeTypeReference(typeof(string));
            codememberfield.Name = "SQL_Update";
            if (_sqlitetable.HasPrimaryKey)
                codememberfield.Comments.Add(new CodeCommentStatement("<remarks>This _databasename represents the full UPDATE string for the table " + _sqlitetable.tableName + ", with the WHERE clause.</remarks>", true));
            else
                codememberfield.Comments.Add(new CodeCommentStatement("<remarks>This _databasename represents the full UPDATE string for the table " + _sqlitetable.tableName + ", without any WHERE clause.</remarks>", true));
            codememberfield.InitExpression = new CodePrimitiveExpression(updstr);
            tableclass_CodeTypeDeclaration.Members.Add(codememberfield);
        }


        public static void AddFieldSQLDeleteString(SQLiteTable _sqlitetable, CodeTypeDeclaration tableclass_CodeTypeDeclaration)
        {
            CodeMemberField codememberfield = new CodeMemberField();
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

            codememberfield.Attributes = MemberAttributes.Assembly | MemberAttributes.Static;
            codememberfield.Type = new CodeTypeReference(typeof(string));
            codememberfield.Name = "SQL_Delete";
            if (_sqlitetable.HasPrimaryKey)
                codememberfield.Comments.Add(new CodeCommentStatement("<remarks>This _databasename represents the DELETE string for the table " + _sqlitetable.tableName + ", with the WHERE clause.</remarks>", true));
            else
                codememberfield.Comments.Add(new CodeCommentStatement("<remarks>This _databasename represents the DELETE string for the table " + _sqlitetable.tableName + ", with WHERE clause if there is any Primary keys on the table.</remarks>", true));
            codememberfield.InitExpression = new CodePrimitiveExpression(delstr);

            tableclass_CodeTypeDeclaration.Members.Add(codememberfield);
        }

        public static void FieldColumnCreate(SQLiteTableColumn _sqlitetablecolumn, CodeTypeDeclaration tableclass_CodeTypeDeclaration)
        {
            CodeMemberField codememberfield = new CodeMemberField();
            codememberfield.Attributes = MemberAttributes.Final | CodeDomGenerator.GetModifier(CodeDomGenerator.FieldModifier);
            //CodeTypeReference thistype = new CodeTypeReference(_sqlitetablecolumn.NetType);
            //thistype.BaseType = "System.Nullable";
            codememberfield.Type = new CodeTypeReference(_sqlitetablecolumn.NetType);
            codememberfield.Name = "_"+_sqlitetablecolumn.ColumnName;
            if (_sqlitetablecolumn.NetType == typeof(string))
            {
                codememberfield.InitExpression = new CodeSnippetExpression("\"\"");
            }
            if (_sqlitetablecolumn.NetType == typeof(long))
            {
                codememberfield.InitExpression = new CodeSnippetExpression("-1");
            }
            

            if (CodeDomGenerator.AddComments)
                codememberfield.Comments.Add(new CodeCommentStatement("<remarks>SQL Type:" + _sqlitetablecolumn.NetType + "</remarks>", true));

            tableclass_CodeTypeDeclaration.Members.Add(codememberfield);
        }


        public static void PropertyColumnCreate(SQLiteTableColumn _sqlitetablecolumn, CodeTypeDeclaration tableclass_CodeTypeDeclaration)
        {
            CodeMemberProperty codememberproperty = new CodeMemberProperty();
            codememberproperty.Attributes = MemberAttributes.Final | CodeDomGenerator.GetModifier(CodeDomGenerator.PropertyModifier);
            //if (CodeDomGenerator.MapDescription && !string.IsNullOrEmpty(SQLObject.Description))
            //    codememberproperty.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression(SQLObject.Description.Trim()))));
            codememberproperty.CustomAttributes.Add(new CodeAttributeDeclaration("DisplayName", new CodeAttributeArgument(new CodePrimitiveExpression(CodeDomGenerator.TryCorrectName(_sqlitetablecolumn.ColumnName)))));
            if (_sqlitetablecolumn.IsPrimaryKey && !_sqlitetablecolumn.IsForeignKey)
                codememberproperty.CustomAttributes.Add(new CodeAttributeDeclaration("Category", new CodeAttributeArgument(new CodePrimitiveExpression("Primary Key"))));
            else if (!_sqlitetablecolumn.IsPrimaryKey && _sqlitetablecolumn.IsForeignKey)
                codememberproperty.CustomAttributes.Add(new CodeAttributeDeclaration("Category", new CodeAttributeArgument(new CodePrimitiveExpression("Foreign Key"))));
            else if (_sqlitetablecolumn.IsPrimaryKey && _sqlitetablecolumn.IsForeignKey)
                codememberproperty.CustomAttributes.Add(new CodeAttributeDeclaration("Category", new CodeAttributeArgument(new CodePrimitiveExpression("Primary and Foreign Key"))));
            else
                codememberproperty.CustomAttributes.Add(new CodeAttributeDeclaration("Category", new CodeAttributeArgument(new CodePrimitiveExpression("Column"))));
            codememberproperty.Name = _sqlitetablecolumn.ColumnName;
            codememberproperty.Type = new CodeTypeReference(_sqlitetablecolumn.NetType);
            codememberproperty.HasGet = true;

            
            CodeFieldReferenceExpression codefieldreferenceexpression = new CodeFieldReferenceExpression();
            codefieldreferenceexpression.FieldName = "_" + _sqlitetablecolumn.ColumnName;
            CodeMethodReturnStatement codemethodreturnstatement = new CodeMethodReturnStatement(codefieldreferenceexpression);
            codememberproperty.GetStatements.Add(codemethodreturnstatement);

            CodeFieldReferenceExpression codefieldreferenceexpressionleft = new CodeFieldReferenceExpression();
            codefieldreferenceexpressionleft.FieldName = "_" + _sqlitetablecolumn.ColumnName;

            CodeAssignStatement AssignValue = new CodeAssignStatement(codefieldreferenceexpressionleft, new CodeVariableReferenceExpression("value"));
            codememberproperty.SetStatements.Add(AssignValue);

            codememberproperty.HasSet = true;
            tableclass_CodeTypeDeclaration.Members.Add(codememberproperty);
            
        }

        public static void ListobjectCreate(SQLiteTable _sqlitetable, CodeTypeDeclaration tableclass_CodeTypeDeclaration)
        {
            CodeMemberField codememberfield = new CodeMemberField();
            codememberfield.Attributes = MemberAttributes.Final | CodeDomGenerator.GetModifier(CodeDomGenerator.FieldModifier);

            //CodeTypeReference ListParms = new CodeTypeReference("List", new CodeTypeReference(_sqlitetable.tableName));
            codememberfield.InitExpression = new CodeObjectCreateExpression(new CodeTypeReference("List", new CodeTypeReference(_sqlitetable.tableName)), new CodeExpression[] { });

            codememberfield.Type = new CodeTypeReference("List", new CodeTypeReference(_sqlitetable.tableName));
            codememberfield.Name = "list";

            tableclass_CodeTypeDeclaration.Members.Add(codememberfield);

        }

        public static void FieldRefObjectCreate(SQLiteTableColumn _sqlitetablecolumn, CodeTypeDeclaration tableclass_CodeTypeDeclaration)
        {

            CodeMemberField codememberfield = new CodeMemberField();
            codememberfield.Attributes = MemberAttributes.Final | CodeDomGenerator.GetModifier(CodeDomGenerator.FieldModifier);
            codememberfield.Type = new CodeTypeReference(_sqlitetablecolumn.ReferncedTablelist[0].TableName);
            //codememberfield.Name = "_" + _sqlitetablecolumn.ReferncedTablelist[0].ColumnName + "_" + _sqlitetablecolumn.ReferncedTablelist[0].TableName;
            codememberfield.Name = "_Parent_"  + _sqlitetablecolumn.ReferncedTablelist[0].TableName;
            if (CodeDomGenerator.AddComments)
                codememberfield.Comments.Add(new CodeCommentStatement("<remarks>Represents the foreign key object</remarks>", true));

            tableclass_CodeTypeDeclaration.Members.Add(codememberfield);
        }

        public static void PropertyRefObjectCreate(SQLiteTableColumn _sqlitetablecolumn, CodeTypeDeclaration tableclass_CodeTypeDeclaration)
        {
            CodeMemberProperty codememberproperty = new CodeMemberProperty();
            codememberproperty.Attributes = MemberAttributes.Final | CodeDomGenerator.GetModifier(CodeDomGenerator.PropertyModifier);
            codememberproperty.Type = new CodeTypeReference(_sqlitetablecolumn.ReferncedTablelist[0].TableName);
            if (CodeDomGenerator.MapDescription)
                codememberproperty.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression("Represents the foreign key object of the type " + _sqlitetablecolumn.ReferncedTablelist[0].ColumnName))));
            //codememberproperty.Name = "m_"+_sqlitetablecolumn.ReferncedTablelist[0].ColumnName + "_" + _sqlitetablecolumn.ReferncedTablelist[0].TableName;
            codememberproperty.Name = "m_Parent_" + _sqlitetablecolumn.ReferncedTablelist[0].TableName;
            codememberproperty.HasGet = true;

            CodeFieldReferenceExpression codefieldreferenceexpression = new CodeFieldReferenceExpression();
            //codefieldreferenceexpression.FieldName = "_" + _sqlitetablecolumn.ReferncedTablelist[0].ColumnName + "_" + _sqlitetablecolumn.ReferncedTablelist[0].TableName;
            codefieldreferenceexpression.FieldName = "_Parent_"  + _sqlitetablecolumn.ReferncedTablelist[0].TableName;
            CodeMethodReturnStatement codemethodreturnstatement = new CodeMethodReturnStatement(codefieldreferenceexpression);

            codememberproperty.GetStatements.Add(codemethodreturnstatement);


            CodeFieldReferenceExpression codefieldreferenceexpressionleft = new CodeFieldReferenceExpression();
            //codefieldreferenceexpressionleft.FieldName = "_" + _sqlitetablecolumn.ReferncedTablelist[0].ColumnName + "_" + _sqlitetablecolumn.ReferncedTablelist[0].TableName;
			codefieldreferenceexpressionleft.FieldName =  "_Parent_" + _sqlitetablecolumn.ReferncedTablelist[0].TableName;
            CodeAssignStatement AssignValue = new CodeAssignStatement(codefieldreferenceexpressionleft, new CodeVariableReferenceExpression("value"));
            codememberproperty.SetStatements.Add(AssignValue);

            codememberproperty.HasSet = true;
            
            tableclass_CodeTypeDeclaration.Members.Add(codememberproperty);

           

        }

        public static void FieldRefObjectCollectionLockCreate(SQLiteTableColumn _sqlitetablecolumn, int index, CodeTypeDeclaration tableclass_CodeTypeDeclaration)
        {
            CodeMemberField codememberfield = new CodeMemberField();
            codememberfield.Attributes = MemberAttributes.Final | CodeDomGenerator.GetModifier(CodeDomGenerator.FieldModifier) ;
            codememberfield.Type = new CodeTypeReference("readonly object"); //new CodeTypeReference("System.object");
            codememberfield.Name = "_" + _sqlitetablecolumn.ReferncedTablelist[index].TableName + "CollectionLock";
            codememberfield.InitExpression = new CodeObjectCreateExpression(new CodeTypeReference("System.object"));
            if (CodeDomGenerator.AddComments)
                codememberfield.Comments.Add(new CodeCommentStatement("<remarks>Lock for accessing collection</remarks>", true));

            tableclass_CodeTypeDeclaration.Members.Add(codememberfield);

        }
        
        
        
        public static void FieldRefObjectCollectionCreate(SQLiteTableColumn _sqlitetablecolumn,int index,  CodeTypeDeclaration tableclass_CodeTypeDeclaration)
        {    	
        	CodeMemberField codememberfield = new CodeMemberField();
            codememberfield.Attributes = MemberAttributes.Final | CodeDomGenerator.GetModifier(CodeDomGenerator.FieldModifier);
            codememberfield.Type = new CodeTypeReference("List", new CodeTypeReference(_sqlitetablecolumn.ReferncedTablelist[index].TableName));
            codememberfield.Name ="_" +_sqlitetablecolumn.ReferncedTablelist[index].TableName + "Collection";
            tableclass_CodeTypeDeclaration.Members.Add(codememberfield);
         }
             
        
        public static void PropertyRefObjectCollectionCreate(SQLiteTableColumn _sqlitetablecolumn,int index, CodeTypeDeclaration tableclass_CodeTypeDeclaration)
        {
            CodeMemberProperty codememberproperty = new CodeMemberProperty();
            codememberproperty.Attributes = MemberAttributes.Final | CodeDomGenerator.GetModifier(CodeDomGenerator.PropertyModifier);
            codememberproperty.Type = new CodeTypeReference("List", new CodeTypeReference(_sqlitetablecolumn.ReferncedTablelist[index].TableName));
            if (CodeDomGenerator.MapDescription)
                codememberproperty.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression("Represents the foreign key object of the type " + _sqlitetablecolumn.ReferncedTablelist[index].ColumnName))));
            codememberproperty.Name = "m_"+_sqlitetablecolumn.ReferncedTablelist[index].TableName + "Collection";
            codememberproperty.HasGet = true;

            CodeFieldReferenceExpression codefieldreferenceexpression = new CodeFieldReferenceExpression();
            codefieldreferenceexpression.FieldName = "_" +_sqlitetablecolumn.ReferncedTablelist[index].TableName + "Collection";
            string temp = _sqlitetablecolumn.ReferncedTablelist[index].TableName + "Collection";
            
            
            CodeTypeReference ListParms = new CodeTypeReference("List", new CodeTypeReference(_sqlitetablecolumn.ReferncedTablelist[index].TableName));
           // codemembermethod.Statements.Add(new CodeVariableDeclarationStatement(ListParms, "SqlParmColl", new CodeObjectCreateExpression(ListParms, new CodeExpression[] { })));

            
            CodeVariableReferenceExpression CodeVariableReferenceExpression = new CodeVariableReferenceExpression(codefieldreferenceexpression.FieldName + " == null");
            CodeAssignStatement as1 = new CodeAssignStatement(new CodeVariableReferenceExpression(codefieldreferenceexpression.FieldName), new CodeObjectCreateExpression(ListParms, new CodeExpression[] { }));
            //CodeExpressionStatement as2 = new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("_" + _sqlitetablecolumn.ReferncedTablelist[index].TableName + "Collection"), "Load"));
            
//            CodeMethodInvokeExpression cmre = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "Load");
//            CodeExpressionStatement as2 = new CodeExpressionStatement(cmre);
            //codeWhile.Statements.Add(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(null, "AddFromRecordSet"), new CodeExpression[] { new CodeVariableReferenceExpression("rs") }));
            
            
            CodeConditionStatement IfLength = new CodeConditionStatement(CodeVariableReferenceExpression, new CodeStatement[] { as1 /* ,as2*/});
            
            
            CodeSnippetStatement snippet1 = new CodeSnippetStatement();
            snippet1.Value = "              lock(_" + temp+"Lock)";
            CodeSnippetStatement snippet2 = new CodeSnippetStatement();
            snippet2.Value = "              {";
           
            codememberproperty.GetStatements.Add(snippet1);
            codememberproperty.GetStatements.Add(snippet2);
            
            
            codememberproperty.GetStatements.Add(IfLength);

            CodeSnippetStatement snippet3 = new CodeSnippetStatement();
            
            
            
            CodeMethodReturnStatement codemethodreturnstatement = new CodeMethodReturnStatement(codefieldreferenceexpression);
            

            codememberproperty.GetStatements.Add(codemethodreturnstatement);

            snippet3.Value = "              }";

            codememberproperty.GetStatements.Add(snippet3);

            CodeFieldReferenceExpression codefieldreferenceexpressionleft = new CodeFieldReferenceExpression();
            codefieldreferenceexpressionleft.FieldName = "_" +_sqlitetablecolumn.ReferncedTablelist[index].TableName + "Collection";

            CodeAssignStatement AssignValue = new CodeAssignStatement(codefieldreferenceexpressionleft, new CodeVariableReferenceExpression("value"));
            codememberproperty.SetStatements.Add(AssignValue);

            codememberproperty.HasSet = true;
            
            tableclass_CodeTypeDeclaration.Members.Add(codememberproperty);

           

        }

        public static void SelectMethod(string _tablename,CodeTypeDeclaration tableclass_CodeTypeDeclaration)
        {
            CodeMemberMethod codemembermethod = new CodeMemberMethod();
            codemembermethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            codemembermethod.Name = "Select";

            codemembermethod.ReturnType = new CodeTypeReference(typeof(Int32));
            //codemembermethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)), "ConnectionString"));

            CodeTryCatchFinallyStatement Try = new CodeTryCatchFinallyStatement();


            CodeCatchClause Catch = new CodeCatchClause("ex", new CodeTypeReference("SQLiteException"));
            Catch.Statements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("ex.ErrorCode")));
            Try.CatchClauses.Add(Catch);

            
            CodeVariableReferenceExpression CodeVariableReferenceExpression = new CodeVariableReferenceExpression("Common.Conn == null");
            CodeAssignStatement as1 = new CodeAssignStatement( new CodeSnippetExpression ("Common.Conn"), new CodeObjectCreateExpression("SQLiteConnection", new CodeFieldReferenceExpression(null, "Common.ConnectionString") ) );
            CodeExpressionStatement as2 = new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Common.Conn"), "Open"));
            CodeConditionStatement IfLength = new CodeConditionStatement(CodeVariableReferenceExpression, new CodeStatement[] { as1, as2 });

            Try.TryStatements.Add(IfLength);
           
			 Try.TryStatements.Add(new CodeVariableDeclarationStatement("SQLiteCommand", "Com", new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Common.Conn"), "CreateCommand")));
            Try.TryStatements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeTypeReferenceExpression("Com"), "CommandText"), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(_tablename), "SQL_Select")));

            CodeMethodInvokeExpression cmieAddRange = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com.Parameters"), "AddRange");
            cmieAddRange.Parameters.Add(new CodeMethodInvokeExpression(null, "GetSqlParameters", new CodeExpression[] { }));
            Try.TryStatements.Add(cmieAddRange);

            //Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Open"));
            Try.TryStatements.Add(new CodeVariableDeclarationStatement("SQLiteDataReader", "rs", new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com"), "ExecuteReader")));

            CodeIterationStatement codeWhile = new CodeIterationStatement();
            codeWhile.TestExpression = new CodeSnippetExpression("rs.Read()");
            codeWhile.Statements.Add(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(null, "AddFromRecordSet"), new CodeExpression[] { new CodeVariableReferenceExpression("rs") }));
            codeWhile.IncrementStatement = new CodeSnippetStatement(null);
            codeWhile.InitStatement = new CodeSnippetStatement(null);
            Try.TryStatements.Add(codeWhile);

            Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("rs"), "Close"));
            //Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Close"));
            Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("rs"), "Dispose"));
            Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com"), "Dispose"));
           // Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Dispose"));
            Try.TryStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(0)));
            codemembermethod.Statements.Add(Try);

            tableclass_CodeTypeDeclaration.Members.Add(codemembermethod);
        }




        public static void InsertMethod(SQLiteTable sqlitetable, CodeTypeDeclaration tableclass_CodeTypeDeclaration)
        {
            string _tablename = sqlitetable.tableName;
            CodeMemberMethod codemembermethod = new CodeMemberMethod();
            codemembermethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            codemembermethod.Name = "Insert";

            //codemembermethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)), "ConnectionString"));

            codemembermethod.ReturnType = new CodeTypeReference(typeof(Int32));

            CodeTryCatchFinallyStatement Try = new CodeTryCatchFinallyStatement();
            //        catch (SqlException ae)
        //        {
        //            MessageBox.Show(ae.Message.ToString());
        //        }
           

            CodeCatchClause Catch = new CodeCatchClause("ex", new CodeTypeReference("SQLiteException"));
            Catch.Statements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("ex.ErrorCode")));
            Try.CatchClauses.Add(Catch);


            CodeVariableReferenceExpression CodeVariableReferenceExpression = new CodeVariableReferenceExpression("Common.Conn == null");
            CodeAssignStatement as1 = new CodeAssignStatement(new CodeSnippetExpression("Common.Conn"), new CodeObjectCreateExpression("SQLiteConnection", new CodeFieldReferenceExpression(null, "Common.ConnectionString")));
            CodeExpressionStatement as2 = new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Common.Conn"), "Open"));
            CodeConditionStatement IfLength = new CodeConditionStatement(CodeVariableReferenceExpression, new CodeStatement[] { as1, as2 });

            Try.TryStatements.Add(IfLength);
            Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "PreInsertTriger"));
            Try.TryStatements.Add(new CodeVariableDeclarationStatement("SQLiteCommand", "Com", new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Common.Conn"), "CreateCommand")));
            Try.TryStatements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeTypeReferenceExpression("Com"), "CommandText"), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(_tablename), "SQL_Insert")));

            CodeMethodInvokeExpression cmieAddRange = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com.Parameters"), "AddRange");
            cmieAddRange.Parameters.Add(new CodeMethodInvokeExpression(null, "GetSqlParameters", new CodeExpression[] { }));
            Try.TryStatements.Add(cmieAddRange);

            foreach (SQLiteTableColumn column in sqlitetable.listSQLiteTableColumn)
            {
                if (column.IsPrimaryKey)
                {

                    CodeCastExpression cc = new CodeCastExpression(typeof(System.Int64), new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Convert"), "ToInt64", new CodeExpression[] { new CodeVariableReferenceExpression("Com.ExecuteScalar()") }));
                    CodeAssignStatement cas = new CodeAssignStatement(new CodePropertyReferenceExpression(null, CodeDomGenerator.TryCorrectNameNoWhitespace(column.ColumnName)), cc);
                    Try.TryStatements.Add(cas);
                    break;
                }

            }


            //Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Close"));
            Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com"), "Dispose"));
            //Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Dispose"));
            Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "PostInsertTriger"));
            Try.TryStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(0)));
            //Try.TryStatements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("rowseffected")));  
            codemembermethod.Statements.Add(Try);
            tableclass_CodeTypeDeclaration.Members.Add(codemembermethod);
        }



        public static void UpdateMethod(string _tablename, CodeTypeDeclaration tableclass_CodeTypeDeclaration)
        {
            CodeMemberMethod codemembermethod = new CodeMemberMethod();
            codemembermethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            codemembermethod.Name = "Update";

            //codemembermethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)), "ConnectionString"));

            codemembermethod.ReturnType = new CodeTypeReference(typeof(Int32));

            CodeTryCatchFinallyStatement Try = new CodeTryCatchFinallyStatement();

            CodeCatchClause Catch = new CodeCatchClause("ex", new CodeTypeReference("SQLiteException"));
            Catch.Statements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("ex.ErrorCode")));
            Try.CatchClauses.Add(Catch);

            CodeVariableReferenceExpression CodeVariableReferenceExpression = new CodeVariableReferenceExpression("Common.Conn == null");
            CodeAssignStatement as1 = new CodeAssignStatement(new CodeSnippetExpression("Common.Conn"), new CodeObjectCreateExpression("SQLiteConnection", new CodeFieldReferenceExpression(null, "Common.ConnectionString")));
            CodeExpressionStatement as2 = new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Common.Conn"), "Open"));
            CodeConditionStatement IfLength = new CodeConditionStatement(CodeVariableReferenceExpression, new CodeStatement[] { as1, as2 });

            Try.TryStatements.Add(IfLength);
            Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "PreUpdateTriger"));
            Try.TryStatements.Add(new CodeVariableDeclarationStatement("SQLiteCommand", "Com", new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Common.Conn"), "CreateCommand")));
            Try.TryStatements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeTypeReferenceExpression("Com"), "CommandText"), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(_tablename), "SQL_Update")));

            CodeMethodInvokeExpression cmieAddRange = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com.Parameters"), "AddRange");
            cmieAddRange.Parameters.Add(new CodeMethodInvokeExpression(null, "GetSqlParameters", new CodeExpression[] { }));
            Try.TryStatements.Add(cmieAddRange);
            //Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Open"));
            Try.TryStatements.Add(new CodeVariableDeclarationStatement(typeof(int), "rowseffected", new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com"), "ExecuteNonQuery")));
            //Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Close"));
            Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com"), "Dispose"));
            //Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Dispose"));
            Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "PostUpdateTriger"));
            Try.TryStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(0)));

            codemembermethod.Statements.Add(Try);
            tableclass_CodeTypeDeclaration.Members.Add(codemembermethod);
        }



        public static void DeleteMethod(string _tablename, CodeTypeDeclaration tableclass_CodeTypeDeclaration)
        {
            CodeMemberMethod codemembermethod = new CodeMemberMethod();
            codemembermethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            codemembermethod.Name = "Delete";

            //codemembermethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)), "ConnectionString"));

            codemembermethod.ReturnType = new CodeTypeReference(typeof(Int32));

            CodeTryCatchFinallyStatement Try = new CodeTryCatchFinallyStatement();

            CodeCatchClause Catch = new CodeCatchClause("ex", new CodeTypeReference("SQLiteException"));
            Catch.Statements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("ex.ErrorCode")));
            Try.CatchClauses.Add(Catch);


            CodeVariableReferenceExpression CodeVariableReferenceExpression = new CodeVariableReferenceExpression("Common.Conn == null");
            CodeAssignStatement as1 = new CodeAssignStatement(new CodeSnippetExpression("Common.Conn"), new CodeObjectCreateExpression("SQLiteConnection", new CodeFieldReferenceExpression(null, "Common.ConnectionString")));
            CodeExpressionStatement as2 = new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Common.Conn"), "Open"));
            CodeConditionStatement IfLength = new CodeConditionStatement(CodeVariableReferenceExpression, new CodeStatement[] { as1, as2 });

            Try.TryStatements.Add(IfLength);

            
            Try.TryStatements.Add( new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "PreDeleteTriger")) ;
            //GetProperty.SetStatements.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "On" + _sqlitetable.tableName + "Changed", new CodeVariableReferenceExpression("EventArgs.Empty"))));

            Try.TryStatements.Add(new CodeVariableDeclarationStatement("SQLiteCommand", "Com", new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Common.Conn"), "CreateCommand")));
            Try.TryStatements.Add(new CodeVariableDeclarationStatement("SQLiteCommand", "ComSync", new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Common.Conn"), "CreateCommand")));
            Try.TryStatements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeTypeReferenceExpression("Com"), "CommandText"), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(_tablename), "SQL_Delete")));
            Try.TryStatements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeTypeReferenceExpression("ComSync"), "CommandText"), new CodeSnippetExpression( "\"PRAGMA foreign_keys=ON\"") ));

            CodeMethodInvokeExpression cmieAddRange = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com.Parameters"), "AddRange");
            cmieAddRange.Parameters.Add(new CodeMethodInvokeExpression(null, "GetSqlParameters", new CodeExpression[] { }));
            Try.TryStatements.Add(cmieAddRange);
            //Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Open"));

            Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("ComSync"), "ExecuteNonQuery"));
            
            Try.TryStatements.Add(new CodeVariableDeclarationStatement(typeof(int), "rowseffected", new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com"), "ExecuteNonQuery")));
            //Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Close"));
            Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("ComSync"), "Dispose"));
            Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com"), "Dispose"));
            //Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Dispose"));
            Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "PostDeleteTriger"));
            Try.TryStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(0)));

            codemembermethod.Statements.Add(Try);
            tableclass_CodeTypeDeclaration.Members.Add(codemembermethod);
        }

        
        public static void LoadMethod(string _tablename, SQLiteTableColumn _sqlitetablecolumn,int index, CodeTypeDeclaration tableclass_CodeTypeDeclaration)
        {
            CodeMemberMethod codemembermethod = new CodeMemberMethod();
            codemembermethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            codemembermethod.Name = "Load"+_sqlitetablecolumn.ReferncedTablelist[index].TableName;

            //codemembermethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)), "ConnectionString"));

            codemembermethod.ReturnType = new CodeTypeReference(typeof(bool));

            CodeTryCatchFinallyStatement Try = new CodeTryCatchFinallyStatement();

            CodeCatchClause Catch = new CodeCatchClause("ex", new CodeTypeReference("SQLiteException"));
            Catch.Statements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("ex.ErrorCode")));
            Try.CatchClauses.Add(Catch);


            CodeVariableReferenceExpression CodeVariableReferenceExpression = new CodeVariableReferenceExpression("Common.Conn == null");
            CodeAssignStatement as1 = new CodeAssignStatement(new CodeSnippetExpression("Common.Conn"), new CodeObjectCreateExpression("SQLiteConnection", new CodeFieldReferenceExpression(null, "Common.ConnectionString")));
            CodeExpressionStatement as2 = new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Common.Conn"), "Open"));
            CodeConditionStatement IfLength = new CodeConditionStatement(CodeVariableReferenceExpression, new CodeStatement[] { as1, as2 });

            Try.TryStatements.Add(IfLength);

            Try.TryStatements.Add(new CodeVariableDeclarationStatement("SQLiteDataReader", "myReader", new CodePrimitiveExpression(null)));
            Try.TryStatements.Add(new CodeVariableDeclarationStatement("SQLiteCommand", "myCommand", new CodeObjectCreateExpression("SQLiteCommand")));
            Try.TryStatements.Add(new CodeAssignStatement(new CodeVariableReferenceExpression("myReader"), new CodePrimitiveExpression(null)));
            string str =  "\"" + "SELECT * FROM [" + _sqlitetablecolumn.ReferncedTablelist[index].TableName+"]  WHERE ["+ _sqlitetablecolumn.ColumnName + "]= " + _sqlitetablecolumn.ColumnName + ";" + "\"";
            Try.TryStatements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeTypeReferenceExpression("myCommand"), "CommandText"), new CodeSnippetExpression( str) ));
            Try.TryStatements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeTypeReferenceExpression("myCommand"), "Connection"), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("Common"), "Conn")));
            Try.TryStatements.Add(new CodeAssignStatement(new CodeVariableReferenceExpression("myReader"), new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("myCommand"), "ExecuteReader")));
            
            
            CodeIterationStatement codeWhile = new CodeIterationStatement();
            codeWhile.TestExpression = new CodeSnippetExpression("myReader.Read()");
            str  = _sqlitetablecolumn.ReferncedTablelist[index].TableName;
            codeWhile.Statements.Add(new CodeVariableDeclarationStatement(str, str.ToLower(), new CodeObjectCreateExpression(str, new CodeThisReferenceExpression())));
            
            codeWhile.IncrementStatement = new CodeSnippetStatement(null);
            codeWhile.InitStatement = new CodeSnippetStatement(null);
            Try.TryStatements.Add(codeWhile);
            
//            Try.TryStatements.Add(new CodeVariableDeclarationStatement("SQLiteCommand", "Com", new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Common.Conn"), "CreateCommand")));
//            Try.TryStatements.Add(new CodeVariableDeclarationStatement("SQLiteCommand", "ComSync", new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Common.Conn"), "CreateCommand")));
            Try.TryStatements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeTypeReferenceExpression("Com"), "CommandText"), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(_tablename), "SQL_Delete")));
            Try.TryStatements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeTypeReferenceExpression("ComSync"), "CommandText"), new CodeSnippetExpression( "\"PRAGMA foreign_keys=ON\"") ));

            CodeMethodInvokeExpression cmieAddRange = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com.Parameters"), "AddRange");
            cmieAddRange.Parameters.Add(new CodeMethodInvokeExpression(null, "GetSqlParameters", new CodeExpression[] { }));
            Try.TryStatements.Add(cmieAddRange);
            //Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Open"));

            Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("ComSync"), "ExecuteNonQuery"));
            
            Try.TryStatements.Add(new CodeVariableDeclarationStatement(typeof(int), "rowseffected", new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com"), "ExecuteNonQuery")));
            //Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Close"));
            Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("ComSync"), "Dispose"));
            Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Com"), "Dispose"));
            //Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Conn"), "Dispose"));
            Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "PostDeleteTriger"));
            Try.TryStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(0)));

            codemembermethod.Statements.Add(Try);
            tableclass_CodeTypeDeclaration.Members.Add(codemembermethod);
        }
        
        public static void AddFromRecordSetMethod(SQLiteTable _sqlitetable, CodeTypeDeclaration tableclass_CodeTypeDeclaration)
        {
            CodeMemberMethod codemembermethod = new CodeMemberMethod();
            codemembermethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            codemembermethod.Name = "AddFromRecordSet";
            codemembermethod.Parameters.Add(new CodeParameterDeclarationExpression("SQLiteDataReader", "rs"));
            CodeMethodReferenceExpression IsDBNullMethod = new CodeMethodReferenceExpression(new CodeSnippetExpression("rs"), "IsDBNull");
            CodeTryCatchFinallyStatement Try = new CodeTryCatchFinallyStatement();

            #region ExceptionCatcher SQL

            CodeCatchClause SQLCatch = new CodeCatchClause("sqlExc", new CodeTypeReference("SQLiteException"));
            SQLCatch.Statements.Add(new CodeThrowExceptionStatement(new CodeVariableReferenceExpression("sqlExc")));

            Try.CatchClauses.Add(SQLCatch);

            CodeCatchClause Catch = new CodeCatchClause("Exc", new CodeTypeReference("Exception"));
            Catch.Statements.Add(new CodeThrowExceptionStatement(new CodeVariableReferenceExpression("Exc")));
            Try.CatchClauses.Add(Catch);

            #endregion

            int rscounter = 0;

            foreach (SQLiteTableColumn column in _sqlitetable.listSQLiteTableColumn)
            {
                if (column.ColumnType.ToLower() != "color")
                {
                    CodeConditionStatement ccs = new CodeConditionStatement();
                    CodeMethodReferenceExpression GetOrdinalMethod = new CodeMethodReferenceExpression(new CodeSnippetExpression("rs"), "GetOrdinal");
                    CodeMethodInvokeExpression InvokeGetOrdinalMethod = new CodeMethodInvokeExpression(GetOrdinalMethod, new CodeExpression[] { new CodePrimitiveExpression(column.ColumnName) });
                    CodeMethodInvokeExpression InvokeIsDBNullMethod = new CodeMethodInvokeExpression(IsDBNullMethod, new CodeExpression[] { InvokeGetOrdinalMethod });
                    ccs.Condition = new CodeBinaryOperatorExpression(InvokeIsDBNullMethod, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(false));
                    CodeMethodReferenceExpression RSReaderMethod = new CodeMethodReferenceExpression(new CodeSnippetExpression("rs"), CodeDomGenerator.SqlDataReaderGetMethod(column.NetType));
                    CodeMethodInvokeExpression InvokeRSReaderMethod = new CodeMethodInvokeExpression(RSReaderMethod, new CodeExpression[] { InvokeGetOrdinalMethod });

                    CodeAssignStatement cas = new CodeAssignStatement(new CodePropertyReferenceExpression(null, CodeDomGenerator.TryCorrectNameNoWhitespace(column.ColumnName)),
                                           new CodeCastExpression(column.NetType,
                                               new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Convert"), "ChangeType", new CodeExpression[] { new CodeArrayIndexerExpression(new CodeVariableReferenceExpression("rs"), new CodePrimitiveExpression(column.ColumnName)), new CodeTypeOfExpression(column.NetTypeColor) }
                                               )
                                               ));


                    ccs.TrueStatements.Add(cas);
                    Try.TryStatements.Add(new CodeCommentStatement("if value from the recordset, to the " + column.ColumnName + " _databasename is NOT null then set the value."));
                    Try.TryStatements.Add(ccs);
                    rscounter++;
                }
                else
                {
                    CodeConditionStatement ccs = new CodeConditionStatement();
                    CodeMethodReferenceExpression GetOrdinalMethod = new CodeMethodReferenceExpression(new CodeSnippetExpression("rs"), "GetOrdinal");
                    CodeMethodInvokeExpression InvokeGetOrdinalMethod = new CodeMethodInvokeExpression(GetOrdinalMethod, new CodeExpression[] { new CodePrimitiveExpression(column.ColumnName) });
                    CodeMethodInvokeExpression InvokeIsDBNullMethod = new CodeMethodInvokeExpression(IsDBNullMethod, new CodeExpression[] { InvokeGetOrdinalMethod });
                    ccs.Condition = new CodeBinaryOperatorExpression(InvokeIsDBNullMethod, CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(false));
                    CodeMethodReferenceExpression RSReaderMethod = new CodeMethodReferenceExpression(new CodeSnippetExpression("rs"), CodeDomGenerator.SqlDataReaderGetMethod(column.NetType));
                    CodeMethodInvokeExpression InvokeRSReaderMethod = new CodeMethodInvokeExpression(RSReaderMethod, new CodeExpression[] { InvokeGetOrdinalMethod });
                    CodePropertyReferenceExpression cpre = new CodePropertyReferenceExpression(null, CodeDomGenerator.TryCorrectNameNoWhitespace(column.ColumnName));
                    CodeCastExpression cce = new CodeCastExpression(typeof(System.Int32), new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Convert"), "ChangeType", new CodeExpression[] { new CodeArrayIndexerExpression(new CodeVariableReferenceExpression("rs"), new CodePrimitiveExpression(column.ColumnName)), new CodeTypeOfExpression(column.NetTypeColor) }));
                    CodeMethodInvokeExpression cmie = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Color"), "FromArgb", new CodeExpression[] { cce });
                    CodeAssignStatement cas = new CodeAssignStatement(cpre, cmie);
                    ccs.TrueStatements.Add(cas);
                    Try.TryStatements.Add(new CodeCommentStatement("if value from the recordset, to the " + column.ColumnName + " _databasename is NOT null then set the value."));
                    Try.TryStatements.Add(ccs);
                    rscounter++;
                }
            }
            codemembermethod.Statements.Add(Try);

            tableclass_CodeTypeDeclaration.Members.Add(codemembermethod);
        }


        public static void AddFromString(SQLiteTable _sqlitetable, CodeTypeDeclaration tableclass_CodeTypeDeclaration)
        {
            CodeMemberMethod codemembermethod = new CodeMemberMethod();
            codemembermethod.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            codemembermethod.Name = "AddFromString";

            CodeParameterDeclarationExpression param1 = new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string[])), "_strs");
            codemembermethod.Parameters.Add(param1);
            CodeParameterDeclarationExpression param2 = new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)), "arg1");
            codemembermethod.Parameters.Add(param2);
            CodeParameterDeclarationExpression param3 = new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(string)), "_log");
            param3.Direction = FieldDirection.Ref;
            codemembermethod.Parameters.Add(param3);

            CodeMethodReferenceExpression IsDBNullMethod = new CodeMethodReferenceExpression(new CodeSnippetExpression("rs"), "IsDBNull");
            CodeTryCatchFinallyStatement Try = new CodeTryCatchFinallyStatement();

            #region ExceptionCatcher SQL

            CodeCatchClause SQLCatch = new CodeCatchClause("sqlExc", new CodeTypeReference("SQLiteException"));
            SQLCatch.Statements.Add(new CodeThrowExceptionStatement(new CodeVariableReferenceExpression("sqlExc")));

            Try.CatchClauses.Add(SQLCatch);

            CodeCatchClause Catch = new CodeCatchClause("Exc", new CodeTypeReference("Exception"));
            Catch.Statements.Add(new CodeThrowExceptionStatement(new CodeVariableReferenceExpression("Exc")));
            Try.CatchClauses.Add(Catch);

            #endregion

            //   int rscounter = 0;



           
            ///////////////////////////////////////////////


            // Declares a parameter passed by reference using a CodeDirectionExpression.
            CodeDirectionExpression paramlog = new CodeDirectionExpression(FieldDirection.Ref, new CodeVariableReferenceExpression( "_log"));
            // Invokes a method on this named TestMethod using the direction expression as a parameter.
            //CodeMethodInvokeExpression methodInvoke1 = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "TestMethod", paramlog);



           // CodeVariableReferenceExpression ss = new CodeVariableReferenceExpression("_log");
            Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "PreAddFromString", paramlog));



            Try.TryStatements.Add(new CodeVariableDeclarationStatement(typeof(int), "i"));
            foreach (SQLiteTableColumn column in _sqlitetable.listSQLiteTableColumn)
            {
                if (!column.IsPrimaryKey)
                {
                    CodeVariableReferenceExpression cex1 = new CodeVariableReferenceExpression("i");
                    CodeMethodInvokeExpression cex2 = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "ColumnExistInHeader", new CodeExpression[] { new CodePrimitiveExpression(column.ColumnName) });
                    CodeAssignStatement cas1 = new CodeAssignStatement(cex1, cex2);
                    Try.TryStatements.Add(cas1);
                    CodeConditionStatement ccs = new CodeConditionStatement();
                    CodeMethodReferenceExpression GetOrdinalMethod = new CodeMethodReferenceExpression(new CodeSnippetExpression("rs"), "GetOrdinal");
                    CodeMethodInvokeExpression InvokeGetOrdinalMethod = new CodeMethodInvokeExpression(GetOrdinalMethod, new CodeExpression[] { new CodePrimitiveExpression(column.ColumnName) });
                    ccs.Condition = new CodeBinaryOperatorExpression(new CodeVariableReferenceExpression("i"), CodeBinaryOperatorType.GreaterThanOrEqual, new CodePrimitiveExpression(0));
                    CodeAssignStatement cas;
                    if (column.ColumnType.ToLower() != "color")
                    {

                        //CodeMethodReferenceExpression RSReaderMethod = new CodeMethodReferenceExpression(new CodeSnippetExpression("rs"), CodeDomGenerator.SqlDataReaderGetMethod(column.NetType));
                        //CodeMethodInvokeExpression InvokeRSReaderMethod = new CodeMethodInvokeExpression(RSReaderMethod, new CodeExpression[] { InvokeGetOrdinalMethod });
                        cas = new CodeAssignStatement(new CodePropertyReferenceExpression(null, CodeDomGenerator.TryCorrectNameNoWhitespace(column.ColumnName)),
                                              new CodeCastExpression(column.NetType, new CodeMethodInvokeExpression(
                                                  new CodeTypeReferenceExpression("Convert"), "ChangeType", new CodeExpression[] { new CodeArrayIndexerExpression(new CodeVariableReferenceExpression("_strs"), new CodeVariableReferenceExpression("i")), new CodeTypeOfExpression(column.NetTypeColor) })
                                                  ));

                    }
                    else
                    {

                        //CodeMethodReferenceExpression RSReaderMethod = new CodeMethodReferenceExpression(new CodeSnippetExpression("rs"), CodeDomGenerator.SqlDataReaderGetMethod(column.NetType));
                        //CodeMethodInvokeExpression InvokeRSReaderMethod = new CodeMethodInvokeExpression(RSReaderMethod, new CodeExpression[] { InvokeGetOrdinalMethod });
                        CodePropertyReferenceExpression cpre = new CodePropertyReferenceExpression(null, CodeDomGenerator.TryCorrectNameNoWhitespace(column.ColumnName));

                        CodeCastExpression cce = new CodeCastExpression(typeof(System.Int32), new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Convert"), "ChangeType", new CodeExpression[] { new CodeArrayIndexerExpression(new CodeVariableReferenceExpression("_strs"), new CodeVariableReferenceExpression("i")), new CodeTypeOfExpression(column.NetTypeColor) }));

                        CodeMethodInvokeExpression cmie = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("Color"), "FromArgb", new CodeExpression[] { cce });
                        cas = new CodeAssignStatement(cpre, cmie);

                    }

                    ccs.TrueStatements.Add(cas);
                    Try.TryStatements.Add(ccs);
                    //  rscounter++;
                }
            }

            Try.TryStatements.Add(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "PostAddFromString", paramlog));
            codemembermethod.Statements.Add(Try);

            tableclass_CodeTypeDeclaration.Members.Add(codemembermethod);
        }

        public static void GetSqlParameters(SQLiteTable _sqlitetable, CodeTypeDeclaration tableclass_CodeTypeDeclaration)
        {
            CodeMemberMethod codemembermethod = new CodeMemberMethod();
            codemembermethod.ReturnType = new CodeTypeReference("SQLiteParameter", 1);
            codemembermethod.Name = "GetSqlParameters";

            CodeTypeReference ListParms = new CodeTypeReference("List", new CodeTypeReference("SQLiteParameter"));
            codemembermethod.Statements.Add(new CodeVariableDeclarationStatement(ListParms, "SqlParmColl", new CodeObjectCreateExpression(ListParms, new CodeExpression[] { })));

            CodeTryCatchFinallyStatement Try = new CodeTryCatchFinallyStatement();

            #region ExceptionCatcher

            CodeCatchClause Catch = new CodeCatchClause("Exc", new CodeTypeReference("SQLiteException"));
            Catch.Statements.Add(new CodeThrowExceptionStatement(new CodeVariableReferenceExpression("Exc")));
            Try.CatchClauses.Add(Catch);

            #endregion
            //SqlParmColl.Add(CommonDB.AddSqlParm("@BoarderColor1", BoarderColor1.ToArgb(), DbType.Int32));
            foreach (SQLiteTableColumn column in _sqlitetable.listSQLiteTableColumn)
            {
                CodeMethodInvokeExpression cmie = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("CommonDB"), "AddSqlParm");
                cmie.Parameters.Add(new CodePrimitiveExpression("@" + column.ColumnName));
                if (column.ColumnType.ToLower() == "color")
                {

                    cmie.Parameters.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(column.ColumnName), "ToArgb"));
                }
                else
                {
                    cmie.Parameters.Add(new CodeVariableReferenceExpression(CodeDomGenerator.TryCorrectNameNoWhitespace(column.ColumnName)));
                }
                cmie.Parameters.Add(new CodeSnippetExpression(column.SQLDBType));
                CodeMethodInvokeExpression cmieAdd = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("SqlParmColl"), "Add");
                cmieAdd.Parameters.Add(cmie);
                Try.TryStatements.Add(cmieAdd);
            }
            Try.TryStatements.Add(new CodeMethodReturnStatement(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("SqlParmColl"), "ToArray", new CodeExpression[] { })));
            codemembermethod.Statements.Add(Try);

            tableclass_CodeTypeDeclaration.Members.Add(codemembermethod);
        }

        

        public static void ConstructorMethod(SQLiteTable _sqlitetable, CodeTypeDeclaration tableclass_CodeTypeDeclaration)
        {
            /*
            CodeMemberMethod partialConstructor = new CodeMemberMethod();
            partialConstructor.Name = "partialConstructor";
            partialConstructor.Attributes = MemberAttributes.ScopeMask;
            partialConstructor.ReturnType = new CodeTypeReference("partial void");
            tableclass_CodeTypeDeclaration.Members.Add(partialConstructor);
            */
            /*
            CodeTypeDelegate delegate1 = new CodeTypeDelegate(_sqlitetable.tableName + "ChangedEventHandler");
            delegate1.Parameters.Add(new CodeParameterDeclarationExpression("System.Object", "sender"));
            delegate1.Parameters.Add(new CodeParameterDeclarationExpression("System.EventArgs", "e"));
            */
            //CodeTypeMember codetypemember = new CodeTypeMember();
            //codetypemember.Attributes = MemberAttributes.ScopeMask;
            //codetypemember.Name = "partialConstructor";
            //tableclass_CodeTypeDeclaration.Members.Add(codetypemember);

            // Declare the constructor
            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes = MemberAttributes.Public | MemberAttributes.Final ;

            
            //////////////

            //if (_sqlitetable.IsReferenced)
            //{
            //    foreach (SQLiteTableColumn sqlitetablecolumn in _sqlitetable.listSQLiteTableColumn)
            //    {
            //        if (sqlitetablecolumn.IsReference)
            //        {
            //            for (int index = 0; index < sqlitetablecolumn.ReferncedTablelist.Count; index++)
            //            {
            //                string Name = "m_" + sqlitetablecolumn.ReferncedTablelist[index].TableName + "Collection";
            //                string temp = sqlitetablecolumn.ReferncedTablelist[index].TableName + "Collection";
            //                CodeFieldReferenceExpression codefieldreferenceexpression = new CodeFieldReferenceExpression();
            //                CodeAssignStatement as1 = new CodeAssignStatement(new CodeVariableReferenceExpression(Name), new CodeVariableReferenceExpression(" new " + temp + "(this)"));
            //                constructor.Statements.Add(as1);
            //            }
            //        }
            //    }
            //}
            ///////////////

            //CodeExpressionStatement as2 = new CodeAssignStatement(new CodeVariableReferenceExpression(Name), new CodeVariableReferenceExpression(" new " + temp + "(this)"));
            //constructor.Statements.Add(as1);

            tableclass_CodeTypeDeclaration.Members.Add(constructor);
        }

        public static void ClassCollection(SQLiteTable _sqlitetable, CodeNamespace SourceCodeNamespace)
        {
            
            CodeTypeDeclaration codetypedeclaration = new CodeTypeDeclaration();
            #region ClassCollection Setup

            codetypedeclaration.Name = _sqlitetable.tableName + "Collection";
            codetypedeclaration.IsClass = true;
            codetypedeclaration.IsPartial = true;
            codetypedeclaration.TypeAttributes = TypeAttributes.Public | TypeAttributes.Class;
            codetypedeclaration.BaseTypes.Add("SQLiteTableCollection");
            if (!string.IsNullOrEmpty(_sqlitetable.Description))
                codetypedeclaration.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression(_sqlitetable.Description))));

            #endregion

            
            //ListobjectCreate(_sqlitetable, codetypedeclaration);

            #region Event Member
            CodeMemberEvent codememberevent = new CodeMemberEvent();

            codememberevent.Attributes = MemberAttributes.Public;



            codememberevent.Type = new CodeTypeReference(_sqlitetable.tableName + "ChangedEventHandler");
            codememberevent.Name = _sqlitetable.tableName + "Changed";
            if (CodeDomGenerator.AddComments)
                codememberevent.Comments.Add(new CodeCommentStatement("<remarks>SQL Type:" + _sqlitetable.tableName + "ChangedEventHandler" + "</remarks>", true));

            codetypedeclaration.Members.Add(codememberevent);


            //public event tblDisplayChangedEventHandler tblDisplayChanged;

            //
            #endregion


            #region Parent
            if (_sqlitetable.HasForeignKey)
            {
                foreach (SQLiteTableColumn sqlitetablecolumn in _sqlitetable.listSQLiteTableColumn)
                {
                    if (sqlitetablecolumn.IsForeignKey)
                    {
                        FieldRefObjectCreate(sqlitetablecolumn, codetypedeclaration);
                        PropertyRefObjectCreate(sqlitetablecolumn, codetypedeclaration);
                    }
                }
            } 
            #endregion

            
            #region Constructor

            CodeConstructor codeconstructor = new CodeConstructor();
            codeconstructor.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression("Constructor"))));
            foreach (SQLiteTableColumn sqlitetablecolumn in _sqlitetable.listSQLiteTableColumn)
            {
                if (sqlitetablecolumn.IsForeignKey)
                {
                    codeconstructor.Parameters.Add(new CodeParameterDeclarationExpression(sqlitetablecolumn.ReferncedTablelist[0].TableName, "_parent"));


                    CodeFieldReferenceExpression codefieldreferenceexpression = new CodeFieldReferenceExpression();
                    codefieldreferenceexpression.FieldName = "_" + sqlitetablecolumn.ReferncedTablelist[0].ColumnName + "_" + sqlitetablecolumn.ReferncedTablelist[0].TableName;

                    CodeAssignStatement AssignValue = new CodeAssignStatement(codefieldreferenceexpression, new CodeVariableReferenceExpression("_parent"));
                    codeconstructor.Statements.Add(AssignValue);
                }
                //"_" + _sqlitetablecolumn.ReferncedTablelist[0].ColumnName + "_" + _sqlitetablecolumn.ReferncedTablelist[0].TableName;
            }


            codeconstructor.Attributes = MemberAttributes.Public;


            codetypedeclaration.Members.Add(codeconstructor);

            #endregion

           

            #region Event Hadler Method

            CodeMemberMethod EventHadlerMethod = new CodeMemberMethod();
            EventHadlerMethod.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression("Invoke the Changed event; called whenever list changes"))));
            EventHadlerMethod.Name = "On" + _sqlitetable.tableName + "Changed";
            EventHadlerMethod.ReturnType = new CodeTypeReference(typeof(void));
            //EventHadlerMethod.Attributes = MemberAttributes.Family | MemberAttributes.Override;
            ////  protected override void OntblUserGroupChanged(System.EventArgs e)
            EventHadlerMethod.Attributes = MemberAttributes.Family | MemberAttributes.Overloaded;

            
            EventHadlerMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(EventArgs)), "e"));

            CodeVariableReferenceExpression CodeVariableReferenceExpression = new CodeVariableReferenceExpression( _sqlitetable.tableName + "Changed" + " != null");

            CodeMethodInvokeExpression codemethodinvokeexpression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), _sqlitetable.tableName + "Changed", new CodeVariableReferenceExpression("index"), new CodeVariableReferenceExpression("item"));

            CodeConditionStatement IfLength = new CodeConditionStatement(CodeVariableReferenceExpression, new CodeStatement[] { new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), _sqlitetable.tableName + "Changed", new CodeThisReferenceExpression(), new CodeVariableReferenceExpression("e"))) });

            EventHadlerMethod.Statements.Add(IfLength);



            codetypedeclaration.Members.Add(EventHadlerMethod);
            /*
            protected override void OntblDisplayChanged(System.EventArgs e)
            */
            #endregion


            #region Get Property

            CodeMemberProperty GetProperty = new CodeMemberProperty();
            GetProperty.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression("Gets a  " + _sqlitetable.tableName + " from the collection."))));
            GetProperty.Name = "this[int index]";
            GetProperty.Type = new CodeTypeReference(_sqlitetable.tableName);
            GetProperty.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            GetProperty.HasGet = true;
            GetProperty.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(int)), "index"));

            GetProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeCastExpression(_sqlitetable.tableName, new CodeArrayIndexerExpression(new CodeVariableReferenceExpression("List"), new CodeVariableReferenceExpression("index")))));

            CodeFieldReferenceExpression codefieldreferenceexpressionleft = new CodeFieldReferenceExpression();
            codefieldreferenceexpressionleft.FieldName = "List[index]";
            GetProperty.SetStatements.Add(new CodeAssignStatement(codefieldreferenceexpressionleft, new CodeVariableReferenceExpression("value")));
            GetProperty.SetStatements.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(),"On"+ _sqlitetable.tableName + "Changed", new CodeVariableReferenceExpression("EventArgs.Empty"))));
            // OnControllerChanged(EventArgs.Empty);
            GetProperty.HasSet = true;

            codetypedeclaration.Members.Add(GetProperty);

            #endregion


           

            #region Get Method

            CodeMemberMethod GetMethod = new CodeMemberMethod();
            GetMethod.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression("Gets a  " + _sqlitetable.tableName + " from the collection."))));
            GetMethod.Name = "Get";
            GetMethod.ReturnType = new CodeTypeReference(_sqlitetable.tableName);
            GetMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;

            GetMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(int)), "index"));
            //GetMethod.Statements.Add(new CodeVariableDeclarationStatement(typeof(int), "newindex", new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("List"), "Get", new CodeVariableReferenceExpression("item"))));

            GetMethod.Statements.Add(new CodeMethodReturnStatement(new CodeCastExpression(_sqlitetable.tableName, new CodeArrayIndexerExpression(new CodeVariableReferenceExpression("List"), new CodeVariableReferenceExpression("index")))));

            codetypedeclaration.Members.Add(GetMethod);

            #endregion


            #region Add Method

            CodeMemberMethod AddMethod = new CodeMemberMethod();
            AddMethod.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression("Adds a new " + _sqlitetable.tableName + " to the collection."))));
            AddMethod.Name = "Add";
            AddMethod.ReturnType = new CodeTypeReference(typeof(void));
            AddMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            AddMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(_sqlitetable.tableName), "item"));
            AddMethod.Statements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("List"), "Add", new CodeVariableReferenceExpression("item")));
            AddMethod.Statements.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "On" + _sqlitetable.tableName + "Changed", new CodeVariableReferenceExpression("EventArgs.Empty"))));
            //AddMethod.Statements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("newindex")));
           
            codetypedeclaration.Members.Add(AddMethod);

            #endregion

            #region Remove Method

            CodeMemberMethod RemoveMethod = new CodeMemberMethod();
            RemoveMethod.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression("Removes a " + _sqlitetable.tableName + " from the collection."))));
            RemoveMethod.Name = "Remove";
            RemoveMethod.ReturnType = new CodeTypeReference(typeof(void));
            RemoveMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            RemoveMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(_sqlitetable.tableName), "item"));
            RemoveMethod.Statements.Add(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("List"), "Remove", new CodeVariableReferenceExpression("item")));
            RemoveMethod.Statements.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(),"On"+ _sqlitetable.tableName + "Changed", new CodeVariableReferenceExpression("EventArgs.Empty"))));

            codetypedeclaration.Members.Add(RemoveMethod);


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
            InsertMethod.Statements.Add(new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(),"On"+  _sqlitetable.tableName + "Changed", new CodeVariableReferenceExpression("EventArgs.Empty"))));
            codetypedeclaration.Members.Add(InsertMethod);


            #endregion

            #region IndexOf Method

            CodeMemberMethod IndexOfMethod = new CodeMemberMethod();
            IndexOfMethod.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression("Returns the index value of the " + _sqlitetable.tableName + " class in the collection."))));
            IndexOfMethod.Name = "IndexOf";
            IndexOfMethod.ReturnType = new CodeTypeReference(typeof(int));
            IndexOfMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            IndexOfMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(_sqlitetable.tableName), "item"));
            IndexOfMethod.Statements.Add(new CodeMethodReturnStatement(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("List"), "IndexOf", new CodeVariableReferenceExpression("item"))));
            codetypedeclaration.Members.Add(IndexOfMethod);

            #endregion

            #region Contains Method

            CodeMemberMethod ContainsMethod = new CodeMemberMethod();
            ContainsMethod.CustomAttributes.Add(new CodeAttributeDeclaration("Description", new CodeAttributeArgument(new CodePrimitiveExpression("Returns true if the " + _sqlitetable.tableName + " class is present in the collection."))));
            ContainsMethod.Name = "Contains";
            ContainsMethod.ReturnType = new CodeTypeReference(typeof(bool));
            ContainsMethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            ContainsMethod.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(_sqlitetable.tableName), "item"));
            ContainsMethod.Statements.Add(new CodeMethodReturnStatement(new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("List"), "Contains", new CodeVariableReferenceExpression("item"))));
            codetypedeclaration.Members.Add(ContainsMethod);

            #endregion

            SourceCodeNamespace.Types.Add(codetypedeclaration);
        }

         public static void Classdelegate(SQLiteTable _sqlitetable, CodeNamespace SourceCodeNamespace)
        {
        //public delegate void ControllerChangedEventHandler(object sender, EventArgs e);

            // Declares a delegate type called TestDelegate with an EventArgs parameter.
            CodeTypeDelegate delegate1 = new CodeTypeDelegate(_sqlitetable.tableName + "ChangedEventHandler");
            delegate1.Parameters.Add(new CodeParameterDeclarationExpression("System.Object", "sender"));
            delegate1.Parameters.Add(new CodeParameterDeclarationExpression("System.EventArgs", "e"));

            SourceCodeNamespace.Types.Add(delegate1);
            // A C# code generator produces the following source code for the preceeding example code: 

            //     public delegate void _sqlitetable.tableName + "ChangedEventHandler"(object sender, System.EventArgs e);
            
             
             
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
                return "GetSingle";
            else if (type == typeof(System.String))
                return "GetString";
            else
                return "GetValue";
        }

        //internal static string SqliteDbTypeConst(string type)
        //{

        //    Single dd = 0;
        //    string delimStr = "()";
        //    char[] delimiter = delimStr.ToCharArray();
        //    string words = type;
        //    string[] split = null;


        //    split = words.Split(delimiter);

        //    switch (split[0].ToLower())
        //    //switch (type)
        //    {
        //        case "bigint": return "DbType.BigInt";
        //        case "binary": return "DbType.Binary";
        //        case "bit": return "DbType.Bit";
        //        case "char": return "DbType.Char";
        //        case "date": return "DbType.Date";
        //        case "datetime": return "DbType.DateTime";
        //        case "datetime2": return "DbType.DateTime2";
        //        case "datetimeoffset": return "DbType.DateTimeOffset";
        //        case "decimal": return "DbType.Decimal";
        //        case "float": return "DbType.Float";
        //        case "image": return "DbType.Image";
        //        case "int": return "DbType.Int";
        //        case "money": return "DbType.Money";
        //        case "nchar": return "DbType.NChar";
        //        case "ntext": return "DbType.NText";
        //        case "nvarchar": return "DbType.NVarChar";
        //        case "real": return "DbType.Real";
        //        case "smalldatetime": return "DbType.SmallDateTime";
        //        case "smallint": return "DbType.SmallInt";
        //        case "smallmoney": return "DbType.SmallMoney";
        //        case "structured": return "DbType.Structured";
        //        case "text": return "DbType.Text";
        //        case "time": return "DbType.Time";
        //        case "timestamp": return "DbType.Timestamp";
        //        case "tinyint": return "DbType.TinyInt";
        //        case "udt": return "DbType.Udt";
        //        case "uniqueidentifier": return "DbType.UniqueIdentifier";
        //        case "varbinary": return "DbType.VarBinary";
        //        case "varchar": return "DbType.VarChar";
        //        case "variant": return "DbType.Variant";
        //        case "xml": return "DbType.Xml";
        //        default: return "";
        //    }
        //}

   
    }
}
