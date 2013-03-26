using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zanfranceschi.MsgEa.Model;

namespace Zanfranceschi.MsgEa.Messages.Requests
{
	[Serializable]
	public class GetAddressServiceRequest
		: Request
	{
		public string CEP { get; private set; }
		public GetAddressServiceRequest(User requestor, string CEP)
			: base(requestor) 
		{
			this.CEP = CEP;
		}
	}
}
