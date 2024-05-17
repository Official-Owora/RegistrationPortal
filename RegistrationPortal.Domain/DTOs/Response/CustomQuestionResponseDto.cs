using RegistrationPortal.Domain.Enums;

namespace RegistrationPortal.Domain.DTOs.Response
{
    public record CustomQuestionResponseDto
    {
        public string? id {  get; init; }
        public QuestionType questionType { get; init; }
        public string? questionText { get; init; }
        public byte maxChoiceAllowed { get; init; }
        public ICollection<ChoiceResponseDto>? choices { get; init; }
    }
}
