using Assignment_8.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_8.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(String login, String password)
        {
            var obj = EAD.BAL.UserBO.ValidateUser(login, password);
            if (obj != null)
            {
                Session["user"] = obj;
                return Redirect("~/Home/User");
            }

            Response.Write("<script>alert('invalid Login/Password')</script>");
            ViewBag.Login = login;

            return View();
        }
        [HttpGet]
        public ActionResult Logout()
        {
            SessionManager.ClearSession();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult OpenFolder(int FolderId)
        {

            return View();
        }

        //[HttpGet]
        //public ActionResult CreateFolder()
        //{
        //    SessionManager.ClearSession();
        //    return RedirectToAction("Login");
        //}
	}
}