using Microsoft.AspNetCore.Mvc;
using RegistrationPortal.Application.Services.Abstractions;
using RegistrationPortal.Common.Pagination;
using RegistrationPortal.Domain.DTOs.Request.CreationDto;
using RegistrationPortal.Domain.DTOs.Request.UpdateDto;
using RegistrationPortal.Domain.Enums;

namespace RegistrationPortal.Presentation.Controllers
{
    public class ProgramController : BaseController
    {
        private readonly IProgramService _programService;
        public ProgramController(IProgramService programService)
        {
            _programService = programService;
        }
        [HttpPost, Route("create-program")]
        public async Task<IActionResult> CreateProgramAsync(ProgramRequestDto programRequestDto)
        {
            var result = await _programService.CreateProgramAsync(programRequestDto);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPut, Route("upddate-program")]
        public async Task<IActionResult> UpdateProgramAsync(ProgramUpdateDto programUpdateDto)
        {
            var result = await _programService.UpdateProgramAsync(programUpdateDto);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPut, Route("update-question")]
        public async Task<IActionResult> UpdateQuestionAsync(QuestionUpdateDto questionUpdateDto)
        {
            var result = await _programService.UpdateQuestionAsync(questionUpdateDto);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPut, Route("update-custom-question")]
        public async Task<IActionResult> UpdateCustomQuestionAsync(CustomQuestionUpdateDto customQuestion)
        {
            var result = await _programService.UpdateCustomQuestionAsync(customQuestion);
            return StatusCode(result.StatusCode, result);
        }
        [HttpDelete, Route("delete-question")]
        public async Task<IActionResult> DeleteQuestionByIdAsync(string questionId)
        {
            var result = await _programService.DeleteQuestionByIdAsync(questionId);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet, Route("get-all-program")]
        public async Task<IActionResult> GetAllProgramsAsync([FromQuery] PaginationParams paginationParams)
        {
            var result = await _programService.FindAllProgramAsync(paginationParams);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet, Route("get-program-by-id")]
        public async Task<IActionResult> GetProgramByIdAsync([FromQuery] string programId)
        {
            var result = await _programService.FindProgramByIdAsync(programId);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet, Route("get-questions-by-type")]
        public async Task<IActionResult> GetQuestionsByType(QuestionType questionType)
        {
            var result = await _programService.GetQuestionsByQuestionType(questionType);
            return StatusCode(result.StatusCode, result);
        }
    }
}
