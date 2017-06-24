
using System;
using System.Text;
namespace MB.Code.Common.Util
{
    public class CommonUtil
    {
        public static string GetChineseSpell(string strText)
        {
            int len = strText.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                myStr += CommonUtil.GetSpell(strText.Substring(i, 1));
            }
            return myStr;
        }
        private static string GetSpell(string cnChar)
        {
            byte[] arrCn = Encoding.Default.GetBytes(cnChar);
            if (arrCn.Length > 1)
            {
                int area = (int)arrCn[0];
                int pos = (int)arrCn[1];
                int code = (area << 8) + pos;
                int[] areacode = new int[]
                {
                    45217,
                    45253,
                    45761,
                    46318,
                    46826,
                    47010,
                    47297,
                    47614,
                    48119,
                    48119,
                    49062,
                    49324,
                    49896,
                    50371,
                    50614,
                    50622,
                    50906,
                    51387,
                    51446,
                    52218,
                    52698,
                    52698,
                    52698,
                    52980,
                    53689,
                    54481
                };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25)
                    {
                        max = areacode[i + 1];
                    }
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[]
                        {
                            (byte)(65 + i)
                        });
                    }
                }
                return string.Empty;
            }
            if (arrCn[0] >= 97 && arrCn[0] <= 122)
            {
                return Encoding.Default.GetString(new byte[]
                {
                    (byte)(arrCn[0] - 32)
                });
            }
            return cnChar;
        }
    }
}
