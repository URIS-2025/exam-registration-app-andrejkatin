using Microsoft.AspNetCore.Mvc;
using URIS2025_ExamRegistration_Grupa4.Data;
using URIS2025_ExamRegistration_Grupa4.Models.DTO;
using URIS2025_ExamRegistration_Grupa4.Models;
using AutoMapper;
using URIS2025_ExamRegistration_Grupa4.Data;
using URIS2025_ExamRegistration_Grupa4.Models.DTO;
using URIS2025_ExamRegistration_Grupa4.Models;

namespace URIS2025_ExamRegistration_Grupa4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamRegistrationController : Controller
    {
        private readonly IExamRegistrationRepository _examRegistrationRepository;
        private readonly IMapper _mapper;

        //Pomoću dependency injection-a dodajemo potrebne zavisnosti
        public ExamRegistrationController(IExamRegistrationRepository examRegistrationRepository, IMapper mapper)
        {
            _examRegistrationRepository = examRegistrationRepository;
            _mapper = mapper;
        }

        // https://localhost:#port/api/examregistration
        /// <summary>
        /// Vraca sve prijave ispita.
        /// </summary>
        /// <returns>Lista prijavljenih ispita.</returns>
        /// <response code="200">Uspesno vraca listu prijavljenih ispita.</response>
        /// <response code="204">Nije pronadjena nijedna prijava ispita.</response>
        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<ExamRegistrationDTO>> GetExamRegistrations()
        {
            var examRegistrations = _examRegistrationRepository.GetExamRegistrations();
            if (examRegistrations == null || !examRegistrations.Any())
            {
                return NoContent();
            }

            return Ok(examRegistrations);
        }

        // https://localhost:#port/api/examregistration/{id}
        /// <summary>
        /// Vraća prijavu ispita na osnovu ID-ja.
        /// </summary>
        /// <param name="id">ID prijave ispita</param>
        /// <returns>Prijava ispita.</returns>
        /// <response code="200">Uspešno vraća prijavu ispita.</response>
        /// <response code="404">Nije pronađena prijava ispita.</response>
        [HttpGet("{id}")]
        public ActionResult<ExamRegistrationDTO> GetExamRegistrationById(Guid id)
        {
            var examRegistration = _examRegistrationRepository.GetExamRegistrationById(id);
            if (examRegistration == null)
            {
                return NotFound();
            }

            return Ok(examRegistration);
        }

        // https://localhost:#port/api/examregistration
        /// <summary>
        /// Kreira novu prijavu ispita.
        /// </summary>
        /// <param name="examRegistration">Model prijave ispita</param>
        /// <returns>Potvrdu o kreiranoj prijavi.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove prijave ispita \
        /// POST /api/ExamRegistration \
        /// {     \
        ///     "StudentId": "2841defc-761e-40d8-b8a3-d3e58516dca7", \
        ///     "SubjectId": "4563cf92-b8d0-4574-9b40-a725f884da36", \
        ///     "ExamDate": "2026-01-15T10:30:00" \
        ///}
        /// </remarks>
        /// <response code="201">Vraća kreiranu prijavu ispita</response>
        /// <response code="400">Došlo je do greške na serveru prilikom prijave ispita</response>
        [HttpPost]
        public ActionResult<ExamRegistrationConfirmationDTO> AddExamRegistration
            ([FromBody] ExamRegistrationCreationDTO examRegistration)
        {
            try
            {
                var newExamRegistration = _examRegistrationRepository.AddExamRegistration(examRegistration);
                return Created("", newExamRegistration);
            }
            catch
            {
                return BadRequest();
            }
        }

         // https://localhost:#port/api/examregistration
        /// <summary>
        /// Ažurira postojeću prijavu ispita.
        /// </summary>
        /// <param name="examRegistrationDto">Model prijave ispita</param>
        /// <returns>Potvrdu o ažuriranoj prijavi.</returns>
        /// <remarks>
        /// Primer zahteva za ažuriranje prijave ispita \
        /// PUT /api/ExamRegistration \
        /// {     \
        ///     "id": "6a411c13-a195-48f7-8dbd-67596c3974c0", \
        ///     "StudentId": "2841defc-761e-40d8-b8a3-d3e58516dca7", \
        ///     "SubjectId": "4563cf92-b8d0-4574-9b40-a725f884da36", \
        ///     "ExamDate": "2026-01-15T10:30:00" \
        ///}
        /// </remarks>
        /// <response code="200">Vraća ažuriranu prijavu ispita</response>
        /// <response code="404">Nije pronađena prijava koju pokušavate da ažurirate</response>
        /// <response code="400">Došlo je do greške na serveru prilikom ažuriranja ispita</response>
        [HttpPut]
        public ActionResult<ExamRegistrationConfirmationDTO> UpdateExamRegistration(ExamRegistrationUpdateDto examRegistrationDto)
        {
            try
            {
                var examRegistrationToCheck = _examRegistrationRepository.GetExamRegistrationById(examRegistrationDto.Id);
                if (examRegistrationToCheck == null)
                {
                    return NotFound();
                }
                var examRegistration = _mapper.Map<ExamRegistration>(examRegistrationDto);
                var updatedExamRegistration = _examRegistrationRepository.UpdateExamRegistration(examRegistration);
                return Ok(updatedExamRegistration);
            }
            catch
            {
                return BadRequest();
            }
        }

        // https://localhost:#port/api/examregistration/{id}
        /// <summary>
        /// Briše prijavu ispita.
        /// </summary>
        /// <param name="examRegistrationId">ID prijave ispita</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Uspešno obrisana prijava ispita.</response>
        /// <response code="404">Nije pronađena prijava ispita.</response>
        /// <response code="500">Greška prilikom brisanja.</response>
        [HttpDelete("{examRegistrationId}")]
        public IActionResult DeleteExamRegistration(Guid examRegistrationId)
        {
            try
            {
                var examRegistrationModel = _examRegistrationRepository.GetExamRegistrationById(examRegistrationId);
                if (examRegistrationModel == null)
                {
                    return NotFound();
                }
                _examRegistrationRepository.DeleteExamRegistration(examRegistrationId);
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
    }
}
