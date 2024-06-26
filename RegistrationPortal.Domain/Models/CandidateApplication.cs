﻿namespace RegistrationPortal.Domain.Models
{
    public class CandidateApplication : BaseEntity
    {
        public string? ApplicantId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber {  get; set; }
        public string? Nationality { get; set; }
        public string? IDNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public Program? Program { get; set; }
        public string? ProgramId { get; set; }
        public virtual ICollection<Answer>? Answers { get; set; }
    }
}
