using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Band.Mensagens.Wf
{
	public class ColaboradorContratado
		: MensagemBase
	{
		public string Nome { get; set; }
		public string Sobrenome { get; set; }
		public string Departamento { get; set; }
		public DateTime InicioColaboracao { get; set; }

		public override string Msg_Versao
		{
			get { return "0.1"; }
		}
	}
}
