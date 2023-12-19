namespace DMS.Web.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void Update(Doctor doctor)
        {
            db.Doctors.Update(doctor);
        }
    }
}
