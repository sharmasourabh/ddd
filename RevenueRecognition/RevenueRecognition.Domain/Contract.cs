using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NodaTime;
using NodaMoney;

namespace RevenueRecognition.Domain
{
    public class Contract
    {
        public static Currency CURRENCY = Currency.FromCode("USD");

        public ContractId contractId { get; }

        public Product product { get; }

        public Money revenue { get; }

        public LocalDate dateSigned { get; }

        public List<RevenueRecognition> revenueRecognitions;// { get; private set; }

        public Contract(ContractId contractId, Product product, Money revenue, LocalDate dateSigned)
        {
            this.contractId = contractId;
            this.product = product;
            this.revenue = revenue;
            this.dateSigned = dateSigned;
            this.revenueRecognitions = new List<RevenueRecognition>();
        }

        public void calculateRevenueRecognitions()
        {
            product.CalculateRevenueRecognition(this);
        }

        public void addRevenueRecognition(Money Money, LocalDate dateRecognized)
        {
            revenueRecognitions.Add(
                    new RevenueRecognition(this, dateRecognized, Money));
        }

        public ReadOnlyCollection<RevenueRecognition> getRevenueRecognitions()
        {
            return revenueRecognitions.AsReadOnly();
        }

        public Money recognizedRevenue(LocalDate asOf)
        {
            Money result =
                    new Money(0L, CURRENCY);
            foreach (RevenueRecognition revenueRecognition in revenueRecognitions)
            {
                if (revenueRecognition.isRecognizableBy(asOf))
                {
                    result = Decimal.Add(result.Amount, revenueRecognition.getAmount().Amount);
                }
            }
            return result;
        }

    }
}
