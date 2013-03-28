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

				DateTime start;
				DateTime end;
				TimeSpan span;
				Customer customer;
				while (true)
				{
					Console.WriteLine("Type the new customer name:");
					input = Console.ReadLine();

					if (input == "exit")
						break;

					start = DateTime.Now;
					customer = services.RegisterCustomer(user, input, out message);
					end = DateTime.Now;
					span = end - start;

					Console.WriteLine("{0} (em {1}ms)", message.Text, span.TotalMilliseconds);
				}
				Customer[] customers;
				while (true)
				{
					Console.WriteLine("Type the customer search term:");
					input = Console.ReadLine();

					if (input == "exit")
						break;

					start = DateTime.Now;
					customers = services.SearchCustomers(user, input, out message);
					end = DateTime.Now;
					span = end - start;

					foreach (var cust in customers)
					{
						Console.WriteLine("{0} - {1}", cust.Id, cust.Name);
					}

					Console.WriteLine("{0} (em {1}ms)", message.Text, span.TotalMilliseconds);
				}
				Console.WriteLine("terminated...");
			}
		}
	}
}
