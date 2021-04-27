using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using meetings_and_events.Data;
using meetings_and_events.Models;
using Microsoft.AspNetCore.Mvc;

namespace meetings_and_events.Controllers
{
    public class AccountRegisterController : Controller
    {
        // insert user
        public JsonResult Post(string username, string password, string email)
        {
            //CRUD
            if (username != null && email != null && password != null)
            {
                try
                {
                    using (var _context = new AppDBContext())
                    {
                        // TODO add extra validation

                        Users users = new Users();
                        users.username = username;
                        using (SHA256 mySHA256 = SHA256.Create())
                        {
                            users.password = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(password));
                        }
                        users.email = email;
                        users.create_date = DateTime.Now;
                        
                        // add to database
                        _context.users.Add(users);
                        _context.SaveChanges();
                        
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