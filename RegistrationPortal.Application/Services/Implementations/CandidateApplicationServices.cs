using AutoMapper;
using RegistrationPortal.Application.Services.Abstractions;
using RegistrationPortal.Domain.DTOs.Request.CreationDto;
using RegistrationPortal.Domain.DTOs.Response;
using RegistrationPortal.Domain.DTOs.ResponseWrapper;
using RegistrationPortal.Domain.Models;
using RegistrationPortal.Infrastructure.GenericRepository.IRepoBase;
using Microsoft.EntityFrameworkCore;

namespace RegistrationPortal.Application.Services.Implementations
{
    public class CandidateApplicationServices : ICandidateApplicationServices
    {
        private readonly IRepositoryBase<Answer> _answerRepository;
        private readonly IRepositoryBase<Choice> _choiceRepository;
        private readonly IRepositoryBase<CandidateApplication> _candidateAppRepository;
        private readonly IRepositoryBase<Program> _programRepository;
        private readonly IMapper _mapper;

        public CandidateApplicationServices(IRepositoryBase<Answer> answerRepository, IRepositoryBase<Choice> choiceRepository, 
            IRepositoryBase<CandidateApplication> candidateAppRepository, IRepositoryBase<Program> programRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
            _choiceRepository = choiceRepository;
            _candidateAppRepository = candidateAppRepository;
            _programRepository = programRepository;
            _mapper = mapper;
        }
        public async Task<ResponseObject<IEnumerable<CandidateAppResponseDto>>> GetApplicationsByProgramId(string programId)
        {
            var applications = await _candidateAppRepository
            .FindByCondition(app => app.ProgramId == programId, trackChanges: false)
            .Include(app => app.Answers)
            .ThenInclude(ans => ans.Choices).ToListAsync();
            var response = _mapper.Map<IEnumerable<CandidateAppResponseDto>>(applications);
            return ResponseObject<IEnumerable<CandidateAppResponseDto>>.SuccessResponse(data: response);
        }
        public async Task<ResponseObject<CandidateAppResponseDto>> ApplyForProgram(CandidateAppRequestDto candidateAppRequest)
        {
            var program = await _programRepository
                .FindByCondition(prog => prog.Id == candidateAppRequest.programId, trackChanges: false)
                .SingleOrDefaultAsync();
            if (program is null)
            {
                var errorMsg = "Program not found";
                return ResponseObject<CandidateAppResponseDto>.FailureResponse(message: errorMsg);
            }
            var candidateApp = _mapper.Map<CandidateApplication>(candidateAppRequest);
            await _answerRepository.CreateManyAsync(candidateApp.Answers);
            foreach (var answer in candidateApp.Answers)
            {
                await _choiceRepository.CreateManyAsync(answer.Choices);
            }
            await _candidateAppRepository.CreateAsync(candidateApp);
            await _candidateAppRepository.SaveChangesAsync();
            var responseDto = _mapper.Map<CandidateAppResponseDto>(candidateApp);
            return ResponseObject<CandidateAppResponseDto>.SuccessResponse(data: responseDto);
        }
    }
}
