using System.ComponentModel.DataAnnotations;

namespace WebApi.NET.Dtos
{
    public class CreatePatientRequest
    {
        [Required]
        public string PatientName { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
