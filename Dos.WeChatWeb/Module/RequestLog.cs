using Dos.WeChatWeb.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dos.WeChatWeb.Module
{
    
    /// <summary>
    /// 记录请求日志
    /// </summary>
    public class RequestLog : IHttpModule
    {
        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.EndRequest += Application_EndRequest;
        }

        /// <summary>
        /// 添加请求日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_EndRequest(object sender, EventArgs e)
        {
            var context = HttpContext.Current;
          
            string para = "";

            foreach (var item in context.Request.Form.AllKeys)
            {
                para += item + ":" + context.Request.Form[item]+ "; ";
            }

        

           
             string  RequestUrl = HttpContext.Current.Request.Url.ToString();
             string  RequestParameter = para;
            
            LogHelper.WriteLog(RequestUrl+"----参数："+RequestParameter);
        }
    }
}
