namespace Zanfranceschi.MsgEa.Messages.Responses
{
	using System;
	using Zanfranceschi.MsgEa.Model;
	
	[Serializable]
	public abstract class Response
	{
		public Message Message { get; set; }

		protected Response(Message message)
		{
			Message = message;
		}
	}
}