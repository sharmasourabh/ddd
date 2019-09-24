using System;
using System.Collections.Generic;
using System.Text;
using RevenueRecognition.Core;

namespace RevenueRecognition.Domain
{
    public class ProductId : IEquatable<ProductId>
    {
        private Guid Value { get; }

        public static ProductId FromGuid(Guid value)
        {
            CheckValidity(value);
            return new ProductId(value);
        }

        internal ProductId(Guid value) => Value = value;

        public static implicit operator Guid(ProductId id) => id.Value;

        private static void CheckValidity(Guid value)
        {
            if (value == default)
                throw new ArgumentOutOfRangeException(
                    "Id cannot be empty",
                    nameof(value));
        }

        public static implicit operator ProductId(string value)
            => new ProductId(Guid.Parse(value));

        public override string ToString() => Value.ToString();

        public bool Equals(ProductId other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ProductId) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
