using Dos.WeChat;
using Dos.WeChatWeb.Helper;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dos.WeChatWeb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/


        public bool Check(string signature, string timestamp, string nonce, string echostr)
        {

            LogHelper.WriteLog("22222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222");

            JoinToken joinToken = new JoinToken();
            joinToken.signature = signature;
            joinToken.timestamp = timestamp;
            joinToken.nonce = nonce;
            joinToken.echostr = echostr;
            bool bo=joinToken.Check();
            LogHelper.WriteLog("返回结果："+bo);
            return bo;
        }



        public ActionResult Index1()
        {
           //获取access-token
           TokenResult token=TokenHelper.GetAccessToken();

            //关注者列表集合
           var CareUserList=UserHelper.GetCareUsers();

            //获取用户信息
           WeChatParam weparamUser = new WeChatParam()
           {
               OpenId=CareUserList.next_openid,
               WeChatType = EnumHelper.WeChatType.Public
           };
           var UserInfo=UserHelper.GetUserInfo(weparamUser);

            return View();
        }

    }
}
