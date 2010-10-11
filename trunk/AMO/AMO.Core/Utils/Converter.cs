using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMO.Core.Utils
{
	internal class ConverterHelper
	{
		internal static int ToInt(string value)
		{
			int result = -1;

			int.TryParse(value, out result);

			return result;
		}
	}
}
