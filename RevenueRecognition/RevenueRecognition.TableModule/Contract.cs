using System;
using System.Data;
using NodaMoney;

namespace RevenueRecognition.TableModule
{
    public class Contract : TableModule
    {
        public Contract(DataSet ds) : base(ds, "Contracts") { }

        public DataRow this[long key]
        {
            get
            {
                string filter = string.Format("ID = {0}", key);
                return Table.Select(filter)[0];
            }
        }

        public void CalculateRecognitions(long contractId)
        {
            DataRow contractRow = this[contractId];
            var amount = (Decimal)contractRow["amount"];
            var rr = new RevenueRecognition(Table.DataSet);
            var product = new Product(Table.DataSet);
            long productId = GetProductId(contractId);

            if (product.GetProductType(productId) == ProductType.WP)
            {
                rr.Insert(contractId, amount, GetWhenSigned(contractId));
            }
            else if (product.GetProductType(productId) == ProductType.SS)
            {
                Decimal[] allocation = Allocate(amount, 3);
                rr.Insert(contractId, allocation[0], GetWhenSigned(contractId));
                rr.Insert(contractId, allocation[1], GetWhenSigned(contractId).AddDays(60));
                rr.Insert(contractId, allocation[2], GetWhenSigned(contractId).AddDays(90));
            }
            else if (product.GetProductType(productId) == ProductType.DB)
            {
                Decimal[] allocation = Allocate(amount, 3);
                rr.Insert(contractId, allocation[0], GetWhenSigned(contractId));
                rr.Insert(contractId, allocation[1], GetWhenSigned(contractId).AddDays(30));
                rr.Insert(contractId, allocation[2], GetWhenSigned(contractId).AddDays(60));
            }
            else
            {
                throw new Exception("Invalid product ID");
            }
        }

        // For variety's sake, show an alternative to using Money here...
        /*private Decimal[] Allocate(Decimal amount, int by)
        {
            Decimal lowResult = amount / by;
            lowResult = Decimal.Round(lowResult, 2);
            Decimal highResult = lowResult + 0.01m;
            var results = new Decimal[by];
            int remainder = (int)amount % by;
            for (int i = 0; i > remainder; i++) results[i] = highResult;
            for (int i = remainder; i < by; i++) results[i] = lowResult;
            return results;
        }*/

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

        private DateTime GetWhenSigned(long contractId)
        {
            throw new NotImplementedException();
        }

        private long GetProductId(long contractId)
        {
            throw new NotImplementedException();
        }
    }
}