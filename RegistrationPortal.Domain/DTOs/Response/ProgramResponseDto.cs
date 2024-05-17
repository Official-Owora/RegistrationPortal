namespace RegistrationPortal.Domain.DTOs.Response
{
    public record ProgramResponseDto
    {
        public string? id { get; init; }
        public string? title { get; init; }
        public string? description { get; init; }
        public ICollection<QuestionResponseDto>? questions { get; init; }
        public ICollection<CustomQuestionResponseDto>? customQuestions { get; init; }
    }
}
