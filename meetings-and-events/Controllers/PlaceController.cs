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

        [AllowAnonymous]
        public JsonResult Placeinfo(int id)
        {
            ResultPlaceList result = null;

            using (var _context = new AppDBContext())
            {
                var place = _context.Place.Where(place1 => (place1.id_place == id)).First();

                result = PlaceToResult(place);
            }

            return new JsonResult(result);
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

        [HttpPost]
        [Authorize]
        public IActionResult CreatePlace([FromBody] CreatePlaceModel place)
        {
            if (place == null || place.Title == null || place.Country == null || place.City == null ||
                place.Street == null || place.Number == null)
                return BadRequest("Invalid request");

            string get_email = "";
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity.Claims;
            var usernameClaim = claim
                .Where(x => x.Type == ClaimTypes.Name)
                .FirstOrDefault();
            if (usernameClaim != null)
                get_email = usernameClaim.Value;
            if (get_email.Length < 1)
                return Unauthorized("Invalid user");

            Users user = LoadUser(get_email);
            if (user == null)
                return Unauthorized("Invalid user");

            /*
            Console.WriteLine($"\nUser: {get_email} try to add place as:\n" +
                              $"country: {place.Country}\ncity: {place.City}\n" +
                              $"street: {place.Street}\n Number: {place.Number}\n");

            if (place.Description != null)
                Console.WriteLine($"description: {place.Description}\n");
            else
                Console.WriteLine("description: null\n");

            if (place.TimeOC1 != null)
                Console.WriteLine($"time PN: {place.TimeOC1[0]} - {place.TimeOC1[1]}");
            if (place.TimeOC2 != null)
                Console.WriteLine($"time WT: {place.TimeOC2[0]} - {place.TimeOC2[1]}");
            if (place.TimeOC3 != null)
                Console.WriteLine($"time SR: {place.TimeOC3[0]} - {place.TimeOC3[1]}");
            if (place.TimeOC4 != null)
                Console.WriteLine($"time CZ: {place.TimeOC4[0]} - {place.TimeOC4[1]}");
            if (place.TimeOC5 != null)
                Console.WriteLine($"time PT: {place.TimeOC5[0]} - {place.TimeOC5[1]}");
            if (place.TimeOC6 != null)
                Console.WriteLine($"time SO: {place.TimeOC6[0]} - {place.TimeOC6[1]}");
            if (place.TimeOC7 != null)
                Console.WriteLine($"time ND: {place.TimeOC7[0]} - {place.TimeOC7[1]}");
                */

            if (place.Title.Length < 1)
                return Unauthorized("Missing title");
            if (place.Country.Length < 1)
                return Unauthorized("Missing country name");
            if (place.City.Length < 1)
                return Unauthorized("Missing city name");
            if (place.Street.Length < 1)
                return Unauthorized("Missing street name");
            if (place.Number.Length < 1)
                return Unauthorized("Missing number name");

            if (String.IsNullOrEmpty(place.Description))
                place.Description = null;

            TimeSpan timeOpen;
            TimeSpan timeClose;
            if (place.TimeOC1 != null)
            {
                timeOpen = TimeSpan.Parse(place.TimeOC1[0]);
                timeClose = TimeSpan.Parse(place.TimeOC1[1]);
                if (timeOpen >= timeClose)
                    return Unauthorized("Wrong time");
            }

            if (place.TimeOC2 != null)
            {
                timeOpen = TimeSpan.Parse(place.TimeOC2[0]);
                timeClose = TimeSpan.Parse(place.TimeOC2[1]);
                if (timeOpen >= timeClose)
                    return Unauthorized("Wrong time");
            }

            if (place.TimeOC3 != null)
            {
                timeOpen = TimeSpan.Parse(place.TimeOC3[0]);
                timeClose = TimeSpan.Parse(place.TimeOC3[1]);
                if (timeOpen >= timeClose)
                    return Unauthorized("Wrong time");
            }

            if (place.TimeOC4 != null)
            {
                timeOpen = TimeSpan.Parse(place.TimeOC4[0]);
                timeClose = TimeSpan.Parse(place.TimeOC4[1]);
                if (timeOpen >= timeClose)
                    return Unauthorized("Wrong time");
            }

            if (place.TimeOC5 != null)
            {
                timeOpen = TimeSpan.Parse(place.TimeOC5[0]);
                timeClose = TimeSpan.Parse(place.TimeOC5[1]);
                if (timeOpen >= timeClose)
                    return Unauthorized("Wrong time");
            }

            if (place.TimeOC6 != null)
            {
                timeOpen = TimeSpan.Parse(place.TimeOC6[0]);
                timeClose = TimeSpan.Parse(place.TimeOC6[1]);
                if (timeOpen >= timeClose)
                    return Unauthorized("Wrong time");
            }

            if (place.TimeOC7 != null)
            {
                timeOpen = TimeSpan.Parse(place.TimeOC7[0]);
                timeClose = TimeSpan.Parse(place.TimeOC7[1]);
                if (timeOpen >= timeClose)
                    return Unauthorized("Wrong time");
            }

            using (var _context = new AppDBContext())
            {
                using (var _contextTransaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        Place new_place = new Place();
                        new_place.id_user = user.id_user;
                        new_place.title = place.Title;
                        new_place.description = place.Description;

                        new_place.multi_time = true;
                        new_place.Place_data_multitime = new List<Place_data_multitime>();

                        if (place.TimeOC1 != null)
                        {
                            Place_data_multitime new_multitime = new Place_data_multitime();
                            new_multitime.day_week = E_day_week.MONDAY;
                            new_multitime.start_date = TimeSpan.Parse($"{place.TimeOC1[0]}:00");
                            new_multitime.end_date = TimeSpan.Parse($"{place.TimeOC1[1]}:00");
                            new_place.Place_data_multitime.Add(new_multitime);
                        }

                        if (place.TimeOC2 != null)
                        {
                            Place_data_multitime new_multitime = new Place_data_multitime();
                            new_multitime.day_week = E_day_week.TUESDAY;
                            new_multitime.start_date = TimeSpan.Parse($"{place.TimeOC2[0]}:00");
                            new_multitime.end_date = TimeSpan.Parse($"{place.TimeOC2[1]}:00");
                            new_place.Place_data_multitime.Add(new_multitime);
                        }

                        if (place.TimeOC3 != null)
                        {
                            Place_data_multitime new_multitime = new Place_data_multitime();
                            new_multitime.day_week = E_day_week.WEDNESDAY;
                            new_multitime.start_date = TimeSpan.Parse($"{place.TimeOC3[0]}:00");
                            new_multitime.end_date = TimeSpan.Parse($"{place.TimeOC3[1]}:00");
                            new_place.Place_data_multitime.Add(new_multitime);
                        }

                        if (place.TimeOC4 != null)
                        {
                            Place_data_multitime new_multitime = new Place_data_multitime();
                            new_multitime.day_week = E_day_week.THURSDAY;
                            new_multitime.start_date = TimeSpan.Parse($"{place.TimeOC4[0]}:00");
                            new_multitime.end_date = TimeSpan.Parse($"{place.TimeOC4[1]}:00");
                            new_place.Place_data_multitime.Add(new_multitime);
                        }

                        if (place.TimeOC5 != null)
                        {
                            Place_data_multitime new_multitime = new Place_data_multitime();
                            new_multitime.day_week = E_day_week.FRIDAY;
                            new_multitime.start_date = TimeSpan.Parse($"{place.TimeOC5[0]}:00");
                            new_multitime.end_date = TimeSpan.Parse($"{place.TimeOC5[1]}:00");
                            new_place.Place_data_multitime.Add(new_multitime);
                        }

                        if (place.TimeOC6 != null)
                        {
                            Place_data_multitime new_multitime = new Place_data_multitime();
                            new_multitime.day_week = E_day_week.SATURDAY;
                            new_multitime.start_date = TimeSpan.Parse($"{place.TimeOC6[0]}:00");
                            new_multitime.end_date = TimeSpan.Parse($"{place.TimeOC6[1]}:00");
                            new_place.Place_data_multitime.Add(new_multitime);
                        }

                        if (place.TimeOC7 != null)
                        {
                            Place_data_multitime new_multitime = new Place_data_multitime();
                            new_multitime.day_week = E_day_week.SUNDAY;
                            new_multitime.start_date = TimeSpan.Parse($"{place.TimeOC7[0]}:00");
                            new_multitime.end_date = TimeSpan.Parse($"{place.TimeOC7[1]}:00");
                            new_place.Place_data_multitime.Add(new_multitime);
                        }

                        new_place.Place_address = new Place_address();
                        new_place.Place_address.country = place.Country;
                        new_place.Place_address.city = place.City;
                        new_place.Place_address.street = place.Street;
                        new_place.Place_address.number = place.Number;

                        _context.Place.Add(new_place);

                        _context.SaveChanges();
                        _contextTransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        _contextTransaction.Rollback();
                        throw e;
                        return BadRequest("Can't create new place");
                    }
                }
            }

            //TODO check and return index in database

            return Ok();
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateMeeting([FromBody] CreateMeetingModel place)
        {
            if (place == null || place.Title == null || place.Country == null || place.City == null ||
                place.Street == null || place.Number == null || place.Datepicker == null || place.TimeOC == null ||
                place.TimeOC.Length != 2)
                return BadRequest("Invalid request");

            string get_email = "";
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity.Claims;
            var usernameClaim = claim
                .Where(x => x.Type == ClaimTypes.Name)
                .FirstOrDefault();
            if (usernameClaim != null)
                get_email = usernameClaim.Value;
            if (get_email.Length < 1)
                return Unauthorized("Invalid user");

            Users user = LoadUser(get_email);
            if (user == null)
                return Unauthorized("Invalid user");

            /*
            Console.WriteLine($"\nUser: {get_email} try to add meeting as:\n" +
                              $"country: {place.Country}\ncity: {place.City}\n" +
                              $"street: {place.Street}\n Number: {place.Number}\n");

            if (place.Description != null)
                Console.WriteLine($"description: {place.Description}\n");
            else
                Console.WriteLine("description: null\n");


            Console.WriteLine($"day: {place.Datepicker.Day} {place.Datepicker.Month} {place.Datepicker.Year}");
            Console.WriteLine($"time: {place.TimeOC[0]} - {place.TimeOC[1]}");
            */

            if (place.Title.Length < 1)
                return Unauthorized("Missing title");
            if (place.Country.Length < 1)
                return Unauthorized("Missing country name");
            if (place.City.Length < 1)
                return Unauthorized("Missing city name");
            if (place.Street.Length < 1)
                return Unauthorized("Missing street name");
            if (place.Number.Length < 1)
                return Unauthorized("Missing number name");

            if (String.IsNullOrEmpty(place.Description))
                place.Description = null;

            TimeSpan timeOpen = TimeSpan.Parse(place.TimeOC[0]);
            TimeSpan timeClose = TimeSpan.Parse(place.TimeOC[1]);

            if (timeOpen >= timeClose)
                return Unauthorized("Wrong time");

            using (var _context = new AppDBContext())
            {
                using (var _contextTransaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        Place new_place = new Place();
                        new_place.id_user = user.id_user;
                        new_place.title = place.Title;
                        new_place.description = place.Description;

                        new_place.multi_time = false;
                        new_place.Place_data_onetime = new Place_data_onetime();
                        new_place.Place_data_onetime.start_date = DateTime.Parse(
                            $"{place.Datepicker.Day}/{place.Datepicker.Month}/{place.Datepicker.Year} {place.TimeOC[0]}:00");
                        new_place.Place_data_onetime.end_date = DateTime.Parse(
                            $"{place.Datepicker.Day}/{place.Datepicker.Month}/{place.Datepicker.Year} {place.TimeOC[1]}:00");

                        new_place.Place_address = new Place_address();
                        new_place.Place_address.country = place.Country;
                        new_place.Place_address.city = place.City;
                        new_place.Place_address.street = place.Street;
                        new_place.Place_address.number = place.Number;

                        _context.Place.Add(new_place);

                        _context.SaveChanges();
                        _contextTransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        _contextTransaction.Rollback();
                        throw e;
                        return BadRequest("Can't create new event");
                    }
                }
            }

            //TODO check and return index in database

            return Ok();
        }

        private Users LoadUser(string email)
        {
            Users user;
            try
            {
                using (var _context = new AppDBContext())
                {
                    user = _context.Users.Where(users => (users.email == email)).First();
                }
            }
            catch
            {
                user = null;
            }

            return user;
        }

        private ResultPlaceList PlaceToResult(Place place1)
        {
            ResultPlaceList result1 = new ResultPlaceList();
            result1.Id_place = place1.id_place;
            result1.Title = place1.title;
            result1.Description = place1.description;
            if (result1.Description != null)
                result1.Description = result1.Description;
            result1.Image = place1.image;
            if (result1.Image == null)
                result1.Image = defaultImage;
            result1.Multitime = place1.multi_time;

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