using System.ComponentModel.DataAnnotations;

namespace WebApi.NET.Models
{
    public class Appointment:BaseEntity
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        [Required]
        public Patient Patient { get; set; }
        [Required]
        public Doctor Doctor { get; set; }
    }
}
