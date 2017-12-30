using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.ComponentModel;
using Utility;
using DataAccessLayer;

namespace BussinessLogic.BussinessLogic
{
    public class LocalDataSource
    {
        public static List<T> GetListDataFromProcedure<T>(string ConnectionString, string procName, params object[] paramsProcs) where T : class, new()
        {
            try
            {
                List<T> ListDatas = new List<T>();
                DataSet ds = new DataSet();
                if (paramsProcs == null || paramsProcs.Count() == 0)
                {
                    ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, procName);
                }
                else
                {
                    ds = SqlHelper.ExecuteDataset(ConnectionString, procName, paramsProcs);
                }
                ListDatas = DataTableToList<T>(ds.Tables[0]);
                return ListDatas;
            }
            catch (Exception ex)
            {
                string errorSource = "Fisbank.Cbs.BusinessLogic.LocalDataSource.GetListDataFromProcedure<T>(string ConnectionString, string procName, params object[] paramsProcs)";
                ErrorLog.Log(Environment.NewLine + "  - ErrorSource: " + errorSource + Environment.NewLine + "  - ErrorMessage: " + ex.Message);
                return null;
            }
        }

        public static List<T> GetListDataFromCommand<T>(string ConnectionString, string sqlCommand) where T : class, new()
        {
            try
            {
                List<T> ListDatas = new List<T>();
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sqlCommand);
                ListDatas = DataTableToList<T>(ds.Tables[0]);
                return ListDatas;
            }
            catch (Exception ex)
            {
                string errorSource = "Fisbank.Cbs.BusinessLogic.LocalDataSource.GetListDataFromCommand<T>(string ConnectionString, string sqlCommand)";
                ErrorLog.Log(Environment.NewLine + "  - ErrorSource: " + errorSource + Environment.NewLine + "  - ErrorMessage: " + ex.Message);
                return null;
            }
        }

        public static T GetDataFromProcedure<T>(string ConnectionString, string procName, params object[] paramsProcs) where T : class, new()
        {
            try
            {
                List<T> ListDatas = new List<T>();
                DataSet ds = new DataSet();
                if (paramsProcs == null || paramsProcs.Count() == 0)
                {
                    ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, procName);
                }
                else
                {
                    ds = SqlHelper.ExecuteDataset(ConnectionString, procName, paramsProcs);
                }
                ListDatas = DataTableToList<T>(ds.Tables[0]);
                return ListDatas.FirstOrDefault();
            }
            catch (Exception ex)
            {
                string errorSource = "Fisbank.Cbs.BusinessLogic.LocalDataSource.GetDataFromProcedure<T>(string ConnectionString, string procName, params object[] paramsProcs)";
                ErrorLog.Log(Environment.NewLine + "  - ErrorSource: " + errorSource + Environment.NewLine + "  - ErrorMessage: " + ex.Message);
                return null;
            }
        }

        public static T GetDataFromCommand<T>(string ConnectionString, string sqlCommand) where T : class, new()
        {
            try
            {
                List<T> ListDatas = new List<T>();
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sqlCommand);
                ListDatas = DataTableToList<T>(ds.Tables[0]);
                return ListDatas.FirstOrDefault();
            }
            catch (Exception ex)
            {
                string errorSource = "Fisbank.Cbs.BusinessLogic.LocalDataSource.GetDataFromCommand<T>(string ConnectionString, string sqlCommand)";
                ErrorLog.Log(Environment.NewLine + "  - ErrorSource: " + errorSource + Environment.NewLine + "  - ErrorMessage: " + ex.Message);
                return null;
            }
        }

        public static DataSet GetDataSetFromProcedure(string ConnectionString, string procName, params object[] paramsProcs)
        {
            try
            {
                var ds = new DataSet();
                if (paramsProcs == null || paramsProcs.Length == 0)
                {
                    ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, procName);
                }
                else
                {
                    ds = SqlHelper.ExecuteDataset(ConnectionString, procName, paramsProcs);
                }
                return ds;
            }
            catch (Exception ex)
            {
                string errorSource = "Fisbank.Cbs.BusinessLogic.LocalDataSource.GetAllCacheData(string ConnectionString, string procName)";
                ErrorLog.Log(Environment.NewLine + "  - ErrorSource: " + errorSource + Environment.NewLine + "  - ErrorMessage: " + ex.Message);
                return new DataSet();
            }
        }

        public static DataTable GetDatatableFromCommand(string ConnectionString, string sqlCommand)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sqlCommand);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                string errorSource = "Fisbank.Cbs.BusinessLogic.LocalDataSource.DataTable GetDatatableFromCommand(string ConnectionString, string sqlCommand)";
                ErrorLog.Log(Environment.NewLine + "  - ErrorSource: " + errorSource + Environment.NewLine + "  - ErrorMessage: " + ex.Message);
                return null;
            }
        }
        public static DataTable GetDatatableFromProceduce(string ConnectionString, string proceduce, params object [] obj)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(ConnectionString, proceduce, obj);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                string errorSource = "Fisbank.Cbs.BusinessLogic.LocalDataSource.DataTable GetDatatableFromCommand(string ConnectionString, string sqlCommand)";
                ErrorLog.Log(Environment.NewLine + "  - ErrorSource: " + errorSource + Environment.NewLine + "  - ErrorMessage: " + ex.Message);
                return null;
            }
        }

        public static List<T> DataTableToList<T>(DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

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

                    list.Add(obj);
                }

                return list;
            }
            catch (Exception ex)
            {
                string errorSource = "Fisbank.Cbs.BusinessLogic.LocalDataSource.DataTableToList<T>(DataTable table)";
                ErrorLog.Log(Environment.NewLine + "  - ErrorSource: " + errorSource + Environment.NewLine + "  - ErrorMessage: " + ex.Message.ToUpper());
                return null;
            }
        }

        public static DataTable ConvertToDatatable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static object[] ConvertObjectToArrayParam<T>(T data) where T : class, new()
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            object[] values = new object[props.Count];
            for (int i = 0; i < props.Count; i++)
            {
                values[i] = props[i].GetValue(data);
            }
            return values;
        }

        public static int CallProcedure(string ConnectionString, string procName, params object[] paramsProcs)
        {
            try
            {
                int eCode;

                if (paramsProcs == null || paramsProcs.Count() == 0)
                {
                    eCode = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, procName);
                }
                else
                {
                    eCode = SqlHelper.ExecuteNonQuery(ConnectionString, procName, paramsProcs);
                }

                return eCode;
            }
            catch (Exception ex)
            {
                string errorSource = "Fisbank.Cbs.BusinessLogic.LocalDataSource.CallProcedure(string ConnectionString, string procName, params object[] paramsProcs)";
                ErrorLog.Log(Environment.NewLine + "  - ErrorSource: " + errorSource + Environment.NewLine + "  - ErrorMessage: " + ex.Message);
                return -1;
            }
        }

        public static int CallCommandExecute(string ConnectionString, string Command)
        {
            try
            {
                int eCode = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, Command);
                return eCode;
            }
            catch (Exception ex)
            {
                string errorSource = "Fisbank.Cbs.BusinessLogic.LocalDataSource.CallCommandExecute(string ConnectionString, string Command)";
                ErrorLog.Log(Environment.NewLine + "  - ErrorSource: " + errorSource + Environment.NewLine + "  - ErrorMessage: " + ex.Message);
                return -1;
            }
        }

        public static List<string> GetListStringFromProcedure(string ConnectionString, string procName, params object[] paramsProcs)
        {
            try
            {
                List<string> ListDatas = new List<string>();
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(ConnectionString, procName, paramsProcs);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListDatas.Add(dr[0].ToString());
                }
                return ListDatas;
            }
            catch (Exception ex)
            {
                string errorSource = "Fisbank.Cbs.BusinessLogic.LocalDataSource.GetDataFromCommand<T>(string ConnectionString, string procName, params object[] paramsProcs)";
                ErrorLog.Log(Environment.NewLine + "  - ErrorSource: " + errorSource + Environment.NewLine + "  - ErrorMessage: " + ex.Message);
                return null;
            }
        }

        public static List<string> GetListStringFromCommand(string ConnectionString, string Command)
        {
            try
            {
                List<string> ListDatas = new List<string>();
                DataSet ds = new DataSet();
                ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, Command);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListDatas.Add(dr[0].ToString());
                }
                return ListDatas;
            }
            catch (Exception ex)
            {
                string errorSource = "Fisbank.Cbs.BusinessLogic.LocalDataSource.GetDataFromCommand<T>(string ConnectionString, string procName, params object[] paramsProcs)";
                ErrorLog.Log(Environment.NewLine + "  - ErrorSource: " + errorSource + Environment.NewLine + "  - ErrorMessage: " + ex.Message);
                return null;
            }
        }

        public static int GetReturnValueFromProcedure(string ConnectionString, string procName, params object[] paramsProcs)
        {
            try
            {
                int retVal = 0;
                retVal = (Int32)SqlHelper.ExecuteScalar(ConnectionString, procName, paramsProcs);
                return retVal;
            }
            catch (Exception ex)
            {
                string errorSource = "Fisbank.Cbs.BusinessLogic.LocalDataSource.GetReturnValueFromProcedure(string ConnectionString, string procName, params object[] paramsProcs)";
                ErrorLog.Log(Environment.NewLine + "  - ErrorSource: " + errorSource + Environment.NewLine + "  - ErrorMessage: " + ex.Message);
                return 0;
            }
        }

        public static string GetReturnValueFromCommand(string ConnectionString, string sqlCommand)
        {
            try
            {
                return SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, sqlCommand).ToString();
            }
            catch (Exception ex)
            {
                string errorSource = "Fisbank.Cbs.BusinessLogic.LocalDataSource.GetReturnValueFromCommand(string ConnectionString, string sqlCommand)";
                ErrorLog.Log(Environment.NewLine + "  - ErrorSource: " + errorSource + Environment.NewLine + "  - ErrorMessage: " + ex.Message);
                return "";
            }
        }

        public static long GetNextSequenceValue(string ConnectionString, string sequence_name)
        {
            try
            {
                string cmd = "SELECT NEXT VALUE FOR " + sequence_name + " ";
                var rs = SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, cmd);
                long nextVal = Convert.ToInt64(rs);
                return nextVal;
            }
            catch (Exception ex)
            {
                string errorSource = "Fisbank.Cbs.BusinessLogic.LocalDataSource.GetDataFromCommand<T>(string ConnectionString, string procName, params object[] paramsProcs)";
                ErrorLog.Log(Environment.NewLine + "  - ErrorSource: " + errorSource + Environment.NewLine + "  - ErrorMessage: " + ex.Message);
                return 0;
            }
        }

    }
}
