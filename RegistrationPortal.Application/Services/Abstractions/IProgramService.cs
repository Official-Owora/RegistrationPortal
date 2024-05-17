using RegistrationPortal.Common.Pagination;
using RegistrationPortal.Domain.DTOs.Request.CreationDto;
using RegistrationPortal.Domain.DTOs.Request.UpdateDto;
using RegistrationPortal.Domain.DTOs.Response;
using RegistrationPortal.Domain.DTOs.ResponseWrapper;
using RegistrationPortal.Domain.Enums;

namespace RegistrationPortal.Application.Services.Abstractions
{
    public interface IProgramService
    {
        Task<ResponseObject<ProgramResponseDto>> FindProgramByIdAsync(string programId);
        Task<ResponseObject<PagedList<ProgramResponseDto>>> FindAllProgramAsync(PaginationParams paginationParams);
        Task<ResponseObject<IEnumerable<CustomQuestionResponseDto>>> GetQuestionsByQuestionType(QuestionType questionType);
        Task<ResponseObject<ProgramResponseDto>> CreateProgramAsync(ProgramRequestDto programRequestDto);
        Task<ResponseObject<string>> DeleteQuestionByIdAsync(string programId);
        Task<ResponseObject<ProgramResponseDto>> UpdateProgramAsync(ProgramUpdateDto programUpdateDto);
        Task<ResponseObject<QuestionResponseDto>> UpdateQuestionAsync(QuestionUpdateDto questionUpdateDto);
        Task<ResponseObject<CustomQuestionResponseDto>> UpdateCustomQuestionAsync(CustomQuestionUpdateDto customQuesUpdateDto);
    }
}
