using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.NET.Data;
using WebApi.NET.Dtos;
using WebApi.NET.Dtos.DtosPatient;
using WebApi.NET.Models;
using WebApi.NET.Services;

namespace WebApi.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService patientService;
        public PatientController(IPatientService _patientService) 
        {
            patientService = _patientService;
        }

        [HttpGet("Get-All-Patients")]
        public async Task<IActionResult> getAllPatient()
        {
            var patients = await patientService.GetAllAsync();

            if (!patients.IsSuccess)
                return BadRequest(patients.Error);
            
            return Ok(patients.Value);
        }

        [HttpGet("Get-By-Id/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await patientService.GetById(id);

            if(!patient.IsSuccess)
                return NotFound(patient.Error);

            return Ok(patient.Value);
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
            var result = await patientService.CreateAsync(patient);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePatientRequest request)
        {
            var patient = new Patient
            {
                Id = request.PatientId,
                Name = request.PatientName,
                DateOfBirth = request.DateOfBirth,
                Appointments = request.Appointments,
            };
            
            var result = await patientService.UpdateAsync(patient);

            if (!result.IsSuccess)
                return NotFound(result.Error);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            var patient = await patientService.DeleteAsync(id);

            if(!patient.IsSuccess)
                return NotFound(patient.Error);

            return NoContent();
        }
    }
}
