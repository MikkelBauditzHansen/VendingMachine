using System;

public class Payment
{
	public int CostumerPayment {  get; set; }
    public List<int> OneCrown { get; set; }
    public List<int> FiveCrown { get; set; }
    public List<int> TwentyCrown { get; set; }

    public Payment(int costumerPayment, List<int> oneCrown, List<int> fiveCrown, List<int> twentyCrown)
	{
		CostumerPayment = costumerPayment;
		OneCrown = oneCrown;
		FiveCrown = fiveCrown;
		TwentyCrown = twentyCrown;
	}
	
}
