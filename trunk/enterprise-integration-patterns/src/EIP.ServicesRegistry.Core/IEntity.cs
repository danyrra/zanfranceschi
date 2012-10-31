using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace EIP.ServicesRegistry.Core
{
	public interface IEntity
	{
		string Id { get; }
	}
}
