using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculationsController : BaseApiController
    {
        private ICalculationRepository _calcRepo;
        public CalculationsController(ICalculationRepository calcRepo)
        {
            _calcRepo = calcRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLoanTypes()
        {
            var loanTypes = await _calcRepo.GetLoanTypes();

            return Ok(loanTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoanType(int id)
        {
            var loanType = await _calcRepo.GetLoanType(id);

            return Ok(loanType);
        }

        [HttpPost("createrepaymentplan")]
        public IActionResult CreateRepaymentPlan(RepaymentInput input)
        {
            var repaymentPlan = _calcRepo.CreateRepaymentPlan(input);

            return Ok(repaymentPlan);
        }
    }
}