using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Filters
{
    public class AdminFiter : ActionFilterAttribute           //自定义过滤器   使无法直接URL得到画面 
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!Convert.ToBoolean(filterContext.HttpContext.Session["IsAdmin"]))
            {
                filterContext.Result = new ContentResult()
                {
                    Content = "Unauthorized to access specified resource."
                };
            }
            //base.OnActionExecuted(filterContext);
        }
    }
}