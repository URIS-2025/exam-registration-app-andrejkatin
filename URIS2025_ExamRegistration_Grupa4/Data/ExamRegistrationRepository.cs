using URIS2025_ExamRegistration_Grupa4.Models;
using URIS2025_ExamRegistration_Grupa4.Models.DTO;

namespace URIS2025_ExamRegistration_Grupa4.Data
{
    public class ExamRegistrationRepository : IExamRegistrationRepository
    {
        private List<ExamRegistration> ExamRegistrations;
        private List<Student> Students;
        private List<Subject> Subjects;

        public ExamRegistrationRepository()
        {
            ExamRegistrations = new List<ExamRegistration>();
            Students = new List<Student>();
            Subjects = new List<Subject>();

            FillData();
        }

        private void FillData()
        {
            ExamRegistrations.AddRange(new List<ExamRegistration>
            {
                new ExamRegistration
                {
                    Id = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    StudentId = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    SubjectId = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f"),
                    ExamDate = DateTime.Parse("2024-12-15T09:00:00")
                },
                new ExamRegistration
                {
                    Id = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    StudentId = Guid.Parse("32cd906d-8bab-457c-ade2-fbc4ba523029"),
                    SubjectId = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f3"),
                    ExamDate = DateTime.Parse("2024-12-15T09:00:00")
                },
            });

                    Students.AddRange(new List<Student>
            {
                new Student
                {
                    Id = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    FirstName = "Petar",
                    LastName = "Petrovic",
                    Index = "IT 1/2022",
                    Year = 2
                },
                new Student
                {
                    Id = Guid.Parse("32cd906d-8bab-457c-ade2-fbc4ba523029"),
                    FirstName = "Marko",
                    LastName = "Markovic",
                    Index = "IT 1/2021",
                    Year = 3
                },
            });

                    Subjects.AddRange(new List<Subject>
            {
                new Subject
                {
                    Id = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f"),
                    Name = "Upravljanje razvojem informacionih sistema",
                    ECTS = 6
                },
                new Subject
                {
                    Id = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f3"),
                    Name = "Projektovanje skladista podataka",
                    ECTS = 6
                },
            });
        }

        public ExamRegistrationConfirmationDTO AddExamRegistration(ExamRegistrationCreationDTO examRegistration)
        {
            var newExamRegistration = new ExamRegistration
            {
                Id = Guid.NewGuid(),
                StudentId = examRegistration.StudentId,
                SubjectId = examRegistration.SubjectId,
                ExamDate = examRegistration.ExamDate
            };

            ExamRegistrations.Add(newExamRegistration);

            var student = Students.FirstOrDefault(s => s.Id == newExamRegistration.StudentId);
            return new ExamRegistrationConfirmationDTO
            {
                StudentName = student.FirstName + " " + student.LastName,
                SubjectName = Subjects.FirstOrDefault(s => s.Id == newExamRegistration.SubjectId).Name,
                ExamDate = newExamRegistration.ExamDate
            };
        }

        public void DeleteExamRegistration(Guid id)
        {
            var examRegistration = ExamRegistrations.FirstOrDefault(er => er.Id == id);
            ExamRegistrations.Remove(examRegistration);
        }

        public ExamRegistrationDTO GetExamRegistrationById(Guid id)
        {
            var examRegistration = ExamRegistrations.FirstOrDefault(er => er.Id == id);
            return new ExamRegistrationDTO
            {
                Student = Students.FirstOrDefault(s => s.Id == examRegistration.StudentId),
                Subject = Subjects.FirstOrDefault(s => s.Id == examRegistration.SubjectId),
                ExamDate = examRegistration.ExamDate
            };
        }

        public IEnumerable<ExamRegistrationDTO> GetExamRegistrations()
        {
            var examRegistrationDTOs = new List<ExamRegistrationDTO>();
            foreach (var examRegistration in ExamRegistrations)
            {
                var student = Students.FirstOrDefault(s => s.Id == examRegistration.StudentId);
                var subject = Subjects.FirstOrDefault(s => s.Id == examRegistration.SubjectId);

                if (student != null && subject != null)
                {
                    examRegistrationDTOs.Add(new ExamRegistrationDTO
                    {
                        Student = student,
                        Subject = subject,
                        ExamDate = examRegistration.ExamDate
                    });
                }
            }
            return examRegistrationDTOs;
        }

        public ExamRegistrationConfirmationDTO UpdateExamRegistration(ExamRegistration examRegistration)
        {
            var existingExamRegistration = ExamRegistrations.FirstOrDefault(er => er.Id == examRegistration.Id);
            
            if (existingExamRegistration != null)
            {
                existingExamRegistration.StudentId = examRegistration.StudentId;
                existingExamRegistration.SubjectId = examRegistration.SubjectId;
                existingExamRegistration.ExamDate = examRegistration.ExamDate;
            }

            var student = Students.FirstOrDefault(s => s.Id == existingExamRegistration.StudentId);
            return new ExamRegistrationConfirmationDTO
            {
                StudentName = student.FirstName + " " + student.LastName,
                SubjectName = Subjects.FirstOrDefault(s => s.Id == existingExamRegistration.SubjectId).Name,
                ExamDate = existingExamRegistration.ExamDate
            };
        }
    }
}
