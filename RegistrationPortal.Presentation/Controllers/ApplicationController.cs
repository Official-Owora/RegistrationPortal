using Microsoft.AspNetCore.Mvc;
using RegistrationPortal.Application.Services.Abstractions;
using RegistrationPortal.Domain.DTOs.Request.CreationDto;

namespace RegistrationPortal.Presentation.Controllers
{
    public class ApplicationController : BaseController
    {
        private readonly ICandidateApplicationServices _candidateAppServices;
        public ApplicationController(ICandidateApplicationServices candidateAppServices)
        {
            _candidateAppServices = candidateAppServices;
        }
        [HttpPost]
        public async Task<IActionResult> ApplyForProgram(CandidateAppRequestDto candidateAppRequestDto)
        {
            var result = await _candidateAppServices.ApplyForProgram(candidateAppRequestDto);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("get-program-applications")]
        public async Task<IActionResult> GetProgramApplicationAsync([FromQuery] string programId)
        {
            var result = await _candidateAppServices.GetApplicationsByProgramId(programId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
