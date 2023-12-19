namespace Clinic.Core.Models

{
    public class Doctor
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        [Display(Name = "Dr Name")]
        public string Name { get; set; } = null!;
        [MaxLength(500)]
        [Required]
        public string WorkingDays { get; set; } = null!;

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public List<Schedule> Schedule { get; set; }

    }
}
