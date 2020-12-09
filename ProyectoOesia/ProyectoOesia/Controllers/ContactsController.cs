using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;
using Microsoft.AspNetCore.Identity;
using ProyectoOesia.DTOs;

namespace ProyectoOesia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ContactsController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDTO>> GetLatestContact(int id)
        {
            var contacts = await _context.Contacts.Where(x => x.CompanyId == id).Include(x=>x.User).OrderByDescending(x => x.Date).ToListAsync();
            var contactsDto = new List<ContactDTO>();
            foreach (var contact in contacts)
            {
                contactsDto.Add(new ContactDTO
                {
                    Id = contact.Id,
                    CompanyId = id,
                    Contacted = contact.Contacted,
                    Date = contact.Date,
                    User = new UserDtoContact
                    {
                        Avatar = contact.User.Avatar,
                        Email = contact.User.Email,
                        Ext = contact.User.Ext,
                        FirstName = contact.User.FirstName,
                        LastName = contact.User.LastName,
                        Phone = contact.User.PhoneNumber,
                    }
                });
            }

            return Ok(contactsDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Contacted(int id)
        {
            var contacts = await _context.Contacts.FindAsync(id);
            contacts.Contacted = true;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();

            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var contact = new Contact()
            {
                UserId = user.Id,
                CompanyId = id,
                Contacted = false,
                Date = DateTime.UtcNow,
            };

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
        }


    }
}
