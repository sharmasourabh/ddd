using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.DomainServices.Insurance.Application
{
  class Class1
  {
    public void test()
    {

      var ourCustomer = new OurCustomer("Sheen", "Dayal");
      var adapter = new CustomerAdapter();
      var customer = adapter.ConvertToCustomer(ourCustomer);

      var adaptorInterface = new CustomerAdapter2();
      customer = adaptorInterface.Convert(ourCustomer);
    }
  }
}


public class Customer // Legacy
{
  public string FullName { get; set; }

  public Customer(string fullName)
  {
    this.FullName = fullName;
  }
}

public class OurCustomer // New Code Class
{
  public string FirstName { get; set; }
  public string LastName { get; set; }

  public OurCustomer(string firstName, string lastName)
  {
    FirstName = firstName;
    LastName = lastName;
  }
}


public class CustomerAdapter
{
  public Customer ConvertToCustomer(OurCustomer customer)
  {
    return new Customer($"{customer.FirstName} {customer.LastName}");
  }
}



public interface IAdapter<O, I>
{
  O Convert(I input);
}


public class CustomerAdapter2 : IAdapter<Customer, OurCustomer>
{

  public Customer Convert(OurCustomer customer)
  {
    return new Customer($"{customer.FirstName} {customer.LastName}");
  }

}



// The 'Facade' class
/*class Mortgage
{
  private Bank _bank = new Bank();
  private Loan _loan = new Loan();
  private Credit _credit = new Credit();

  public bool IsEligible(Customer cust, int amount)
  {
    Console.WriteLine("{0} applies for {1:C} loan\n",
      cust.Name, amount);

    bool eligible = true;

    // Check creditworthyness of applicant

    if (!_bank.HasSufficientSavings(cust, amount))
    {
      eligible = false;
    }
    else if (!_loan.HasNoBadLoans(cust))
    {
      eligible = false;
    }
    else if (!_credit.HasGoodCredit(cust))
    {
      eligible = false;
    }

    return eligible;
  }
}*/