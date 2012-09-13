using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Db4objects.Db4o;
using TI.Usuarios.Modelos;
using Db4objects.Db4o.CS;
using System.Net;
using System.Threading;

namespace TI.Usuarios.BUS
{
    class Program
    {
        static void Main(string[] args)
        {
            Db4oServer.StartServer();
            
			new Thread(LostMessagesRetriever.Start).Start();
			new Thread(RescisaoListener.StartListening).Start();
        }
    }
}
