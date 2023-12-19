using DMS.Web.Core.ViewModel;
using DMS.Web.Data;

namespace Clinic.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Schedule, ScheduleViewModel>().ReverseMap()
                .ForMember(d => d.Patient, opt => opt.MapFrom(src => src.Patient))
                .ForMember(d => d.Doctor, opt => opt.MapFrom(src => src.Doctor));


            CreateMap<Patient, PatientViewModel>().ReverseMap();
            CreateMap<Doctor, DoctorViewModel>().ReverseMap();
             

            CreateMap<Doctor, SelectListItem>()
                .ForMember(des => des.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.Text, opt => opt.MapFrom(src => src.Name));


            CreateMap<Schedule, SelectListItem>()
                  .ForMember(des => des.Value, opt => opt.MapFrom(src => src.Id))
                 .ForMember(des => des.Text, opt => opt.MapFrom(src => src.Doctor.Name));


            //Users
            CreateMap<ApplicationUser, UserViewModel>();
            CreateMap<UserFormViewModel, ApplicationUser>()
                .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
                .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.UserName.ToUpper()))
                .ReverseMap();
        }
    }
}
