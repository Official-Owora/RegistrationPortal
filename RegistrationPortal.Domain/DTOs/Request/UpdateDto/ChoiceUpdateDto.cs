namespace RegistrationPortal.Domain.DTOs.Request.UpdateDto
{
    public record ChoiceUpdateDto
    {
        public string id {  get; init; }
        public string choiceText { get; init; }
    }
}
