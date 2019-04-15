using Assignment_8.Security;
using EAD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_8.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult UserHome()
        {
           var files = EAD.DAL.FileDAO.GetFiles();
           return View(files);            
           
        }
    }
}
