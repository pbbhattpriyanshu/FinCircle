using FinCircle.API.DTOs;
using FinCircle.API.Repositories.Interfaces;
using FinCircle.API.Services.Interfaces;
using FinCircle.API.Models;


namespace FinCircle.API.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _repository;

        public MemberService(IMemberRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateMemberAsync(CreateMemberDto dto)
        {
            if (await _repository.ExistsByEmailAsync(dto.Email))
            {
                throw new Exception("Email alredy exists.");
            }

            if (await _repository.ExistsByPhoneAsync(dto.PhoneNumber))
            {
                throw new Exception("Phone number alredy exists.");
            }

            var memberCode = "FC" + Guid.NewGuid().ToString("N")[..6].ToUpper();

            var member = new Member
            {
                MemberCode = memberCode,
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Address = dto.Address,
                JoinedOn = DateTime.UtcNow,
                IsActive = true
            };

            await _repository.AddAsync(member);
        }

        public async Task<List<Member>> GetAllMemberAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Member?> GetMemberByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateMemberAsync(int id, UpdateMemberDto dto)
        {
            var member = await _repository.GetByIdAsync(id);

            if (member == null)
                return false;

            member.FullName = dto.FullName;
            member.PhoneNumber = dto.PhoneNumber;
            member.Email = dto.Email;
            member.Address = dto.Address;

            await _repository.UpdateAsync(member);

            return true;
        }

        public async Task<bool> DeleteMemberAsync(int id)
        {
            var member = await _repository.GetByIdAsync(id);

            if (member == null)
                return false;

            await _repository.DeleteAsync(member);

            return true;
        }
    }
}
