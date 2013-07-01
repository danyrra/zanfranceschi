using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bandeirantes.Servicos.Comum;

namespace Bandeirantes.Servicos.Tv.Programacao
{
	public class ListarProgramasResposta
		: Resposta
	{
		public ListarProgramasRespostaEntidade Programas { get; private set; }
		
		public ListarProgramasResposta(ListarProgramasRespostaEntidade programas)
			: base()
		{
			Programas = programas;
		}
	}

	public class ListarProgramasRespostaEntidade
	{
		public string Mnemonico { get; set; }
		public string Titulo { get; set; }
	}
}