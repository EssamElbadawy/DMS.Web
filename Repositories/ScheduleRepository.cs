
namespace DMS.Web.Repositories
{
    public class ScheduleRepository : GenericRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(ApplicationDbContext context) : base(context)
        {

        }

        public void Update(Schedule schedule)
        {
            db.Schedules.Update(schedule);
        }
    }
}
