using System;
using VendingMachine.Repository;


public class PaymentCollectionRepo : IPaymentRepo
{
	public List<Payment> Payment;

	public PaymentCollectionRepo()
	{
		Payment = new List<Payment>();
	}

	public virtual void Add(Payment payment)
	{
		Payment.Add(payment);
	}
	public List<Payment> GetAll()
	{
		return Payment;
	}
	public virtual void Delete(Payment payment)
	{
		Payment.Remove(payment);
	}

	
}
