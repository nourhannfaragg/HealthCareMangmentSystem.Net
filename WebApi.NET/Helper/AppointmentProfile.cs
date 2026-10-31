using AutoMapper;
using WebApi.NET.Dtos.DtosAppointment;
using WebApi.NET.Models;

namespace WebApi.NET.Helper
{
    public class AppointmentProfile: Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, GetAppointmentResponse>()
                .ForMember(des => des.patientName
                , opt => opt.MapFrom(src => src.Patient.Name));

            CreateMap<Appointment,GetAppointmentResponse>()
                .ForMember(des=>des.doctorName
                ,opt=>opt.MapFrom(src=>src.Doctor.Name));
        }
    }
}
