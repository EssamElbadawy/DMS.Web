using DMS.Web.Data;

namespace Clinic.Controllers
{
    [Authorize(Roles = AppRoles.Doctors)]

    public class DoctorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
 

        public DoctorController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<IActionResult> Index()
        {
            var schedules = await _unitOfWork.ScheduleRepository.GetAllAsync(IncludeProperties: "Doctor,Patient");
            var viewModel = _mapper.Map<IEnumerable<Schedule>, IEnumerable<ScheduleViewModel>>(schedules);

            return View(viewModel);
        }

        public static Dictionary<DateTime, string> GetNextDates(DateTime startDate, List<string> dayNames)
        {
            Dictionary<DateTime, string> nextDates = new Dictionary<DateTime, string>();

            foreach (string dayName in dayNames)
            {
                //DayOfWeek targetDay = (DayOfWeek)(((int)Enum.Parse<DayOfWeek>(dayName) - (int)startDate.DayOfWeek + 7) % 7);
                DayOfWeek targetDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), dayName, true);
                int daysUntilTargetDay = (targetDay - startDate.DayOfWeek + 7) % 7;
                DateTime nextDate = startDate.AddDays(daysUntilTargetDay);
                if (nextDate < startDate)
                {
                    nextDate = nextDate.AddDays(7);
                }
                nextDates.Add(nextDate, dayName);
            }
            return nextDates;
        }


        [HttpGet]
        public async Task<IActionResult> GetWorkingDays(int selectedId)
        {
            // Retrieve data based on the selectedId
            // Replace this with your actual logic to fetch the data from the database

            var doctor = await _unitOfWork.DoctorRepository.GetOneAsync(c => c.Id == selectedId);
            var workingdays = doctor.WorkingDays;
            List<string> list = new();
            foreach (var day in workingdays.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                list.Add(day);
            }
            var all = GetNextDates(DateTime.Today, list);
            return Json(new { data = all, Doctor = doctor });
        }

    }
}
