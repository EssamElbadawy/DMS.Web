namespace Clinic.Core.ViewModel
{
    public class PatientViewModel
    {
        public int Id { get; set; }
        [MaxLength(100, ErrorMessage = Errors.MaxLength)]
        [Required(ErrorMessage = Errors.RequiredField)]
        [Display(Name = "Patient Name")]
        [RegularExpression(RegexPatterns.CharactersOnly_Eng, ErrorMessage = Errors.DenySpecialCharacters)]
        public string Name { get; set; } = null!;

        [Display(Name = "Birth Date 'Patient'")]
        [Required(ErrorMessage = Errors.RequiredField)]
        [AssertThat("BirthDate < Today()", ErrorMessage = Errors.AllowOldDates)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Phone Number"), RegularExpression(RegexPatterns.MobileNumber, ErrorMessage = Errors.InvalidMobileNumber)]
        public string PhoneNumber { get; set; } = null!;
    }
}
