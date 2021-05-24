using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using meetings_and_events.Data;
using meetings_and_events.Models;
using Microsoft.AspNetCore.Mvc;

namespace meetings_and_events.Controllers
{
    public class AccountLoginController : Controller
    {
        public JsonResult Get(string email, string password)
        {
            ResultLogin resultLogin = new ResultLogin();
            if (email == null || password == null)
            {
                resultLogin.ErrorMessage = "Please, type login and password";
                return Json(resultLogin);
            }

            try
            {
                using (var _context = new AppDBContext())
                {
                    byte[] pass;
                    using (SHA256 mySHA256 = SHA256.Create())
                    {
                        pass = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    }

                    Users users = _context.Users.Where(users => (users.email == email && users.password == pass))
                        .FirstOrDefault();

                    if (users != null)
                        resultLogin.Logged = true;
                    resultLogin.Username = users.username;
                    resultLogin.IdUser = users.id_user;

                    // TODO JWT Authentication
                    return Json(resultLogin);
                }
            }
            catch (NullReferenceException)
            {
                resultLogin.ErrorMessage = "Check login and password!";
                return Json(resultLogin);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
