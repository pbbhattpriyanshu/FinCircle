using FinCircle.API.Models;
using FinCircle.API.Repositories.Interfaces;
using FinCircle.API.Repositories;
using FinCircle.API.Services.Interfaces;
using FinCircle.API.DTOs;


namespace FinCircle.API.Services
{
    public class ContributionService : IContributionService
    {
        private readonly IContributionRepository _repository;
        private readonly IMemberRepository _memberRepository;

        public ContributionService(IContributionRepository repository, IMemberRepository memberRepository)
        {
            _repository = repository;
            _memberRepository = memberRepository;
        }

        public async Task CreateContributionAsync(CreateContributionDto dto)
        {
            var member = await _memberRepository.GetByIdAsync(dto.MemberId);

            if (member == null)
            {
                throw new Exception("Member not found.");
            }

            var contribution = new Contribution
            {
                MemberId = dto.MemberId,
                Amount = dto.Amount,
                ContributionMonth = dto.ContributionMonth,
                ContributionYear = dto.ContributionYear,
                PaymentDate = dto.PaymentDate,
                PaymentMethod = dto.PaymentMethod,
                Remarks = dto.Remarks,
                Status = "Paid",
                CreatedOn = DateTime.UtcNow
            };

            await _repository.AddAsync(contribution);
        }

        public async Task<List<Contribution>> GetAllContributionAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Contribution?> GetContributionByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
