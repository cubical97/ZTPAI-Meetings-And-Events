using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using meetings_and_events.Data;
using meetings_and_events.Models;
using Microsoft.AspNetCore.Mvc;

namespace meetings_and_events.Controllers
{
    public class AccountRegisterController : Controller
    {
        public static bool IsValidEmail(string email)
        {
            const string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";    
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);    
            return regex.IsMatch(email);
        }

        // insert user
        public JsonResult Post(string username, string password, string email)
        {
            LoginBlock loginBlockRegister = new LoginBlock();
            loginBlockRegister.Logged = false;

            if (email == null)
            {
                loginBlockRegister.ErrorMessage = "Please, type email";
                return Json(loginBlockRegister);
            }

            if (password == null)
            {
                loginBlockRegister.ErrorMessage = "Please, type password";
                return Json(loginBlockRegister);
            }

            if (username == null)
            {
                loginBlockRegister.ErrorMessage = "Please, type username";
                return Json(loginBlockRegister);
            }

            if (!IsValidEmail(email))
            {
                loginBlockRegister.ErrorMessage = "Bad email!";
                return Json(loginBlockRegister);
            }

            try
            {
                using (var _context = new AppDBContext())
                {
                    Users users2 = _context.Users.Where(users => (users.email == email))
                        .FirstOrDefault();
                    if (users2 != null)
                    {
                        loginBlockRegister.ErrorMessage = "This login is alreadu used!";
                        return Json(loginBlockRegister);
                    }

                    Users users = new Users();
                    users.username = username;
                    using (SHA256 mySHA256 = SHA256.Create())
                    {
                        users.password = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    }

                    users.email = email;
                    users.username = username;
                    users.create_date = DateTime.Now;

                    // add to database
                    try
                    {
                        _context.Users.Add(users);
                        _context.SaveChanges();
                        
                        loginBlockRegister.Logged = true;
                        loginBlockRegister.Username = users.username;
                        loginBlockRegister.IdUser = users.id_user;
                    }
                    catch
                    {
                        loginBlockRegister.ErrorMessage = "Can't create account!";
                    }
                    
                    return Json(loginBlockRegister);
                }
            }
            catch (NullReferenceException)
            {
                loginBlockRegister.ErrorMessage = "Check login and password!";
                return Json(loginBlockRegister);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}