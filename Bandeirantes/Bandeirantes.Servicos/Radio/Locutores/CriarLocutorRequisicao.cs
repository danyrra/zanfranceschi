using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bandeirantes.Servicos.Comum;

namespace Bandeirantes.Servicos.Radio.Locutores
{
	public class CriarLocutorRequisicao
		: Requisicao
	{
		public string Nome { get; private set; }
		public string TipoVoz { get; private set; }

		public CriarLocutorRequisicao(string nome, string tipoVoz)
		{
			Nome = nome;
			TipoVoz = tipoVoz;
		}
	}
}