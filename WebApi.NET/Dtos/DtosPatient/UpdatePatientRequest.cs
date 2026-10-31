using System.ComponentModel.DataAnnotations;
using WebApi.NET.Models;

namespace WebApi.NET.Dtos
{
    public class UpdatePatientRequest
    {
        public int PatientId { get; set; }
        [Required]
        public string PatientName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Appointment> Appointments { get; set; }

    }
}
