using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bandeirantes.Servicos.Bus
{
	public interface IServicesBus
		: IDisposable
	{
		IPublishChannel OpenPublishChannel();
		void Respond<RequestType, ResponseType>(Func<RequestType, ResponseType> func);
		void Subscribe<NotificationType>(string subscriptionId, Action<NotificationType> handler);
		void Subscribe<NotificationType>(string subscriptionId, string routingKey, Action<NotificationType> handler);
	}
}