using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using System.Runtime.Serialization;

namespace EIP.ServicesRegistry.Core.Entities
{
	public interface IEntity
	{
		string Id { get; }
	}
}
