namespace RegistrationPortal.Domain.DTOs.Request.UpdateDto
{
    public record AnswerUpdateDto
    {
        public string id { get; init; }
        public ICollection<ChoiceUpdateDto> choices { get; init; }
    }
}
