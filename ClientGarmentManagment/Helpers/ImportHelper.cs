using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClientGarmentManagment.Helpers
{
    public static class ImportHelper
    {
        public static IEnumerable<DataRow> ReadFrom(string file, string sheet)
        {

            using (OleDbConnection con = new OleDbConnection(string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", file)))
            {
                OleDbDataAdapter da = new OleDbDataAdapter(string.Format("select * from [{0}$]", sheet), con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.AsEnumerable())
                {
                    yield return dr;

                }
            }

        }
        public static DataTable ReadFromToDataTable(string file, string sheet)
        {

            using (OleDbConnection con = new OleDbConnection(string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", file)))
            {
                OleDbDataAdapter da = new OleDbDataAdapter(string.Format("select * from [{0}$]", sheet), con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return (dt);
            }

        }
        public static IEnumerable<DataRow> ReadFromLongColumn(string file, string sheet)
        {
            using (OleDbConnection con = new OleDbConnection(string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=No;IMEX=1""", file)))
            {
                OleDbDataAdapter da = new OleDbDataAdapter(string.Format("select * from [{0}$]", sheet), con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                DataTable dt2 = new DataTable();

                foreach (DataColumn dc in dt.Columns)
                {
                    dt2.Columns.Add(dt.Rows[0][dc.ColumnName].ToString());
                }
                foreach (DataRow drreal in dt.AsEnumerable().Skip(1))
                {
                    DataRow newRow = dt2.NewRow();
                    int count = 0;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        newRow[count] = drreal[dc];
                        count++;
                    }
                    dt2.Rows.Add(newRow);
                }

                foreach (DataRow dr in dt2.AsEnumerable())
                {
                    yield return dr;

                }
            }
        }
        public static List<string> Columns(string file, string sheet)
        {
            List<string> columnNames = new List<string>();
            using (OleDbConnection con = new OleDbConnection(string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=No;IMEX=1""", file)))
            {
                OleDbDataAdapter da = new OleDbDataAdapter(string.Format("select * from [{0}$]", sheet), con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataColumn dc in dt.Columns)
                {
                    columnNames.Add(dt.Rows[0][dc.ColumnName].ToString());
                }
            }
            return (columnNames);
        }
        public static List<T> GetObjectFromJsonArray<T>(string filename)
        {
            List<T> collection = new List<T>();
            using (StreamReader reader = new StreamReader(filename))
            {
                string jsonText = reader.ReadToEnd();
                JArray json = JArray.Parse(jsonText);
                foreach (var c in json)
                {
                    T obj = JsonConvert.DeserializeObject<T>(c.ToString());
                    collection.Add(obj);
                }
            }
            return (collection);
        }
        public static List<T> GetObjectsFromDataTable<T>(DataTable Table)
        {
            List<T> collection = new List<T>();

            foreach (var row in Table.AsEnumerable())
            {
                T obj = (T)Activator.CreateInstance(typeof(T), null);

                foreach (var prop in obj.GetType().GetProperties())
                {
                    try
                    {
                        PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                        propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                    }
                    catch
                    {
                        continue;
                    }
                }
                collection.Add(obj);
            }

            return (collection);
        }
    }
}
