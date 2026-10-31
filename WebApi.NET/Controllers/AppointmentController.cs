using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.NET.Data;
using WebApi.NET.Dtos.DtosAppointment;
using WebApi.NET.Models;

namespace WebApi.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly ApplicationContext context;
        private readonly IMapper mapper;
        public AppointmentController(ApplicationContext _context , IMapper _mapper)
        {
            context = _context;
            mapper= _mapper;
        }

        [HttpGet("Get-All-Appointments")]
        public async Task<IActionResult> GetAll()
        {
            var appointments = await context.Appointments.Include(a => a.Patient)
                                                        .Include(a => a.Doctor)
                                                        .AsNoTracking()
                                                        .ToListAsync();
            var appointmentsResponse =mapper.Map<List<Appointment>,List<GetAppointmentResponse>>(appointments);
            return Ok();
        }
        [HttpGet("Get-By-Id/{doctorId:int}/{patientId:int}")]
        public async Task<IActionResult> GetById(int doctorId, int patientId)
        {
            var appointment = await context.Appointments.Include(a=>a.Doctor)
                                                        .Include(a=> a.Patient)
                                                        .AsNoTracking()
                                                        .FirstOrDefaultAsync(a=>a.DoctorId==doctorId && a.PatientId==patientId);

            if (appointment is null)
                return NotFound($"Appointment with DoctorId {doctorId} and PatientId {patientId} is not found");

            var appointmentResponse = mapper.Map<Appointment,GetAppointmentResponse>(appointment);

            return Ok(appointmentResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAppointmentRequest request)
        {
            var patient = await context.patients.FindAsync(request.IdPatient);
            if (patient is null)
                return NotFound($"Doctor With Id={request.IdPatient} Is Not Found");

            var doctor = await context.Doctors.FindAsync(request.IdDoctor);
            if(doctor is null)
                return NotFound($"Doctor With Id={request.IdDoctor} Is Not Found");

            var existingAppointment = await context.Appointments.AnyAsync(a=> 
                                                   a.PatientId == request.IdPatient && a.DoctorId == request.IdDoctor);

            var appointment = new Appointment
            {
                PatientId = request.IdPatient,
                DoctorId = request.IdDoctor,
                AppointmentDate = request.appointmentDate
            };
            await context.Appointments.AddAsync(appointment);
            await context.SaveChangesAsync();
            return Ok(appointment);

        }
        [HttpPut]
        public async Task<IActionResult> update(UpdateAppointmentRequest request)
        {
            var appointment =await context.Appointments.FindAsync(request.PatientId, request.DoctorId);

            if (appointment is null)
                return NotFound("Appointment Is Null");
            appointment.AppointmentDate = request.appointmentDate;

            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int patientId , int doctorId)
        {
            var appointment = await context.Appointments.FindAsync(patientId, doctorId);
            if (appointment is null)
                return NotFound("Appointment Is Null");
            appointment.IsDeleted = true;
            context.Appointments.Update(appointment);
            await context.SaveChangesAsync();
            return NoContent();
        }
        
    }
}
