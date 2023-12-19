namespace DMS.Web.Repositories.Interfaces
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        public void Update(Doctor doctor);
    }
}
