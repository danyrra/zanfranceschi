namespace Zanfranceschi.MsgEa.Model
{
	using System;
	
	[Serializable]
	public class Message
	{
		public string Text { get; private set; }
		
		public Message(string text)
		{
			Text = text;
		}

		public override string ToString()
		{
			return Text;
		}
	}
}
