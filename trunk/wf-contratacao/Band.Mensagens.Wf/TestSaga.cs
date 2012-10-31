using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using EasyNetQ;

namespace Band.Mensagens.Wf
{
	 [Export(typeof(ISaga))]
	class TestSaga : ISaga
	{

	}
}
