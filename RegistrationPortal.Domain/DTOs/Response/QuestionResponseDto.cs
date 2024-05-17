namespace RegistrationPortal.Domain.DTOs.Response
{
    public record QuestionResponseDto
    {
        public string? id { get; init; }
        public string? questionText { get; init; }
    }
}
