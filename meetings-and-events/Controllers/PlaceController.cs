﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using meetings_and_events.Data;
using meetings_and_events.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
                    result.Add(PlaceToResult(place1));
                }
            }

            return new JsonResult(result.ToArray());
        }

        [AllowAnonymous]
        public JsonResult Placeinfo(int id)
        {
            ResultPlaceList result = null;
            int this_user_id = -1;

            try
            {
                using (var _context = new AppDBContext())
                {
                    string get_email = null;
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    IEnumerable<Claim> claim = identity.Claims;
                    var usernameClaim = claim
                        .Where(x => x.Type == ClaimTypes.Name)
                        .FirstOrDefault();
                    if (usernameClaim != null)
                        get_email = usernameClaim.Value;
                    Users users = _context.Users.Where(users => (users.email == get_email))
                        .FirstOrDefault();
                    if (users != null)
                    {
                        this_user_id = users.id_user;
                    }
                }
            }
            catch
            {
            }

            using (var _context = new AppDBContext())
            {
                try
                {
                    var place = _context.Place.Where(place1 => (place1.id_place == id)).First();

                    result = PlaceToResult(place, this_user_id, true);
                }
                catch
                {
                    result = null;
                }
            }

            return new JsonResult(result);
        }

        [AllowAnonymous]
        public JsonResult Placeinfodataplace(int id)
        {
            ResultPlaceDataPlace result;

            using (var _context = new AppDBContext())
            {
                try
                {
                    Place_data_multitime[] days = _context.Place_data_multitime.Where(place1 => (place1.id_place == id))
                        .ToArray();

                    result = new ResultPlaceDataPlace();

                    foreach (var day in days)
                    {
                        switch (day.day_week)
                        {
                            case E_day_week.MONDAY:
                                result.mo = $"{day.start_date.ToString().Substring(0, 5)}" +
                                            $" - {day.end_date.ToString().Substring(0, 5)}";
                                break;
                            case E_day_week.TUESDAY:
                                result.tu = $"{day.start_date.ToString().Substring(0, 5)}" +
                                            $" - {day.end_date.ToString().Substring(0, 5)}";
                                break;
                            case E_day_week.WEDNESDAY:
                                result.we = $"{day.start_date.ToString().Substring(0, 5)}" +
                                            $" - {day.end_date.ToString().Substring(0, 5)}";
                                break;
                            case E_day_week.THURSDAY:
                                result.th = $"{day.start_date.ToString().Substring(0, 5)}" +
                                            $" - {day.end_date.ToString().Substring(0, 5)}";
                                break;
                            case E_day_week.FRIDAY:
                                result.fr = $"{day.start_date.ToString().Substring(0, 5)}" +
                                            $" - {day.end_date.ToString().Substring(0, 5)}";
                                break;
                            case E_day_week.SATURDAY:
                                result.sat = $"{day.start_date.ToString().Substring(0, 5)}" +
                                             $" - {day.end_date.ToString().Substring(0, 5)}";
                                break;
                            case E_day_week.SUNDAY:
                                result.sun = $"{day.start_date.ToString().Substring(0, 5)}" +
                                             $" - {day.end_date.ToString().Substring(0, 5)}";
                                break;
                        }
                    }

                    Place_special_close[] close_days =
                        _context.Place_special_close.Where(close => (close.id_place == id)).ToArray();
                    List<string> close_dates = new List<string>();
                    foreach (Place_special_close close_day in close_days)
                    {
                        close_dates.Add(close_day.date.ToString("dd.MM"));
                    }

                    result.closed = close_dates.ToArray();
                }
                catch
                {
                    result = null;
                }
            }

            return new JsonResult(result);
        }

        [AllowAnonymous]
        public JsonResult Placeinfodatameeting(int id)
        {
            ResultPlaceDataMeeting result = null;

            using (var _context = new AppDBContext())
            {
                try
                {
                    Place_data_onetime day = _context.Place_data_onetime.Where(place1 => (place1.id_place == id))
                        .First();

                    result = new ResultPlaceDataMeeting();
                    result.date = day.start_date.ToString("yyyy'-'MM'-'dd");
                    result.starttime = day.start_date.ToShortTimeString();
                    result.endtime = day.end_date.ToShortTimeString();
                }
                catch
                {
                    result = null;
                }
            }

            return new JsonResult(result);
        }

        [AllowAnonymous]
        public JsonResult Placeinfocomments(int id)
        {
            List<ResultPlaceListComment> result = new List<ResultPlaceListComment>();

            using (var _context = new AppDBContext())
            {
                try
                {
                    var comments = _context.Place_comments.Where(place1 => (place1.id_place == id)).ToList();
                    comments.Reverse();
                    foreach (Place_comments comment in comments)
                    {
                        ResultPlaceListComment com = new ResultPlaceListComment();
                        com.Comment_id = comment.id_comment;
                        com.Createdate = comment.comment_date.ToString();
                        com.Comment = comment.comment;
                        try
                        {
                            Users users = _context.Users.Where(users => (users.id_user == comment.id_user)).First();
                            if (users != null)
                                com.Author = users.username;
                            else
                                com.Author = "deleted";
                        }
                        catch
                        {
                            com.Author = "deleted";
                        }

                        result.Add(com);
                    }
                }
                catch
                {
                    result = null;
                }
            }

            return new JsonResult(result);
        }

        [Authorize]
        public IActionResult Placeuserlikedislike(int id)
        {
            bool[] result = new bool[2] {false, false};

            using (var _context = new AppDBContext())
            {
                try
                {
                    string get_email = null;
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    IEnumerable<Claim> claim = identity.Claims;
                    var usernameClaim = claim
                        .Where(x => x.Type == ClaimTypes.Name)
                        .FirstOrDefault();
                    if (usernameClaim != null)
                        get_email = usernameClaim.Value;
                    Users users = _context.Users.Where(users => (users.email == get_email))
                        .FirstOrDefault();
                    if (users == null)
                        return Unauthorized("User not exists");

                    var get_rate = _context.Place_rate
                        .Where(place1 => (place1.id_place == id && place1.id_user == users.id_user)).FirstOrDefault();
                    if (get_rate != null)
                    {
                        if (get_rate.like)
                            result[0] = true;
                        else
                            result[1] = true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return Unauthorized("No user");
                }
            }

            return new JsonResult(result);
        }

        [Authorize]
        public IActionResult Placeuserjoinfollow(int id)
        {
            bool[] result = new bool[2] {false, false};

            using (var _context = new AppDBContext())
            {
                try
                {
                    string get_email = null;
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    IEnumerable<Claim> claim = identity.Claims;
                    var usernameClaim = claim
                        .Where(x => x.Type == ClaimTypes.Name)
                        .FirstOrDefault();
                    if (usernameClaim != null)
                        get_email = usernameClaim.Value;

                    Users users = _context.Users.Where(users => (users.email == get_email))
                        .FirstOrDefault();
                    if (users == null)
                        return Unauthorized("User not exists");

                    var join = _context.User_join
                        .Where(place1 => (place1.id_place == id && place1.id_user == users.id_user)).FirstOrDefault();
                    if (join != null)
                        result[0] = true;

                    var follow = _context.User_follow
                        .Where(place1 => (place1.id_place == id && place1.id_user == users.id_user)).FirstOrDefault();
                    if (follow != null)
                        result[1] = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return Unauthorized("No user");
                }
            }

            return new JsonResult(result);
        }

        public class RequestLikeSet
        {
            public int id { get; set; }
            public bool like { get; set; }
            public bool dislike { get; set; }
        }

        public class RequestJoinSet
        {
            public int id { get; set; }
            public bool setto { get; set; }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Placeuserlikeset([FromBody] RequestLikeSet req)
        {
            if (req == null)
                return BadRequest("Invalid request");

            Users users = null;
            using (var _context = new AppDBContext())
            {
                try
                {
                    string get_email = null;
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    IEnumerable<Claim> claim = identity.Claims;
                    var usernameClaim = claim
                        .Where(x => x.Type == ClaimTypes.Name)
                        .FirstOrDefault();
                    if (usernameClaim != null)
                        get_email = usernameClaim.Value;

                    users = _context.Users.Where(users => (users.email == get_email))
                        .FirstOrDefault();
                    if (users == null)
                        return Unauthorized("User not exists");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return Unauthorized("No user");
                }
            }

            using (var _context = new AppDBContext())
            {
                try
                {
                    if (req.like && req.dislike)
                        return BadRequest("Can't add like and dislike");

                    var record = _context.Place_rate.Where(place_rate =>
                        (place_rate.id_user == users.id_user && place_rate.id_place == req.id)).FirstOrDefault();

                    if (!req.like && !req.dislike && record != null)
                    {
                        _context.Place_rate.Remove(record);
                        _context.SaveChanges();
                    }
                    else if (req.like)
                    {
                        if (record != null)
                        {
                            if (!record.like)
                            {
                                record.like = true;
                                _context.Place_rate.Update(record);
                                _context.SaveChanges();
                            }
                        }
                        else
                        {
                            record = new Place_rate();
                            record.id_place = req.id;
                            record.id_user = users.id_user;
                            record.like = true;

                            _context.Place_rate.Add(record);
                            _context.SaveChanges();
                        }
                    }
                    else if (req.dislike)
                    {
                        if (record != null)
                        {
                            if (record.like)
                            {
                                record.like = false;
                                _context.Place_rate.Update(record);
                                _context.SaveChanges();
                            }
                        }
                        else
                        {
                            record = new Place_rate();
                            record.id_place = req.id;
                            record.id_user = users.id_user;
                            record.like = false;

                            _context.Place_rate.Add(record);
                            _context.SaveChanges();
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return Ok();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Placeuserjoinset([FromBody] RequestJoinSet req)
        {
            if (req == null)
                return BadRequest("Invalid request");

            Users users = null;
            using (var _context = new AppDBContext())
            {
                try
                {
                    string get_email = null;
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    IEnumerable<Claim> claim = identity.Claims;
                    var usernameClaim = claim
                        .Where(x => x.Type == ClaimTypes.Name)
                        .FirstOrDefault();
                    if (usernameClaim != null)
                        get_email = usernameClaim.Value;

                    users = _context.Users.Where(users => (users.email == get_email))
                        .FirstOrDefault();
                    if (users == null)
                        return Unauthorized("User not exists");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return Unauthorized("No user");
                }
            }

            using (var _context = new AppDBContext())
            {
                try
                {
                    var record = _context.User_join
                        .Where(user_join => (user_join.id_place == req.id && user_join.id_user == users.id_user))
                        .FirstOrDefault();
                    if (req.setto)
                    {
                        if (record == null)
                        {
                            record = new User_join();
                            record.id_place = req.id;
                            record.id_user = users.id_user;
                            _context.User_join.Add(record);
                            _context.SaveChanges();
                        }
                    }
                    else
                    {
                        if (record != null)
                        {
                            _context.User_join.Remove(record);
                            _context.SaveChanges();
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return Ok();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Placeuserfollowset([FromBody] RequestJoinSet req)
        {
            if (req == null)
                return BadRequest("Invalid request");

            Users users = null;
            using (var _context = new AppDBContext())
            {
                try
                {
                    string get_email = null;
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    IEnumerable<Claim> claim = identity.Claims;
                    var usernameClaim = claim
                        .Where(x => x.Type == ClaimTypes.Name)
                        .FirstOrDefault();
                    if (usernameClaim != null)
                        get_email = usernameClaim.Value;

                    users = _context.Users.Where(users => (users.email == get_email))
                        .FirstOrDefault();
                    if (users == null)
                        return Unauthorized("User not exists");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return Unauthorized("No user");
                }
            }

            using (var _context = new AppDBContext())
            {
                try
                {
                    var record = _context.User_follow
                        .Where(user_join => (user_join.id_place == req.id && user_join.id_user == users.id_user))
                        .FirstOrDefault();
                    if (req.setto)
                    {
                        if (record == null)
                        {
                            record = new User_follow();
                            record.id_place = req.id;
                            record.id_user = users.id_user;
                            _context.User_follow.Add(record);
                            _context.SaveChanges();
                        }
                    }
                    else
                    {
                        if (record != null)
                        {
                            _context.User_follow.Remove(record);
                            _context.SaveChanges();
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return Ok();
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateComment([FromBody] CreateCommentModel new_comment)
        {
            if (new_comment == null)
                return BadRequest("Invalid request");

            if (new_comment.PlaceID < 0 ||
                String.IsNullOrWhiteSpace(new_comment.CommentText))
                return BadRequest("Can't add empty comment");

            string get_email = null;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity.Claims;

            var usernameClaim = claim
                .Where(x => x.Type == ClaimTypes.Name)
                .FirstOrDefault();
            if (usernameClaim != null)
                get_email = usernameClaim.Value;

            if (String.IsNullOrWhiteSpace(get_email))
                return Unauthorized("Account not exists");

            using (var _context = new AppDBContext())
            {
                Users users = _context.Users.Where(users => (users.email == get_email))
                    .FirstOrDefault();
                if (users == null)
                    return Unauthorized("Account not exists");

                try
                {
                    Place_comments com = new Place_comments();
                    com.id_place = Convert.ToInt32(new_comment.PlaceID);
                    com.id_user = users.id_user;
                    com.comment = new_comment.CommentText;
                    com.comment_date = DateTime.Now;
                    _context.Place_comments.Add(com);
                    _context.SaveChanges();
                }
                catch
                {
                    return BadRequest("Can't add comment");
                }

            }

            return Ok();
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

                var joins = _context.User_join.Where(user_join => (user_join.id_user == users.id_user)).ToArray();

                foreach (User_join join in joins)
                {
                    Place place1 = _context.Place
                        .Where(place => (place.id_place == join.id_place))
                        .FirstOrDefault();

                    result.Add(PlaceToResult(place1));
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

                var follows = _context.User_follow.Where(user_follow => (user_follow.id_user == users.id_user))
                    .ToArray();

                foreach (User_follow follow in follows)
                {
                    Place place1 = _context.Place.Where(place =>
                        (place.id_place == follow.id_place && place.id_user == users.id_user)).FirstOrDefault();

                    result.Add(PlaceToResult(place1));
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

                var places = _context.Place.Where(place => (place.id_user == users.id_user)).ToArray();

                foreach (Place place1 in places)
                {
                    result.Add(PlaceToResult(place1));
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
                    catch
                    {
                        _contextTransaction.Rollback();
                        return BadRequest("Can't create new place");
                    }
                }
            }

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
                    catch
                    {
                        _contextTransaction.Rollback();
                        return BadRequest("Can't create new event");
                    }
                }
            }

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

        private ResultPlaceList PlaceToResult(Place place1, int user_id = -1, bool fulladdress = false)
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
                if (fulladdress)
                {
                    result1.Address_city = $"{address.city} {address.number} {address.street} {address.country}";
                }
                else
                    result1.Address_city = address.city;

                var user = _context.Users.Where(users => (users.id_user == place1.id_user)).First();
                result1.Users_username = user.username;

                var rate = _context.Place_rate.Where(placeRate => (placeRate.id_place == place1.id_place)).ToList();

                result1.Rate_likes = 0;
                result1.Rate_dislikes = 0;
                if (user_id == place1.id_user)
                    result1.Own = true;
                else
                    result1.Own = false;
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
