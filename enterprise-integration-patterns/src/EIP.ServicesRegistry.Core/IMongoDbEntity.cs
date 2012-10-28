using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace EIP.ServicesRegistry.Core
{
	internal interface IMongoDbEntity
	{
		ObjectId Id { get; }
	}
}
