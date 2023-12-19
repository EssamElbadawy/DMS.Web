namespace DMS.Web.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IScheduleRepository ScheduleRepository { get; set; }
        public IPatientRepository PatientRepository { get; set; }
        public IDoctorRepository DoctorRepository { get; set; }

        Task CompleteAsync();

        Task Complete();

    }
}
