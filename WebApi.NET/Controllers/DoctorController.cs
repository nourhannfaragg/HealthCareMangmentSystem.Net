using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.NET.Data;
using WebApi.NET.Dtos;
using WebApi.NET.Models;

namespace WebApi.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly ApplicationContext context;
        private readonly IMapper mapper;

        public DoctorController(ApplicationContext _context , IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        [HttpGet("Get-All-Doctors")]
        public async Task<IActionResult> displayAll()
        {
            var doctors = await context.Doctors
                .Include(d => d.Appointments)
                .AsNoTracking()
                .ToListAsync();
            var doctorsResponse = mapper.Map < List<Doctor> ?,List<GetDoctorResponse>>(doctors);
            return Ok(doctorsResponse);   ///body
        }

        [HttpGet("Get-By-Id/{id:int}")]
        
        public async Task<IActionResult> getById(int id)
        {
            var doctor = await context.Doctors
                .Include(d=> d.Appointments)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
            if (doctor is null)
                return NotFound($"Doctor With Id: {id} Is Not Found");

            var doctorResponse = mapper.Map<Doctor,GetDoctorResponse>(doctor);

            return Ok(doctorResponse);
        }


        [HttpPost] //add 
        public async Task<IActionResult> AddDoctors(CreateDoctorRequest request)
        {
            var doctor = new Doctor
            {
                Name = request.Name,
                Specialization = request.Specialization,
                CreateAt = DateTime.Now,
                
            };
            await context.Doctors.AddAsync(doctor);
            await context.SaveChangesAsync();
            return Created();
        }

        [HttpPut] //update by id
        public async Task<IActionResult> Update(UpdateDoctorRequest request) 
        {
            var doctor =await context.Doctors.FindAsync(request.DoctorId);
            if (doctor is null)
                return NotFound($"Doctor With Id={request.DoctorId} Is Not Found");

            doctor.Name = request.DoctorName;
            doctor.Specialization = request.Description;
            doctor.Appointments = request.Appointments;
            doctor.CreateAt = DateTime.Now;

            context.Doctors.Update(doctor);
            await context.SaveChangesAsync();

            return Ok(doctor);
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            var doctor = await context.Doctors.FindAsync(id);
             
            if(doctor is null)
                return NotFound($"Doctor With Id{id} Is Not Found");

            doctor.IsDeleted = true;    
            context.Doctors.Update(doctor);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
