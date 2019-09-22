using System;
using RevenueRecognition.Core;

namespace RevenueRecognition.Domain
{
    public class ProductName : Value<ProductName>
    {
        public string Value { get; }
        public static ProductName FromString(string name)
        {
            CheckValidity(name);
            return new ProductName(name);
        }

        internal ProductName(string value) => Value = value;

        public static implicit operator string(ProductName productName) => productName.Value;

        private static void CheckValidity(string value)
        {
            if (value.Length > 48)
                throw new ArgumentOutOfRangeException(
                    "Name cannot be longer that 48 characters",
                    nameof(value));
        }
    }
}