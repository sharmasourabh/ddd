using System;

namespace DDD.EventSourcing
{
  public abstract class AccountEvents
  {
    public Guid TransactionId { get; }
    public decimal Amount { get; }
    public DateTime DateTime { get; }

    public AccountEvents(Guid transactionId, decimal amount, DateTime dateTime)
    {
      TransactionId = transactionId;
      Amount = amount;
      DateTime = dateTime;
    }
  }

  public class Deposited : AccountEvents
  {
    public Deposited(Guid transactionId, decimal amount, DateTime dateTime) : base(transactionId, amount, dateTime)
    {
    }
  }

  public class Withdrawn : AccountEvents
  {
    public Withdrawn(Guid transactionId, decimal amount, DateTime dateTime) : base(transactionId, amount, dateTime)
    {
    }
  }
}