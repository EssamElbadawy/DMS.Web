namespace Clinic.Core.Consts
{
    public class RegexPatterns
    {
        public const string MobileNumber = "^01[0,1,2,5]{1}[0-9]{8}$";
        public const string CharactersOnly_Eng = @"^[A-Za-z]+(?:-[A-Za-z]+)*(?:\s+[A-Za-z]+(?:-[A-Za-z]+)*)*$";
        public const string DenySpecialCharacters = "^[^<>!#%$]*$";
        public const string Password = "(?=(.*[0-9]))(?=.*[\\!@#$%^&*()\\\\[\\]{}\\-_+=~`|:;\"'<>,./?])(?=.*[a-z])(?=(.*[A-Z]))(?=(.*)).{8,}";
        public const string Username = "^[a-zA-Z0-9-._@+]*$";

    }
}
