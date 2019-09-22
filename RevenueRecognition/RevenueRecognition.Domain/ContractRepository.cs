using System;
using System.Collections.Generic;
using System.Text;

namespace RevenueRecognition.Domain
{
    public interface ContractRepository : GenericRepository<Contract, ContractId>
    {
    }
}
