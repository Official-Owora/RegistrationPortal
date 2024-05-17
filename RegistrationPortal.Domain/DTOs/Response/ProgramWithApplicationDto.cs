using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationPortal.Domain.DTOs.Response
{
    public record ProgramWithApplicationDto
    {
        public string? Title { get; init; }
        public string? Description { get; init; }
        public string? programId { get; init; }
        public ICollection<QuestionResponseDto>? questions { get; init; }
        public ICollection<CustomQuestionResponseDto>? customQuestions { get; init; }
        public ICollection<AnswerResponseDto>? answers { get; init; }
    }
}
