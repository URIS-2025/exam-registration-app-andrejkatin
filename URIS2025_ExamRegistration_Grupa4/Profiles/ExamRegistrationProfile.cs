using AutoMapper;
using URIS2025_ExamRegistration_Grupa4.Models.DTO;
using URIS2025_ExamRegistration_Grupa4.Models;

namespace URIS2025_ExamRegistration_Grupa4.Profiles
{
    public class ExamRegistrationProfile : Profile
    {
        public ExamRegistrationProfile()
        {
            CreateMap<ExamRegistrationCreationDTO, ExamRegistration>()
                .ReverseMap();
            CreateMap<ExamRegistration, ExamRegistrationDTO>()
                .ReverseMap();
            CreateMap<ExamRegistrationUpdateDto, ExamRegistration>();
            CreateMap<ExamRegistration, ExamRegistrationConfirmationDTO>();
        }
    }
}
