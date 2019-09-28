using System;
using System.Linq;
using NoCqrs.Domain;
using NodaMoney;
using Xunit;
using static Xunit.Assert;

namespace NoCqrs.Tests
{
    public class PolicyTerminationUnitTests
    {
        [Fact]
        public void CanTerminatePolicyInTheMiddle()
        {
            var policy = PolicyTestData.StandardOneYearPolicy(new DateTime(2019, 1, 1));
            var terminationDate = new DateTime(2019, 7, 1);
            policy.TerminatePolicy(terminationDate);
            
            Equal(2, policy.Versions.Count());
            Equal(Money.Euro(500), policy.Versions.WithNumber(1).TotalPremium);
            Equal(PolicyVersionStatus.Active, policy.Versions.WithNumber(1).VersionStatus);
            
            Equal(PolicyStatus.Terminated, policy.Versions.WithNumber(2).PolicyStatus);
            Equal(PolicyVersionStatus.Draft, policy.Versions.WithNumber(2).VersionStatus);
            Equal(terminationDate, policy.Versions.WithNumber(2).VersionValidityPeriod.ValidFrom);
            Equal(terminationDate.AddDays(-1), policy.Versions.WithNumber(2).CoverPeriod.ValidTo);
            Equal(new DateTime(2019,1,1), policy.Versions.WithNumber(2).CoverPeriod.ValidFrom);
            Equal(Money.Euro(246.58), policy.Versions.WithNumber(2).TotalPremium);
        }
        
        [Fact]
        public void CannotTerminatePolicyAfterCoverEnds()
        {
            var policy = PolicyTestData.StandardOneYearPolicy(new DateTime(2019, 1, 1));
            var terminationDate = new DateTime(2020, 1, 2);
            var ex = Assert.Throws<ApplicationException>(() => policy.TerminatePolicy(terminationDate));
            Equal("No active version at given date", ex.Message);
        }
    }
}