using EAD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAD.BAL
{
    public class UserBO
    {
        public static UserDTO ValidateUser(String pLogin, String pPassword)
        {
            return EAD.DAL.UserDAO.ValidateUser(pLogin, pPassword);
        }
        public static UserDTO GetUserById(int pid)
        {
            return EAD.DAL.UserDAO.GetUserById(pid);
        }
        public static UserDTO GetUserByLogin(string login)
        {
            return EAD.DAL.UserDAO.GetUserByLogin(login);
        }

    }
}
