using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using meetings_and_events.Data;
using meetings_and_events.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;

namespace meetings_and_events.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : Controller
    {

        [AllowAnonymous]
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            if (user == null || user.Email == null || user.Password == null)
                return BadRequest("invalid client request");

            if (String.IsNullOrWhiteSpace(user.Email) || String.IsNullOrWhiteSpace(user.Password))
                return Unauthorized("Please, type login and password");

            try
            {
                using (var _context = new AppDBContext())
                {
                    byte[] pass;
                    using (SHA256 mySHA256 = SHA256.Create())
                    {
                        pass = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                    }

                    Users users = _context.Users.Where(users => (users.email == user.Email && users.password == pass))
                        .FirstOrDefault();

                    if (users != null)
                    {
                        return Ok(new { Token = CreateTokenString(users) });
                    }
                }
            }
            catch (Exception)
            {
            }

            return Unauthorized("Wrong email or password");
        }

        [HttpGet, Route("getusername")]
        [Authorize]
        public JsonResult GetUserName()
        {
            string get_username = "";
            string get_email = "";

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity.Claims;

            var usernameClaim = claim
                .Where(x => x.Type == ClaimTypes.Name)
                .FirstOrDefault();

            if (usernameClaim != null)
                get_email = usernameClaim.Value;


            using (var _context = new AppDBContext())
            {
                Users users = _context.Users.Where(users => (users.email == get_email))
                    .FirstOrDefault();
                if (users != null)
                    get_username = users.username;
            }

            return new JsonResult(get_username);
        }

        private static bool IsValidEmail(string email)
        {
            const string pattern =
                @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }

        private string CreateTokenString(Users users)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey.getKey()));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, users.email),
                new Claim(ClaimTypes.Role, "user")
            };

            var tokerOptions = new JwtSecurityToken(
                issuer: SecretKey.getIssuer(),
                audience: SecretKey.getAudiencey(),
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingCredentials
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(tokerOptions);

            return tokenString;
        }

        // insert user
        [AllowAnonymous]
        [HttpPost, Route("register")]
        public IActionResult Register([FromBody] RegisterModel user)
        {
            if (user == null || user.Email == null || user.Password == null || user.Username == null)
                return BadRequest("invalid client request");

            if (String.IsNullOrWhiteSpace(user.Email) || String.IsNullOrWhiteSpace(user.Password) ||
                String.IsNullOrWhiteSpace(user.Username))
                return Unauthorized("Please, type login and password");

            if (!IsValidEmail(user.Email))
            {
                return Unauthorized("Bad email!");
            }

            if (user.Password.Length < 8)
            {
                return Unauthorized("Password must contain at least 8 characters!");
            }

            try
            {
                using (var _context = new AppDBContext())
                {
                    Users users2 = _context.Users.Where(users => (users.email == user.Email))
                        .FirstOrDefault();
                    if (users2 != null)
                    {
                        return Unauthorized("This login is alreadu used!");
                    }

                    Users users = new Users();
                    users.username = user.Username;
                    using (SHA256 mySHA256 = SHA256.Create())
                    {
                        users.password = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                    }

                    users.email = user.Email;
                    users.username = user.Username;
                    users.create_date = DateTime.Now;

                    // add to database
                    try
                    {
                        _context.Users.Add(users);
                        _context.SaveChanges();

                        return Ok(new { Token = CreateTokenString(users) });
                    }
                    catch
                    {
                        return Unauthorized("Can't create account!");
                    }
                }
            }
            catch (NullReferenceException)
            {
                return Unauthorized("Check login and password!");
            }
            catch
            {
            }

            return Unauthorized("Can't create account!");
        }

        // delete user
        [HttpPost, Route("delete")]
        [Authorize]
        public IActionResult Delete([FromBody] LoginModel user)
        {
            if (user == null || user.Email == null || user.Password == null)
                return BadRequest("invalid request");

            if (String.IsNullOrWhiteSpace(user.Email) || String.IsNullOrWhiteSpace(user.Password))
                return Unauthorized("Please, type login and password");

            string get_email = "";
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity.Claims;
            var usernameClaim = claim
                .Where(x => x.Type == ClaimTypes.Name)
                .FirstOrDefault();
            if (usernameClaim != null)
                get_email = usernameClaim.Value;
            if (!get_email.Equals(user.Email))
                return Unauthorized("Nice try");

            try
            {
                using (var _context = new AppDBContext())
                {
                    byte[] pass;
                    using (SHA256 mySHA256 = SHA256.Create())
                    {
                        pass = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                    }

                    Users users = _context.Users.Where(users => (users.email == user.Email && users.password == pass))
                        .FirstOrDefault();

                    if (users != null)
                    {
                        using (var _contextTransaction = _context.Database.BeginTransaction())
                        {
                            try
                            {
                                var places = _context.Place.Where(place => (place.id_user == users.id_user));
                                foreach (var place in places)
                                {
                                    //remove all created places with account
                                    var record1 =
                                        _context.Place_data_multitime.Where(rec => rec.id_place == place.id_place);
                                    foreach (var record in record1)
                                    {
                                        _context.Place_data_multitime.Remove(record);
                                    }

                                    var records2 =
                                        _context.Place_data_onetime.Where(rec => rec.id_place == place.id_place);
                                    foreach (var record in records2)
                                    {
                                        _context.Place_data_onetime.Remove(record);
                                    }

                                    var records3 =
                                        _context.Place_address.Where(rec => rec.id_place == place.id_place);
                                    foreach (var record in records3)
                                    {
                                        _context.Place_address.Remove(record);
                                    }

                                    var records4 =
                                        _context.Place_special_close.Where(rec => rec.id_place == place.id_place);
                                    foreach (var record in records4)
                                    {
                                        _context.Place_special_close.Remove(record);
                                    }

                                    var records5 =
                                        _context.Place_comments.Where(rec => rec.id_place == place.id_place);
                                    foreach (var record in records5)
                                    {
                                        _context.Place_comments.Remove(record);
                                    }

                                    var records6 =
                                        _context.Place_rate.Where(rec => rec.id_place == place.id_place);
                                    foreach (var record in records6)
                                    {
                                        _context.Place_rate.Remove(record);
                                    }

                                    var records7 =
                                        _context.User_follow.Where(rec => rec.id_place == place.id_place);
                                    foreach (var record in records7)
                                    {
                                        _context.User_follow.Remove(record);
                                    }

                                    var records8 =
                                        _context.User_join.Where(rec => rec.id_place == place.id_place);
                                    foreach (var record in records8)
                                    {
                                        _context.User_join.Remove(record);
                                    }
                                }

                                //remove all created places with account
                                var record9 =
                                    _context.User_follow.Where(rec => rec.id_user == users.id_user);
                                foreach (var record in record9)
                                {
                                    _context.User_follow.Remove(record);
                                }

                                var record10 =
                                    _context.User_join.Where(rec => rec.id_user == users.id_user);
                                foreach (var record in record10)
                                {
                                    _context.User_join.Remove(record);
                                }

                                var record11 =
                                    _context.Place_rate.Where(rec => rec.id_user == users.id_user);
                                foreach (var record in record11)
                                {
                                    _context.Place_rate.Remove(record);
                                }

                                _context.Users.Remove(users);

                                _context.SaveChanges();
                                _contextTransaction.Commit();
                            }
                            catch (Exception e)
                            {
                                _contextTransaction.Rollback();
                                throw e;
                            }
                        }

                        return Ok();
                    }
                }
            }
            catch (Exception)
            {
                return Unauthorized("Error while deleting");
            }

            return Unauthorized("Wrong email or password");
        }

        [NonAction]
        public static int GetUserIp(ClaimsIdentity identity)
        {
            int result = -1;
            string get_email = null;

            IEnumerable<Claim> claim = identity.Claims;
            var usernameClaim = claim
                .Where(x => x.Type == ClaimTypes.Name)
                .FirstOrDefault();
            if (usernameClaim == null)
                return result;

            get_email = usernameClaim.Value;

            using (var _context = new AppDBContext())
            {
                try
                {
                    Users users = _context.Users.Where(users => (users.email == get_email))
                        .FirstOrDefault();
                    if (users != null)
                        result = users.id_user;
                }
                catch
                {
                }
            }

            return result;
        }
    }
}
