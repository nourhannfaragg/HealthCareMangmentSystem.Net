using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.NET.Data;
using WebApi.NET.Dtos;
using WebApi.NET.Dtos.DtosPatient;
using WebApi.NET.Models;

namespace WebApi.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly ApplicationContext context;
        private readonly IMapper mapper;
        public PatientController(ApplicationContext _context , IMapper _mapper) 
        {
            context = _context;
            mapper = _mapper;
        }

        [HttpGet("Get-All-Patients")]
        public async Task<IActionResult> Get()
        {
            var patients = await context.patients.Include(p => p.Appointments)
                                                .AsNoTracking()
                                                .ToListAsync();
            var patientsResponse = mapper.Map <List<Patient>?,List<GetPatientResponsecs>> (patients);
            return Ok(patientsResponse);
        }

        [HttpGet("Get-By-Id/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await context.patients.Include(p=> p.Appointments)
                                                .AsNoTracking()
                                                .FirstOrDefaultAsync(p=>p.Id == id);
            var patientResponse = mapper.Map<Patient, GetPatientResponsecs>(patient);
            return Ok(patientResponse);
        }
        [HttpPost]
        public async Task<IActionResult> AddPatient(CreatePatientRequest request)
        {
            var patient = new Patient
            {
                Name = request.PatientName,
                DateOfBirth = request.DateOfBirth,
                CreateAt = DateTime.Now,
            };
            await context.AddAsync(patient);
            await context.SaveChangesAsync();
            return Created();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePatientRequest request)
        {
            var patient = await context.patients.FindAsync( request.PatientId);
            if (patient is null)
                return NotFound($"Doctor With Id={request.PatientId} Is Not Found");

            patient.Name = request.PatientName;
            patient.DateOfBirth = request.DateOfBirth;
            patient.Appointments = request.Appointments;
            patient.CreateAt = DateTime.Now;

             context.patients.Update(patient);
            await context.SaveChangesAsync();

            return Ok(patient);
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            var patient = await context.patients.FindAsync(id);
            if(patient is null)
                return NotFound($"Doctor With Id={id} Is Not Found");
            patient.IsDeleted = true;
            context.patients.Update(patient);
            await context.SaveChangesAsync();

            return NoContent();

        }
    }
}
