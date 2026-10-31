using AutoMapper;
using WebApi.NET.Dtos;
using WebApi.NET.Models;

namespace WebApi.NET.Helper
{
    public class DoctorProfile: Profile
    {
        public DoctorProfile()
        {           //Src     //Destnision
            CreateMap<Doctor, GetDoctorResponse>()
                .ForMember(des => des.DoctorId,
                 opt => opt.MapFrom(src => src.Id))

                .ForMember(des => des.DoctorName,
                    opt => opt.MapFrom(src => src.Name));
                

        }
    }
}
