using Data;
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

        public SearchController(ApplicationDbContext context, ILogger<SearchController> logger) {
            _context = context;
            _logger = logger;
        
        }
        [HttpGet]
        public async Task<IActionResult> SearchWorkers(string search)
        {
            var data = await _context.Activity.Where(x => x.Description.Contains(search)).ToListAsync();
            if (data.Count != 0)
            {
                return Ok(data);

            }
            else {
                return NotFound("NO EXISTE NA!");
            }
            
             
        }
    }
}
