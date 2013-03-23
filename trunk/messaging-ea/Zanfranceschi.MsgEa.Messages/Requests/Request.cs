namespace Zanfranceschi.MsgEa.Messages.Requests
{
	using System;
	using Zanfranceschi.MsgEa.Model;
	
	[Serializable]
	public abstract class Request
	{
		public User Requestor { get; private set; }
		
		protected Request(User requestor)
		{ 
		
		}
	}
}
