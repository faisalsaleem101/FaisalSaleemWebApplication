using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DocumentGenerator.Helpers
{
    public class Helper
    {
        const string outputFolder = "Outputs";
        const string templateFolder = "Templates";

        public static string GetOutputPath()
        {
            string startDirectory = GetRootDirectory();
            return Path.Combine(startDirectory, outputFolder);
        }

        public static string GetTemplatePath()
        {
            string startDirectory = GetRootDirectory();
            return Path.Combine(startDirectory, templateFolder);
        }

        private static string GetRootDirectory()
        {
            string dataDir = Directory.GetCurrentDirectory();
            return Directory.GetParent(dataDir).FullName;
        }

        // ExEnd:ImportTableFromDataTable

        public static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        // get coumn names which replace the existing column name with _ to a space 
        public static List<string> GetColumnNames(DataTable dataTable)
        {
            var columns = new List<string>();

            foreach (var column in dataTable.Columns)
                columns.Add(column.ToString().Replace("_", " "));

            return columns;
        }

        public static DataTable GetColumnNamesForDataTable(DataTable dataTable)
        {
            foreach (var column in dataTable.Columns)
                dataTable.Columns[column.ToString()].ColumnName = string.Concat(column.ToString().Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');

            return dataTable;
        }
    }
}
