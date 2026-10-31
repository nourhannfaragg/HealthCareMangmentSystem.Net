using System.ComponentModel.DataAnnotations;

namespace WebApi.NET.Models
{
    public class Doctor : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Specialization { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
