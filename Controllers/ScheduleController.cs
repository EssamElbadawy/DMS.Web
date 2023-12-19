namespace Clinic.Controllers
{
    [Authorize(Roles = AppRoles.Secretary)]
    public class ScheduleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ScheduleController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var schedules = _unitOfWork.ScheduleRepository.GetAll(IncludeProperties: "Doctor,Patient");

            var viewModel = _mapper.Map<IEnumerable<Schedule>, IEnumerable<ScheduleViewModel>>(schedules);

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View(PopulateViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ScheduleViewModel model)
        {

            if (!ModelState.IsValid)
                return View(PopulateViewModel());

            string format = "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(Eastern European Standard Time)'";

            model.DateSlot = DateTime.ParseExact(model.Date, format, CultureInfo.InvariantCulture);

            var appointment = await _unitOfWork.ScheduleRepository.GetOneAsync(c => c.DoctorId == model.DoctorId && c.DateSlot == model.DateSlot && c.TimeSlot == model.TimeSlot);
            if (appointment != null)
            {
                ModelState.AddModelError("", "This Appointment is Already reserved");
                return View(model);
            }

            var schedule = _mapper.Map<Schedule>(model);

            await _unitOfWork.ScheduleRepository.AddAsync(schedule);
            await _unitOfWork.Complete();

            return RedirectToAction(nameof(Index));
        }

        private ScheduleViewModel PopulateViewModel(ScheduleViewModel? model = null)
        {
            ScheduleViewModel viewModel = model is null ? new ScheduleViewModel() : model;

            var doctors = _unitOfWork.DoctorRepository.GetAll(tracked: true);
            doctors.ToList();
            viewModel.Doctors = _mapper.Map<IEnumerable<SelectListItem>>(doctors);

            return viewModel;
        }

        [HttpPost]
        public IActionResult ToggleStatus(int id)
        {
            var book = _unitOfWork.ScheduleRepository.GetOne(c => c.Id == id);

            if (book is null)
                return NotFound();
            book.IsDeleted = !book.IsDeleted;

            _unitOfWork.Complete();
            return Ok();
        }
    }
}

