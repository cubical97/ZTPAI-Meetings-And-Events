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
            LoginBlock loginBlock = new LoginBlock();
            if (email == null || password == null)
            {
                loginBlock.ErrorMessage = "Please, type login and password";
                return Json(loginBlock);
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
                        loginBlock.Logged = true;
                    loginBlock.Username = users.username;
                    loginBlock.IdUser = users.id_user;

                    return Json(loginBlock);
                }
            }
            catch (NullReferenceException)
            {
                loginBlock.ErrorMessage = "Check login and password!";
                return Json(loginBlock);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
