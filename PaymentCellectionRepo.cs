using System;
using VendingMachine.Models;

public class PaymentCollectionRepo
{
	public List<int> OneCrown { get; set; }
	public List<int> FiveCrown { get; set; }
	public List<int> TwentyCrown { get; set; }

	public PaymentCollectionRepo(list<int> oneCrown, list<int> fiveCrown, list <int> twentyCrown )
	{
		OneCrown = oneCrown;
		FiveCrown = fiveCrown;
		TwentyCrown = twentyCrown;
	}
}
