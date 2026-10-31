using System.ComponentModel.DataAnnotations;

namespace WebApi.NET.Dtos
{
    public class CreateDoctorRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Specialization {  get; set; }
    }
}
