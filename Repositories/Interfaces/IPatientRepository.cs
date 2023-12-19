namespace DMS.Web.Repositories.Interfaces
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        public void Update(Patient patient);
    }
}
