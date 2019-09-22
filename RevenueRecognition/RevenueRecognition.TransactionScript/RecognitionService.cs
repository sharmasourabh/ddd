using System;
using System.Data.SqlClient;
using NodaMoney;
//using RevenueRecogniction.BasePatterns;

namespace RevenueRecognition.TransactionScript
{
    public class RecognitionService
    {
        private readonly Gateway _gateway;

        public RecognitionService(Gateway gateway)
        {
            _gateway = gateway;
        }

        // A transaction script
        public Money RecognizedRevenue(long contractNumber, DateTime asOf)
        {
            // This logic is certainly simple enough for a transaction script.
            // A Domain Model would certainly be an overkill.
            Money result = Money.USDollar(0);
            try
            {
                var dr = _gateway.FindRecognitionsFor(contractNumber, asOf);
                while (dr.Read())
                {
                    result = Money.Add(result, Money.USDollar(dr.GetDecimal(0)));
                }
                return result;
            }
            catch (SqlException e)
            {
                throw new ApplicationException(e.Message, e);
            }
        }

        // A transaction script
        public void CalculateRevenueRecognitions(long contractNumber)
        {
            var contracts = _gateway.FindContract(contractNumber);
            contracts.Read();
            Decimal totalRevenue = contracts.GetDecimal(0);
            var recognitionDate = contracts.GetDateTime(1);

            // Here comes the real domain logic in this example.
            // These rules may be simple enough for a transaction script,
            // but if the rules become much more complicated, consider using a Domain Model
            string type = contracts.GetString(2);
            if (type == "S")
            {
                var allocation = Allocate(totalRevenue, 3);
                _gateway.InsertRecognition(contractNumber, allocation[0], recognitionDate);
                _gateway.InsertRecognition(contractNumber, allocation[1], recognitionDate.AddDays(60));
                _gateway.InsertRecognition(contractNumber, allocation[2], recognitionDate.AddDays(90));
            }
            else if (type == "W")
            {
                _gateway.InsertRecognition(contractNumber, totalRevenue, recognitionDate);
            }
            else if (type == "D")
            {
                var allocation = Allocate(totalRevenue, 3);
                _gateway.InsertRecognition(contractNumber, allocation[0], recognitionDate);
                _gateway.InsertRecognition(contractNumber, allocation[1], recognitionDate.AddDays(30));
                _gateway.InsertRecognition(contractNumber, allocation[2], recognitionDate.AddDays(60));
            }
        }

        private Decimal[] Allocate(Decimal totalRevenue, int count)
        {
            double defaultFractionDigits = Currency.FromCode("USD").DecimalDigits;
            Decimal onePart = Decimal.Round(Decimal.Divide(totalRevenue, new Decimal(count)), MidpointRounding.ToEven);
            Decimal[] allocations = new Decimal[count];
            Decimal newTotal = Decimal.Round(Decimal.Zero, (int)defaultFractionDigits, MidpointRounding.ToEven);
            for (int i = 0; i < count; i++)
            {
                allocations[i] = onePart;
                newTotal = Decimal.Add(newTotal, onePart);
            }
            if (!newTotal.Equals(totalRevenue))
            {
                // Adjust last allocation to achieve total revenue
                Decimal lastAllocation = allocations[count - 1];
                lastAllocation = Decimal.Add(lastAllocation, Decimal.Subtract(totalRevenue, newTotal));
                //lastAllocation = lastAllocation.add(totalRevenue. Subtract(newTotal));
                allocations[count - 1] = lastAllocation;
            }
            return allocations;
        }
    }
}