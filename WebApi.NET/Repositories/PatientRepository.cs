using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.NET.Data;
using WebApi.NET.Dtos.DtosPatient;
using WebApi.NET.Models;
using WebApi.NET.Results;

namespace WebApi.NET.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationContext _dbContext;
        public PatientRepository(ApplicationContext dbContext) => _dbContext = dbContext;

        //getAll
        public async Task<IEnumerable<Patient>> GetAllAsync() 
            => await _dbContext.patients.ToListAsync();
        //GetAllPaginated
        public async Task<PaginatedList<Patient>> GetAllAsync( int pageNumber ,int pageSize, string? searchTerm)
        {
            List<Patient> patients;
            var totalCounts = 0;
            if (searchTerm is not null)
            {
                patients = await _dbContext.patients
                                   .Where(c => c.Name.Contains(searchTerm))
                                   .Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();
                totalCounts = await _dbContext.patients.CountAsync(c => c.Name.Contains(searchTerm));
            }
            else
            {
                patients = await _dbContext.patients
                                          .Skip((pageNumber - 1) * pageSize)
                                          .Take(pageSize)
                                          .ToListAsync();
                totalCounts = await _dbContext.patients.CountAsync();
            }
            var paginatedList = new PaginatedList<Patient>
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalRecords = totalCounts,
                TotalPages = (int)Math.Ceiling(totalCounts / (double)pageSize),
                Data = patients
            };
            return paginatedList;
        }

        public async Task<Patient?> GetByIdAsync(int id)
        => await _dbContext.patients.FindAsync(id);
        

        public async Task<Patient> CreateAsync(Patient patient)
        {
            await _dbContext.patients.AddAsync(patient);
            await _dbContext.SaveChangesAsync();
            return patient;
        }

        public async Task<bool> UpdateAsync(Patient patient)
        {
            var entity = await GetByIdAsync(patient.Id);
            if (entity is null) return false;
             
            _dbContext.patients.Update(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity is null) return false;

            _dbContext.patients.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;

        }

        
    }
}
