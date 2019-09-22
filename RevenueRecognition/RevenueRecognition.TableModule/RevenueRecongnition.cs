using System;
using System.Data;
using System.Linq;

namespace RevenueRecognition.TableModule
{
    public class RevenueRecognition : TableModule
    {
        public RevenueRecognition(DataSet ds) : base(ds, "RevenueRecognitions") { }

        // Note: purely an in-memory insertion
        public long Insert(long contractId, Decimal amount, DateTime date)
        {
            DataRow newRow = Table.NewRow();
            long id = GetNextId();
            newRow["ID"] = id;
            newRow["contractID"] = contractId;
            newRow["amount"] = amount;
            newRow["date"] = string.Format("{0:s}", date);
            Table.Rows.Add(newRow);
            return id;
        }

        // Allthough this methods takes a contract ID as an argument, it operates
        // on the RevenueRecognition table, so it makes sense to have it in the
        // RevenueRecognition TableModule.
        public Decimal RecognizedRevenue(long contractId, DateTime asof)
        {
            string filter = string.Format("ContractID = {0} AND date <= #{1:d}#", contractId, asof);
            var rows = Table.Select(filter);
            return rows.Sum(row => (Decimal)row["amount"]);
        }

        private long GetNextId()
        {
            throw new NotImplementedException();
        }
    }
}