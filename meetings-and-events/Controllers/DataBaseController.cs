using System;
using System.Collections.Generic;
using meetings_and_events.Data;
using meetings_and_events.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace meetings_and_events.Controllers
{
    public class DataBaseController : Controller
    {
        
        /*
        // GET
        public JsonResult ConnectionTest()
        {
            DatabaseLinkController database = DatabaseLinkController.GetInstance();

            string sql = "SELECT version();";
            using NpgsqlCommand cmd = new NpgsqlCommand(sql, database.GetConnection());
            string version = cmd.ExecuteScalar().ToString();
            if (version.Length > 5)
                return new JsonResult(true);
            return new JsonResult(false);
        }

        // insert user
        public IEnumerable<Users> Get()
        {
            //CRUD
            using (var _context = new AppDBContext())
            {
                Users users = new Users();
                users.username = "user1";
                users.password = "abaca";
                users.email = "user1@mail.com";
                _context.users.Add(users);
                _context.SaveChanges();
            }

            return new List<Users>();
        }
        */
        
    }
}
