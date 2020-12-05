﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;
using ProyectoOesia.Services;

namespace ProyectoOesia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHereService _here;

        public CompaniesController(ApplicationDbContext context, IHereService hereS)
        {
            _here = hereS;
            _context = context;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult> GetCompanies()
        {
            return Ok(await _context.Users.Include(x=>x.Company).Where(x=>x.Company!=null).ToListAsync());
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        // PUT: api/Companies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, Company company)
        {
            if (id != company.Id)
            {
                return BadRequest();
            }
            var position = await _here.SearchGeocoding(company.Address, company.City, company.Pc);
            var lat = position.items[0].position.lat;
            var lng = position.items[0].position.lng;
            company.Latitude = lat;
            company.Longitude = lng;
            _context.Entry(company).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Companies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(Company company)
        {
            var userId = User.Claims.First().Value;
            company.UserId = userId;
            var position = await _here.SearchGeocoding(company.Address, company.City, company.Pc);
            var lat = position.items[0].position.lat;
            var lng = position.items[0].position.lng;
            company.Latitude = lat;
            company.Longitude = lng;
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCompany), new { id = company.Id },company);
        }

        // DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
    }
}
