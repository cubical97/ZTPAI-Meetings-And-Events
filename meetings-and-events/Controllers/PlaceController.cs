using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using meetings_and_events.Data;
using meetings_and_events.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace meetings_and_events.Controllers
{
    public class PlaceController : Controller
    {
        private string defaultImage = "testimage.jpg";
        
        [AllowAnonymous]
        public JsonResult List(int type = 0, int size = 10)
        {
            List<ResultPlaceList> result = new List<ResultPlaceList>();

            using (var _context = new AppDBContext())
            {
                var place = _context.Place.ToList();

                foreach (Place place1 in place)
                {
                    result.Add(PlaceToResult((place1)));
                }
            }

            return new JsonResult(result.ToArray());
        }
        
        
        [Authorize]
        public JsonResult ListJoined()
        { 
            List<ResultPlaceList> result = new List<ResultPlaceList>();

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

                //var joins = _context.User_join.Where(rec => (rec.id_user == users.id_user));
                var joins = _context.User_join.ToList();

                foreach (User_join join in joins)
                {
                    Place place1 = _context.Place.Where(place => (place.id_place == join.id_place))
                        .FirstOrDefault();

                    result.Add(PlaceToResult((place1)));
                }
            }

            return new JsonResult(result.ToArray());
        }

        [Authorize]
        public JsonResult ListFollowed()
        {
            List<ResultPlaceList> result = new List<ResultPlaceList>();

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

                // var follows = _context.User_follow.Where(rec => (rec.id_user == users.id_user));
                var follows = _context.User_follow.ToList();
                
                foreach (User_follow follow in follows)
                {
                    Place place1 = _context.Place.Where(place => (place.id_place == follow.id_place))
                        .FirstOrDefault();

                    result.Add(PlaceToResult((place1)));
                }
            }

            return new JsonResult(result.ToArray());
        }
        
        [Authorize]
        public JsonResult ListUserOwn()
        {
            List<ResultPlaceList> result = new List<ResultPlaceList>();

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

                var places = _context.Place.Where(place => (place.id_user == users.id_user));

                foreach (Place place1 in places)
                {
                    result.Add(PlaceToResult((place1)));
                }
            }

            return new JsonResult(result.ToArray());
        }

        private ResultPlaceList PlaceToResult(Place place1)
        {
            ResultPlaceList result1 = new ResultPlaceList();
            result1.Id_place = place1.id_place;
            result1.Title = place1.title;
            result1.Description = place1.description;
            if (result1.Description.Length > 46)
                result1.Description = result1.Description.Substring(0, 45) + "..."; // skrucenie opisu do wyświetlania
            result1.Image = place1.image;
            if (result1.Image == null)
                result1.Image = defaultImage;

            using (var _context = new AppDBContext())
            {
                var address = _context.Place_address
                    .Where(placeAddress => (placeAddress.id_place == place1.id_place)).First();
                result1.Address_city = address.city;

                var user = _context.Users.Where(users => (users.id_user == place1.id_user)).First();
                result1.Users_username = user.username;

                var rate = _context.Place_rate.Where(placeRate => (placeRate.id_place == place1.id_place)).ToList();

                result1.Rate_likes = 0;
                result1.Rate_dislikes = 0;
                foreach (var rate1 in rate)
                {
                    if (rate1.like)
                        result1.Rate_likes++;
                    else
                        result1.Rate_dislikes++;
                }
            }

            return result1;
        }
    }
}