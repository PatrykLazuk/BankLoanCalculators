using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace API.Data.Interfaces
{
    public interface ICalculationRepository
    {
        Task<IEnumerable<LoanInterestModel>> GetLoanTypes();
        Task<LoanInterestModel> GetLoanType(int id);
        List<RepaymentPlanModel> CreateRepaymentPlan(RepaymentInput input);
    }
}