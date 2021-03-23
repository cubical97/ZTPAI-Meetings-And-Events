using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace meetings_and_events.Controllers
{
    public class EventController : Controller
    {
        public IEnumerable<Event> Index()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Event
                {
                    title = "Tytuł",
                    image = "image.png",
                    date_start = DateTime.Now.AddDays(index),
                    date_end = DateTime.Now.AddDays(index + 1),
                    description =
                        "Przykładowyt opis. Przykładowyt opis, przykładowyt opis, przykładowyt opis, przykładowyt opis.",
                    address = "Kraków 1",
                    address_2 = "",
                    state = "małopolskie",
                    zip = "",
                    stars_1 = rng.Next(0, 50),
                    stars_2 = rng.Next(0, 50),
                    stars_3 = rng.Next(0, 50),
                    stars_4 = rng.Next(0, 50),
                    stars_5 = rng.Next(0, 50),
                })
                .ToArray();
        }
    }
}