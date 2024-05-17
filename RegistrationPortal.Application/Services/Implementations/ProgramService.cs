using AutoMapper;
using RegistrationPortal.Application.Services.Abstractions;
using RegistrationPortal.Domain.DTOs.Request.CreationDto;
using RegistrationPortal.Domain.DTOs.Request.UpdateDto;
using RegistrationPortal.Domain.DTOs.Response;
using RegistrationPortal.Domain.DTOs.ResponseWrapper;
using RegistrationPortal.Domain.Enums;
using RegistrationPortal.Domain.Models;
using RegistrationPortal.Infrastructure.GenericRepository.IRepoBase;
using Microsoft.EntityFrameworkCore;
using RegistrationPortal.Common.Pagination;
using AutoMapper.QueryableExtensions;

namespace RegistrationPortal.Application.Services.Implementations
{
    public sealed class ProgramService : IProgramService
    {
        private readonly IRepositoryBase<Program> _programRepository;
        private readonly IRepositoryBase<Question> _questionRepository;
        private readonly IRepositoryBase<CustomQuestion> _customQuestionRepository;
        private readonly IRepositoryBase<Choice> _choiceRepository;
        private readonly IMapper _mapper;

        public ProgramService(IRepositoryBase<Program> programRepository, IRepositoryBase<Question> questionRepository, 
            IRepositoryBase<CustomQuestion> customQuestionRepository, IRepositoryBase<Choice> choiceRepository, IMapper mapper)
        {
            _programRepository = programRepository;
            _questionRepository = questionRepository;
            _customQuestionRepository = customQuestionRepository;
            _choiceRepository = choiceRepository;
            _mapper = mapper;
        }
        public async Task<ResponseObject<ProgramResponseDto>> CreateProgramAsync(ProgramRequestDto programRequestDto)
        {
            var program = _mapper.Map<Program>(programRequestDto);
            await _questionRepository.CreateManyAsync(program.Questions);
            var checkForCustomQuestion = CheckForCustomQuestion(programRequestDto.customQuestions);
            if (checkForCustomQuestion)
            {
                await _customQuestionRepository.CreateManyAsync(program.CustomQuestions);

                foreach (var question in program.CustomQuestions)
                {
                    if (question.QuestionType == QuestionType.MultipleChoice)
                    {
                        await _choiceRepository.CreateManyAsync(question.Choices);
                    }
                }
            }
            await _programRepository.CreateAsync(program);
            await _programRepository.SaveChangesAsync();
            var responseDto = _mapper.Map<ProgramResponseDto>(program);
            return ResponseObject<ProgramResponseDto>.SuccessResponse(data: responseDto, statusCode: 201);
        }

        public async Task<ResponseObject<string>> DeleteQuestionByIdAsync(string questionId)
        {
            var question = await _questionRepository.FindByCondition(ques => ques.Id == questionId, trackChanges: true).SingleOrDefaultAsync();
            var customQues = await _customQuestionRepository.FindByCondition(ques => ques.Id == questionId, trackChanges: true).SingleOrDefaultAsync();
            if (question is null && customQues is null)
            {
                var errorMsg = "Question not found";
                return ResponseObject<string>.FailureResponse(message: errorMsg);
            }
            if (question is null)
            {
                _customQuestionRepository.Delete(customQues);
            }
            else
            {
                _questionRepository.Delete(question);
            }
            await _questionRepository.SaveChangesAsync();
            var successMsg = "Delete successful";
            return ResponseObject<string>.SuccessResponse(data: successMsg);
        }
        public async Task<ResponseObject<ProgramResponseDto>> UpdateProgramAsync(ProgramUpdateDto programUpdateDto)
        {
            var programToUpdate = await GetProgramById(programUpdateDto.id);

            if (programToUpdate is null)
            {
                var errorMsg = "Program not found";
                return ResponseObject<ProgramResponseDto>.FailureResponse(message: errorMsg);
            }

            _mapper.Map(programUpdateDto, programToUpdate);
            await _programRepository.SaveChangesAsync();
            var programResponse = _mapper.Map<ProgramResponseDto>(programToUpdate);
            return ResponseObject<ProgramResponseDto>.SuccessResponse(data: programResponse);
        }
        public async Task<ResponseObject<QuestionResponseDto>> UpdateQuestionAsync(QuestionUpdateDto questionUpdateDto)
        {
            var questionToUpdate = await _questionRepository
                .FindByCondition(ques => ques.Id == questionUpdateDto.id, trackChanges: true)
                .SingleOrDefaultAsync();
            if (questionUpdateDto is null)
            {
                var errorMsg = "Question not found";
                return ResponseObject<QuestionResponseDto>.FailureResponse(message: errorMsg);
            }
            _mapper.Map(questionUpdateDto, questionToUpdate);
            await _questionRepository.SaveChangesAsync();
            var response = _mapper.Map<QuestionResponseDto>(questionToUpdate);
            return ResponseObject<QuestionResponseDto>.SuccessResponse(data: response);
        }
        public async Task<ResponseObject<CustomQuestionResponseDto>> UpdateCustomQuestionAsync(CustomQuestionUpdateDto customQuesUpdateDto)
        {
            var questionToUpdate = await _customQuestionRepository
                .FindByCondition(ques => ques.Id == customQuesUpdateDto.id, trackChanges: true)
                .SingleOrDefaultAsync();
            if (questionToUpdate is null)
            {
                var errorMsg = "Question not found";
                return ResponseObject<CustomQuestionResponseDto>.FailureResponse(message: errorMsg);
            }
            _mapper.Map(customQuesUpdateDto, questionToUpdate);

            if (questionToUpdate.QuestionType == QuestionType.MultipleChoice)
            {
                await _choiceRepository.UpdateManyAsync(questionToUpdate.Choices);
            }
            await _questionRepository.SaveChangesAsync();
            var response = _mapper.Map<CustomQuestionResponseDto>(questionToUpdate);
            return ResponseObject<CustomQuestionResponseDto>.SuccessResponse(data: response);
        }
        public async Task<ResponseObject<PagedList<ProgramResponseDto>>> FindAllProgramAsync(PaginationParams paginationParams)
        {
            var programQuery = _programRepository.FindAll(trackChanges: false)
                .Include(prog => prog.Questions)
                .Include(prog => prog.CustomQuestions)
                .ThenInclude(customQues => customQues.Choices);
            var programResponse = programQuery.ProjectTo<ProgramResponseDto>(_mapper.ConfigurationProvider);
            var pagedProgramResponse = await PagedList<ProgramResponseDto>.CreateAsync(programResponse, paginationParams.PageNumber, paginationParams.PageSize);
            return ResponseObject<PagedList<ProgramResponseDto>>.SuccessResponse(data: pagedProgramResponse);
        }
        public async Task<ResponseObject<ProgramResponseDto>> FindProgramByIdAsync(string programId)
        {
            var program = await _programRepository.FindByCondition(prog => prog.Id == programId, trackChanges: false)
                .Include(prog => prog.Questions)
                .Include(prog => prog.CustomQuestions)
                .ThenInclude(customQues => customQues.Choices)
                .SingleOrDefaultAsync();
            if (program is null)
            {
                var errorMsg = "Not Found";
                return ResponseObject<ProgramResponseDto>.FailureResponse(message: errorMsg);
            }
            var programDto = _mapper.Map<ProgramResponseDto>(program);
            return ResponseObject<ProgramResponseDto>.SuccessResponse(data: programDto);

        }
        public async Task<ResponseObject<IEnumerable<CustomQuestionResponseDto>>> GetQuestionsByQuestionType(QuestionType questionType)
        {
            var questions = await _customQuestionRepository
                .FindByCondition(ques => ques.QuestionType == questionType, trackChanges: false)
                .ToListAsync();
            var response = _mapper.Map<IEnumerable<CustomQuestionResponseDto>>(questions);
            return ResponseObject<IEnumerable<CustomQuestionResponseDto>>.SuccessResponse(data: response);
        }
        private async Task<Program> GetProgramById(string programId)
        {
            return await _programRepository.FindByCondition(prog => prog.Id == programId, trackChanges: false)
                .Include(prog => prog.Questions)
                .Include(prog => prog.CustomQuestions)
                .ThenInclude(customQues => customQues.Choices)
                .SingleOrDefaultAsync();
        }
        private bool CheckForCustomQuestion(IEnumerable<CustomQuestionsRequestDto> customQuestions)
        {
            return customQuestions.Any();
        }
    }
}