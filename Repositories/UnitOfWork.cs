namespace DMS.Web.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IScheduleRepository ScheduleRepository { get; set; }
        public IPatientRepository PatientRepository { get; set; }
        public IDoctorRepository DoctorRepository { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ScheduleRepository = new ScheduleRepository(context);
            PatientRepository = new PatientRepository(context);
            DoctorRepository = new DoctorRepository(context);
        }

        public async Task CompleteAsync()
            => await _context.SaveChangesAsync();

        public async Task Complete()
            => _context.SaveChanges();

        public void Dispose()
           => _context.Dispose();
    }
}
