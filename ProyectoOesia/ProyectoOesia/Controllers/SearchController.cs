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
        //Empresas que esten dentro del radio de busqueda del usuario
        [HttpGet("company")]
        public async Task<IActionResult> maxDistance(int? id)
        {
            //saber donde esta el usuario -->coordenadas gps del navegador
            //saber donde esta la empresa -->direccion dentro del objeto

            //api (google)

            var data = await _context.Companies.Include(x => x.MaxDistance).FirstOrDefaultAsync(x => x.Id == id);
            return Ok(data);
        }
        [HttpGet("company_uno")]
        public async Task<IActionResult> fullTime()
        {


            List<Company> companys = await _context.Companies.Where(u => u.FullTime == true)
                                            .OrderBy(u => u.Province).ToListAsync();

            return Ok(companys);


        }
        [HttpGet("filter")]
        public async Task<IActionResult> filter(int? maxdistante, bool? fulltime, string? province, string? search)
        {
            var data = _context.Companies;

            if (search!=null)
            {
               data.Where(x => x.Description.Contains(search));
            }

            if (maxdistante!=0)
            {
                //ejemplo
               data.Include(x => x.Activity);
            }

            if ((bool)fulltime)
            {
               data.Find("");
            }


            return Ok(data);
        }




    }
}