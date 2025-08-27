using System;
using VendingMachine.Repository;

public class PaymentService
{
	private IPaymentRepo _paymentRepo;
	public PaymentService (IPaymentRepo paymentRepo)
	{
		_paymentRepo = paymentRepo;
	}
	public void Add(Payment payment)
	{
		_paymentRepo.Add(payment);
	}
	public List<Payment> GetAll()
	{
		return _paymentRepo.GetAll();
	}
	public void Delete(Payment payment)
	{
		_paymentRepo.Delete(payment);
	}
}
