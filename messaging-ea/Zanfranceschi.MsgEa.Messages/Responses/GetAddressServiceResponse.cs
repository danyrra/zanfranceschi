namespace Zanfranceschi.MsgEa.Messages.Responses
{
	using System;
	using Zanfranceschi.MsgEa.Model;
	
	[Serializable]
	public class GetAddressServiceResponse
		: Response
	{
		public Address Address { get; private set; }

		public GetAddressServiceResponse(Address address, Message message)
			: base(message)
		{
			Address = address;
		}
	}
}