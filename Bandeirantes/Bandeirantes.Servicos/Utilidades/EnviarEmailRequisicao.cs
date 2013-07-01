using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bandeirantes.Servicos.Comum;

namespace Bandeirantes.Servicos.Utilidades
{
	public class EnviarEmailRequisicao
		: Requisicao
	{
		public string De { get; set; }
		public string Titulo { get; set; }
		public string Etc { get; set; }
	}
}