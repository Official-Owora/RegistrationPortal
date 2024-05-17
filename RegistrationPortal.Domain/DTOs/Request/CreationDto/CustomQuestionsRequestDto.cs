using RegistrationPortal.Domain.Enums;

namespace RegistrationPortal.Domain.DTOs.Request.CreationDto
{
    public record CustomQuestionsRequestDto
    {
        public QuestionType questionType {  get; init; }
        public string questionText { get; init; }
        public byte maxChoiceAllowed { get; init; }
        public ICollection<ChoiceRequestDto>? choices { get; init; }
    }
}
