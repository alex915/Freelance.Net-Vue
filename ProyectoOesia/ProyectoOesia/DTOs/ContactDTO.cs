using ProyectoOesia.DTOs;
using System;

namespace Models
{
    public partial class ContactDTO
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public DateTime? Date { get; set; }
        public bool Contacted { get; internal set; }
        public UserDtoContact User { get; set; }
    }
}
