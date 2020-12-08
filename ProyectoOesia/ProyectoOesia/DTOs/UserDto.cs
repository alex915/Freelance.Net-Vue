using Microsoft.AspNetCore.Http;

namespace ProyectoOesia.DTOs
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Ext { get; set; }
        public string Phone { get; set; }
        public bool Company { get; set; }

    }
    public class UserDtoUpdate
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Ext { get; set; }
        public string Phone { get; set; }
        public IFormFile Avatar { get; set; }


    }
}
