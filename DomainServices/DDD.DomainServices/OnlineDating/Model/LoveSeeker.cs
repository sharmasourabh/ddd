﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.DomainServices.OnlineDating.Model
{
    // entity
    public class LoveSeeker
    {
        public Guid Id { get; protected set; }

        public BloodType BloodType { get; private set; }

        // ...
    }
}
