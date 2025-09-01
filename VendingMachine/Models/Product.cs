using System;

public class Product
{
	public string Name { get; set; }
	public int ID { get; set; }
	public int Price { get; set; }
	

    public Product(int id, string name, int price)
	{
		Name = name;
		ID = id;
		Price = price;
		
	}
	
}
