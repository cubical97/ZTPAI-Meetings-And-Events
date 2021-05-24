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
            ResultLogin resultLoginRegister = new ResultLogin();
            resultLoginRegister.Logged = false;

            if (email == null)
            {
                resultLoginRegister.ErrorMessage = "Please, type email";
                return Json(resultLoginRegister);
            }

            if (password == null)
            {
                resultLoginRegister.ErrorMessage = "Please, type password";
                return Json(resultLoginRegister);
            }

            if (username == null)
            {
                resultLoginRegister.ErrorMessage = "Please, type username";
                return Json(resultLoginRegister);
            }

            if (!IsValidEmail(email))
            {
                resultLoginRegister.ErrorMessage = "Bad email!";
                return Json(resultLoginRegister);
            }

            try
            {
                using (var _context = new AppDBContext())
                {
                    Users users2 = _context.Users.Where(users => (users.email == email))
                        .FirstOrDefault();
                    if (users2 != null)
                    {
                        resultLoginRegister.ErrorMessage = "This login is alreadu used!";
                        return Json(resultLoginRegister);
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
                        
                        resultLoginRegister.Logged = true;
                        resultLoginRegister.Username = users.username;
                        resultLoginRegister.IdUser = users.id_user;
                    }
                    catch
                    {
                        resultLoginRegister.ErrorMessage = "Can't create account!";
                    }
                    
                    return Json(resultLoginRegister);
                }
            }
            catch (NullReferenceException)
            {
                resultLoginRegister.ErrorMessage = "Check login and password!";
                return Json(resultLoginRegister);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}