using System;

public class Product
{
	public string Name { get; set; }
	public int ID { get; set; }
	public string Size { get; set; }
	public int Price { get; set; }
	

    public Product(int id, string name, int price, string size)
	{
		Name = name;
		ID = id;
		Size = size;
		Price = price;
		
	}
	
}
