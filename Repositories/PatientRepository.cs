namespace DMS.Web.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void Update(Patient patient)
        {
            db.Patients.Update(patient);
        }
    }
}
