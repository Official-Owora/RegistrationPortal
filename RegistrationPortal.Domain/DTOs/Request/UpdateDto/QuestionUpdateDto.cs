namespace RegistrationPortal.Domain.DTOs.Request.UpdateDto
{
    public record QuestionUpdateDto
    {
        public string id {  get; init; }
        public string? questionText { get; init; }
    }
}
