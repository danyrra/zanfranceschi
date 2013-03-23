namespace Zanfranceschi.MsgEa.Messages.Responses
{
	using System;
	using Zanfranceschi.MsgEa.Model;
	
	[Serializable]
	public class ErrorResponse
		: Response
	{
		public ErrorResponse(Message message) : base(message) { }
	}
}
