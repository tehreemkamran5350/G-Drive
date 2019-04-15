using EAD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Assignment_8.ApiControllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        public UserDTO Login(string login, string pswd)
        {
            var obj = EAD.BAL.UserBO.ValidateUser(login, pswd);
            if (obj != null)
            {
                //var Session = HttpContext.Current.Session;
                // Session["user"] = obj;                
                return obj;
            }
            return obj;
        }

       
       
    }
}