using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Globalization;

namespace SQLRead
{
    public class ExpandableObject : ExpandableObjectConverter
    {
        ////private string getDataSize(long Length)
        ////{
        ////    if (Length < 1024)
        ////        return Length.ToString() + " bytes";
        ////    else if (Length > 1023 && Length < 1048576)
        ////        return ((long)(Length / 1024)).ToString() + " Kb";
        ////    else if (Length > 1048575 && Length < 1073741824)
        ////        return ((long)(Length / 1048576)).ToString() + " Mb";
        ////    else if (Length > 1073741823 && Length < 1099511626752)
        ////        return ((long)(Length / 1073741824)).ToString() + " Gb";
        ////    else
        ////        return ((long)(Length / 1099511626752)).ToString() + " Tb";
        ////}

        ////public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        ////{
        ////    if (destinationType == typeof(string))
        ////    {
        ////        if (value is Database)
        ////        {
        ////            if (((Database)value).Tables.Items.Length > 0)
        ////            {
        ////                long totsize = 0;
        ////                foreach (Table t in ((Database)value).Tables.Items)
        ////                    totsize += (t.DataUsageSize + t.IndexUsageSize) + t.UnusedSize;
        ////                return ((Database)value).name + " [" + getDataSize(totsize) + "]";
        ////            }
        ////            else
        ////                return ((Database)value).name;
        ////        }
        ////        if (value is Table)
        ////            return ((Table)value).name;
        ////        if (value is Index)
        ////            return ((Index)value).name;
        ////        if (value is IndexColumn)
        ////            return ((IndexColumn)value).Column.name;
        ////        if (value is foreign_key)
        ////            return ((foreign_key)value).name;
        ////        if (value is foreign_key_column)
        ////        {
        ////            StringBuilder sb = new StringBuilder();
        ////            sb.Append(((foreign_key_column)value).ParentTable.name);
        ////            sb.Append(".");
        ////            sb.Append(((foreign_key_column)value).ParentColumn.name);
        ////            sb.Append(" - ");
        ////            sb.Append(((foreign_key_column)value).ReferencedTable.name);
        ////            sb.Append(".");
        ////            sb.Append(((foreign_key_column)value).ReferencedColumn.name);
        ////            return sb.ToString();
        ////        }
        ////        if (value is Column)
        ////            return ((Column)value).name;
        ////        if (value is DatabaseFile)
        ////        {
        ////            if(((DatabaseFile)value).size.HasValue)
        ////                return ((DatabaseFile)value).name + " [" + getDataSize(((DatabaseFile)value).size.Value) + "]";
        ////            else
        ////                return ((DatabaseFile)value).name + " []";
        ////        }
        ////        if (value is SQL_Type)
        ////        {
        ////            return ((SQL_Type)value).name;
        ////        }
        ////        if (value is DefaultConstraint)
        ////        {
        ////            return ((DefaultConstraint)value).definition;
        ////        }
        ////        if (value is Trigger)
        ////        {
        ////            return ((Trigger)value).name;
        ////        }
        ////        if (value is Comment)
        ////        {
        ////            if (((Comment)value).text.Length > 20)
        ////            {
        ////                if (((Comment)value).text.Contains(" "))
        ////                {
        ////                    if (((Comment)value).text.IndexOf(" ", 10) < 30 && ((Comment)value).text.IndexOf(" ", 10) > 10)
        ////                        return ((Comment)value).text.Substring(0, ((Comment)value).text.IndexOf(" ", 10)) + "...";
        ////                    else if (((Comment)value).text.IndexOf(" ") < 20)
        ////                        return ((Comment)value).text.Substring(0, ((Comment)value).text.IndexOf(" ")) + "...";
        ////                    else
        ////                        return ((Comment)value).text.Substring(0, 20) + "...";
        ////                }
        ////                else
        ////                    return ((Comment)value).text;
        ////            }
        ////            else
        ////                return ((Comment)value).text;
        ////        }
        ////        if (value is IdentityColumn)
        ////            return "[" + ((IdentityColumn)value).increment_value.ToString() + "," + ((IdentityColumn)value).seed_value.ToString() + "]";
        ////        if (value is Schema)
        ////            return ((Schema)value).name;
        ////        if (value is User)
        ////            return ((User)value).name;
        ////        if (value is ExtendedProperty)
        ////            return ((ExtendedProperty)value).value;
        ////        if (value is KeyConstraint)
        ////            return ((KeyConstraint)value).name;
        ////        if (value is View)
        ////            return ((View)value).name;
        ////    }
        ////    return base.ConvertTo(context, culture, value, destinationType);
        ////}
    }
}
