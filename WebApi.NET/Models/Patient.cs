using System.ComponentModel.DataAnnotations;

namespace WebApi.NET.Models
{
    public class Patient:BaseEntity
    {
     
        [Required]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
