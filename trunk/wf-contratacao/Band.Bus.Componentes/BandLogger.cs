using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyNetQ;

namespace Band.Bus.Componentes
{
	public class BandLogger : IEasyNetQLogger
	{
		public void DebugWrite(string format, params object[] args)
		{
			
		}

		public void ErrorWrite(Exception exception)
		{
			
		}

		public void ErrorWrite(string format, params object[] args)
		{
			
		}

		public void InfoWrite(string format, params object[] args)
		{
			
		}
	}
}
