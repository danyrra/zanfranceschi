﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace EIP.Sandbox
{
	class Program
	{
		
		static void Main(string[] args)
		{
			Console.Title = "Sender";

			var sender = new Sender();
			sender.Start();
		}
	}
}
