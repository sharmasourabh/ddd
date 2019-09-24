using System;
using System.Collections.Generic;
using System.Text;

namespace RevenueRecognition.Domain
{
    class CompleteRecognitionStrategy : RecognitionStrategy
    {
        public override void CalculateRevenueRecognitions(Contract contract)
        {
            contract.addRevenueRecognition(contract.revenue, contract.dateSigned);
        }
    }
}
