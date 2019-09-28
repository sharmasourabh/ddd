using System;
using Dapper;
using Npgsql;
using SeparateModels.Domain;

namespace SeparateModels.ReadModels
{
    public class PolicyInfoDtoProjection
    {
        private readonly string cnString;

        public PolicyInfoDtoProjection(string cnString)
        {
            this.cnString = cnString;
        }

        public void CreatePolicyInfoDto(Policy policy)
        {
            using (var cn = new NpgsqlConnection(cnString))
            {
                var version = policy.Versions.WithNumber(1);
                
                var policyInfo = new PolicyInfoDto
                {
                    PolicyId = policy.Id,
                    PolicyNumber = policy.Number,
                    CoverFrom = version.CoverPeriod.ValidFrom,
                    CoverTo = version.CoverPeriod.ValidTo,
                    PolicyHolder = $"{version.PolicyHolder.LastName} {version.PolicyHolder.FirstName}",
                    Vehicle = $"{version.Car.PlateNumber} {version.Car.Make}",
                    TotalPremiumAmount = version.TotalPremium.Amount
                };
                
                cn.Open();
                cn.Execute(
                    "INSERT INTO public.policy_info_view (policy_id,policy_number,cover_from,cover_to,vehicle,policy_holder,total_premium) " +
                    "VALUES (@PolicyId,@PolicyNumber,@CoverFrom,@CoverTo,@Vehicle,@PolicyHolder,@TotalPremiumAmount)",
                    policyInfo);
            }    
        }

        public void UpdatePolicyInfoDto(Policy policy, PolicyVersion currentVersion)
        {
            using (var cn = new NpgsqlConnection(cnString))
            {
                var policyInfo = new PolicyInfoDto
                {
                    PolicyId = policy.Id,
                    CoverFrom = currentVersion.CoverPeriod.ValidFrom,
                    CoverTo = currentVersion.CoverPeriod.ValidTo,
                    PolicyHolder = $"{currentVersion.PolicyHolder.LastName} {currentVersion.PolicyHolder.FirstName}",
                    Vehicle = $"{currentVersion.Car.PlateNumber} {currentVersion.Car.Make}",
                    TotalPremiumAmount = currentVersion.TotalPremium.Amount
                };

                
                cn.Open();
                cn.Execute(
                    "UPDATE public.policy_info_view " +
                    "SET " +
                    "cover_from = @CoverFrom, " +
                    "cover_to = @CoverTo, " +
                    "vehicle = @Vehicle, " +
                    "policy_holder = @PolicyHolder, " +
                    "total_premium = @TotalPremiumAmount " +
                    "WHERE policy_id = @PolicyId ",
                    policyInfo);
          
            }
        }
        
    }
}