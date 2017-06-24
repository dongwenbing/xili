using System;
using System.Collections.Generic;
using System.Data;
namespace MB.Core.Common.Util
{
    public class ConvertUtil
    {
        public static bool ToBool(object o)
        {
            return Convert.ToBoolean(o);
        }
        public static bool ToBool(object o, bool defaultValue)
        {
            if (o == null || o == DBNull.Value)
            {
                return defaultValue;
            }
            bool result;
            try
            {
                result = Convert.ToBoolean(o);
            }
            catch
            {
                result = defaultValue;
            }
            return result;
        }
        public static int ToInt(object o)
        {
            return Convert.ToInt32(o);
        }
        public static int ToInt(object o, int defaultValue)
        {
            if (o == null || o == DBNull.Value)
            {
                return defaultValue;
            }
            int result;
            try
            {
                result = Convert.ToInt32(o);
            }
            catch
            {
                result = defaultValue;
            }
            return result;
        }
        public static uint ToUInt(object o)
        {
            return Convert.ToUInt32(o);
        }
        public static uint ToUInt(object o, uint defaultValue)
        {
            if (o == null || o == DBNull.Value)
            {
                return defaultValue;
            }
            uint result;
            if (!uint.TryParse(o.ToString(), out result))
            {
                result = defaultValue;
            }
            return result;
        }
        public static sbyte ToSByte(object o)
        {
            return ConvertUtil.ToSByte(o, 0u);
        }
        public static sbyte ToSByte(object o, uint defaultValue)
        {
            if (o == null || o == DBNull.Value)
            {
                return (sbyte)defaultValue;
            }
            sbyte result;
            if (!sbyte.TryParse(o.ToString(), out result))
            {
                result = (sbyte)defaultValue;
            }
            return result;
        }
        public static decimal ToDecimal(object o)
        {
            return Convert.ToDecimal(o);
        }
        public static decimal ToDecimal(object o, decimal defaultValue)
        {
            if (o == null || o == DBNull.Value)
            {
                return defaultValue;
            }
            decimal result;
            try
            {
                result = Convert.ToDecimal(o);
            }
            catch
            {
                result = defaultValue;
            }
            return result;
        }
        public static double ToDouble(object o)
        {
            return Convert.ToDouble(o);
        }
        public static double ToDouble(object o, double defaultValue)
        {
            if (o == null || o == DBNull.Value)
            {
                return defaultValue;
            }
            double result;
            try
            {
                result = Convert.ToDouble(o);
            }
            catch
            {
                result = defaultValue;
            }
            return result;
        }
        public static DateTime ToDateTime(object o)
        {
            return Convert.ToDateTime(o);
        }
        public static DateTime ToDateTime(object o, DateTime defaultValue)
        {
            if (o == null || o == DBNull.Value)
            {
                return defaultValue;
            }
            DateTime result;
            try
            {
                result = Convert.ToDateTime(o);
            }
            catch
            {
                result = defaultValue;
            }
            return result;
        }
        public static string ToString(object o)
        {
            return o.ToString();
        }
        public static string ToString(object o, string defaultValue)
        {
            if (o == null || o == DBNull.Value)
            {
                return defaultValue;
            }
            string result;
            try
            {
                result = o.ToString();
            }
            catch
            {
                result = defaultValue;
            }
            return result;
        }
        public static Guid ToGuid(object o)
        {
            if (o is Guid)
            {
                return (Guid)o;
            }
            return new Guid(o.ToString());
        }
        public static Guid ToGuid(object o, Guid defaultValue)
        {
            if (o == null || o == DBNull.Value)
            {
                return defaultValue;
            }
            Guid result;
            try
            {
                if (o is Guid)
                {
                    result = (Guid)o;
                }
                else
                {
                    result = new Guid(o.ToString());
                }
            }
            catch
            {
                result = defaultValue;
            }
            return result;
        }
        public static string ToMoneyString(object o)
        {
            return ConvertUtil.ToMoneyString(o, "0.00");
        }
        public static string ToMoneyString(object o, string format)
        {
            if (o == null || o == DBNull.Value)
            {
                return string.Empty;
            }
            if (o is decimal)
            {
                return ((decimal)o).ToString(format);
            }
            if (o is float)
            {
                return ((float)o).ToString(format);
            }
            if (o is double)
            {
                return ((double)o).ToString(format);
            }
            if (o is int)
            {
                return ((int)o).ToString(format);
            }
            if (o is long)
            {
                return ((long)o).ToString(format);
            }
            return o.ToString();
        }
        public static long ConvertToLong(object o)
        {
            return ConvertUtil.ConvertToLong(o, 0L);
        }
        public static long ConvertToLong(object o, long defaultValue)
        {
            if (o == null || o == DBNull.Value)
            {
                return defaultValue;
            }
            long result;
            try
            {
                result = Convert.ToInt64(o);
            }
            catch
            {
                result = defaultValue;
            }
            return result;
        }
        public static string ToChineseNumber(decimal num)
        {
            string str = num.ToString();
            string[] splitstr = str.Split(new char[]
            {
                (new char[]
                {
                    '.'
                })[0]
            });
            if (splitstr.Length == 1)
            {
                return ConvertUtil.ConvertData(str) + "圆整";
            }
            string rstr = ConvertUtil.ConvertData(splitstr[0]) + "圆";
            return rstr + ConvertUtil.ConvertXiaoShu(splitstr[1]) + "整";
        }
        private static bool IsPositveDecimal(string str)
        {
            decimal d;
            try
            {
                d = decimal.Parse(str);
            }
            catch (Exception)
            {
                return false;
            }
            return d > 0m;
        }
        private static string ConvertData(string str)
        {
            string rstr = "";
            int strlen = str.Length;
            if (strlen <= 4)
            {
                rstr = ConvertUtil.ConvertDigit(str);
            }
            else
            {
                if (strlen <= 8)
                {
                    string tmpstr = str.Substring(strlen - 4, 4);
                    rstr = ConvertUtil.ConvertDigit(tmpstr);
                    tmpstr = str.Substring(0, strlen - 4);
                    rstr = ConvertUtil.ConvertDigit(tmpstr) + "万" + rstr;
                    rstr = rstr.Replace("零万", "万");
                    rstr = rstr.Replace("零零", "零");
                }
                else
                {
                    if (strlen <= 12)
                    {
                        string tmpstr = str.Substring(strlen - 4, 4);
                        rstr = ConvertUtil.ConvertDigit(tmpstr);
                        tmpstr = str.Substring(strlen - 8, 4);
                        rstr = ConvertUtil.ConvertDigit(tmpstr) + "万" + rstr;
                        tmpstr = str.Substring(0, strlen - 8);
                        rstr = ConvertUtil.ConvertDigit(tmpstr) + "億" + rstr;
                        rstr = rstr.Replace("零億", "億");
                        rstr = rstr.Replace("零万", "零");
                        rstr = rstr.Replace("零零", "零");
                        rstr = rstr.Replace("零零", "零");
                    }
                }
            }
            strlen = rstr.Length;
            string a;
            if (strlen >= 2 && (a = rstr.Substring(strlen - 2, 2)) != null)
            {
                if (!(a == "佰零"))
                {
                    if (!(a == "仟零"))
                    {
                        if (!(a == "万零"))
                        {
                            if (a == "億零")
                            {
                                rstr = rstr.Substring(0, strlen - 2) + "億";
                            }
                        }
                        else
                        {
                            rstr = rstr.Substring(0, strlen - 2) + "万";
                        }
                    }
                    else
                    {
                        rstr = rstr.Substring(0, strlen - 2) + "仟";
                    }
                }
                else
                {
                    rstr = rstr.Substring(0, strlen - 2) + "佰";
                }
            }
            return rstr;
        }
        private static string ConvertXiaoShu(string str)
        {
            int strlen = str.Length;
            if (strlen == 1)
            {
                return ConvertUtil.ConvertChinese(str) + "角";
            }
            string tmpstr = str.Substring(0, 1);
            string rstr = ConvertUtil.ConvertChinese(tmpstr) + "角";
            tmpstr = str.Substring(1, 1);
            rstr = rstr + ConvertUtil.ConvertChinese(tmpstr) + "分";
            rstr = rstr.Replace("零分", "");
            return rstr.Replace("零角", "");
        }
        private static string ConvertDigit(string str)
        {
            int strlen = str.Length;
            string rstr = "";
            switch (strlen)
            {
                case 1:
                    rstr = ConvertUtil.ConvertChinese(str);
                    break;
                case 2:
                    rstr = ConvertUtil.Convert2Digit(str);
                    break;
                case 3:
                    rstr = ConvertUtil.Convert3Digit(str);
                    break;
                case 4:
                    rstr = ConvertUtil.Convert4Digit(str);
                    break;
            }
            rstr = rstr.Replace("拾零", "拾");
            strlen = rstr.Length;
            return rstr;
        }
        private static string Convert4Digit(string str)
        {
            string str2 = str.Substring(0, 1);
            string str3 = str.Substring(1, 1);
            string str4 = str.Substring(2, 1);
            string str5 = str.Substring(3, 1);
            string rstring = "";
            rstring = rstring + ConvertUtil.ConvertChinese(str2) + "仟";
            rstring = rstring + ConvertUtil.ConvertChinese(str3) + "佰";
            rstring = rstring + ConvertUtil.ConvertChinese(str4) + "拾";
            rstring += ConvertUtil.ConvertChinese(str5);
            rstring = rstring.Replace("零仟", "零");
            rstring = rstring.Replace("零佰", "零");
            rstring = rstring.Replace("零拾", "零");
            rstring = rstring.Replace("零零", "零");
            rstring = rstring.Replace("零零", "零");
            return rstring.Replace("零零", "零");
        }
        private static string Convert3Digit(string str)
        {
            string str2 = str.Substring(0, 1);
            string str3 = str.Substring(1, 1);
            string str4 = str.Substring(2, 1);
            string rstring = "";
            rstring = rstring + ConvertUtil.ConvertChinese(str2) + "佰";
            rstring = rstring + ConvertUtil.ConvertChinese(str3) + "拾";
            rstring += ConvertUtil.ConvertChinese(str4);
            rstring = rstring.Replace("零佰", "零");
            rstring = rstring.Replace("零拾", "零");
            rstring = rstring.Replace("零零", "零");
            return rstring.Replace("零零", "零");
        }
        private static string Convert2Digit(string str)
        {
            string str2 = str.Substring(0, 1);
            string str3 = str.Substring(1, 1);
            string rstring = "";
            rstring = rstring + ConvertUtil.ConvertChinese(str2) + "拾";
            rstring += ConvertUtil.ConvertChinese(str3);
            rstring = rstring.Replace("零拾", "零");
            return rstring.Replace("零零", "零");
        }
        private static string ConvertChinese(string str)
        {
            string cstr = "";
            switch (str)
            {
                case "0":
                    cstr = "零";
                    break;
                case "1":
                    cstr = "壹";
                    break;
                case "2":
                    cstr = "贰";
                    break;
                case "3":
                    cstr = "叁";
                    break;
                case "4":
                    cstr = "肆";
                    break;
                case "5":
                    cstr = "伍";
                    break;
                case "6":
                    cstr = "陆";
                    break;
                case "7":
                    cstr = "柒";
                    break;
                case "8":
                    cstr = "捌";
                    break;
                case "9":
                    cstr = "玖";
                    break;
            }
            return cstr;
        }
        public static List<Dictionary<string, object>> ConvertDataTableToList(DataTable dataTable)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            if (dataTable != null)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    Dictionary<string, object> row = new Dictionary<string, object>();
                    foreach (DataColumn dataColumn in dataTable.Columns)
                    {
                        row.Add(dataColumn.ColumnName, dataRow[dataColumn]);
                    }
                    rows.Add(row);
                }
            }
            return rows;
        }
        public static Dictionary<string, List<Dictionary<string, object>>> ConvertDataSetToDictionary(DataSet dataSet)
        {
            Dictionary<string, List<Dictionary<string, object>>> tables = new Dictionary<string, List<Dictionary<string, object>>>();
            if (dataSet != null)
            {
                foreach (DataTable dataTable in dataSet.Tables)
                {
                    List<Dictionary<string, object>> rows = ConvertUtil.ConvertDataTableToList(dataTable);
                    tables.Add(dataTable.TableName, rows);
                }
            }
            return tables;
        }
    }
}