namespace Clinic.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Data Seeding for list of doctors
            modelBuilder.Entity<Doctor>().HasData(
                 new { Id = 1, Name = "Dr. Osama Essam ", WorkingDays = "Monday,Thursday", StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(10, 0, 0) },
                 new { Id = 2, Name = "Dr. Tarek Elshek Thompson ", WorkingDays = "Tuesday,Friday,Monday,Thursday", StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(16, 0, 0) },
                 new { Id = 3, Name = "Dr. Ahmed Mohammady ", WorkingDays = "Monday,Wednesday,Tuesday,Thursday", StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(16, 0, 0) },
                 new { Id = 4, Name = "Dr. Bahaa Rodriguez", WorkingDays = "Tuesday,Thursday", StartTime = new TimeSpan(4, 0, 0), EndTime = new TimeSpan(15, 0, 0) },
                 new { Id = 5, Name = "Dr. Nashwa Muhammed", WorkingDays = "Wednesday,Friday,Tuesday,Thursday", StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(10, 0, 0) },
                 new { Id = 6, Name = "Dr. Jonathan Patel", WorkingDays = "Monday,Thursday", StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(21, 0, 0) },
                 new { Id = 7, Name = "Dr. Sophia Chang", WorkingDays = "Tuesday,Friday,Monday,Thursday", StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(22, 0, 0) },
                 new { Id = 8, Name = "Dr. Maha Ibrahim", WorkingDays = "Monday,Wednesday", StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(19, 0, 0) },
                 new { Id = 9, Name = "Dr. Gabriella Miller", WorkingDays = "Tuesday,Friday,Friday,Monday", StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(18, 0, 0) },
                 new { Id = 10, Name = "Dr. Samuel Williams", WorkingDays = "Wednesday,Friday,Tuesday,Thursday", StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(19, 0, 0) },
                 new { Id = 11, Name = "Dr. Natalie Lee", WorkingDays = "Monday,Thursday", StartTime = new TimeSpan(12, 0, 0), EndTime = new TimeSpan(10, 0, 0) },
                 new { Id = 12, Name = "Dr. Alexander Ramirez", WorkingDays = "Tuesday,Friday,Friday,Monday", StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(22, 0, 0) }
                 );
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Schedule> Schedules { get; set; }


    }
}