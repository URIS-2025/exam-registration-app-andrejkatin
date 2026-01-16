using System.ComponentModel.DataAnnotations;
using URIS2025_ExamRegistration_Grupa4.Validation;

namespace URIS2025_ExamRegistration_Grupa4.Models.DTO
{
    /// <summary>
    /// DTO za ažuriranje prijave ispita.
    /// </summary>
    public class ExamRegistrationUpdateDto
    {
        /// <summary>
        /// ID prijave ispita koja se ažurira.
        /// </summary>
        [Required(ErrorMessage = "Id is required.")]
        public Guid Id { get; set; }

        /// <summary>
        /// ID studenta koji prijavljuje ispit.
        /// </summary>
        [Required(ErrorMessage = "Student ID is required.")]
        public Guid StudentId { get; set; }

        /// <summary>
        /// ID predmeta za koji se prijavljuje ispit.
        /// </summary>
        [Required(ErrorMessage = "Subject ID is required.")]
        public Guid SubjectId { get; set; }

        /// <summary>
        /// Datum ispita.
        /// </summary>
        [Required(ErrorMessage = "Exam date is required.")]
        [FutureDate]
        public DateTime ExamDate { get; set; }
    }
}
