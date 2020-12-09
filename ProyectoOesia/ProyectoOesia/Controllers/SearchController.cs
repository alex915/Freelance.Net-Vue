using Data;
using Geolocation;
using Google.Type;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using Nest;
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
        public async Task<IActionResult> SearchCompany(string search)
        {
            var data = await _context.Companies.Where(x => x.Description.Contains(search)).ToListAsync();
            if (data.Count != 0)
            {
                return Ok(data);

            }
            else
            {
                return NotFound("NO EXISTE");
            }


        }

        [HttpGet("provincia")]
        public async Task<IActionResult> provincia(string province, string city)
        {
            List<Company> companies = await _context.Companies.OrderBy(p => p.Province).ToListAsync();
            var data = companies.Select(g => g.City).Distinct().ToList();

            if (String.IsNullOrEmpty(province) && String.IsNullOrEmpty(city))
            {
                return Ok(data);
            }
            if (!String.IsNullOrEmpty(province))
            {
                companies = companies.Where(x => x.Province.ToLower()
                               .Contains(province.ToLower())).ToList();
            }
            if (!String.IsNullOrEmpty(city))
            {
                companies = companies.Where(x => x.City == city).ToList();
            }
            return Ok(data);
        }
        [HttpGet("disponibilidad")]
        public async Task<IActionResult> disponibilidad(string companyname, bool? fulltime)
        {
            List<Company> companies = await _context.Companies.OrderBy(p => p.CompanyName).ToListAsync();
            var data = companies.Select(g => g.FullTime).Distinct().ToList();

            if (fulltime == true)
            {

            }

            return Ok(data);

        }
        [HttpGet("latlong")]

        public async Task<IActionResult> HaversineDistance(double pos1Lat, double pos1Lng, double pos2Lat, double pos2Lng)
        {
            double distance = GeoCalculator.GetDistance(34.0675918, -118.3977091, 34.076234 - 118.395314, 1);
            double R =  3960;
            var lat = (pos2Lat - pos1Lat).ToRadian();
            var lng = (pos2Lng - pos1Lng).ToRadian();
            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                          Math.Cos(pos1Lat.ToRadian()) * Math.Cos(pos2Lat.ToRadian()) *
                          Math.Sin(lng / 2) * Math.Sin(lng / 2);
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
            return Ok( R * h2);
        }
        


    }
}
