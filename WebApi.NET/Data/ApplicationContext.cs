using Microsoft.EntityFrameworkCore;
using WebApi.NET.Models;

namespace WebApi.NET.Data
{
    public class ApplicationContext:DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>().HasKey(a => new
            {
                a.PatientId,
                a.DoctorId,
                a.AppointmentDate
            });
            modelBuilder.Entity<Appointment>().HasOne(p => p.Patient)
                                              .WithMany(a => a.Appointments)
                                              .HasForeignKey(p => p.PatientId);
            modelBuilder.Entity<Appointment>().HasOne(d => d.Doctor)
                                              .WithMany(a => a.Appointments)
                                              .HasForeignKey(d => d.DoctorId);

            modelBuilder.Entity<Appointment>().HasQueryFilter(a => !a.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }
    }
}
