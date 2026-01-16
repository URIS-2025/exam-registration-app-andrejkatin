namespace URIS2025_ExamRegistration_Grupa4.Models.DTO
{
    /// <summary>
    /// DTO za potvrdu prijave ispita.
    /// </summary>
    public class ExamRegistrationConfirmationDTO
    {
        /// <summary>
        /// Ime i prezime studenta.
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// Naziv predmeta.
        /// </summary>
        public string SubjectName { get; set; }

        /// <summary>
        /// Datum ispita.
        /// </summary>
        public DateTime ExamDate { get; set; }
    }
}
