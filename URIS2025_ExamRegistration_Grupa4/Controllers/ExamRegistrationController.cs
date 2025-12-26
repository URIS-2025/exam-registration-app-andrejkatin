using Microsoft.AspNetCore.Mvc;
using URIS2025_ExamRegistration_Grupa4.Data;
using URIS2025_ExamRegistration_Grupa4.Models.DTO;

namespace URIS2025_ExamRegistration_Grupa4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamRegistrationController : Controller
    {
        private readonly IExamRegistrationRepository _examRegistrationRepository;

        //Pomoću dependency injection-a dodajemo potrebne zavisnosti
        public ExamRegistrationController(IExamRegistrationRepository examRegistrationRepository)
        {
            _examRegistrationRepository = examRegistrationRepository;
        }

        // https://localhost:#port/api/examregistration
        [HttpGet]
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

        // https://localhost:#port/api/examregistration/{id}
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
