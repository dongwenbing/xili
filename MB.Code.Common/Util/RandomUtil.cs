using System;
namespace MB.Core.Common.Util
{
    public class RandomUtil
    {
        public static string GetCode(int num)
        {
            string[] source = new string[]
            {
                "0",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                "A",
                "B",
                "C",
                "D",
                "E",
                "F",
                "G",
                "H",
                "J",
                "K",
                "L",
                "M",
                "N",
                "P",
                "Q",
                "R",
                "S",
                "T",
                "U",
                "V",
                "W",
                "X",
                "Y",
                "Z"
            };
            string code = string.Empty;
            Random rd = new Random();
            for (int i = 0; i < num; i++)
            {
                code += source[rd.Next(0, source.Length)];
            }
            return code;
        }
        public static string GetNum(int num)
        {
            string[] source = new string[]
            {
                "0",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9"
            };
            string code = string.Empty;
            Random rd = new Random();
            for (int i = 0; i < num; i++)
            {
                code += source[rd.Next(0, source.Length)];
            }
            return code;
        }
        public static string GetNumNoneZero(int num)
        {
            string[] source = new string[]
            {
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9"
            };
            string code = string.Empty;
            Random rd = new Random();
            for (int i = 0; i < num; i++)
            {
                code += source[rd.Next(0, source.Length)];
            }
            return code;
        }
    }
}
