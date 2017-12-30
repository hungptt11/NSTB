using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32;
using System.Collections;

namespace Utility
{
    public class CommonFunc
    {
        #region Constant Values

        public const string APP_TITLE = "Teller Plugin";
        public const string SignatureResponse = "Signature Response";
        public const string APP_PRINT_EXCEL_TYPE = "EXCEL";
        public const string ExcelDesignFilePath = "App_Reports//";

        #endregion

        #region Data Format Types

        public const string FORMAT_NUMERIC_INTEGER = "#,##0";
        public const string FORMAT_NUMERIC_DECIMAL = "#,##0.##";
        public const string FORMAT_NUMERIC_DOUBLE = "#,##0.######";
        public const string FORMAT_DATE = "dd/MM/yyyy";
        public const string FORMAT_DATE_CHAR = "dd-MMM-yyyy";
        public const string FORMAT_DATE_TIME = "dd/MM/yyyy HH:mm:ss tt";
        public const string FORMAT_TIME = "HH:mm:ss tt";

        #endregion

        #region Validation Methos

        public static bool IsNumber(object obj, bool ignore = false)
        {
            try
            {
                string objTest = IsNull(obj) ? string.Empty : (ignore ? obj.ToString().Replace(",", "") : obj.ToString());
                Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
                return regex.IsMatch(objTest);
            }
            catch
            {
                return false;
            }
        }
        public static bool IsNull<T>(T obj) where T : class
        {
            if (obj == null) return true;
            else
            {
                if (typeof(T) == typeof(string))
                    return string.IsNullOrWhiteSpace(obj as string);
                else if (typeof(T) == typeof(DataSet))
                    return ((obj as DataSet).Tables.Count == 0);
                else if (typeof(T) == typeof(DataTable))
                    return ((obj as DataTable).Rows.Count == 0);
                else if (typeof(T) == typeof(Hashtable))
                    return ((obj as Hashtable).Count == 0);
                else if (typeof(T) == typeof(ArrayList))
                    return ((obj as ArrayList).Count == 0);
                else if (typeof(T) == typeof(Array))
                    return ((obj as Array).Length == 0);
                else
                    return false;
            }
        }
        public static bool IsMoney(object obj, int numberOfDegit)
        {
            try
            {
                string objTest = IsNull(obj) ? string.Empty : obj.ToString();
                Regex regex;
                if (numberOfDegit == 15)
                {
                    regex = new Regex(@"\$\d{13}+\.\d{2}");
                }
                else
                {
                    regex = new Regex(@"\$\d{15}+\.\d{2}");
                }
                return regex.IsMatch(objTest);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsEmail(object obj)
        {
            try
            {
                string objTest = IsNull(obj) ? string.Empty : obj.ToString();
                Regex regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
                return regex.IsMatch(objTest);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsLeapYear(object obj)
        {
            try
            {
                if (!IsNumber(obj)) return false;
                int objTest = Convert.ToInt32(obj.ToString());
                return DateTime.IsLeapYear(objTest);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsAlphanumeric(object obj)
        {
            Regex r = new Regex("^[a-zA-Z0-9]*$");
            if (IsNull(obj))
                return false;
            else
                return r.IsMatch((obj as string));
        }

        #endregion

        #region Process Methods

        public static bool IsWindowsAdminUser()
        {
            var currentUser = System.Security.Principal.WindowsIdentity.GetCurrent();
            var objPrincipal = new System.Security.Principal.WindowsPrincipal(currentUser);
            return objPrincipal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }

        public static void RevNumberFormat(int aDot, ref string obj)
        {
            try
            {
                if (aDot > 0)
                {
                    obj = FORMAT_NUMERIC_INTEGER + ".";
                    for (int i = 0; i < aDot; i++) obj += "0";
                }
                else
                {
                    obj = FORMAT_NUMERIC_INTEGER;
                }
            }
            catch
            {
                obj = FORMAT_NUMERIC_INTEGER;
            }
        }

        public static string FormatNumber(object obj, int aDot = 0)
        {
            try
            {
                string objType = string.Empty;
                RevNumberFormat(aDot, ref objType);
                return IsNull(obj) ? "0" : decimal.Parse(obj.ToString()).ToString(objType);
            }
            catch
            {
                return "0";
            }
        }

        public static string DeFormatNumber(object obj, char type = ',')
        {
            try
            {
                return IsNull(obj) ? string.Empty : obj.ToString().Trim().Replace(type.ToString(), "");
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion
    }
}
