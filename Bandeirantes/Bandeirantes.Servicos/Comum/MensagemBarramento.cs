using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bandeirantes.Servicos.Comum
{
	public abstract class MensagemBarramento
	{
		public Guid Id { get; set; }
		public string UsuarioInterface { get; set; }
		public string UsuarioSistema { get; set; }
		public DateTime EnviadaEm { get; set; }
	}
}