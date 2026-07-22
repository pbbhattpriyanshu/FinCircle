using FinCircle.API.Data;
using FinCircle.API.Models;
using FinCircle.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinCircle.API.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext _context;

        //Contructor - injection
        public MemberRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Member member)
        {
            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Member>> GetAllAsync()
        {
            return await _context.Members.ToListAsync();
        }

        public async Task<Member?> GetByIdAsync(int id)
        {
            return await _context.Members.FindAsync(id);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Members.AnyAsync(x => x.Email == email);
        }

        public async Task<bool> ExistsByPhoneAsync(string phone)
        {
            return await _context.Members.AnyAsync(x => x.PhoneNumber == phone);
        }

        public async Task UpdateAsync(Member member)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Member member)
        {
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
        }
    }
}
