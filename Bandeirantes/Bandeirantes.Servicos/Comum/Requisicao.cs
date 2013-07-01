using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bandeirantes.Servicos.Comum
{
	/// <summary>
	/// Requisição que não requer resposta; comando.
	/// </summary>
	public abstract class Requisicao
		: MensagemBarramento
	{

	}

	/// <summary>
	/// Marca para que um objeto Resposta não precise ser Generics.
	/// </summary>
	public interface IRequisicaoComResposta
	{
		Guid Id { get; set; }
		Resposta Resposta { get; set; }
	}
	
	/// <summary>
	/// Base para implementação do padrão Request/Response
	/// </summary>
	/// <typeparam name="TipoResposta"></typeparam>
	public class Requisicao<TipoResposta>
		: MensagemBarramento, IRequisicaoComResposta
	{
		public Resposta Resposta { get; set; }
	}
}