namespace Zanfranceschi.MsgEa.Messages.Responses
{
	using System;
	using Zanfranceschi.MsgEa.Model;

	[Serializable]
	public class CustomerUpdateOrDeleteServiceResponse
		: Response
	{
		public CustomerUpdateOrDeleteServiceResponse(Message message)
			: base(message)
		{

		}
	}
}