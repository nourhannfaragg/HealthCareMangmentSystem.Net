using AutoMapper;
using WebApi.NET.Dtos.DtosPatient;
using WebApi.NET.Models;

namespace WebApi.NET.Helper
{
    public class PatientProfile:Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, GetPatientResponsecs>()
                .ForMember(des => des.patientId,
                opt => opt.MapFrom(src => src.Id))

                .ForMember(des => des.PatientName,
                opt => opt.MapFrom(src => src.Name));
                
        }
    }
}
