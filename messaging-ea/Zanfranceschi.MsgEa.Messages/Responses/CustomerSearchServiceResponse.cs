namespace Zanfranceschi.MsgEa.Messages.Responses
{
	using System;
	using Zanfranceschi.MsgEa.Model;

	[Serializable]
	public class CustomerSearchServiceResponse
		: Response
	{
		public Customer[] Customers { get; private set; }

		public CustomerSearchServiceResponse(Customer[] customers, Message message)
			: base(message)
		{
			Customers = customers;
		}
	}
}