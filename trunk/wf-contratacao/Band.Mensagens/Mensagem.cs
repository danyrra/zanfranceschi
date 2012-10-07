using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Band.Mensagens
{
	public abstract class MensagemBase
	{
		public string Msg_Id { get { return Guid.NewGuid().ToString(); } }
		public abstract string Msg_Versao { get; }
		public DateTime Msg_Criada { get { return DateTime.Now; } }
	}
}
