namespace Clinic.Core.ViewModel
{
    public class DoctorViewModel
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = Errors.MaxLength), Display(Name = "Dr Name"),
         RegularExpression(RegexPatterns.CharactersOnly_Eng, ErrorMessage = Errors.OnlyEnglishLetters)]
        //[Remote("AllowItem", null!, AdditionalFields = "Id", ErrorMessage = Errors.Duplicated)]
        [Required(ErrorMessage = Errors.RequiredField)]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        public string WorkingDays { get; set; } = null!;

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

    }
}
