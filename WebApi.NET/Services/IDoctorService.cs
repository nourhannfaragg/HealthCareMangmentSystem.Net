using WebApi.NET.Dtos;
using WebApi.NET.Models;
using WebApi.NET.Repositories;
using WebApi.NET.Results;

namespace WebApi.NET.Services
{
    public interface IDoctorService
    {
        Task<GenericResult<IEnumerable<Doctor>>> GetAllAsync();

        Task<GenericResult<GetDoctorResponse>> GetByIdAsync(int id);

        Task<GenericResult<Doctor>> CreateAsync(Doctor doctor);

        Task<GenericResult<bool>> UpdateAsync(Doctor doctor);

        Task<GenericResult<bool>> DeleteAsync(int id);
    }
}
