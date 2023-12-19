namespace Clinic.Core.Models
{
    //[Index(nameof(Name) , nameof(PhoneNumber) , IsUnique =true)]
    public class Patient
    {
        public int Id { get; set; }
        [MaxLength(100)]
        [Display(Name = "Patient Name")]
        [Required]
        [RegularExpression(RegexPatterns.CharactersOnly_Eng, ErrorMessage = Errors.DenySpecialCharacters)]
        public string Name { get; set; } = null!;
        [Display(Name = "Birth Date")]
        [Required]
        [AssertThat("BirthDate < Today()", ErrorMessage = Errors.AllowOldDates)]
        public DateTime BirthDate { get; set; }

        [MaxLength(50)]
        [Display(Name = "Phone Number"), RegularExpression(RegexPatterns.MobileNumber, ErrorMessage = Errors.InvalidMobileNumber)]
        [Required]
        public string PhoneNumber { get; set; } = null!;

        public List<Schedule> Schedule { get; set; }
    }
}
