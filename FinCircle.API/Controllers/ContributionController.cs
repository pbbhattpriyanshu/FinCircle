using FinCircle.API.DTOs;
using FinCircle.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinCircle.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContributionController : ControllerBase
    {
        private readonly IContributionService _contributionService;

        public ContributionController(IContributionService contributionService)
        {
            _contributionService = contributionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateContribution(CreateContributionDto dto)
        {
            await _contributionService.CreateContributionAsync(dto);

            return Ok(new
            {
                message = "Contribution created successfully."
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContributions()
        {
            var contributions = await _contributionService.GetAllContributionAsync();

            return Ok(contributions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContributionById(int id)
        {
            var contribution = await _contributionService.GetContributionByIdAsync(id);

            if (contribution == null)
                return NotFound(new
                {
                    message = "Contribution not found."
                });

            return Ok(contribution);
        }
    }
}