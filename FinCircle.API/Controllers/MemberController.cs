using FinCircle.API.DTOs;
using FinCircle.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinCircle.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMember(CreateMemberDto dto)
        {
            await _memberService.CreateMemberAsync(dto);

            return Ok(new
            {
                message = "Member created sucessfully."
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMembers()
        {
            var members = await _memberService.GetAllMemberAsync();
            return Ok(members);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemberById(int id)
        {
            var member = await _memberService.GetMemberByIdAsync(id);

            if (member == null)
            {
                return NotFound(new
                {
                    message = "Member not found"
                });
            }
            return Ok(member);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, UpdateMemberDto dto)
        {
            var result = await _memberService.UpdateMemberAsync(id, dto);

            if (!result)
                return NotFound(new { message = "Member not found." });

            return Ok(new { message = "Member updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var result = await _memberService.DeleteMemberAsync(id);

            if (!result)
                return NotFound(new { message = "Member not found." });

            return Ok(new { message = "Member deleted successfully." });
        }
    }
}
