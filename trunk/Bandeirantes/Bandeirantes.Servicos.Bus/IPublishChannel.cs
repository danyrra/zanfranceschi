using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bandeirantes.Servicos.Bus
{
	public interface IPublishChannel
		: IDisposable
	{
		void Publish<T>(T message);
		void Publish<T>(T message, string routingKey);
		void Request<T>(T message);
		void BatchRequest<RequestType, ResponseType>(IEnumerable<RequestType> requests, Action<ResponseType> handler);
		void Request<RequestType, ResponseType>(RequestType request, Action<ResponseType> handler);
	}
}