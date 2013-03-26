using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zanfranceschi.MsgEa.Model
{
	[Serializable]
	public class Address
	{
		public string Region { get; set; }
		public string City { get; set; }
		public string District { get; set; }
		public string AddressType { get; set; }
		public string AddressDescription { get; set; }
	}
}
