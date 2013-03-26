namespace Zanfranceschi.MsgEa.UIs.CEPConsole
{
	using System;
	using Zanfranceschi.MsgEa.Domain.ClientEndPointImpl;
	using Zanfranceschi.MsgEa.Domain.Services;
	using Zanfranceschi.MsgEa.Model;

	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "Client CEP";
			Console.WindowHeight = 20;
			Console.WindowWidth = 60;

			User user = null;
			Message message;

			using (IUtilServices services = new ClientEndPointImplUtilServices())
			{
				string input = string.Empty;

				while (true)
				{
					Console.WriteLine("Type the CEP to search for the address ('exit' to quit):");
					input = Console.ReadLine();

					if (input == "exit")
						break;

					Address address = services.GetAddressByCEP(user, input, out message);
					Console.WriteLine(message.Text);
					Console.WriteLine("{0} - {1}", address.AddressDescription, address.Region);
				}
				Console.WriteLine("terminated...");
			}
		}
	}
}
