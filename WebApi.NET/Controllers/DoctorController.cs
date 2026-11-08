using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.NET.Data;
using WebApi.NET.Dtos;
using WebApi.NET.Models;
using WebApi.NET.Services;

namespace WebApi.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService doctorService;

        public DoctorController(IDoctorService _doctorService)
        {
            doctorService = _doctorService;
        }

        [HttpGet("Get-All-Doctors")]
        public async Task<IActionResult> getAllDoctors()
        {
            var doctors = await doctorService.GetAllAsync();
            if (!doctors.IsSuccess)
                return BadRequest(doctors.Error);

            return Ok(doctors.Value);
        }

        [HttpGet("Get-By-Id/{id:int}")]
        
        public async Task<IActionResult> getById(int id)
        {
            var doctor = await doctorService.GetByIdAsync(id);

            if (!doctor.IsSuccess)
                return BadRequest(doctor.Error);

            return Ok(doctor.Value);
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
           var result = await doctorService.CreateAsync(doctor);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPut] //update by id
        public async Task<IActionResult> Update(UpdateDoctorRequest request) 
        {
            var doctor = new Doctor
            {
                Name = request.DoctorName,
                Specialization = request.Description,
                Appointments = request.Appointments,
            };

            var result = await doctorService.UpdateAsync(doctor);

            if(!result.IsSuccess)
                return NotFound(result.Error);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            var doctor = await doctorService.DeleteAsync(id);

            if (!doctor.IsSuccess)
                return NotFound(doctor.Error);
            
            return NoContent();
        }
    }
}
