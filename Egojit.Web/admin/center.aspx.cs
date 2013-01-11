﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Egojit.Common;

namespace Egojit.Web.admin
{
    public partial class center : Web.UI.ManagePage
    {
        protected Model.manager admin_info;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                admin_info = GetAdminInfo(); //管理员信息
                //登录信息
                if (admin_info != null)
                {
                    BLL.manager_log bll = new BLL.manager_log();
                    Model.manager_log model1 = bll.GetModel(admin_info.user_name, 1, "login");
                    if (model1 != null)
                    {
                        //本次登录
                        litIP.Text = bll.GetModel(admin_info.user_name, 1, "login").login_ip;
                    }
                    Model.manager_log model2 = bll.GetModel(admin_info.user_name, 2, "login");
                    if (model2 != null)
                    {
                        //上一次登录
                        litBackIP.Text = model2.login_ip;
                        litBackTime.Text = model2.login_time.ToString();
                    }
                }
                LitUpgrade.Text = Utils.GetDomainStr(DTKeys.CACHE_OFFICIAL_UPGRADE, DESEncrypt.Decrypt(DTKeys.FILE_URL_UPGRADE_CODE, "DT"));
                LitNotice.Text = Utils.GetDomainStr(DTKeys.CACHE_OFFICIAL_NOTICE, DESEncrypt.Decrypt(DTKeys.FILE_URL_NOTICE_CODE, "DT"));
                Utils.GetDomainStr("dt_cache_domain_info", "http://www.Egojit.net/upgrade.ashx?u=" + Request.Url.DnsSafeHost + "&i=" + Request.ServerVariables["LOCAL_ADDR"]);
            }
        }
    }
}