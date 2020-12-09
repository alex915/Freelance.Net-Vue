using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;
using ProyectoOesia.Services;
using System.IO;

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

        [HttpGet]
        public async Task<ActionResult> GetCompanies()
        {
            return Ok(await _context.Users.Include(x => x.Company).Where(x => x.Company != null).ToListAsync());
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetAllCompanies()
        {
            var companies = await _context.Companies.Include(x => x.Activity).ToListAsync();
            foreach (var company in companies)
            {







                company.Activity = new Activity
                {
                    Id = company.Activity.Id,
                    Description = company.Activity.Description,
                    Title = company.Activity.Title
                };
            }
            return Ok(companies);
        }

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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany( int id,[FromForm] CompanyDto companyDto)
        {
            if (id != companyDto.Id)
            {
                return BadRequest();
            }
            Company company = await GetCompany(companyDto);

            _context.Companies.Update(company);
            //_context.Entry(company).State = EntityState.Modified;

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

        private async Task<Company> GetCompany(CompanyDto companyDto)
        {
            var position = await _here.SearchGeocoding(companyDto.Address, companyDto.City, companyDto.Pc);
            var lat = position.items[0].position.lat;
            var lng = position.items[0].position.lng;

            var company = new Company()
            {
                Id = companyDto.Id,
                Country = companyDto.Country,
                Email = companyDto.Email,
                Description = companyDto.Description,
                Identifier = companyDto.Identifier,
                City = companyDto.City,
                CompanyName = companyDto.CompanyName,
                Address = companyDto.Address,
                ActivityId = companyDto.ActivityId,
                Availability = companyDto.Availability,
                Ext = companyDto.Ext,
                FullTime = companyDto.FullTime,
                MaxDistance = companyDto.MaxDistance,
                Latitude = lat,
                Longitude = lng,
                Pc = companyDto.Pc,
                Phone = companyDto.Phone,
                Province = companyDto.Province,
                UserId = companyDto.UserId
            };
            if (companyDto.Image!= null && companyDto.Image.Length > 0)
            {
                using var ms = new MemoryStream();
                companyDto.Image.CopyTo(ms);
                var fileBytes = ms.ToArray();
                company.Image = fileBytes;
            }

            return company;
        }

        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany([FromForm]CompanyDto companyDto)
        {
            var userId = User.Claims.First().Value;
            Company company = await GetCompany(companyDto);
            company.UserId = userId;
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCompany), new { id = company.Id }, company);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            ///_context.Companies.Remove(company);
            //await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
    }
}
