﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Egojit.Common;

namespace Egojit.Web.UI.Page
{
    public partial class login : Web.UI.BasePage
    {
        protected string turl = string.Empty;
        /// <summary>
        /// 重写父类的虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
            this.Init += new EventHandler(UserPage_Init);
        }

        /// <summary>
        /// OnInit事件,检查用户是否已经登录
        /// </summary>
        void UserPage_Init(object sender, EventArgs e)
        {
            turl = DTRequest.GetString("turl");
            if (string.IsNullOrEmpty(turl))
            {
                turl = linkurl("usercenter", "index");
            }
            Model.users model = GetUserInfo();
            if (model != null)
            {
                //写入登录日志
                //new BLL.user_login_log().Add(model.id, model.user_name, "自动登录", DTRequest.GetIP());
                //自动登录,跳转URL
                HttpContext.Current.Response.Redirect(turl);
            }
        }

    }
}
