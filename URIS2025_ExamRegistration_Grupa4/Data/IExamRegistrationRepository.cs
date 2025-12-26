using URIS2025_ExamRegistration_Grupa4.Models;
using URIS2025_ExamRegistration_Grupa4.Models.DTO;

namespace URIS2025_ExamRegistration_Grupa4.Data
{
    public interface IExamRegistrationRepository
    {
        IEnumerable<ExamRegistrationDTO> GetExamRegistrations();
        ExamRegistrationDTO GetExamRegistrationById(Guid id);
        ExamRegistrationConfirmationDTO AddExamRegistration(ExamRegistrationCreationDTO examRegistration);
        ExamRegistrationConfirmationDTO UpdateExamRegistration(ExamRegistration examRegistration);
        void DeleteExamRegistration(Guid id);
    }
}
