namespace RegistrationPortal.Domain.DTOs.Request.CreationDto
{
    public record ProgramRequestDto
    {
        public string title { get; init; }
        public string description { get; init; }
        public ICollection<QuestionRequestDto> questions { get; init; }
        public ICollection<CustomQuestionsRequestDto>? customQuestions { get; init; }
        public string? employerId { get; init; }
    }
}
