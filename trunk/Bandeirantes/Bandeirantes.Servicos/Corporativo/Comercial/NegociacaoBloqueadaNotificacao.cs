using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bandeirantes.Servicos.Comum;

namespace Bandeirantes.Servicos.Corporativo.Comercial
{
	public class NegociacaoBloqueadaNotificacao
		: Notificacao
	{
		public int NegociacaoId { get; private set; }

		public NegociacaoBloqueadaNotificacao(int negociacaoId)
		{
			NegociacaoId = negociacaoId;
		}
	}
}