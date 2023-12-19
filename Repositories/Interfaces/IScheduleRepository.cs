namespace DMS.Web.Repositories.Interfaces
{
    public interface IScheduleRepository : IGenericRepository<Schedule>
    {
        public void Update(Schedule schedule);
    }
}
