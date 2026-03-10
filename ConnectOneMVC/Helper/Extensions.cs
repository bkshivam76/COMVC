using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;

public static class Extensions
{
    /// <summary>
    /// Converts datatable to list<T> dynamically
    /// </summary>
    /// <typeparam name="T">Class name</typeparam>
    /// <param name="dataTable">data table to convert</param>
    /// <returns>List<T></returns>
    ///public static List<T> ToList<T>(this DataTable dataTable) where T : new()
    public static List<T> ToList<T>(DataTable dataTable) where T : new()
    {
        var dataList = new List<T>();
        if (dataTable==null)
        {
            return dataList;
        }

        //Define what attributes to be read from the class
        const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

        //Read Attribute Names and Types
        var objFieldNames = typeof(T).GetProperties(flags).Cast<PropertyInfo>().
            Select(item => new
            {
                Name = item.Name,
                Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
            }).ToList();

        //Read Datatable column names and types
        var dtlFieldNames = dataTable.Columns.Cast<DataColumn>().
            Select(item => new
            {
                Name = item.ColumnName,
                Type = item.DataType
            }).ToList();

        foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
        {
            var classObj = new T();

            foreach (var dtField in dtlFieldNames)
            {
                PropertyInfo propertyInfos = classObj.GetType().GetProperty(dtField.Name);

                var field = objFieldNames.Find(x => x.Name == dtField.Name);

                if (field != null)
                {

                    if (propertyInfos.PropertyType == typeof(DateTime) || propertyInfos.PropertyType == typeof(DateTime?))
                    {
                        propertyInfos.SetValue
                        (classObj, convertToDateTime(dataRow[dtField.Name]), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(int) || propertyInfos.PropertyType == typeof(int?))
                    {
                        propertyInfos.SetValue
                        (classObj, ConvertToInt(dataRow[dtField.Name]), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(long) || propertyInfos.PropertyType == typeof(long?))
                    {
                        propertyInfos.SetValue
                        (classObj, ConvertToLong(dataRow[dtField.Name]), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(decimal) || propertyInfos.PropertyType == typeof(decimal?))
                    {
                        propertyInfos.SetValue
                        (classObj, ConvertToDecimal(dataRow[dtField.Name]), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(double) || propertyInfos.PropertyType == typeof(double?))
                    {
                        propertyInfos.SetValue
                        (classObj, ConvertToDouble(dataRow[dtField.Name]), null);
                    }
                    else if (propertyInfos.PropertyType == typeof(String))
                    {
                        if (dataRow[dtField.Name].GetType() == typeof(DateTime))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToDateString(dataRow[dtField.Name]), null);
                        }
                        else
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToString(dataRow[dtField.Name]), null);
                        }
                    }
                }
            }
            dataList.Add(classObj);
        }
        return dataList;
    }

    private static string ConvertToDateString(object date)
    {
        if (date == null)
            return string.Empty;

        //return SpecialDateTime.ConvertDate(Convert.ToDateTime(date));
        return Convert.ToDateTime(date).ToString();
    }

    private static string ConvertToString(object value)
    {
        return Convert.ToString(ReturnEmptyIfNull(value));
    }

    private static int? ConvertToInt(object value)
    {
        if (CheckNull(value))
        {
            return null;
        }
        return Convert.ToInt32(ReturnNullIfDbNull(value));
    }

    private static long? ConvertToLong(object value)
    {
        if (CheckNull(value))
        {
            return null;
        }
        return Convert.ToInt64(ReturnNullIfDbNull(value));
    }

    private static decimal? ConvertToDecimal(object value)
    {
        if (CheckNull(value))
        {
            return null;
        }
        return Convert.ToDecimal(ReturnNullIfDbNull(value));
    }

    private static decimal? ConvertToDouble(object value)
    {
        if (CheckNull(value))
        {
            return null;
        }
        return Convert.ToDecimal(ReturnNullIfDbNull(value));
    }

    private static DateTime? convertToDateTime(object date)
    {
        if (CheckNull(date))
        {
            return null;
        }
        return Convert.ToDateTime(ReturnNullIfDbNull(date));
    }

    public static object ReturnZeroIfNull(this object value)
    {
        if (value == DBNull.Value)
            return 0;
        if (value == null)
            return 0;
        return value;
    }

    public static object ReturnEmptyIfNull(this object value)
    {
        if (value == DBNull.Value)
            return string.Empty;
        if (value == null)
            return string.Empty;
        return value;
    }

    public static object ReturnFalseIfNull(this object value)
    {
        if (value == DBNull.Value)
            return false;
        if (value == null)
            return false;
        return value;
    }

    public static object ReturnDateTimeMinIfNull(this object value)
    {
        if (value == DBNull.Value)
            return DateTime.MinValue;
        if (value == null)
            return DateTime.MinValue;
        return value;
    }

    public static object ReturnNullIfDbNull(this object value)
    {
        if (value == DBNull.Value)
            return '\0';
        if (value == null)
            return '\0';
        return value;
    }
    public static bool CheckNull(this object value)
    {
        if (value == DBNull.Value)
            return true;
        if (value == null)
            return true;
        return false;
    }

}