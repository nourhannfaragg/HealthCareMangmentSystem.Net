namespace WebApi.NET.Dtos.DtosAppointment
{
    public class UpdateAppointmentRequest
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime appointmentDate { get; set; }
    }
}
