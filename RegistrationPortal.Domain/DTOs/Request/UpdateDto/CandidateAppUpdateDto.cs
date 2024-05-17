using RegistrationPortal.Domain.DTOs.Request.CreationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationPortal.Domain.DTOs.Request.UpdateDto
{
    public record CandidateAppUpdateDto
    {
        public string id {  get; init; }
        public string? Nationality { get; set; }
        public string? IDNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public ICollection<AnswerRequestDto>? answers { get; init; }
    }
}
