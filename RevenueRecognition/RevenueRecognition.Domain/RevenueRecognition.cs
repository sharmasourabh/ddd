using System;
using System.Collections.Generic;
using System.Text;
using NodaTime;
using NodaMoney;

namespace RevenueRecognition.Domain
{
    public class RevenueRecognition
    {
        public RevenueRecognitionId revenueRecognitionId { get; }

        private Money _amount;

        private readonly String currencyCode = Contract.CURRENCY.Code;

        public RevenueRecognition(Contract contract, LocalDate recognizedOn, Money amount)
        {
            if (contract == null)
            {
                throw new ArgumentException("Contract cannot be null");
            }
            if (recognizedOn == null)
            {
                throw new ArgumentException("Recognized date cannot be null");
            }
            if (amount == null)
            {
                throw new ArgumentException("Amount cannot be null");
            }
            // this.contract = contract;
            // this.contractId = contract.getContractId().getValue();
            //this.recognizedOn = recognizedOn;
            this.revenueRecognitionId = new RevenueRecognitionId(contract.contractId, recognizedOn);
            this._amount = amount;
        }

        public LocalDate getRecognizedOn()
        {
            return revenueRecognitionId.RecognizedOn;
        }

        public bool isRecognizableBy(LocalDate asOf)
        {
            LocalDate recognizedOn = getRecognizedOn();
            return asOf.CompareTo(recognizedOn) > 0 || asOf.Equals(recognizedOn);
        }

        public Money getAmount()
        {
            return _amount;
        }
    }
}