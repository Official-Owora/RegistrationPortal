using RegistrationPortal.Domain.Enums;

namespace RegistrationPortal.Domain.DTOs.Request.UpdateDto
{
    public record CustomQuestionUpdateDto
    {
        public string id {  get; init; }
        public QuestionType questionType { get; init; }
        public string questionText { get; init; }
        public byte maxChoiceAllowed { get; init; }
        public ICollection<ChoiceUpdateDto>? choices { get; init; }
    }
}
