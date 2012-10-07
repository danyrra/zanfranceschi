using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Band.Mensagens.Wf
{
	public class LoginColaboradorCriado
		: MensagemBase
	{
		public ColaboradorContratado Colaborador { get; set; }
		public string Login { get; set; }

		public override string Msg_Versao
		{
			get { return "0.1"; }
		}
	}
}
