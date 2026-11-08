using AutoMapper;
using WebApi.NET.Dtos;
using WebApi.NET.Models;
using WebApi.NET.Repositories;
using WebApi.NET.Results;

namespace WebApi.NET.Services
{
    public class DoctorService(IGenericRepository<Doctor> repository , IMapper mapper): IDoctorService
    {
        public async Task<GenericResult<Doctor>> CreateAsync(Doctor doctor)
        {
            var model = await repository.CreateAsync(doctor);

            if (model is null)
                return GenericResult<Doctor>.Failure("");

            return GenericResult<Doctor>.Success(model);
        }

        public async Task<GenericResult<bool>> DeleteAsync(int id)
        {
            var isDeleted = await repository.DeleteAsync(id);

            return isDeleted ? GenericResult<bool>.Success(true) : GenericResult<bool>.Failure();
        }

        public async Task<GenericResult<IEnumerable<Doctor>>> GetAllAsync()
        {
            var model = await repository.GetAllAsync();

            if (model is null)
                return GenericResult<IEnumerable<Doctor>>.Failure();

            return GenericResult<IEnumerable<Doctor>>.Success(model);
        }

        public async Task<GenericResult<GetDoctorResponse>> GetByIdAsync(int id)
        {
            var entity = await repository.GetByIdAsync(id);

            if (entity is null)
                return GenericResult<GetDoctorResponse>.Failure($"Doctor With Id ={id} Is Not Found");

            var response = mapper.Map<GetDoctorResponse>(entity);

            return GenericResult<GetDoctorResponse>.Success(response);
        }

        public async Task<GenericResult<bool>> UpdateAsync(Doctor doctor)
        {
            var isUpdated = await repository.UpdateAsync(doctor);

            return isUpdated ? GenericResult<bool>.Success(true) : GenericResult<bool>.Failure();
        }
    }
}
