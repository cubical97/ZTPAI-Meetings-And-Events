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
        // https://localhost:44348/accountlogin/get?password=abaca&email=admin2@mail.com
        public JsonResult Get(string email, string password)
        {
            if (email != null && password != null)
            {
                try
                {
                    using (var _context = new AppDBContext())
                    {
                        byte[] pass;
                        using (SHA256 mySHA256 = SHA256.Create())
                        {
                            pass = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(password));
                        }

                        Users users = _context.users.Where(users => (users.email == email && users.password == pass)).FirstOrDefault();
                        
                        // TODO prepare different json
                        
                        return Json(users);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return Json(null);
        }
    }
}
