using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using meetings_and_events.Data;
using meetings_and_events.Models;
using meetings_and_events.Models.PlaceEdit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace meetings_and_events.Controllers
{
    public class PlaceEdit : Controller
    {
        [HttpPost]
        [Authorize]
        public IActionResult EditTitle([FromBody] EditTitleModel credentials)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int id_user = AccountController.GetUserIp(identity);
            bool update = false;

            if (id_user < 0)
                return Unauthorized("No user");

            using (var _context = new AppDBContext())
            {
                try
                {
                    Place place = _context.Place.Where(place => (place.id_place == credentials.place_id))
                        .FirstOrDefault();
                    if (place == null)
                        return BadRequest("Place not exists");

                    if (place.id_user != id_user)
                        return Unauthorized("Account is not associated with place");

                    if (!place.title.Equals(credentials.title))
                    {
                        place.title = credentials.title.Trim();
                        update = true;
                    }

                    if (credentials.description != null && credentials.description.Length > 0)
                        if (!place.description.Equals(credentials.description))
                        {
                            place.description = credentials.description.Trim();
                            update = true;
                        }

                    if (update)
                    {
                        _context.Place.Update(place);
                        _context.SaveChanges();
                    }
                }
                catch
                {
                }
            }

            if (update)
                return Ok();
            return BadRequest("No action");
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditDatePlace([FromBody] EditDatePlaceModel credentials)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int id_user = AccountController.GetUserIp(identity);
            bool update = false;

            if (id_user < 0)
                return Unauthorized("No user");

            if (credentials.timeOC1 == null && credentials.timeOC2 == null && credentials.timeOC3 == null &&
                credentials.timeOC4 == null && credentials.timeOC5 == null && credentials.timeOC6 == null &&
                credentials.timeOC7 == null)
                return BadRequest("No time set");

            TimeSpan timeOpen;
            TimeSpan timeClose;
            if (credentials.timeOC1 != null)
            {
                timeOpen = TimeSpan.Parse(credentials.timeOC1[0]);
                timeClose = TimeSpan.Parse(credentials.timeOC1[1]);
                if (timeOpen >= timeClose)
                    return Unauthorized("Wrong time");
            }

            if (credentials.timeOC2 != null)
            {
                timeOpen = TimeSpan.Parse(credentials.timeOC2[0]);
                timeClose = TimeSpan.Parse(credentials.timeOC2[1]);
                if (timeOpen >= timeClose)
                    return Unauthorized("Wrong time");
            }

            if (credentials.timeOC3 != null)
            {
                timeOpen = TimeSpan.Parse(credentials.timeOC3[0]);
                timeClose = TimeSpan.Parse(credentials.timeOC3[1]);
                if (timeOpen >= timeClose)
                    return Unauthorized("Wrong time");
            }

            if (credentials.timeOC4 != null)
            {
                timeOpen = TimeSpan.Parse(credentials.timeOC4[0]);
                timeClose = TimeSpan.Parse(credentials.timeOC4[1]);
                if (timeOpen >= timeClose)
                    return Unauthorized("Wrong time");
            }

            if (credentials.timeOC5 != null)
            {
                timeOpen = TimeSpan.Parse(credentials.timeOC5[0]);
                timeClose = TimeSpan.Parse(credentials.timeOC5[1]);
                if (timeOpen >= timeClose)
                    return Unauthorized("Wrong time");
            }

            if (credentials.timeOC6 != null)
            {
                timeOpen = TimeSpan.Parse(credentials.timeOC6[0]);
                timeClose = TimeSpan.Parse(credentials.timeOC6[1]);
                if (timeOpen >= timeClose)
                    return Unauthorized("Wrong time");
            }

            if (credentials.timeOC7 != null)
            {
                timeOpen = TimeSpan.Parse(credentials.timeOC7[0]);
                timeClose = TimeSpan.Parse(credentials.timeOC7[1]);
                if (timeOpen >= timeClose)
                    return Unauthorized("Wrong time");
            }

            using (var _context = new AppDBContext())
            {
                try
                {
                    var record = _context.Place.Where(place => (place.id_place == credentials.place_id))
                        .FirstOrDefault();
                    if (record == null)
                        return BadRequest("Place not exists");
                    if (record.id_user != id_user)
                        return Unauthorized("Account is not associated with place");

                    var times = _context.Place_data_multitime.Where(place => id_user == credentials.place_id).ToArray();
                    if (times != null && times.Length > 0)
                    {
                        List<Place_data_multitime> new_times_list = new List<Place_data_multitime>();
                        if (credentials.timeOC1 != null)
                        {
                            Place_data_multitime new_multitime = new Place_data_multitime();
                            new_multitime.day_week = E_day_week.MONDAY;
                            new_multitime.start_date = TimeSpan.Parse($"{credentials.timeOC1[0]}:00");
                            new_multitime.end_date = TimeSpan.Parse($"{credentials.timeOC1[1]}:00");
                            new_times_list.Add(new_multitime);
                        }

                        if (credentials.timeOC2 != null)
                        {
                            Place_data_multitime new_multitime = new Place_data_multitime();
                            new_multitime.day_week = E_day_week.TUESDAY;
                            new_multitime.start_date = TimeSpan.Parse($"{credentials.timeOC2[0]}:00");
                            new_multitime.end_date = TimeSpan.Parse($"{credentials.timeOC2[1]}:00");
                            new_times_list.Add(new_multitime);
                        }

                        if (credentials.timeOC3 != null)
                        {
                            Place_data_multitime new_multitime = new Place_data_multitime();
                            new_multitime.day_week = E_day_week.WEDNESDAY;
                            new_multitime.start_date = TimeSpan.Parse($"{credentials.timeOC3[0]}:00");
                            new_multitime.end_date = TimeSpan.Parse($"{credentials.timeOC3[1]}:00");
                            new_times_list.Add(new_multitime);
                        }

                        if (credentials.timeOC4 != null)
                        {
                            Place_data_multitime new_multitime = new Place_data_multitime();
                            new_multitime.day_week = E_day_week.THURSDAY;
                            new_multitime.start_date = TimeSpan.Parse($"{credentials.timeOC4[0]}:00");
                            new_multitime.end_date = TimeSpan.Parse($"{credentials.timeOC4[1]}:00");
                            new_times_list.Add(new_multitime);
                        }

                        if (credentials.timeOC5 != null)
                        {
                            Place_data_multitime new_multitime = new Place_data_multitime();
                            new_multitime.day_week = E_day_week.FRIDAY;
                            new_multitime.start_date = TimeSpan.Parse($"{credentials.timeOC5[0]}:00");
                            new_multitime.end_date = TimeSpan.Parse($"{credentials.timeOC5[1]}:00");
                            new_times_list.Add(new_multitime);
                        }

                        if (credentials.timeOC6 != null)
                        {
                            Place_data_multitime new_multitime = new Place_data_multitime();
                            new_multitime.day_week = E_day_week.SATURDAY;
                            new_multitime.start_date = TimeSpan.Parse($"{credentials.timeOC6[0]}:00");
                            new_multitime.end_date = TimeSpan.Parse($"{credentials.timeOC6[1]}:00");
                            new_times_list.Add(new_multitime);
                        }

                        if (credentials.timeOC7 != null)
                        {
                            Place_data_multitime new_multitime = new Place_data_multitime();
                            new_multitime.day_week = E_day_week.SUNDAY;
                            new_multitime.start_date = TimeSpan.Parse($"{credentials.timeOC7[0]}:00");
                            new_multitime.end_date = TimeSpan.Parse($"{credentials.timeOC7[1]}:00");
                            new_times_list.Add(new_multitime);
                        }

                        var old_times_list = _context.Place_data_multitime
                            .Where(place => (place.id_place == credentials.place_id)).ToArray();
                        if (old_times_list != null)
                        {
                            int count_old = old_times_list.Length;
                            int count_new = new_times_list.Count;
                            int index = 0;

                            if (count_old < count_new)
                            {
                                while (count_new > count_old)
                                {
                                    var new_record = new Place_data_multitime();
                                    new_record.day_week = new_times_list[count_new - 1].day_week;
                                    new_record.id_place = credentials.place_id;
                                    new_record.start_date = new_times_list[count_new - 1].start_date;
                                    new_record.end_date = new_times_list[count_new - 1].end_date;
                                    _context.Place_data_multitime.Add(new_record);
                                    count_new--;
                                }
                            }
                            else if (count_old > count_new)
                            {
                                while (count_old > count_new)
                                {
                                    _context.Place_data_multitime.Remove(old_times_list[count_old - 1]);
                                    count_old--;
                                }
                            }

                            while (index < count_new)
                            {
                                old_times_list[index].day_week = new_times_list[index].day_week;
                                old_times_list[index].start_date = new_times_list[index].start_date;
                                old_times_list[index].end_date = new_times_list[index].end_date;

                                _context.Place_data_multitime.Update(old_times_list[index]);
                                index++;
                            }

                            _context.SaveChanges();
                        }
                    }
                }
                catch
                {
                    return BadRequest("Invalid request");
                }
            }

            if (update)
                return Ok();
            return BadRequest("No action");
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditDateMeeting([FromBody] EditDateMeetingModel credentials)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int id_user = AccountController.GetUserIp(identity);
            bool update = false;

            if (id_user < 0)
                return Unauthorized("No user");

            using (var _context = new AppDBContext())
            {
                try
                {

                    TimeSpan timeOpen = TimeSpan.Parse(credentials.timeOC[0]);
                    TimeSpan timeClose = TimeSpan.Parse(credentials.timeOC[1]);
                    if (timeOpen >= timeClose)
                        return BadRequest("Close time must be after open time");

                    var record = _context.Place.Where(place => (place.id_place == credentials.place_id))
                        .FirstOrDefault();
                    if (record == null)
                        return BadRequest("Place not exists");
                    if (record.id_user != id_user)
                        return Unauthorized("Account is not associated with place");

                    var old_date = _context.Place_data_onetime.Where(place => place.id_place == credentials.place_id)
                        .FirstOrDefault();
                    if (old_date != null)
                    {
                        update = true;
                        old_date.start_date = DateTime.Parse(
                            $"{credentials.datepicker.Day}/{credentials.datepicker.Month}/{credentials.datepicker.Year}" +
                            $" {credentials.timeOC[0]}:00");
                        old_date.end_date = DateTime.Parse(
                            $"{credentials.datepicker.Day}/{credentials.datepicker.Month}/{credentials.datepicker.Year}" +
                            $" {credentials.timeOC[1]}:00");

                        _context.Place_data_onetime.Update(old_date);
                        _context.SaveChanges();
                    }
                }
                catch
                {
                    return BadRequest("Invalid request");
                }
            }

            if (update)
                return Ok();
            return BadRequest("No action");
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddClosedDate([FromBody] AddClosedDateModel credentials)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int id_user = AccountController.GetUserIp(identity);
            
            if (id_user < 0)
                return Unauthorized("No user");
            
            using (var _context = new AppDBContext())
            {
                try
                {
                    var record = _context.Place.Where(place => (place.id_place == credentials.place_id))
                        .FirstOrDefault();
                    if (record == null)
                        return BadRequest("Place not exists");
                    if (record.id_user != id_user)
                        return Unauthorized("Account is not associated with place");

                    Place_special_close new_close_time = new Place_special_close();
                    new_close_time.id_place = credentials.place_id;
                    new_close_time.date = new DateTime(credentials.datepicker.Year, credentials.datepicker.Month,
                        credentials.datepicker.Day);
                    _context.Place_special_close.Add(new_close_time);
                    _context.SaveChanges();
                }
                catch
                {
                    return BadRequest("Invalid request");
                }
            }

            return Ok();
        }

        [HttpPost]
        [Authorize]
        public IActionResult RemoveClosedDate([FromBody] RemoveClosedDateModel credentials)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int id_user = AccountController.GetUserIp(identity);

            if (id_user < 0)
                return Unauthorized("No user");

            using (var _context = new AppDBContext())
            {
                try
                {
                    var record = _context.Place.Where(place => (place.id_place == credentials.place_id))
                        .FirstOrDefault();
                    if (record == null)
                        return BadRequest("Place not exists");
                    if (record.id_user != id_user)
                        return Unauthorized("Account is not associated with place");

                    var records_dates = _context.Place_special_close
                        .Where(closed => (closed.id_place == credentials.place_id)).ToArray();

                    if (records_dates == null || records_dates.Length < 1)
                        return BadRequest("No action");

                    foreach (var records in records_dates)
                    {
                        _context.Place_special_close.Remove(records);
                    }

                    _context.SaveChanges();
                }
                catch
                {
                    return BadRequest("Invalid request");
                }
            }

            return Ok();
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditImage([FromBody] EditImageModel credentials)
        {
            return BadRequest("TODO send image");

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int id_user = AccountController.GetUserIp(identity);
            bool update = false;

            if (id_user < 0)
                return Unauthorized("No user");

            using (var _context = new AppDBContext())
            {
                try
                {
                    //TODO image 
                }
                catch
                {
                }
            }

            if (update)
                return Ok();
            return BadRequest("No action");
        }

        [HttpPost]
        [Authorize]
        public IActionResult RemovePlace([FromBody] RemovePlaceModel credentials)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int id_user = AccountController.GetUserIp(identity);

            if (id_user < 0)
                return Unauthorized("No user");

            using (var _context = new AppDBContext())
            {
                try
                {
                    var record = _context.Place.Where(place => (place.id_place == credentials.place_id))
                        .FirstOrDefault();
                    if (record == null)
                        return BadRequest("Place not exists");
                    if (record.id_user != id_user)
                        return Unauthorized("Account is not associated with place");

                    _context.Place.Remove(record);
                    _context.SaveChanges();
                }
                catch
                {
                    return BadRequest("Invalid request");
                }
            }

            return Ok();
        }
    }
}
