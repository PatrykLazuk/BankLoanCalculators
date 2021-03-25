
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Enums;
using API.Data.Interfaces;
using API.Models;
using Microsoft.Extensions.Configuration;

namespace API.Data.Repositories
{
    public class CalculationRepository : ICalculationRepository
    {
        private readonly IDbConnect _db;
        private string _connectionString;

        public CalculationRepository(IDbConnect dbConnect)
        {
            this._connectionString = GetConnectionString();
            this._db = dbConnect;
        }

        public async Task<LoanInterestModel> GetLoanType(int id)
        {
            string sql = "SELECT Id, LoanType, InterestRate FROM Loans WHERE Id=@Id";

            var post = await _db.LoadData<LoanInterestModel, dynamic>(sql, new { Id = id }, _connectionString);

            return post.FirstOrDefault();
        }

        public async Task<IEnumerable<LoanInterestModel>> GetLoanTypes()
        {
            string sql = "SELECT Id, LoanType, InterestRate FROM Loans";

            var loanTypes = await _db.LoadData<LoanInterestModel, dynamic>(sql, new { }, _connectionString);

            return loanTypes;
        }

        public List<RepaymentPlanModel> CreateRepaymentPlan(RepaymentInput input)
        {
            var output = new List<RepaymentPlanModel>();

            var monthlyCapitalInstallment = input.LoanAmount / (input.InstallmentsYears * 12);

            var yearOfRepay = DateTime.Today.Year;

            var capitalToRepay = input.LoanAmount;

            for (int i = 0; i < input.InstallmentsYears; i++)
            {
                var monthlyInteresAmout = (capitalToRepay * (input.InterestRate/100))/12;

                for (int j = 0; j < 12; j++)
                {
                    var installmentName = $"{(Months)j} {yearOfRepay}";

                    output.Add(new RepaymentPlanModel()
                    {
                        Month = installmentName,
                        Installment = Math.Round((monthlyCapitalInstallment + monthlyInteresAmout),3)
                    });
                    capitalToRepay -= monthlyCapitalInstallment;
                }
                yearOfRepay++;
            }

            return output;

        }

        private static string GetConnectionString(string connectionStringName = "Default")
        {
            string output = "";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json");

            var config = builder.Build();

            output = config.GetConnectionString(connectionStringName);

            return output;
        }

        
    }
}