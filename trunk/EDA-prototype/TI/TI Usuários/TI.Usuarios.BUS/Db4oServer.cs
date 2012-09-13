using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Db4objects.Db4o.CS;
using System.Configuration;
using Db4objects.Db4o;

namespace TI.Usuarios.BUS
{
    internal static class Db4oServer
    {
		private static string serverName = ConfigurationManager.AppSettings["server_file"].ToString();
        private static int serverPort = int.Parse(ConfigurationManager.AppSettings["server_port"].ToString());
        private static string username = ConfigurationManager.AppSettings["db4o_user"].ToString();
        private static string password = ConfigurationManager.AppSettings["db4o_password"].ToString();

        internal static void StartServer()
        {
            var server = Db4oClientServer.OpenServer(serverName, serverPort);
            server.GrantAccess(username, password);
        }
    }
}