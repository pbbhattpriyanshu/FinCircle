using FinCircle.API.DTOs;
using FinCircle.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinCircle.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLoan(CreateLoanDto dto)
        {
            await _loanService.CreateLoanAsync(dto);

            return Ok(new
            {
                message = "Loan created successfully."
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLoan()
        {
            var loans = await _loanService.GetAllLoanAsync();

            return Ok(loans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoanById(int id)
        {
            var loan = await _loanService.GetLoanByIdAsync(id);

            if (loan == null)
                return NotFound();

            return Ok(loan);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/approve")]
        public async Task<IActionResult> ApproveLoan(int id)
        {
            var result = await _loanService.ApproveLoanAsync(id);

            if (!result)
                return NotFound();

            return Ok(new
            {
                message = "Loan approved successfully."
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/reject")]
        public async Task<IActionResult> RejectLoan(int id)
        {
            var result = await _loanService.RejectLoanAsync(id);

            if (!result)
                return NotFound();

            return Ok(new
            {
                message = "Loan rejected successfully."
            });
        }
    }
}