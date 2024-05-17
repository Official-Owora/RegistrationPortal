using RegistrationPortal.Domain.Enums;

namespace RegistrationPortal.Domain.Models
{
    public class CustomQuestion : BaseEntity
    {
        public QuestionType QuestionType { get; set; }
        public string? QuestionText { get; set; }
        public byte MaxChoiceAllowed { get; set; }
        public virtual ICollection<Choice>? Choices { get; set; }
        public Program? Program { get; set; }
        public string? ProgramId { get; set; }
    }
}
