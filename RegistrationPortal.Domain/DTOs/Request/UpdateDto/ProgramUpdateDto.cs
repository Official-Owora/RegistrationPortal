namespace RegistrationPortal.Domain.DTOs.Request.UpdateDto
{
    public record ProgramUpdateDto
    {
        public string id { get; init; }
        public string title { get; init; }
        public string description { get; init; }
    }
}
