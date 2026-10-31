using System.ComponentModel.DataAnnotations;

namespace WebApi.NET.Dtos.DtosAppointment
{
    public class CreateAppointmentRequest
    {
        //الـ Client هيبعت الـ IDs وتاريخ الحجز بس.
        [Required]
        public int IdPatient {  get; set; }
        [Required]
        public int IdDoctor { get; set; }
        [Required]
        public DateTime appointmentDate {  get; set; }
    }
}
