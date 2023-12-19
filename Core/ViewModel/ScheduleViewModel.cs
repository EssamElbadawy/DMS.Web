namespace Clinic.Core.ViewModel
{
    public class ScheduleViewModel
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public int PatientId { get; set; }

        [Required]
        [Display(Name = "Reservation Date")]
        public DateTime DateSlot { get; set; }

        public string Date { get; set; } = null!;


        [Display(Name = "Reservation Time")]
        public TimeSpan TimeSlot { get; set; }

        [Display(Name = "Additional details")]
        public string Description { get; set; } = null!;


        [Display(Name = "Is Canceled")]
        public bool IsDeleted { get; set; }
        [ValidateNever]
        public PatientViewModel Patient { get; set; } = null!;

        [ValidateNever]
        public DoctorViewModel Doctor { get; set; } = null!;

        public IEnumerable<SelectListItem>? Doctors { get; set; }

    }
}
