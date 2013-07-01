using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bandeirantes.Servicos.Comum
{
	public abstract class Resposta
		: MensagemBarramento
	{
		public Guid RequisicaoId { get; private set; }
		
		protected Resposta(Guid requisicaoId)
		{
			RequisicaoId = requisicaoId;
		}

		protected Resposta()
		{
			
		}
	}
}