using System.ComponentModel.DataAnnotations;
using WebApi.NET.Models;

namespace WebApi.NET.Dtos
{
    public class UpdateDoctorRequest
    {
        public int DoctorId { get; set; }
        [Required]
        public string DoctorName { get; set; }
        public string Description { get; set; }
        public List<Appointment> Appointments { get; set; }

    }
}
