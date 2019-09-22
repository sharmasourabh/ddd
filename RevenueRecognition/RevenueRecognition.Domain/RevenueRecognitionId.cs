using System;
using NodaTime;
using RevenueRecognition.Core;

namespace RevenueRecognition.Domain
{
    public class RevenueRecognitionId : Value<RevenueRecognitionId>
    {
        private Guid Value { get; }
        public LocalDate RecognizedOn { get; private set; }

        public RevenueRecognitionId(Guid value, LocalDate recognizedOn)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "Revenue Recognition id cannot be empty");
            if (recognizedOn == default)
                throw new ArgumentNullException(nameof(value), "Revenue Recognition date cannot be empty");

            Value = value;
            RecognizedOn = recognizedOn;
        }

        public static implicit operator Guid(RevenueRecognitionId self) => self.Value;

        public static implicit operator RevenueRecognitionId(string value)
            => new RevenueRecognitionId(Guid.Parse(value), LocalDate.FromDateTime(DateTime.Now));
    }
}
