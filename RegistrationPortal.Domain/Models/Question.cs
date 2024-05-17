namespace RegistrationPortal.Domain.Models
{
    public class Question : BaseEntity
    {
        public string? QuestionText { get; set; }
        public Program? Program { get; set; }
        public string? ProgramId { get; set; }
    }
}
