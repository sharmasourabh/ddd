using System;
using System.Collections.Generic;
using System.Text;
using NodaTime;
using NodaMoney;

namespace RevenueRecognition.Domain
{
    class ThreeWayRecognitionStrategy : RecognitionStrategy
    {
        private readonly int _firstRecognitionOffset;
        private readonly int _secondRecognitionOffset;

        public ThreeWayRecognitionStrategy(int firstRecognitionOffset,
                int secondRecognitionOffset)
        {
            this._firstRecognitionOffset = firstRecognitionOffset;
            this._secondRecognitionOffset = secondRecognitionOffset;
        }


        public override void CalculateRevenueRecognitions(Contract contract)
        {
            LocalDate dateSigned = contract.dateSigned;
            Money revenue = contract.revenue;
            Decimal[] allocations = Allocate(revenue.Amount, 3);

            contract.addRevenueRecognition(new Money(allocations[0], Contract.CURRENCY), dateSigned);
            contract.addRevenueRecognition(new Money(allocations[1], Contract.CURRENCY),
                    dateSigned.PlusDays(_firstRecognitionOffset));
            contract.addRevenueRecognition(new Money(allocations[2], Contract.CURRENCY),
                    dateSigned.PlusDays(_secondRecognitionOffset));
        }

        private Decimal[] Allocate(Decimal totalRevenue, int count)
        {
            double defaultFractionDigits = Contract.CURRENCY.DecimalDigits;
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
                allocations[count - 1] = lastAllocation;
            }
            return allocations;
        }
    }
}
