using System;
using System.Data;

namespace RevenueRecognition.TableModule
{
    public enum ProductType { WP, SS, DB }

    public class Product : TableModule
    {
        public Product(DataSet ds) : base(ds, "Products") { }

        public DataRow this[long key]
        {
            get
            {
                string filter = string.Format("ID = {0}", key);
                return Table.Select(filter)[0];
            }
        }

        // Encapsulates the data in the data table (as an enum). There's an argument for
        // doing this all columns of data, as opposed to accessing them directly as we did
        // with the amount on the contract. This is not used here because it doesn't fit with
        // the assumption of the environment that different parts of the system access
        // the data set directly.
        public ProductType GetProductType(long productId)
        {
            var typeCode = (string)this[productId]["type"];
            return (ProductType)Enum.Parse(typeof(ProductType), typeCode);
        }
    }
}