namespace RegistrationPortal.Domain.DTOs.Response
{
    public record ChoiceResponseDto
    {
        public string id { get; init; }
        public string choiceText { get; init; }
    }
}
