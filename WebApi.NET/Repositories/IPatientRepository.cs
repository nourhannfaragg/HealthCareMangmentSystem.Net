using WebApi.NET.Models;
using WebApi.NET.Results;

namespace WebApi.NET.Repository
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllAsync();

        Task<PaginatedList<Patient>> GetAllAsync(int pageNumber , int pageSize , string? searchTerm);

        Task<Patient?> GetByIdAsync(int id);

        Task<Patient> CreateAsync(Patient patient);

        Task<bool> UpdateAsync(Patient patient);

        Task<bool> DeleteAsync(int id);
    }
}
