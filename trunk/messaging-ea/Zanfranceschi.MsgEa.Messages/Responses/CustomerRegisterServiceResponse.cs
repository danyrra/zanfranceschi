namespace Zanfranceschi.MsgEa.Messages.Responses
{
	using System;
	using Zanfranceschi.MsgEa.Model;
	
	[Serializable]
	public class CustomerRegisterServiceResponse
		: Response
	{
		public Customer NewlyRegisteredCustomer { get; private set; }

		public CustomerRegisterServiceResponse(Customer customer, Message message)
			: base(message)
		{
			NewlyRegisteredCustomer = customer;
		}
	}
}