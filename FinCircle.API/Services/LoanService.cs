using FinCircle.API.DTOs;
using FinCircle.API.Models;
using FinCircle.API.Repositories;
using FinCircle.API.Repositories.Interfaces;
using FinCircle.API.Services.Interfaces;

namespace FinCircle.API.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _repository;
        private readonly IMemberRepository _memberRepository;

        public LoanService(ILoanRepository repository, IMemberRepository memberRepository)
        {
            _repository = repository;
            _memberRepository = memberRepository;
        }

        public async Task CreateLoanAsync(CreateLoanDto dto)
        {
            var member = await _memberRepository.GetByIdAsync(dto.MemberId);

            if (member == null)
            {
                throw new Exception("Member not found.");
            }

            var loan = new Loan
            {
                MemberId = dto.MemberId,
                LoanAmount = dto.LoanAmount,
                InterestRate = dto.InterestRate,
                DurationMonths = dto.DurationMonths,
                Purpose = dto.Purpose,
                Status = "Pending",
                CreatedOn = DateTime.UtcNow
            };

            await _repository.AddAsync(loan);
        }

        public async Task<List<Loan>> GetAllLoanAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Loan?> GetLoanByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> ApproveLoanAsync(int id)
        {
            var loan = await _repository.GetByIdAsync(id);

            if (loan == null)
                return false;

            loan.Status = "Approved";
            loan.ApprovedDate = DateTime.UtcNow;

            await _repository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RejectLoanAsync(int id)
        {
            var loan = await _repository.GetByIdAsync(id);

            if (loan == null)
                return false;

            loan.Status = "Rejected";

            await _repository.SaveChangesAsync();

            return true;
        }
    }
}