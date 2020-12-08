using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Models
{
    public class CompanyDto
    {

        public int Id { get; set; }
        public string Identifier { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Pc { get; set; }
        public string Country { get; set; }
        public string Ext { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int ActivityId { get; set; }
        public int? MaxDistance { get; set; }
        public string Description { get; set; }
        public bool? Availability { get; set; }
        public bool? FullTime { get; set; }
        public IFormFile Image { get; set; }
        public string UserId { get; set; }
 
    }
}
