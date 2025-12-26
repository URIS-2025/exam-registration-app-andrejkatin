namespace URIS2025_ExamRegistration_Grupa4.Models.DTO
{
    public class ExamRegistrationCreationDTO
    {
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public DateTime ExamDate { get; set; }
    }
}
