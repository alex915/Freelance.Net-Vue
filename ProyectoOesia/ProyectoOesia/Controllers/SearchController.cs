using Data;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
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
        [HttpGet("company")]
        public async Task<IActionResult> maxDistance(int? id)
        {

            var data = await _context.Companies.Include(x => x.MaxDistance).FirstOrDefaultAsync(x => x.Id == id);
            return Ok(data);
        }
        [HttpGet("company_uno")]
        public async Task<IActionResult> fullTime(bool? disponible)
        {
            var data = _context.Companies.Include(u => u.Availability);

            List<Company> companys = await _context.Companies.OrderBy(u => u.Availability).ToListAsync();


            if (disponible == true)
            {
                return Ok(companys);
            }
            //if (disponible == true)
            //{
            //    companys = companys.Where(x => x.Activity == fullTime).ToList();
            //}

            else
            {
                //companys = companys.Where(x => x.Activity != fullTime).ToList();
                //return Ok(companys);
                return Ok(companys);
            }




        }


    }
}