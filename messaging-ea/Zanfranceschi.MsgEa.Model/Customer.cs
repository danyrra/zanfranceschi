namespace Zanfranceschi.MsgEa.Model
{
	using System;
	
	[Serializable]
	public class Customer
	{
		public string Id { get; set; }
		public string Name { get; set; }

		public Customer(string name)
		{
			Id = Guid.NewGuid().ToString();
			Name = name;
		}

		public Customer(string id, string name)
		{
			Id = id;
			Name = name;
		}
	}
}