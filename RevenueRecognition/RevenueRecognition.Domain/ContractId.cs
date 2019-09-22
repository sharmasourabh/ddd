using System;
using System.Collections.Generic;
using System.Text;
using RevenueRecognition.Core;

namespace RevenueRecognition.Domain
{
    public class ContractId : Value<ContractId>
    {
        private Guid Value { get; }

        public ContractId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "Contract id cannot be empty");

            Value = value;
        }

        public static implicit operator Guid(ContractId self) => self.Value;

        public static implicit operator ContractId(string value)
            => new ContractId(Guid.Parse(value));

        public bool Equals(ContractId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value.Equals(other.Value);
        }
    }
}
