using RegistrationPortal.Domain.DTOs.Request.CreationDto;
using RegistrationPortal.Domain.DTOs.Response;
using RegistrationPortal.Domain.DTOs.ResponseWrapper;

namespace RegistrationPortal.Application.Services.Abstractions
{
    public interface ICandidateApplicationServices
    {
        Task<ResponseObject<IEnumerable<CandidateAppResponseDto>>> GetApplicationsByProgramId(string programId);
        Task<ResponseObject<CandidateAppResponseDto>> ApplyForProgram(CandidateAppRequestDto candidateAppRequest);
    }
}
