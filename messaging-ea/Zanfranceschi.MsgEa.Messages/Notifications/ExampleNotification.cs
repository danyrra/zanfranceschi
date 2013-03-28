namespace Zanfranceschi.MsgEa.Messages.Notifications
{
	using System;
	using Zanfranceschi.MsgEa.Model;
	
	[Serializable]
	public class ExampleNotification
	{
		public string Message { get; private set; }

		public ExampleNotification(string message)
		{
			Message = message;
		}
	}
}
