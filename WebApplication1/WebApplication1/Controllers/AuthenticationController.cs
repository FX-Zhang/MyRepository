using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoLogin(UserDetails u)
        {
            if (ModelState.IsValid)
            {
                EmployeeBusinessLayer bal = new EmployeeBusinessLayer();

                UserStatus status = bal.GetUserValidity(u);
                bool IsAdmin = false;
                if(status == UserStatus.AuthenticatedAdmin)
                {
                    IsAdmin = true;
                }
                else if(status == UserStatus.AuthenticatedUser)
                {
                    IsAdmin = false;
                }
                else
                {
                    ModelState.AddModelError("CredentialError", "Invalid user name or password");
                    return View("Login");
                }
                FormsAuthentication.SetAuthCookie(u.UserName, false);
                Session["IsAdmin"] = IsAdmin;

                return RedirectToAction("Index", "Employee");   //方法名 和 controller名

            }
            else
            {
                return View("Login");
            }
        }
        //[HttpPost]
        //public ActionResult DoLogin(UserDetails u)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        EmployeeBusinessLayer bal = new EmployeeBusinessLayer();
        //        if (bal.IsValidUser(u))
        //        {
        //            FormsAuthentication.SetAuthCookie(u.UserName, false);          //创建认证cookie  false确定是否创建永久有用的Cookie  不能缺写SetAuthCookie
        //            return RedirectToAction("Index", "Employee");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("CredentialError", "Invalid user name or password");
        //            return View("Login");
        //        }

        //    }
        //    else
        //        return View("Login");         
        //}

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();      //FormsAuthentication  管理 Web 应用程序的窗体身份验证服务                    SignOut() 从浏览器中移除窗体身份验证票证
            Session.Abandon();                     //必须增加清除Seesion的代码，否则 logout后还是能访问AddNew
            return RedirectToAction("Login");
        }
    }
}