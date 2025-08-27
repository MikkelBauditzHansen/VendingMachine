using System;

public class Product
{
	public string Name { get; set; }
	public int ID { get; set; }
	public string Size { get; set; }
	public int Price { get; set; }

	public Product(string name, int id, string size, int price)
	{
		Name = name;
		ID = id;
		Size = size;
		Price = price;
	}
	
}
