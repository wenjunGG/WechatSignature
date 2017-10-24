
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dos.WeChatWeb.Helper
{
     public class LogHelper
    {
        /// <summary>
        ///     文本Inof日志
        /// </summary>
        private static readonly ILog Loginfo = LogManager.GetLogger("loginfo");

        /// <summary>
        ///     文本error日志
        /// </summary>
        private static readonly ILog Logerror = LogManager.GetLogger("logerror");

        /// <summary>
        ///     写入数据库的日志
        /// </summary>
        private static readonly ILog LogAdoNet = LogManager.GetLogger("logAdoNet");

        public static void InitConfigure()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
        
        /// <summary>
        ///     需要写入数据库的日志
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="level">自定义级别:1info,2warn,3error,4fatal</param>
        public static void SaveMessage(LogMessage logMessage, int level)
        {
            switch (level)
            {
                case 1: //记录一般日志
                    {
                        if (LogAdoNet.IsInfoEnabled)
                        {
                            LogAdoNet.Info(logMessage);
                        }
                        break;
                    }
                case 2: //记录警告
                    {
                        if (LogAdoNet.IsWarnEnabled)
                        {
                            LogAdoNet.Warn(logMessage);
                        }
                        break;
                    }
                case 3: //错误
                    {
                        if (LogAdoNet.IsErrorEnabled)
                        {
                            LogAdoNet.Error(logMessage);
                        }
                        break;
                    }
                case 4: // 严重错误
                    {
                        if (LogAdoNet.IsFatalEnabled)
                        {
                            LogAdoNet.Fatal(logMessage);
                        }
                        break;
                    }
            }
        }

        /// <summary>
        ///     文本日志
        /// </summary>
        /// <param name="info"></param>
        public static void WriteLog(string info)
        {
            if (Loginfo.IsInfoEnabled)
            {
                Loginfo.Info(info);
            }
        }

        /// <summary>
        ///     错误记录
        /// </summary>
        /// <param name="info">附加信息</param>
        /// <param name="ex">错误</param>
        public static void ErrorLog(string info, Exception ex)
        {
            if (!string.IsNullOrEmpty(info) && ex == null)
            {
                Logerror.ErrorFormat("【附加信息】 : {0}<br>", new object[] { info });
            }
            else if (!string.IsNullOrEmpty(info) && ex != null)
            {
                string errorMsg = BeautyErrorMsg(ex);
                Logerror.ErrorFormat("【附加信息】 : {0}<br>{1}", new object[] { info, errorMsg });
            }
            else if (string.IsNullOrEmpty(info) && ex != null)
            {
                string errorMsg = BeautyErrorMsg(ex);
                Logerror.Error(errorMsg);
            }
        }

        /// <summary>
        ///     美化错误信息
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>错误信息</returns>
        private static string BeautyErrorMsg(Exception ex)
        {
            if (ex.InnerException != null)
            {
                string errorMsg = string.Format("【异常类型】：{0} <br>【异常信息】：{1} <br>【堆栈调用】：{2}",
                    ex.InnerException.GetType().Name, ex.InnerException.Message, ex.InnerException.StackTrace);
                errorMsg = errorMsg.Replace("\r\n", "<br>");
                errorMsg = errorMsg.Replace("位置", "<strong style=\"color:red\">位置</strong>");
                return errorMsg;
            }
            else
            {
                string errorMsg = string.Format("【异常类型】：{0} <br>【异常信息】：{1} <br>【堆栈调用】：{2}",
                    ex.GetType().Name, ex.Message, ex.StackTrace);
                errorMsg = errorMsg.Replace("\r\n", "<br>");
                errorMsg = errorMsg.Replace("位置", "<strong style=\"color:red\">位置</strong>");
                return errorMsg;
            }
        }
    }

    public class LogMessage
    {
        /// <summary>
        ///     企业ID
        /// </summary>
        public Guid EnterpriseId { get; set; }

        /// <summary>ss
        ///     应用key
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        ///     应用密钥
        /// </summary>
        public string AppSecretCode { get; set; }
    }
}
