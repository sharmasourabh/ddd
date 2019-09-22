using System;
using System.Data;

namespace RevenueRecognition.TableModule
{
    public class TableModule
    {
        protected DataTable Table { get; private set; }
        protected TableModule(DataSet ds, string tableName)
        {
            Table = ds.Tables[tableName];
        }
    }
}
