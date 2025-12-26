namespace URIS2025_ExamRegistration_Grupa4.Models
{
    public class ExamRegistration
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public DateTime ExamDate { get; set; }
    }
}
