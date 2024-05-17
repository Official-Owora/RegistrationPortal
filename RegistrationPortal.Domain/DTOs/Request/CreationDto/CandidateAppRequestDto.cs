namespace RegistrationPortal.Domain.DTOs.Request.CreationDto
{
    public record CandidateAppRequestDto
    {
        public string? applicantId { get; init; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public string? Nationality { get; set; }
        public string? IDNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string programId { get; init; }
        public ICollection<AnswerRequestDto> answers { get; init; }
    }
}
