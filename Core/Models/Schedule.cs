using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Clinic.Core.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        [ValidateNever]
        public Patient Patient { get; set; }

        public int DoctorId { get; set; }

        [ValidateNever]
        public Doctor Doctor { get; set; }

        public DateTime DateSlot { get; set; }

        public TimeSpan TimeSlot { get; set; }

        public bool IsDeleted { get; set; }

        [MaxLength(400)]
        public string Description { get; set; } = null!;

    }
}
