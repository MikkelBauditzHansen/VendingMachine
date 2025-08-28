using System;

public class Product
{
	public string Name { get; set; }
	public int ID { get; set; }
	public string Size { get; set; }
	public int Price { get; set; }
	public int Quantity { get; set; }

    public Product(int id, string name, int price, string size, int quantity)
	{
		Name = name;
		ID = id;
		Size = size;
		Price = price;
		Quantity = quantity;
	}
	
}
