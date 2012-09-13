using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tutorials.Core.Infrastructure.DomainBase
{
	public abstract class EntityBase
		: IEntity
	{
		public string Key { get; internal set; }

		protected EntityBase()
			: this(null)
		{

		}
		
		protected EntityBase(string key)
		{
			Key = key ?? Guid.NewGuid().ToString();
		}

		public bool EqualsByKey(object key)
		{
			if (key == null)
				return false;

			return Key.Equals(key.ToString());
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			EntityBase entity = obj as EntityBase;

			if (entity == null)
				return false;

			return this.Key.Equals(entity.Key);
		}
	}
}
