using WebApi.NET.Dtos.DtosPatient;
using WebApi.NET.Models;
using WebApi.NET.Results;

namespace WebApi.NET.Services
{
    public interface IPatientService
    {
        Task<GenericResult<IEnumerable<Patient>>> GetAllAsync();

        Task<GenericResult<GetPatientResponsecs>> GetById(int id);

        Task<GenericResult<Patient>> CreateAsync(Patient entity);

        Task<GenericResult<bool>> UpdateAsync(Patient entity);

        Task<GenericResult<bool>> DeleteAsync(int  id);
    }
}
