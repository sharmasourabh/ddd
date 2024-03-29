﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.DomainServices.Insurance.Model
{
    // Domain Service interface
    public interface IMultiMemberPremiumCalculator
    {
        Quote CalculatePremium(Policy mainPolicy, IEnumerable<Member> additionalMembers);
    }
}
