using System;
using System.Data;
//using RevenueRecogniction.BasePatterns;
using NodaMoney;

namespace RevenueRecognition.TransactionScript
{
    // Table Data Gateway
    // Implement using DataReader, EF or whatever...
    public class Gateway
    {
        public IDataReader FindRecognitionsFor(long contractId, DateTime asof)
        {
            throw new NotImplementedException();
        }

        public IDataReader FindContract(long contractNumber)
        {
            throw new NotImplementedException();
        }

        public void InsertRecognition(long contractNumber, Money money, DateTime recognitionDate)
        {
            throw new NotImplementedException();
        }
    }
}