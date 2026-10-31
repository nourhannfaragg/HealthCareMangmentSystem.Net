namespace WebApi.NET.Dtos.DtosAppointment
{
    public class GetAppointmentResponse
    {
        //هنا عايزين لما نعرض الحجز نرجّع اسم المريض والدكتور بدل ما نرجّع الـ Objects كاملة.
        
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime appointmentDate { get; set; }

        public string patientName { get; set; }
        public string doctorName { get; set; }
    }
}
