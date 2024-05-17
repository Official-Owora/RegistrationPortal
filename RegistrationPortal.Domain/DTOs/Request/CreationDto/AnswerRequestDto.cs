namespace RegistrationPortal.Domain.DTOs.Request.CreationDto
{
    public record AnswerRequestDto
    {
        public string questionId { get; init; }
        public ICollection<ChoiceRequestDto> choices { get; init; }
    }
}
