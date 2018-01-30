using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketDemo
{
    public class LogHelper
    {
        public delegate void LogEvent(string msg);
        public static List<string> allLines = new List<string>();
        public static int displayLength = 0;
        private static LogEvent OnLog { get; set; }

        /// <summary>
        /// winform使用后台线程写显示Log
        /// </summary>
        public static void SetOnLog(LogEvent e)
        {
            OnLog = e;
            //每100毫秒重新渲染到界面
            var timer = new System.Timers.Timer(100);
            timer.Elapsed += new System.Timers.ElapsedEventHandler((s, x) =>
            {
                try
                {
                    int count = allLines.Count;
                    if (displayLength == count)
                        return;
                    //最多保留Log行数
                    if (allLines.Count > 5000)
                        allLines.RemoveRange(0, 20);

                    displayLength = allLines.Count;
                    OnLog(string.Join("\r\n", allLines));
                }
                catch { }
            });
            timer.Enabled = true;
            timer.Start();
        }
        /// <summary>
        /// 普通的文件记录日志
        /// </summary>
        /// <param name="info"></param>
        public static void WriteLog(string info)
        {
            if (OnLog != null)
            {
                allLines.Add(string.Join(" ", DateTime.Now.ToString("HH:mm:ss"), info));
            }
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="info"></param>
        /// <param name="se"></param>
        public static void WriteLog(string info, Exception se)
        {
            if (OnLog != null)
            {
                allLines.Add(info);
            }
        }
    }
}
