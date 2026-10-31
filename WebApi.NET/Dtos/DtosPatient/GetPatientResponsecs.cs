using System.ComponentModel.DataAnnotations;

namespace WebApi.NET.Dtos.DtosPatient
{
    public class GetPatientResponsecs
    {
        public int patientId { get; set; }
        [Required]
        public string PatientName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
