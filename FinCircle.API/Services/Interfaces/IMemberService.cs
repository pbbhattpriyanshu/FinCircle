using FinCircle.API.DTOs;
using FinCircle.API.Models;

namespace FinCircle.API.Services.Interfaces
{
    public interface IMemberService
    {
        Task CreateMemberAsync(CreateMemberDto dto);
        Task<List<Member>> GetAllMemberAsync();
        Task<Member?> GetMemberByIdAsync(int id);
        Task<bool> UpdateMemberAsync(int id, UpdateMemberDto dto);
        Task<bool> DeleteMemberAsync(int id);
    }
}
