using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.Common

{
    public class WriteLog
    {
        private string _fileType;
        private static Dictionary<long, long> lockDic = new Dictionary<long, long>();
        /// <summary>  
        /// 获取或设置文件名称  
        /// </summary>  
        public string FileName
        {
            get { return _fileType; }
            set { _fileType = value; }
        }
        /// <summary>  
        /// 构造函数  
        /// </summary>  
        /// <param name="byteCount">每次开辟位数大小，这个直接影响到记录文件的效率</param>  
        /// <param name="fileName">文件全路径名</param>  
        public WriteLog(string fileType)
        {
            fileType = AppDomain.CurrentDomain.BaseDirectory + @"\Doc\Log\" + fileType + @"\" + fileType + " " + DateTime.Now.ToString("yyyyMMdd") + ".Log";
            _fileType = fileType;
        }
        /// <summary>  
        /// 创建文件  
        /// </summary>  
        /// <param name="fileName"></param>  
        public void Create(string fileType)
        {
            if (!System.IO.File.Exists(fileType))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(fileType))
                {
                    fs.Close();
                }
            }
        }
        /// <summary>  
        /// 写入文本  
        /// </summary>  
        /// <param name="content">文本内容</param>  
        private void Write(string content, string newLine)
        {
            content = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss>>> ") + content;
            if (string.IsNullOrEmpty(_fileType))
            {
                throw new Exception("FileName不能为空！");
            }
            using (System.IO.FileStream fs = new System.IO.FileStream(_fileType, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite, 8, System.IO.FileOptions.Asynchronous))
            {

                Byte[] dataArray = System.Text.Encoding.UTF8.GetBytes(content + newLine);
                bool flag = true;
                long slen = dataArray.Length;
                long len = 0;
                while (flag)
                {
                    try
                    {
                        if (len >= fs.Length)
                        {
                            fs.Lock(len, slen);
                            lockDic[len] = slen;
                            flag = false;
                        }
                        else
                        {
                            len = fs.Length;
                        }
                    }
                    catch (Exception ex)
                    {
                      
                        while (!lockDic.ContainsKey(len))
                        {
                            len += lockDic[len];
                        }
                    }
                }
                fs.Seek(len, System.IO.SeekOrigin.Begin);
                fs.Write(dataArray, 0, dataArray.Length);
                fs.Close();
            }
        }
        /// <summary>  
        /// 写入文件内容  
        /// </summary>  
        /// <param name="content"></param>  
        public void WriteLine(string content)
        {
            this.Write(content, System.Environment.NewLine);
        }
        /// <summary>  
        /// 写入文件  
        /// </summary>  
        /// <param name="content"></param>  
        public void Write(string content)
        {
            this.Write(content, "");
        }

    }
    public enum LogType
    {
        Trace,
        Error,
        Exception
            //此处增加枚举类型，需要记得在项目\log目录下新建对应类型日志目录
    }
    public  static class Logs
    {

        public delegate void Log(LogType type, string con);
        
        public  static void WirteLog(LogType type, string con)
        {


            WriteLog writelog = new WriteLog(type.ToString());
            writelog.WriteLine(con);
        }

    }

}
