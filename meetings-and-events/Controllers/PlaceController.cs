using System;
using System.Collections.Generic;
using System.Linq;
using meetings_and_events.Data;
using meetings_and_events.Models;
using Microsoft.AspNetCore.Mvc;

namespace meetings_and_events.Controllers
{
    public class PlaceController
    {
        public JsonResult List(int type = 0, int size = 10)
        {
            string defaultImage = "testimage.jpg";

            List<ResultPlaceList> result = new List<ResultPlaceList>();
            ResultPlaceList result1;

            using (var _context = new AppDBContext())
            {
                var place = _context.Place.ToList();

                foreach (Place place1 in place)
                {
                    result1 = new ResultPlaceList();
                    result1.Id_place = place1.id_place;
                    result1.Title = place1.title;
                    result1.Description = place1.description;
                    if (result1.Description.Length > 46)
                        result1.Description = result1.Description.Substring(0, 45) + "...";
                    result1.Image = place1.image;
                    if (result1.Image == null)
                        result1.Image = defaultImage;

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

                    result.Add(result1);
                }
            }

            return new JsonResult(result.ToArray());
        }
    }
}