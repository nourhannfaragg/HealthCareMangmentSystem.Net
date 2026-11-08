using AutoMapper;
using Microsoft.IdentityModel.Abstractions;
using WebApi.NET.Dtos.DtosPatient;
using WebApi.NET.Models;
using WebApi.NET.Repositories;
using WebApi.NET.Results;

namespace WebApi.NET.Services
{
    public class PatientService (IGenericRepository<Patient> repository ,IMapper mapper):IPatientService
    {
        public async Task<GenericResult<Patient>> CreateAsync(Patient entity)
        {
            var model = await repository.CreateAsync(entity);
            if (model is null) return GenericResult<Patient>.Failure("");

            return GenericResult<Patient>.Success(model);
        }

        public async Task<GenericResult<bool>> DeleteAsync(int id)
        {
            var isDeleted = await repository.DeleteAsync(id);
            return isDeleted ? GenericResult<bool>.Success(true) : GenericResult<bool>.Failure();
        }

        public async Task<GenericResult<IEnumerable<Patient>>> GetAllAsync()
        {
            var model = await repository.GetAllAsync();

            if (model is null)
                return GenericResult<IEnumerable<Patient>>.Failure();

            return GenericResult<IEnumerable<Patient>>.Success(model);
        }

        public async Task<GenericResult<GetPatientResponsecs>> GetById(int id)
        {
            var entity = await repository.GetByIdAsync(id);

            if (entity is null)
                return GenericResult<GetPatientResponsecs>.Failure($"Patient With Id= {id} is not found");


            var response = mapper.Map<GetPatientResponsecs>(entity);
            return GenericResult<GetPatientResponsecs>.Success(response);
        }

        public async Task<GenericResult<bool>> UpdateAsync(Patient entity)
        {
            var isUpdated = await repository.UpdateAsync(entity);

            return isUpdated? GenericResult<bool>.Success(true): GenericResult<bool>.Failure();
        }
    }
}
