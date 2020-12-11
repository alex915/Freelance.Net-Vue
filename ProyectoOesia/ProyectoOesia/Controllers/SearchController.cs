using Data;
using Geolocation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoOesia.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SearchController> _logger;

        public SearchController(ApplicationDbContext context, ILogger<SearchController> logger)
        {
            _context = context;
            _logger = logger;

        }
        [HttpGet]
        public async Task<IActionResult> SearchWorkers(string? search, double? lat, double? lng, int? maxDistance, int? activityId, string? city, bool? remote, bool? fulltime)
        {
            var data = _context.Companies.AsEnumerable();

            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x => x.Description.ToLower().Contains(search.ToLower()) || x.CompanyName.ToLower().Contains(search.ToLower()));

            }

            if (remote == true)
            {
                data = data.Where(x => x.MaxDistance == -1);

            }
            else
            {
                if (lat != null && lng != null && maxDistance != null)
                {
                    data = data.Where(x =>
                       ( x.MaxDistance >= GetDistanceHaversine(lat, lng, x.Latitude, x.Longitude) &&
                     maxDistance >= GetDistanceHaversine(lat, lng, x.Latitude, x.Longitude))|| x.MaxDistance == -1
                    );

                }

            }

            if (!string.IsNullOrEmpty(city))
            {
                data = data.Where(x => x.City.ToLower().Contains(city.ToLower()));
            }

            if (fulltime == true)
            {
                data = data.Where(x => x.FullTime == fulltime);
            }
            if (activityId != null)
            {
                data = data.Where(x => x.ActivityId == activityId);
            }
            return Ok(data.ToList());
        }

        private int GetDistanceHaversine(double? pos1Lat, double? pos1Lng, double? pos2Lat, double? pos2Lng)
        {
            double R = 3960;
            var lat = ((double)(pos2Lat - pos1Lat)).ToRadian();
            var lng = ((double)(pos2Lng - pos1Lng)).ToRadian();
            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                          Math.Cos(((double)pos1Lat).ToRadian()) * Math.Cos(((double)pos2Lat).ToRadian()) *
                          Math.Sin(lng / 2) * Math.Sin(lng / 2);
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
            var result = (int)(R * h2);
            return result;

        }
    }
}
