using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tutorials.Core.Domain
{
	public interface ISearchItemResult
	{
		string Title { get; }
		string Description { get; }
	}
}
