namespace Zanfranceschi.MsgEa.UIs._Console
{
	using System;
	using Zanfranceschi.MsgEa.Domain.ClientEndPointImpl;
	using Zanfranceschi.MsgEa.Domain.Services;
	using Zanfranceschi.MsgEa.Model;
	
	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "Client";
			Console.WindowHeight = 20;
			Console.WindowWidth = 60;

			User user = null;
			Message message;

			using (ICustomerServices services = new ClientEndPointImplCustomerServices())
			{

				string input = string.Empty;

				while (true)
				{
					Console.WriteLine("Type the new customer name:");
					input = Console.ReadLine();

					if (input == "exit")
						break;

					Customer customer = services.RegisterCustomer(user, input, out message);
					Console.WriteLine(message.Text);
				}

				while (true)
				{
					Console.WriteLine("Type the customer search term:");
					input = Console.ReadLine();

					if (input == "exit")
						break;

					Customer[] customers = services.SearchCustomers(user, input, out message);
					Console.WriteLine(message.Text);
					
					foreach (var customer in customers)
					{
						Console.WriteLine("{0} - {1}", customer.Id, customer.Name);
					}
				}
				Console.WriteLine("terminated...");
			}
		}
	}
}
