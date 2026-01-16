namespace URIS2025_ExamRegistration_Grupa4.Models.DTO
{
    /// <summary>
    /// DTO za prikaz prijave ispita.
    /// </summary>
    public class ExamRegistrationDTO
    {
        /// <summary>
        /// Podaci o studentu.
        /// </summary>
        public Student Student { get; set; }

        /// <summary>
        /// Podaci o predmetu.
        /// </summary>
        public Subject Subject { get; set; }

        /// <summary>
        /// Datum ispita.
        /// </summary>
        public DateTime ExamDate { get; set; }
    }
}
